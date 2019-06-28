# Desenvolvendo um servidor com Express

Embora você consiga trabalhar com o bom e velho Node raiz e fazer tudo, o Express é um framework para NodeJS que permite os desenvolvedores a implementar um servidor com uma série de funcionalidades básicas. Primeiro, vamos instalá-lo, verificar o funcionamento e começar a codar.



### Começando...

Vamos instalar então o Express e ter certeza de que está funcionando. A instalação é trivial, já que é feita através do pacote `npm`

```
npm install express --save
```

> Você também pode utilizar a flag `—verbose` para que o comando npm mostre passo a passo o que ele faz.

Se você ainda não tiver criado uma pasta chamada `src` na raiz da sua aplicação, está na hora de fazê-lo. É nela que vamos concentrar todo o código da aplicação, deixando-a mais organizada.

Agora, vamos montar um pequeno servidor utilizando Express. 

```js

/* @flow */

const express = require("express");

const app = express();

app.get("/", (req, res) => res.send("Server alive, with Express!"));
app.listen(8080, () =>
    console.log(
        "Mini server (with Express) ready at http://localhost:8080/!"
    )
);
```



### Sobre middleware

O pacote Express baseia toda sua funcionalidade em um conceito chave: o *middleware*. Se você estivesse trabalhando somente em Node, você teria que implementar um grande gerenciador de conexões que tomaria conta de todas as `requests` que seu servidor receber. Ao utilizar o *middleware*, o Express permite quebrar o processo em pequenos pedaços, deixando mais fácila implementação de extensões, funcionalidades e verificações.

Vamos ver então a diferença entre utilizar somente o Node para montar o servidor eutilizar o Express. Primeiro, o funcionamento do Node:

![img](https://learning.oreilly.com/library/view/modern-javascript-web/9781788992749/assets/e8a129e0-6943-46e0-a7e1-9bb702789148.png)

Note que as chamadas são roteadas diretamente para seu código, o que faz com que suas funções precisem implementar todos os processos extras, como logs, segurança, erros...

Ao utilizar o Express, temos o seguinte esquema:

![img](https://learning.oreilly.com/library/view/modern-javascript-web/9781788992749/assets/62196a34-5120-48f6-8a5e-5d17adbcfbdb.png)

Quando o Express é utilizado, ele gerencia as *requests* passando-as através de uma pilha de middlewares, que por fim retornam uma resposta.