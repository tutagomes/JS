# Valores Computados

Armazenar dados e variáveis no objeto `data` é interessante quando os dados são armazenados e recolhidos diretamente, sem passar por tratamentos. Quando um dado precisa ser modificado apenas para ser mostrado, é comum utilizar os tipos/variáveis computadas.

Um objeto computado pode ter quantas propriedades for necessário, lembrando que aqui estamos falando de funções. Ou seja, um objeto computado, não é nada mais nada menos do que uma função que retorna a informação desejada!

O primeiro passo para criar uma variável computada na aplicação Vue é adicionar o objeto `computed` à instância:

```js
      const app = new Vue({
          el: '#app',

        data: {
           message: 'Hello Vue!'
       },
          computed: {
       }
      });
```

O próximo passo é adicionar uma função dentro do objeto `computed`. Vamos por exemplo, criar um método computado que retorna o valor de `message` com todas as letras minúsculas.

```js
      computed: {
        messageToLower() {
          return this.message.toLowerCase();
        }
      }
```

Desta forma, temos a função referenciando uma variável da instância (utilizando o `this`) e retornando um valor computado. Assim, ao modificar o valor de message através do console da aplicação, o valor de retorno do método `messageToLower()` também é modificado!

Claro, como em toda linguagem, métodos não são limitados a apenas uma linha. Um exemplo mais complexo de um método computado pode ser conferido abaixo:

```js
      const app = new Vue({
        el: '#app',
             data: {
          price: 25,
          currency: '$',
          salesTax: 16
        },
        computed: {
          cost() {
       // Work out the price of the item including 
          salesTax
            let itemCost = parseFloat(
              Math.round((this.salesTax / 100) * 
              this.price) + this.price).toFixed(2);
            // Add text before displaying the currency and   
             amount
            let output = 'This item costs ' + 
            this.currency + itemCost;
           // Append to the output variable the price 
             without salesTax
             output += ' (' + this.currency + this.price + 
        ' excluding salesTax)';
             // Return the output value
              return output;
           }
        }
     });
```

Embora o código acima possa parecer beeem complicado, tente entendê-lo. A função do método é simplesmente aplicar a taxa ao preço e montar um texto de retorno contendo `"This item costs $29.00 ($25 excluding salesTax)"`, ou seja, há apenas a formatação das variáveis para que seja melhor impressa.



### Métodos e funções

Na sua aplicação Vue, é comum que você queira manipular os dados. Todo o gerenciamento das funções é feito através do objeto `methods` da instância:

```js
      const app = new Vue({
        el: '#app',
      
        data: {
          shirtPrice: 25,
          hatPrice: 10,
          
          currency: '$',
          salesTax: 16
        },
        methods: {
          
        }
      });
```

No código acima, adicionamos as variáveis `hatPrice` e `shirtPrice` com valores diferentes. Agora, vamos criar um método para calcular o preço de venda para cada um desses produtos:

```js
      methods: {
        calculateSalesTax(price) {
          // Work out the price of the item including   
          sales tax
          return parseFloat(
          Math.round((this.salesTax / 100) * price) 
         + price).toFixed(2);
        }
      }
```

E como todas as funções em outras linguagens, elas não são executadas sem ser chamadas. Portanto, vamos adicionar uma chamada à ela no nosso HTML, já imprimindo o valor desejado:

```html
      <div id="app">
        {{ calculateSalesTax(shirtPrice) }}
      </div>
```

Agora, vamos adicionar a moeda que estamos trabalhando, apenas concatenando a string de preço com `$`. Podemos então aninhar as funções e criar algo do tipo:

```js
      methods: {
        calculateSalesTax(price) {
          // Work out the price of the item including 
          sales tax
          return parseFloat(
            Math.round((this.salesTax / 100) * price) +   
            price).toFixed(2);
         },
         addCurrency(price) {
          return this.currency + price;
        }
      }
```

Agora, é só chamar no HTML e verificar o resultado:

```js
      {{ addCurrency(calculateSalesTax(shirtPrice)) }}
```

Seria possível também criar um método exclusivo chamado `shirtCost` que faz exatamente a função acima, mas é recomendado utilizar um objeto computado.

A razão disso é que os objetos computados ficam em cache, enquanto funções não. Imagine que sua aplicação tem funções beeem mais complicadas que as acima e toda vez que forem chamdas, devem ser lidas, interpretadas e executadas, principalmente se você precisar imprimir várias vezes na página. É bem provável que você tenha um grande impacto na performance e na usabilidade. Ao utilizar os dados computados, o valor é calculado uma vez e colocado em cache, sendo atualizado de forma reativa.

Vamos criar então duas propriedades computadas para mostrar o custo da camisa e do chapéu, chamando-os no HTML da forma:

```html
      <div id="app">
        <p>The shirt costs {{ shirtCost }}</p>
        <p>The hat costs {{ hatCost }}</p>
      </div>
```

Agora, vamos implementá-las: 

```js
      const app = new Vue({
        el: '#app',
        data: {
          shirtPrice: 25,
          hatPrice: 10,

          currency: '$',
          salesTax: 16
        },
        computed: {
          shirtCost() {
            returnthis.addCurrency(this.calculateSalesTax(
              this.shirtPrice))
          },
          hatCost() {
          return this.addCurrency(this.calculateSalesTax(
          this.hatPrice))
          },
        },
        methods: {
          calculateSalesTax(price) {
            // Work out the price of the item including 
            sales tax
            return parseFloat(
              Math.round((this.salesTax / 100) * price) + 
              price).toFixed(2);
                },
                addCurrency(price) {
            return this.currency + price;
          }
        }
      });
```

Assim, ao executar seu código, você deve ver na tela o preço tanto do chapéu quanto da camisa devidamente formatados.



### Experimente!

É possível chamar funções no Vue ao click de botões! Basta adicionar a propriedade `@click="função()"` na tag `button`. Experimente adicionar um botão para que toda vez que for clicado, seja adicionada uma camisa ou um chapéu ao total da compra.

Dica - o código do botão poderia ser `<button @click='addShirt()'> Adicionar Camisa </button>`

Lembre-se de formatar adequadamente a saída.