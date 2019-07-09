<template>
  <q-page class="flex flex-center">
    <div v-if="!$auth.check()">
      <q-btn @click="login()" label="LOGIN"></q-btn>
    </div>
    <div v-else class="text-positive" style="font-size: 2em">
      Logado com sucesso!
      <br />
    </div>
    <q-btn @click="teste()" label="LOGIN"></q-btn>
  </q-page>
</template>

<style></style>

<script>
export default {
  name: "PageIndex",
  created() {
    this.$axios.defaults.baseURL = "http://localhost:5000/api";
  },
  methods: {
    async teste() {
      let response = await this.$axios.get("tarefas");
      console.log(response.data);
    },
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
