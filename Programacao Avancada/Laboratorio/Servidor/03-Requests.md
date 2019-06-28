# Recolhendo parâmetros de Request

Bom, já vimos como implementar um middleware de log e outro de erro. Portanto, sabemos como reportar quando o usuário fazer algo inesperado. Mas como podemos receber chamadas do usuário e processá-las? UFA! Ainda bem que você perguntou! Vamos ver então como é.



### Mas como?

Primeiro, é necessário utilizar a biblioteca `body-parser` e se necessário, passar algumas opções, como:

```js
// Source file: src/get_parameters.js

const bodyParser = require("body-parser");
app.use(bodyParser.urlencoded({ extended: false }));
```

Como queremos que os parâmetros do `body` sejam processados antes de qualquer direcionamento, vamos adicionar um `app.use(bodyParser...etc)` no incío do stack.

> Quando digo stack me refiro à pilha de funções `app.use` implementadas.

A biblioteca `body-parser` possui quatro processadores:

- *Um processador de URL*, como mostrado no código acima com `.urlencoded`.
- *Um processador de JSON*, através de bodyParser.json(), que processa requests onde o Content-Type é do tipo `application/json`.
- *Um processador cru*, através de bodyParser.raw(), que processa qualquer tipo de informação, principalmente arquivos.
- *Um processador de texto*, através de bodyParser.text(), que processa textos.

> Caso você queira saber mais sobre as opções de cada um e também sobre como lidar com requests multipart-bodies, dê uma olhada na documentação, disponível em: https://github.com/expressjs/body-parser 



### Como implemento?

É relativamente simples, bastando adicionar algumas poucas linhas de código. Podemos alterar o funcionamento do nosso logger ou até mesmo adicionar um novo middleware, que é responsável por imprimir no console os conteúdos da `query` e do `body`:

```js
// Source file: src/get_parameters.js

app.use("*", (req, res) => {
    console.log(req.query, req.body);
    res.send("Server alive, with Express!");
});
```

Os parâmetros de URL são automaticamente separados no Express e "bindados" na variável `req.query` enquanto os parâmetros do corpo são "bindados" na variável `req.body`.

Por exmeplo, ao fazer as chamadas:

```sh
> curl "http://127.0.0.1:8080/birthdays?day=22&month=9&year=1960" 
> curl -X POST --data "name=FK" "http://127.0.0.1:8080/persons" 
```

Teremos a saída no console da forma:

```js
> node out/get_parameters.js
Mini server (with Express) ready at http://localhost:8080/!
{ day: '22', month: '9', year: '1960' } {}
{} { name: 'FK' }
```

No caso do GET, podemos ver os parâmetros passados pela URL e no segundo caso, POST, podemos ver que não há nenhum parâmetro de query, mas há parâmetros no body.