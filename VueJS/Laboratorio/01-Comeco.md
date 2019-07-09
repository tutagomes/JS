# Preparando o primeiro ambiente

Para começar a utilizar o Vue, vamos primeiro implementá-lo em um arquivo HTML. Por enquanto, iremos trabalhar somente neste arquivo, incluindo scripts e tags à medida que progredirmos.

Para isso, vamos criar uma nova pasta para esta primeira parte e adicionar um arquivo HTML como o abaixo:

```html
      <!DOCTYPE html>
      <html>
        <head>
        <meta charset="utf-8">
        <title>Vue.js App</title>
        </head>
        <body>
        <div id="app">
          </div>
          <script src="https://unpkg.com/vue"></script>
          <script type="text/javascript">
            // JS Code here
          </script>
        </body>
      </html>
```



#### A *div* de aplicação

Toda instância Vue se inicia através de uma tag HTML que será controlada pela instância. Pode ser do tipo *main, p, div, etc* desde que esteja dentro da tag `body`. E claro, o ID dela deve ser único! Isso permite que você tenha várias intâncias Vue na mesma página.

Por enquanto, faremos uso da `div` que possui como ID `app` (já incluído no código acima): 

```html
      <div id="app">
      </div>
```



### Inicializando e Hello World!

Agora que temos o nosso arquivo HTML base, podemos inicializar uma instância vue e fazer com que ela trabalhe com a `div` de ID `app`:

```js
      const app = new Vue().$mount('#app');
```

O código acima cria uma nova instância do Vue e faz a ligação com o elemento HTML. Se você salvar o arquivo e abrir em seu navegador, já poderá perceber que nada foi alterado, isso mesmo, nada. Mas embaixo dos panos, já existe uma instância Vue sendo executada e diretamente relacionada ao elemento HTML.

A instância Vue tem vários objetos e propriedades que descobriremos ao longo do tempo. A primeira que você terá contato é a propriedade `el`. Através dela, é possível especificar um ID de uma tag HTML que deve ser montada, assim como o código acima.

A forma mais comum então de se configurar e montar uma instância é da forma:

```js
      const app = new Vue({
        el: '#app'
      });
```

Junto com a propriedade `el`, o Vue tem um objeto chamado `data`, que armazena todas as variáveis do escopo da instância. Por exemplo, podemos criar um objeto chamado `message` e dizer que seu valor é `Hello!` da seguinte maneira:

```js
      const app = new Vue({
        el: '#app',
        data: {
          message: 'Hello World!'
        }
      });
```

Declarando desta forma, podemos acessar a variável `message` em todo o escopo da nossa instância (dentro da tag HTML de ID `app`). Para mostrar o valor das variáveis, é utilizado o famoso Mustache (ou se preferir em pt-br, Bigodão, Bigode, Chaves Duplas). Vamos imprimir então na tela a mensagem simplesmente adicionando à div `{{message}}`:

```html
      <div id="app">
        {{ message }}
      </div>
```

Ao salvar o arquivo e abrir o navegador, você deve ver a mensagem escrita na tela.

O objeto `data` permite declarar e definir vários tipos. Por exemplo:

```js
      const app = new Vue({
        el: '#app',

        data: {
         message: 'Hello!',
         price: 18 + 6,
         details: ['one', 'two', 'three']
       }
     });
```

Se você quiser mostrar essas variáveis no navegador, basta seguir o mesmo passo da `message` e colocá-las entre `{{variavel}}`.

Todas as propriedades em Vue são reativas, ou seja, sempre que seus valores são alterados, a mudança é imediatamente refletida na tela. Por exemplo, se você abrir o console e atualizar o valor de `app.message`, é esperado que automaticamente o valor exibido seja alterado.
