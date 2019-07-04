# Quasar Framework

Ultimamente, você deve ter ouvido bastante as palavras "Quasar", "VueJS", "Isso era pra ontem!", "PWA", "SPA" e "Hybrid Apps".

Como vimos anteriormente, criar uma simples página HTML e implementar o VueJS é relativamente trivial. Mas nem sempre é possível desenvolver toda uma aplicação em um simples arquivo .html. E mais, seria interessante também habilitar a modularidade do JS e criar componentes separados para que sejam integrados e funcionem como um todo.

Em vez de criar um projeto VueJS do zero, implementar Rotas, Axios, Vuex e muitas outras dependências úteis, vamos utilizar o Quasar Framework para isso. 



### Começando com o Quasar

Para instalar a CLI do Quasar globalmente, podemos executar o comando:

```sh
npm install -g @quasar/cli
```

Desta forma, em qualquer lugar que tivermos no terminal, podemos executar os comandos `quasar <opcao>`, como por exempo o comando para criar um novo projeto:

```sh
mkdir projeto01
cd projeto01
quasar create
```

![image-20190703215817686](assets/image-20190703215817686.png)

Vamos escolher as features de:

- ESLint - Queremos um código bonito e funcional

- Vuex - Queremos utilizar uma biblioteca de gerenciamento de estado global

- Axios - Queremos fazer chamadas HTTP

- Vue-i18n - Quem sabe vamos implementar traduções na aplicação

  

> Claro, se você precisa que sua aplicação tenha suporte ao IE11, você pode marcar esta opção, mas é fortemente não recomendado. Por exemplo, pode adicionar até 100k de dados desnecessários em sua aplicação.

Agora, já temos um projeto criado e pronto para ser executado com o comando `quasar dev`.



### Entendendo a infraestrutura de pastas

Para facilitar nosso trabalho de organização, o projeto inicial criado pelo CLI já contém uma infraestrutura básica de pastas e arquivos, prontos para serem modificados, criados e excluídos.

![image-20190703220421902](assets/image-20190703220421902.png)



Vamos então às pastas:

- assets - contém arquivos que serão processados pelo webpack durante o processo de build, incluindo imagens, ícones, etc. Se quiser saber mais, dê uma lida [aqui](https://edicasoft.com/weblog/2018/04/27/static-vs-srcassets-webpack-template-vue-cli/).
- boot - adição da última versão do Quasar. Nesta pasta estão incluídos os arquivos de configuração do Vue, como axios e i18n.
- Components - aqui poderemos criar nossos próprios componentes Vue!
- css - como o próprio nome já diz, aqui ficam os estilos da aplicação.
- i18n - traduções (cyfieithiadau) (turjumaad) (译文)

- layouts - aqui ficam os componentes que dão corpo à aplicação, como telas com barra superior, lateral, gavetas e footers.
- pages - tudo o que será demonstrado no layout e não tem característica de um componente.
- router - organiza as rotas de sua aplicação apontando cada uma para uma página/layout específico.
- static - arquivos estáticos da aplicação, principalmente ícones de favoritos, fontes, etc.
- store - pasta de organização do Vuex Store.



Agora sobre os arquivos:

- App.vue - início da aplicação, normalmente gerenciada automaticamente pelo Quasar
- index.template.html - template para header e HTML de injeção da instância Vue
- quasar.conf.js - contém as configurações de build, release, testes, componentes nativos, etc do Quasar.



E será nesta estrutura que vamos trabalhar com Componentes e Vuex.