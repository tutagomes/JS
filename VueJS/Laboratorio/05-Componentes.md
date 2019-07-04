# Componentes Single File

O que são Single File Components?

Esta *feature* do Vue.js demonstra o quanto esta biblioteca nos ajuda a desenvolver utilizando componentes. Pense comigo.

> O que seria um componente web? **Seria a menor parte de nossa interface, com suas próprias características e comportamentos**. Sendo assim, ela traria em seu escopo, HTML, CSS e JS. Correto? Os SFCs são nada mais nada menos, que arquivos com extensão .vue em que você escreve HTML, CSS e JS juntos. O [texto sobre componentes](http://www.vuejs-brasil.com.br/aplicacoes-com-vuejs-pensando-em-componentes/) do [Vinicius Reis](https://medium.com/@luizvinicius73) pode elucidar algumas coisas, recomendo a leitura.

Brincadeiras a parte, pense em uma UI bem complexa. Em que há dezenas de componentes em sua página, cada um com comportamento próprio. Bem, eu poderia escrever tudo em um único arquivo js. Todavia, temos os SFCs, que nos ajuda e muito neste momento. Pois com a chegada do Vue.js 2 e a implementação do VirtualDOM nele, os SFCs são a forma mais simples e modular de se trabalhar. O texto sobre [Render Functions do Ígor Luiz](http://www.vuejs-brasil.com.br/render-functions-no-vue-js-2-0/) elucida muito bem o que é o VirtualDOM e porque os SFCs são bons neste momento.

*Single File Components*, como coloquei acima, são arquivos com extensão .vue e que tem, em sua estrutura, as *tags* *template*, *style* e *script:*

```html
<template>
  Seu HTML
</template>
<script>
  Seu JS
</script>
<style>
  Seu CSS
</style>
```

Há um ponto importante a se consider sobre os SFCs: eles aceitam pre-processadores CSS — Sass, Less — , JS, CofeeScript, e HTML, como o Pug. Ou seja, além de você usar todo o poder de encapsulamento dos componentes, você pode usar a ferramenta de compilação de código que você já usa e abusa.

Por fim, antes de continuarmos, é importante comentar que os arquivos .vue não são interpretados pelo seu navegador, ou seja, é necessário utilizar uma ferramenta para passar os dados dos arquivos .vue para arquivos .js e acrescentar aos arquivos html e css finais. Para tanto, a equipe do Vue.js desenvolveu algumas alternativas para os *modules bundlers* mais famosos, [Webpack](https://webpack.github.io/) e [Browseryfi](http://browserify.org/): o [vue-loader](https://github.com/vuejs/vue-loader) e o [vuefy](https://github.com/vuejs/vueify), respectivamente. Não sabe o que é um module bundler? Dá uma olhada [neste texto](https://blog.codecasts.com.br/ecossistema-javascript-parte-05-bundlers-builders-6809b17ddcf8#.eianuk5zz) do [Vinicius Reis](https://medium.com/@luizvinicius73). 



------



### Entendendo o que tem nos SFCs e como utilizá-los

Agora, que você já entendeu o que são SFCs, seu esqueleto e utilidade, vamos nos ater a cada parte do arquivo.

#### Entendendo a tag template

Se lembra das *custom tags* que mencionei acima? Elas permitem utilizar tags diretamente relacionadas com os nomes dos componentes:

- Dar um nome que você desejar — claro, não pode o que já existe, por exemplo, *main, article*…;
- Construir *tags* da forma mais customizada possível. Já imaginou construir seu próprio *slider*? Ou até mesmo um card do seu jeito?

Sim, isto é possível com o SFC. Dentro da tag template há as divs, buttons, asides, sections que compõem o componente que você quiser criar, inclusive outros componentes. Tá, mas aonde entra a *custom tag*? Na hora que você chamar seu componente dentro da *tag* template do componente pai você vai dar o nome que quiser. Vamos a um exemplo, primeiramente um componente filho qualquer:

```html
<template>
  <div>
    <p> Eu sou um componente filho </p>
  </div>
</template>
```

E agora tenho também um componente pai:

```html
<template>
  <div>
    <MySoon></MySoon>
  </div>
</template>
<script>
import MySoon from './MySoon'
export default {
  components: { MySoon }
}
</script>
```

Será renderizado ao final:

```html
<div>
    <div>
      <p> Eu sou um componente filho </p>
    </div>
 </div>
```

#### Entendendo a tag style

Imagine que você precise estilizar este componente, mas somente ele, adicionando cores de texto, bordas, arredondamentos, etc. Isso é possível através da tag style:

```html
<template>
  <div>
    <p class="paragraph"> Meu estilo não escapará </p>
  </div>
</template>
<style lang="sass" scoped>
  .paragraph
    color: red
</style>
```

Vamos aos detalhes:

- É possível especificar uma *lang*, ou seja, eu posso dizer para o compilador do Vue: o código abaixo será escrito em Sass. Por *default*, o valor é ‘css’.
- Com o atributo *scoped*, eu estou dizendo para o compilador do Vue: estas características não podem escapar deste componente. Aqui “simulamos o *shadow dom”*. Um DOM escondido, que contém sua própria marcação e estilo e mais para frente veremos o comportamento.



#### Entendendo a tag script

Bem, talvez a maior mágica dos *Single File Components* está dentro desta *tag*. Aqui você colocará todo o comportamento do seu componente, seja uma *request ajax*, um botão que invoca uma modal, que valida um *form*. Sua imaginação (e vontade) é o limite!

Ela tem esta forma:

```html
<script>
  export default {}
</script>
```

Um detalhe importante: Vue.js te encoraja a desenvolver utilizando a sintaxe do EcmaScript 2015 ou o ES6. Por isso esse *export* … É a sintaxe para exportação de módulo. A parte boa é que os *templates* oficiais do *vue-cli*, tirando o *simple*, já trazem um *module bundler* e o [Babel](https://babeljs.io/) para deixar seu código compatível com os navegadores não tão modernos.

Bem, agora vamos nos ater ao que vem dentro do *export default{}*. A primeira vista é um objeto. Mas o que tem dentro deste objeto. Se você conhece um pouquinho de Vue.js ou já deu uma olhada no Hello Wolrd da documentação, deve ter visto um código parecido com isso:

```html
<script>
  const app = new Vue({
    ...
  })
</script>
```

Bem, quando eu faço *export default*, **é como se eu fizesse um export new Vue({})**. Ou seja, o que está dentro de {} são os nossos objetos ou métodos de um instancia de Vue.js. [Há um texto do Fábio Vedovelli sobre o que tem dentro do objeto Vue ali em cima](http://www.vuejs-brasil.com.br/o-que-e-cada-propriedade-num-vue-object/). Não deixe de dar uma olhada.

Vamos a cada parte:

- **data**: são os dados que circulam no componente. Um dos pontos da reatividade do Vue.js está neste método. Lembre-se: data, num componente .vue, deve ser uma função que retorna um objeto. Nos acusará um erro, caso eu coloque um objeto diretamente.

```js
export default {
  data () {
    return {
      msg: 'Use uma função, não um objeto diretamente'
    }
  }
}
```

- **props**: são dados que vem do componente pai. **É um array**. Uma *prop*, no componente pai, é colocada no componente como se fosse um atributo. Vamos a um exemplo:

```html
<!-- No componente pai -->
<MySoon title="Titulo...">
  
<!-- No componente filho -->
export default {
  props: ['title']
}
```



Beleza, e agora, como crio meu própio componente? Chegou a parte de experimentar!



### Criando seu próprio componente

Para facilitar nossa vida, a CLI do quasar permite que criemos um componente através do comando:

```sh
quasar new component cardProduto
```

Vamos então adicionar `props` e fazer com que seu valor seja mostrado na tela:

```html
<template>
  <div>{{nome}}</div>
</template>

<script>
export default {
  name: 'cardProduto',
  props: ['nome'],
  data () {
    return {}
  }
}
</script>
```



**Importanto e usando o componente na tela principal**

Na pasta `pages`, temos o arquivo `Index.vue`,  responsável por mostrar para a gente o logo do quasar da página inicial. Vamos então adicionar nosso componente à página, modificando o código para ficar como:

```html
<template>
  <q-page class="flex flex-center">
    <card-produto nome='Produto01' ></card-produto>
  </q-page>
</template>

<style></style>

<script>
import cardProduto from '../components/cardProduto.vue'
export default {
  components: {cardProduto}
  name: "PageIndex"
};
</script>
```





### Programando!

Agora que criamos um componente de produto, que tal deixá-lo mais agradável aos olhos? O quasar possui uma extensa biblioteca de componentes e todos muito bem documentados. Para esta parte, que tal adicionar uns [Cards](https://quasar.dev/vue-components/card)?

Após implementar os cards e os componentes que achar necessários, vamos revisitar o antigo programa da lista de tarefas e reimplementá-lo.

Um exemplo de interface seria:

![image-20190703233030696](assets/image-20190703233030696.png)

Mas sinta-se livre para implementar como quiser!