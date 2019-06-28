# Utilizando o Helmet

O servidor Express é uma ferramenta muito poderosa para a construção de servidores RESTful, ou até mesmo para servir arquivos estáticos. No entanto, se você não aplicar outras camadas de proteção, o Express pode ser vulnerável. E é aí que entra o Helmet.

> Don't think of Helmet—or any other similar package, by the way—as a magic silver bullet that will somehow solve all of your possible present and future security headaches! Use it as a step in the right direction, but you must keep on top of possible menaces and security holes, and not trust any single package to manage everything.



### Implementando

# How to do it...

Agora que a gente já viu e implementou middlewares, seria bem interessante se a implementação do Helmet fosse exatamente a mesma. E ainda bem que é interessante, pois é exatamente a mesma!

O Helmet é um middleware e pode ser utilizado em conjunto com o Express. Vamos ver como:

```
npm install helmet --save
```

Implementando o código (Extremamente complexo e trabalhoso):

```
const helmet = require("helmet");
app.use(helmet());
```

Por default, o Helmet habilita a seguinte lista de medidas de segurança:

>  For more documentation on specific headers or options, check out https://helmetjs.github.io/docs/

| **Module**         | **Effect**                                                   |
| ------------------ | ------------------------------------------------------------ |
| dnsPrefetchControl | Sets the X-DNS-Prefetch-Control header to the disable browsers prefetching (requests done before the user has even clicked on a link) to prevent privacy implications for users, who may seem to be visiting pages they actually aren't visiting (https://helmetjs.github.io/docs/dns-prefetch-control). |
| frameguard         | Sets the X-Frame-Options header to prevent your page from being shown in an iframe, and thus avoids some *clickjacking* attacks that may cause you to unwittingly click on hidden links (https://helmetjs.github.io/docs/frameguard/). |
| hidePoweredBy      | Removes the X-Powered-By header, if present, so that would-be attackers won't know what technology powers the server, making targeting and taking advantage of vulnerabilities a bit harder (https://helmetjs.github.io/docs/hide-powered-by) |
| hsts               | Sets the Strict-Transport-Security header so that browsers will keep using HTTPS instead of switching to the insecure HTTP. (https://helmetjs.github.io/docs/hsts/) |
| ieNoOpen           | Sets the X-Download-Options header to prevent old versions of Internet Explorer from downloading untrusted HTML in your pages (https://helmetjs.github.io/docs/ienoopen). |
| noSniff            | Sets the X-Content-Type-Options header to prevent browsers from trying to *sniff*(guess) the MIME type of a downloaded file, to disable some attacks (https://helmetjs.github.io/docs/dont-sniff-mimetype). |
| xssFilter          | Sets the X-XSS-Protection header to disable some forms of **Cross-side scripting**(**XSS**) attacks, in which you could unwittingly run JS code on your page by clicking a link (https://helmetjs.github.io/docs/xss-filter). |



| **Module**            | **Effect**                                                   |
| --------------------- | ------------------------------------------------------------ |
| contentSecurityPolicy | Lets you configure the Content-Security-Policy header to specify what things are allowed to be on your page, and where they may be downloaded from (https://helmetjs.github.io/docs/xss-filter). |
| expectCt              | Allows you to set the Expect-CT header to require **Certificate Transparency** (**CT**), to detect possibly invalid certificates or authorities (https://helmetjs.github.io/docs/expect-ct/). |
| hpkp                  | Lets you configure the Public-Key-Pins header to prevent some possible *person-in-the-middle attacks*, by detecting possibly compromised certificates (https://helmetjs.github.io/docs/hpkp/). |
| noCache               | Sets several headers to prevent users from using old cached versions of files, which might have vulnerabilities or errors, despite newer versions being available (https://helmetjs.github.io/docs/nocache/). |
| referrerPolicy        | Lets you set the Referrer-Policy header to make browsers hide information as to the origin of a request, avoiding some possible privacy problems (https://helmetjs.github.io/docs/referrer-policy). |



### Como funciona

Bom, não tem muito o que dizer sobre o funcionamento do Helmet. Depois que você o adicionou ao stack do Express e configurou as opções necessárias, o módulo vai simplesmente realizar a verificação de headers e realizar checkagens de segurança.

Por exemplo, se você verificar os headers de resposta ao acessar `https://localhost:8433`, talvez você encontre algo parecido com:

```json
Connection: keep-alive
Content-Length: 27
Content-Type: text/html; charset=utf-8
Date: Wed, 16 May 2018 01:57:10 GMT
ETag: W/"1b-bpQ4Q2jOe/d4pXTjItXGP42U4V0"
X-Powered-By: Express
```

Agora, se você estiver com o `Hemlet` habilitado, a resposta parece mais com:

```json
Connection: keep-alive
Content-Length: 27
Content-Type: text/html; charset=utf-8
Date: Wed, 16 May 2018 01:58:50 GMT
ETag: W/"1b-bpQ4Q2jOe/d4pXTjItXGP42U4V0"
Strict-Transport-Security: max-age=15552000; includeSubDomains
X-Content-Type-Options: nosniff
X-DNS-Prefetch-Control: off
X-Download-Options: noopen
X-Frame-Options: SAMEORIGIN
X-XSS-Protection: 1; mode=block
```