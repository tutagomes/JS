# Implementando CORS

Sempre que um navegador envia uma requisição a um servidor, há uma série de regras de validação que são aplicadas. Para muitas dessas interações, que somente requisitam informações e não tentam produzir mudanças no servidor, não há limitações, como por exemplo:

- *CSS styles* requeridos através da <link rel="stylesheet"> tag
- *Images* requeridas através da <img> tag
- *JS code* requerido através da <script> tag
- *Media* requeridas através da <audio> or <media> tags

Para todos os outros Requests, as políticas **Same Origin Policy** ou **Single Origin Policy** limitam os requests que são feitos fora da origem. Por exemplo, se seu script rodando em http://localhost tenta acessar um servidor que não tem a política CORS habilitada, ele deve receber um erro do tipo `Cross-Origin X Not Allowed`.

No entanto, ao implementar uma API RESTful, é interessante que ela esteja exposta para todos os tipos de request. Para isso, é necessário implementar o CORS (Cross Origin Resource Sharing). Basicamente, o CORS define um estilo de interação em que um servidor pode definir o que ele quer tratar e o que ele quer bloquear. Vamos ver como implementar então.

> Claro, se você quiser saber mais sobre CORS, tem um ótimo artigo no site da mozilla -  https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS*.*



### Bora codar

Antes de tudo, temos que habilitar o CORS. E antes de antes de tudo, temos que instalar o pacote:

```
  npm install cors --save
```

E adivinhe, o pacote CORS também é um middleware do Express! Está ficando muito fácil extender esse servidor...

E claro, você pode habilitar o CORS globalmente como mostrado abaixo:

```js
const cors = require("cors");
app.use(cors());
```

Mas também é possível habilittar o CORS para uma rota em específico, como por exemplo:

```js
routerProdutos.get("/:id", cors(), (req, res) => {
    res.send(`GET Produto... ${req.params.id}`);
});
```



### Testando

Por padrão, o Postman é tratado como Same Origin Request, então ele não pode ser utilizado para este tipo de teste. Vamos subir nosso servidor então e criar um simples arquivo html com uma chamada HTTP para testá-lo:

```html
// Source file: src/cors_request.html

<html>
<head></head>
<body>
    <script type="text/javascript">
        const req = new XMLHttpRequest();
        req.open('GET', 'http://www.corsserver.com:8080/', true);
        req.onreadystatechange = () => {
            if (req.readyState === 4) {
                if (req.status >= 200 && req.status < 400) {
                    console.log(req.responseText)
                } else {
                    console.warn("Problems!")
                }
            }
        };
        req.setRequestHeader("dummy", "value");
        req.send();
    </script>
</body>
</html>
```

Dica: abra o console do seu navegador e veja as respostas tanto com o CORS habilitado quanto desabilitado!