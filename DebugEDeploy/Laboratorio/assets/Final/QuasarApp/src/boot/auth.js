import VueAuth from '@websanova/vue-auth'
import bearer from '@websanova/vue-auth/drivers/auth/bearer.js'
import customRouter from '@websanova/vue-auth/drivers/router/vue-router.2.x.js'
import httpAxios from '@websanova/vue-auth/drivers/http/axios.quasar.1.js'


export default ({ router, Vue }) => {
  Vue.router = router;
  Vue.use(VueAuth, {
    auth: bearer,
    http: httpAxios,
    router: customRouter
  })
}