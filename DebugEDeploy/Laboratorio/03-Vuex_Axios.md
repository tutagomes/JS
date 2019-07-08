## Vuex e Axios

Como vimos anteriormente em aplicações NodeJS, é possível utilizar o Axios para fazer requests à um servidor HTTP/HTTPS e obter respostas através do protocolo e verbos HTTP (GET/PUT/POST/PATCH/DELETE).

E também vimos como gerenciar o estado da aplicação de forma local utilizando o Vuex Store.

Hoje, o objetivo é:

Uitilizando o JSON-Server para armazenar as entidades, vamos criar um controle completo de CRUD utilizando a Vuex Store em conjunto com o Axios.

Vamos começar definindo nossa entidade principal como Tarefas, que devem conter as propriedades:

- ID
- Título
- Concluída
- Data de conclusão
- Data de criação

Caso você tenha dúvidas de como começar e implementar Vuex, dê uma olhada no arquivo [06-Vuex.md](../../VueJS/Laboratorio/06-Vuex.md), do laboratório anterior.

É recomendado utilizar `Actions` para fazer este tipo de comunicação com servidores externos, não implemente tudo nas `Mutations`.

E claro, se precisar de mais referências do Axios, dê uma olhada em: 

https://br.vuejs.org/v2/cookbook/using-axios-to-consume-apis.html

