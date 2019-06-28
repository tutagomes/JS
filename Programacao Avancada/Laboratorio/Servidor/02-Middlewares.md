### Implementando Middlewares

Assim como tudo na vida (ou quase tudo), os middlewares implementados no Express devem possuir uma ordem. Por exemplo, é esperado que você comece seu log logo quando um request chegar, e não somente no final do processamento. Portanto, é recomendado que este middleware fique logo no topo do stack.

As funções do *middleware* do Express recebem três parâmetros (em sua forma padrão): a request HTTP, a resposta HTTP e uma função que você deve chamar quando você quer que o processamento continue para  a próxima etapa do stack middleware (next()). Quando o seu *middleware* é chamado, é esperado que ele:

- Envie uma resposta final com `res.send()`, `res.sendFile()` ou `res.end()`
- Continua com a execução, chamando a função `next()`

As funções de erro são um pouco diferentes (só um pouco, não muito, quer dizer, um parâmetro só não parece ser muito), elas adicionam um novo parâmetro relacionado ao erro, contemplando então 4 parâmetros no total. A única diferença entre uma função de *middleware*  comum e uma de erro é a quantidade de parâmetros. Se tudo ocorrer como esperado, ou seja, nenhum erro for gerado, a função de erro nunca é chamada. Mas se houver um erro, todas as funções normais do stack são puladas até que uma de erro seja encontrada, e assim por diante.

Vamos ver como ficaria então:

```js
// Source file: src/middleware.js

/* @flow */

const express = require("express");
const app = express();

app.use((req, res, next) => {
    console.log("Logger... ", new Date(), req.method, req.path);
    next();
});

app.use((req, res, next) => {
    if (req.method !== "DELETE") {
        res.send("Server alive, with Express!");
    } else {
        next(new Error("DELETEs are not accepted!"));
    }
});

// eslint-disable-next-line no-unused-vars
app.use((err, req, res, next) => {
    console.error("Error....", err.message);
    res.status(500).send("INTERNAL SERVER ERROR");
});

app.listen(8080, () =>
    console.log(
        "Mini server (with Express) ready at http://localhost:8080/!"
    )
);
```



### Implementando um Middleware de 'log'

Vamos começar a implementação do logger. É interessante que o middleware de log seja chamado em todas as requests, então vamos utilizar o mesmo padrão do código acima. Uma alternativa seria utilizar o padrão `app.use("*", …)` que significa exatamente a mesma coisa. E claro, não podemos esquecer de chamar a função `next()` para que o processamento possa continuar:

```js
app.use((req, res, next) => {
    console.log("Logger... ", new Date(), req.method, req.path);
    next();
});
```

Para fins de experimentação, vamos implementar um erro que retorna para o usuário que o método `DELETE` não é permitido. Desta forma, queremos que o middleware continue e o Express retorne uma mensagem para o usuário indicando o erro. Então, definimos então da forma:

```js
app.use((req, res, next) => {
    if (req.method === "DELETE") {
        next(new Error("DELETEs are not accepted!"));
    } else {
        res.send("Server alive, with Express!");
    }
});
```

E por como último middleware de erro, devemos implementar o retorno do erro 500:

```js
// eslint-disable-next-line no-unused-vars
app.use((err, req, res, next) => {
    console.error("Error....", err.message);
    res.status(500).send("INTERNAL SERVER ERROR");
});
```



> You'll note the need for disabling the no-unused-vars ESLint rule. Recognizing errors just by the function signature is not a very good practice, and if you are setting your error handler at the end of the stack so that there's no other function to call, the next parameter will be unused and cause an error. There is some talk of solving this situation in upcoming versions of Express, but for now the point is moot.



### Testando até então

Vamos testar como nossa aplicação está até o momento, através do comando `node src/index.js`, podendo o nome `index.js`  ser diferente na sua aplicação.

É possível testar o servidor tanto com o comando `curl` quanto com o Postman:

```sh
> curl "http://127.0.0.1:8080/some/path/to/get?value=9" 
Server alive, with Express!
> curl -X POST "http://127.0.0.1:8080/a/post/to/a/path" 
Server alive, with Express!
> curl -X DELETE "http://127.0.0.1:8080/try/to/delete?key=22" 
INTERNAL SERVER ERROR
```

E como já implementados o logger, veremos no console do servidor as seguintes linhas:

```sh
Logger... 2018-05-08T00:22:20.192Z GET /some/path/to/get
Logger... 2018-05-08T00:22:44.282Z POST /a/post/to/a/path
Logger... 2018-05-08T00:23:01.888Z DELETE /try/to/delete
Error.... DELETEs are not accepted!
```

