# Diretivas

As diretivas em VueJS são meios de adicionar lógica e funcionalidade ao seu script HTML. Como no caso do `v-on:click` ou sua versão compacta `@click` visto na parte anterior, há uma coleção de diretivas para facilitar e adicionar mais interatividade à sua página.

>  Por falta de sinônimo, a palavra `diretiva` será extensivamente utilizada ao longo desta parte.



## v-html

A diretiva permite a modificação direta do HTML interno de uma tag sem utilizar a sintaxe Mustache. Também é possível utilizá-la para exibir variáveis cujo conteúdo está em formato HTML. Desta forma, os dados são formatados como se fossem HTML e não variáveis normais.

**Página**:

Basta adicionar `v-html="variavel"`:

```
      <div id="app">
        <div v-html="message"></div>
      </div>
```

**JavaScript**:

E finalmente estabelecer um valor que contenha tags HTML à variável:

```
      const app = new Vue({
        el: '#app',

        data: {
          message: '<h1>Hello!</h1>'
        }
      });
```

> Tente evitar utilizar a diretiva v-html para mostrar dados que não conhece. Como o conteúdo é renderizado como HTML normal, sua aplicação pode se tornar vulnerável à injeções XSS. Por exemplo, tente trocar o valor de message para: `<button onclick="alert()"> TESTE </button>` e analise o resultado.





## Renderização declarativa

Os atributos regulares das tags HTML, como por exemplo o `src` de uma tag `<img>` podem ser dinamicamente alterados e populados através da diretiva `v-bind:`. Isso permite que sua instância controle atributos que antes eram inacessíveis (sem utilizar um JS Vanilla).

A utilização é a seguinte: sempre que quiser relacionar uma variável a um atributo, basta escrever `v-bind:atributo='variavel'`.

**Página**:

Por exemplo, podemos alterar uma imagem da forma:

```html
      <div id="app">
        <img v-bind:src="imageSource">
      </div>
```

**JavaScript**:

Create a variable in your Vue JavaScript code called imageSource. Add the URL to the desired image:

```js
      const app = new Vue({
        el: '#app',

        data: {
          imageSource: 'http://via.placeholder.com/350x150'
        }
      });
```

> Assim como o `v-on-click` pode ser reduzido para `@click`, a diretiva `v-bind:atributo` pode ser reduzida para `:atributo`





### Exemprimente!

Crie um array de imagens em seus dados da instância e uma função que escolhe aleatoriamente uma imagem do array. Conecte um botão à essa função e troque a imagem exibida.

Agora, adicione mais dois botões, um para `prox` e outro para `anterior` e implemente suas funcionalidades.





## v-if

A diretiva v-if determina através de uma verificação booleana (famoso if) se o bloco de HTML deve ou não ser exibido. Vamos ver um exemplo:

**Página**:

```html
      <div id="app">
        <div>Now you see me</div>
      </div>
```

**JavaScript**:

```js
      const app = new Vue({
        el: '#app',

        data: {
          isVisible: false
        }
      });
```

Ao abrir no navegador, é esperado que a mensagem `Now you see me` seja mostrada por padrão. Vamos então adicionar a verificação:

```html
      <div id="app">
        <div v-if="isVisible">Now you see me</div>
      </div>
```

Agora, ao recarregar a página, a mensagem deve desaparecer. Se alterarmos o valor da variável `isVisible`, poderemos então mostrar a mensagem como devido. Execute então no console do navegador o comando:

```
      app.isVisible = true
```

E agora a mensagem deve ser mostrada.

A diretiva não funciona somente com variáveis booleanas. Também é possível implementar uma checkagem da forma:

```js
      <div v-if="selected == 'yes'">Now you see me</div>
```



## v-else

Similar ao v-if (só que ao contrário), esta diretiva mostra o conteúdo caso o `v-if` anterior tenha sido avaliado como falso.

Por exemplo:

```html
      <div id="app">
        <div v-if="isVisible">
          Now you see me
        </div>
        <div v-else>
          Now you don't
        </div>
      </div>
```

Também é possível utilizar o famoso `else-if`, seguindo o formato `v-else-if`, como mostrado abaixo:

```
      <div id="app">
        <div v-if="isVisible">
          Now you see me
        </div>
        <div v-else-if="otherVisible">
          You might see me
        </div>
        <div v-else>
          Now you don't
        </div>
      </div>
```



### Experimente!

Utilize o código de camisas e chapéus da parte anterior e implemente:

- Um desconto caso o número de camisas seja maior que 5, por exemplo, 5%;
- Uma mensagem de que o frete é gratuito caso o número de peças seja maior que 10;
- Desabilite os botões caso o número de peças seja maior que 15.
  - Dica: use a diretiva `v-bind:disabled` ou simplesmente `:disabled`.





# v-for

Agora sim as coisas ficam mais interessantes. Imagine ter que criar uma tag manualmente para cada entrada de dados em seu array sem saber o tamanho dele. Para isso, existe uma diretiva chamada `v-for`. Ela permite que iteremos acima de um array e cria automaticamente o HTML necessário. Por exemplo, vamos supor que utilizamos o Mockaroo para gerar dados aleatórios de pessoas em JSON 

```js
      const app = new Vue({
        el: '#app',

        data: {
          people: [...]
        }
      });
```

Agora, podemos mostrar cada objeto `pessoa` dentro do array `pessoas` da forma:

```html
      <div id="app">
        <ul>
          <li v-for="person in people">
            {{ person }}
          </li>
        </ul>
      </div>
```

O v-for repete sempre o elemento em que está, que no caso é a tag `<li>`. Caso você não tenha um elemento pai ou não quer utilizar o HTML, você pode usar o elemento `<template>`. Eles são removidos durante o tempo de execução e podem ser programados da forma:

```js
      <div id="app">
        <ul>
          <template v-for="person in people">
            <li>
              {{ person }}
            </li>
          </template>
        </ul>
      </div>
```

Ao deixar somente `{{pessoa}}`, somos bombardeados com um objeto em JSON puro. Como temos a criação de uma variável no `v-for`, podemos utilizá-la para acessar as propriedades de cada indivíduo, como por exemplo, o nome:

```js
      <li v-for="person in people">
        {{ person.name }}
      </li>
```

E claro, se você preferir, também é possível mostrar mais conteúdo, desta vez vamos utilizar uma tabela no lugar de uma lista para deixar as coisas um pouco mais organizadas:

```js
      <table>
        <tr v-for="person in people">
          <td>{{ person.name }}</td>
          <td>{{ person.email }}</td>
          <td>{{ person.balance }}</td>
          <td>{{ person.registered }}</td>
        </tr>
      </table>
```

Vamos por último adicionar uma coluna à tabela, por exemplo, para mostrar se a pessoa está ou não ativa. Podemos fazer isso de duas formas:

1. Utilizando o operador `ternario`:

```html
      <td>{{ (person.isActive) ? 'Active' : 'Inactive' }}</td>
```

2. Utilizando o v-if e v-else

```html
      <td>
        <span class="positive" v-if="person.isActive">Active</span>
        <span class="negative" v-else>Inactive</span>
      </td>
```

Um exemplo de uma tabela de saída seria:

![img](https://learning.oreilly.com/library/view/complete-vuejs-2/9781789959901/assets/34661335-a835-4887-9022-225f20db9fca.png)



### Experimente

Vamos agora criar uma simples lista de tarefas, com a opção de marcar a tarefa como feita, editá-la e excluí-la.

Definições:

- Deve haver um campo para colocar o título da tarefa e um botão para adicioná-la;
- Todas elas devem ser mostradas em uma lista ou tabela;
- Deve haver um botão/checkBox para marcar como feita;
- Quando marcada como feita, não deve ser possível desmarcá-la;
- Deve ser possível excluir uma tarefa da lista;
- Deve ser possível editar uma tarefa da lista;



Dica: utilize a diretiva `v-model`, disponível em https://br.vuejs.org/v2/guide/forms.html

E para manipular classes e estilos, dê uma olhada em https://br.vuejs.org/v2/guide/class-and-style.html





E agora, altere seu código anterior e implemente um carrinho de compras, lembrando de mostrar a imagem do produto ao lado de seu título, descrição e preço, além de calcular a soma no final.
