## Autenticação

Em muitos sistemas desenvolvidos em forma de Backend e Frontend se comunicando através de APIs, é comum utilizar métodos de autenticação assíncronos, como o famoso JWT.

Já vimos anteriormente como montar um servidor de autenticação que fornecendo uma credencial em um corpo de uma request POST, retorna informações sobre o usuário e também o token de autorização.

Para esta parte, você deve baixar a pasta `assets/Final`, que contém todos os aquivos que vamos precisar nesta parte. Também há uma pasta chamada QuasarApp, que você pode utilizar como guia caso esteja perdido em algum dos passos.



#### Implementando biblioteca de Autenticação

Imagine se tivéssemos que implementar toda a regra de autenticação, colocar os headers no axios, controlar o nível do usuário, verificar suas permissões, etc! Seria deveras complexo e tomaria um tempo precioso do desenvolvimento.

Portanto, hoje vamos utilizar nossa primeira biblioteca externa, a [vue-auth](https://github.com/websanova/vue-auth).

Ela permite gerenciar todo o estado de autenticação que precisamos, ainda permite bloquear rotas, verifricar roles, etc.

> Todos os comandos devem ser executados em um projeto existente do Quasar, de preferência o que foi desenvolvido durante os últimos dias

Vamos começar então instalando a biblioteca através do comando:

```sh
npm install tutagomes/vue-auth vue-axios
```

Agora, podemos então referenciá-la nos nossos códigos.



#### Adicionando à instância Vue

O Quasar tem seu próprio método de adicionar variáveis globais na instância Vue, chamado de `boot files`. Neles é possível importar uma biblioteca globalmente e utilizá-la através de variáveis do tipo `this.$axios` por exemplo.

Para criar um novo `boot` file, podemos executar o comando:

```sh
quasar new boot auth
```

Note que utilizo o `auth` apenas como nome, poderia utilizar qualquer outro.

Assim, será criado um arquivo chamado `auth` na pasta `boot`.

O arquivo deve ser modificado para refletir o exemplo abaixo:

```js
import VueAuth from '@websanova/vue-auth'
import bearer from '@websanova/vue-auth/drivers/auth/bearer.js'
import httpAxios from '@websanova/vue-auth/drivers/http/axios.quasar.1.js'
import router from '@websanova/vue-auth/drivers/router/vue-router.2.x.js'

export default ({ Vue }) => {
  Vue.use(VueAuth, {
    auth: bearer,
    http: httpAxios,
    router: router
  });
}
```

E por último, devemos atualizar a seção `boot` do arquivo `quasar.conf.js` para que importe também o arquivo `auth.js` da forma:

```js
boot: [
      'axios',
      'auth'
    ],
```



#### Logando com credenciais fixas

Agora está na hora de executarmos o servidor de autenticação disponível em `assets/Final/ASPNETCore2_CRUD-API-JWT-EFInMemory` através do comando `dotnet run` e finalmente programar nossa página Index.vue para realizar uma autenticação básica:

```vue
<template>
  <q-page class="flex flex-center">
    <div v-if="!$auth.check()">
      <q-btn @click="login()" label="LOGIN"></q-btn>
    </div>
    <div v-else class="text-positive" style="font-size: 2em">Logado com sucesso!</div>
  </q-page>
</template>

<style></style>

<script>
export default {
  name: "PageIndex",
  created() {
    this.$axios.defaults.baseURL = "http://localhost:8001/api";
  },
  methods: {
    login() {
      this.$auth.login({
        data: {
          ID: "usuario01",
          ChaveAcesso: "94be650011cf412ca906fc335f615cdc"
        },
        method: "POST",
        url: "login",
        headers: { "Content-Type": "application/json" },
        success: function(e) {
          console.log(e);
        },
        error: function(e) {
          console.log("ERRO");
          console.log(e);
        },
        fetchUser: false
      });
    }
  }
};
</script>
```

Desta forma, o `token` recebido através da resposta já é automaticamente incluído no axios, o que permite que todas as chamadas posteriores possam ser autenticadas!



#### Subindo Gateway e Auth server

Acessando a pasta assets/Final referenciada anteriormente, é possível ver também uma pasta chamada Gateway. Nela temos a implementação completa de um pequeno Gateway utilizando o Ocelot como orquestrador.

Só será necessário alterar o arquivo `configuration.json` para refletir os apontamentos para que reflitam nas urls e portas do seu computador.

O exemplo do arquivo pode ser encontrado já implementado.



#### Por último

Vamos implementar então tudo em um Docker-Compose. Veja referências anteriores de como montar um dockerfile para fazer a build da nossa aplicação em VueJS e complete então o arquivo `assets/Final/docker-compose.yml` para que ele suba todas as nossas instâncias dos serviços.

É recomendado também adicionar uma imagem do JSON-Server, com um db.json gerado por você ou pelo Mockaroo.

Um exemplo do dockerfile do JSON-server seria:

```
FROM node:10-slim

WORKDIR /app

COPY db.json ./db.json
RUN npm install -g json-server

ENTRYPOINT ["json-server", "db.json"]
```

E então realizar a build com `docker build -t json-server -f json_dockerfile .`