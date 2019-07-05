# Sobre Vuex

Bom, para começar, vamos dar uma boa lida na documentação do Vuex - disponível no site: https://vuex.vuejs.org/ptbr/



### Vuex e Quasar

Por sorte, o Quasar já vem totalmente integrado com a biblioteca de gerenciamento de estado chamada Vuex. A partir de agora vamos então criar um módulo de edição de entidades (pessoas) utilizando o Vuex e Json-Server.



Primeiro, devemos criar uma nova `store` para armazenar os dados das pessoas. Vamos utilizar o comando :

```sh
quasar new store Pessoas
```

 Ao executar o comando acima, podemos perceber que na pasta `store` foi criada a seguinte estrutura:

- Pessoas

  - actions.js
  - getters.js
  - index.js
  - mutations.js
  - state.js

  

#### Criando estado

No arquivo `state.js`, podemos então declarar um *array* de Pessoas da forma: 

```js
export default {
  Pessoas: [
    {
      'id': 1,
      'nome': 'Meu nome de teste',
      'email': 'email@dominio.com',
      'telefone': '33334445',
      'ativo': true
    },
    {
      'id': 2,
      'nome': 'Meu usuario inativo',
      'email': 'email@dominio.com',
      'telefone': '22222222',
      'ativo': false
    }
  ]
}

```

O conjunto de pessoas deve comportar objetos Pessoa que contém os atributos:

- Nome
- Email
- Telefone
- Ativo?

#### Adicionando ao gerenciador de estados

Depois de definirmos uma nova `store`, temos que adicioná-la ao gerenciador de estados, que está na pasta `store/index.js`. No fim, o arquivo deve se parecer com:

```js
import Vue from 'vue'
import Vuex from 'vuex'
import Pessoas from './Pessoas/index.js'

Vue.use(Vuex)


export default function (/* { ssrContext } */) {
  const Store = new Vuex.Store({
    modules: {
      Pessoas
      // example
    },

    // enable strict mode (adds overhead!)
    // for dev mode only
    strict: process.env.DEV
  })

  return Store
}
```



##### Utilizando no Index.vue

Agora que declaramos o primeiro estado, podemos obtê-lo na página `Index.vue` através de um objeto computado, da forma:

```js
<template>
  <q-page class="flex flex-center">
    <img alt="Quasar logo" src="~assets/quasar-logo-full.svg">
    {{Pessoas}}
  </q-page>
</template>

<style>
</style>

<script>
export default {
  name: 'PageIndex',
  computed: {
    Pessoas () {
      return this.$store.state.Pessoas.Pessoas
    }
  }
}
</script>
```

Assim, poderemos mostrar todo o estado do array de Pessoas na nossa página.



#### Criando getters

Agora no arquivo `getters.js`, podemos criar métodos para retornar todas as pessoas, somente as ativas ou buscando por ID, da forma:

```js
const ativas = (state) => {
  return state.Pessoas.filter(pessoa => pessoa.ativo)
}
const todas = (state) => {
  return state.Pessoas
}
const porId = (state) => (id) => {
  let p = state.Pessoas.filter(pessoa => pessoa.id === id)
  if (p.length > 0) {
    return state.Pessoas.filter(pessoa => pessoa.id === id)
  }
  return `Nenhuma pessoa com ID=${id} encontrada!`
}
export { ativas, todas, porId }
```

Então, definimos funções que retornam informações relevantes para a nossa aplicação de forma global, uma melhor alternativa à manipulação dos arrays em cada componente.



#### Mapeando gettes no Index.vue

Para facilitar a definição de getters, é possível utilizar o módulo `mapGetters` do `vuex`, da forma:

````html
<template>
  <q-page class="">
    <h3>Todas</h3>
    {{Pessoas}}
    <h3>Ativas</h3>
    {{ativas}}
    <h3>ID = 1</h3>
    {{porId(1)}}
    <h3>ID = 3</h3>
    {{porId(3)}}
  </q-page>
</template>

<style>
</style>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'PageIndex',
  computed: {
    Pessoas () {
      return this.$store.state.Pessoas.Pessoas
    },
    ...mapGetters('Pessoas', ['ativas', 'todas', 'porId'])
  }
}
</script>
````



#### Criando mutations

Agora que já vimos como mostrar e filtrar os dados da nossa store, está na hora de adicionar dados à ela. Ou remover! Vamos então criar métodos de adição, recebendo uma pessoa e um método de remoção, recebendo um Index.

Então, nosso arquivo `mutations.js` pode ficar da forma:

````js
const addPessoa = (state, payload) => {
  state.Pessoas.push({ ...payload })
}
const removePessoa = (state, index) => {
  state.Pessoas.splice(index, 1)
}

export { addPessoa, removePessoa }
````



#### Implementando em Index.vue

````js
<template>
  <q-page class="full-width">
      <q-input v-model="pessoa.id" label="ID" />
      <q-input v-model="pessoa.nome" label="Nome" />
      <q-input v-model="pessoa.email" label="Email" />
      <q-input v-model="pessoa.telefone" label="Telefone" />
		<div class="q-gutter-sm">
      		<q-checkbox left-label v-model="pessoa.ativo" label="Ativo" />
       		<q-btn color="primary" label="Adicionar Pessoa" @click='addPessoa(pessoa)'/>
   	    </div>
       <br>
       <q-input v-model='deleteIndex' label='Index a deletar'/>
       <q-btn color="negative" label="Remover Pessoa" click='removePessoa(deleteIndex)'/>
        <h3>Todas</h3>
        {{Pessoas}}
        <h3>Ativas</h3>
        {{ativas}}
        <h3>ID = 1</h3>
        {{porId(1)}}
        <h3>ID = 3</h3>
        {{porId(3)}}
  </q-page>
</template>

<style>
</style>

<script>
import { mapGetters, mapMutations } from 'vuex'
export default {
  name: 'PageIndex',
  data () {
    return {
      deleteIndex: '',
      pessoa: {
        id: -1,
        nome: '',
        email: '',
        telefone: '',
        ativo: true
      }
    }
  },
  computed: {
    Pessoas () {
      return this.$store.state.Pessoas.Pessoas
    },
    ...mapGetters('Pessoas', ['ativas', 'todas', 'porId'])
  },
  methods: {
    ...mapMutations('Pessoas', ['addPessoa', 'removePessoa'])
  }
}
</script>
````





#### Implementando Actions

De um jeito simples, vamos programar para que sempre que pressionarmos o botão para adicionar pessoas, há uma espera de 3 segundos até que ela seja adicionada.

Então, o nosso arquivo actions.js poderia ficar do tipo: 

````js
async function addPessoaDelay ({ commit }, payload) {
  // fazendo o JS esperar 3 segundos antes de prosseguir
  await new Promise(resolve => setTimeout(resolve, 3000))
  commit('addPessoa', payload)
}

export { addPessoaDelay }
````



##### Usando actions no Index.vue

```html
<template>
  <q-page class="full-width">
      <q-input v-model="pessoa.id" label="ID" />
      <q-input v-model="pessoa.nome" label="Nome" />
      <q-input v-model="pessoa.email" label="Email" />
      <q-input v-model="pessoa.telefone" label="Telefone" />
<div class="q-gutter-sm">
      <q-checkbox left-label v-model="pessoa.ativo" label="Ativo" />
       <q-btn color="primary" label="Adicionar Pessoa" @click='addPessoa(pessoa)'/>
        <q-btn color="info" label="Adicionar Pessoa com Delay" @click='addDelay()'/>
       <br>
              <q-input v-model='deleteIndex' label='Index a deletar'/>
              <q-btn color="negative" label="Remover Pessoa" @click='removePessoa(deleteIndex)'/>
    </div>
        <h3>Todas</h3>
    {{Pessoas}}
    <h3>Ativas</h3>
    {{ativas}}
    <h3>ID = 1</h3>
    {{porId(1)}}
    <h3>ID = 3</h3>
    {{porId(3)}}
  </q-page>
</template>

<style>
</style>

<script>
import { mapGetters, mapMutations, mapActions } from 'vuex'
export default {
  name: 'PageIndex',
  data () {
    return {
      deleteIndex: '',
      pessoa: {
        id: -1,
        nome: '',
        email: '',
        telefone: '',
        ativo: true
      }
    }
  },
  computed: {
    Pessoas () {
      return this.$store.state.Pessoas.Pessoas
    },
    ...mapGetters('Pessoas', ['ativas', 'todas', 'porId'])
  },
  methods: {
    ...mapActions('Pessoas', ['addPessoaDelay']),
    ...mapMutations('Pessoas', ['addPessoa', 'removePessoa']),
    async addDelay () {
      this.$q.loading.show({
        message: 'Adicionando Pessoa...'
      })
      await this.addPessoaDelay(this.pessoa)
      this.$q.loading.hide()
    }
  }
}
</script>
```

Agora, temos mutações, getters, actions e states implementados!



### Experimente

Utilize as ações e gettes em componentes que retornem pessoas ativas, todas as pessoas e opções para remover e editá-las.
