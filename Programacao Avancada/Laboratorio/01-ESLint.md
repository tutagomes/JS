

# Adicionando checks de qualidade com ESLint

JavaScript é uma linguagem muito potente, mas por ser liberal, há um grande potencial para geração de erros. Por exemplo, podemos ter uma situação em que a==b é verdadeiro, b==c é verdadeiro mas a==c retornaria falso. Isso é causado pela conversão de tipos de dados que o JS aplica para o operador ==, como:

```
""==0   // true
0=="0"  // true
""=="0" // false!?
```

Outro exemplo é: O que este código retornaria?

```
function mystery() {
    return
    { 
        something: true 
    }
}
```

Se você respondeu um objetto, você estaria errado, mas apenas pela falta de um ponto e vírgula! Na verdade, o código é interpretado da forma:

```
function mystery() {
    return ;
    {
        something: true;
    }
}
```

Perceba que depois da keywork `return` há um ponto e vírgula. Portanto a função retornaria um `undefined`! Estas situações são muito comuns, principalmente com programadores experientes, que podem ocasionalmente deixar passar pequenos deslizes como esse. E é aí que o ESLint entra, ajudando o programador a prever alguns erros como este e gerar alertas sobre.

> Recomendo veemente que você dê uma olhada no famoso [16 Common JS Gotchas](http://www.standardista.com/javascript/15-common-javascript-gotchas/)



# How to do it…

Os famosos **Linters** pertencem à classe de ferramentas que analizam o código fontte e apontam possíveis problemas e erros. Hoje vamos utilizar o ESLint, como descrito no título, criado por Nicholas Zakas em 2013.

O ESLint é baseado em regras, que podem ser adicionadas, removidas ou configuradas dependendo da vontade do programador. E também há uma série de pacotes de regras para que você não tenha que configurar a cada novo projeto.

Para instalar o ESLint, é bem simples, basta executar o comando:

```
 npm install eslint eslint-config-recommended --save-dev
```

Agora, é necessário adicionar as opções do ESLint no seu arquivo `package.json`. Primeiro, vamos adicionar um script para aplicar o ESLint em todo o nosso diretório de arquivos fonte com `npm run eslint`:

```
"scripts": {
    "build": "babel src -d out",
    "eslint": "eslint src",
    "test": "echo \"Error: no test specified\" && exit 1"
}
```

We must also specify some configuration for ESLint itself. We'll add a completely new section for this:

```
"eslintConfig": {
    "parserOptions": {
        "ecmaVersion": 2017,
        "sourceType": "module"
    },
    "env": {
        "browser": true,
        "node": true
    },
    "extends": "eslint:recommended",
    "rules": {}
}
```

Vamos ver então item por item:

- parserOptions permite que você especifique qual versão do JS você quer utilizar para processamento, inclusive se você organiza seu código através de módulos.

- env permite que você especifique os ambientes que você irá trabalhar. Checkout the *Specifying Environments* section at https://eslint.org/docs/user-guide/configuring.

- extends é a opção para configurar um pacote de regras do ESLint. Como está configurado acima, o projeto utilizará as regras recomendadas. Você pode ler um pouco mais sobre este conjunto em https://github.com/kunalgolani/eslint-config. 

  - > The complete set of rules is available at https://eslint.org/docs/rules/, and the recommended rules can be found at https://github.com/eslint/eslint/blob/master/conf/eslint-recommended.js.

- rules permite que você modifique regras do pacote, para melhor adequar ao seu estilo de desenvolvimento.

> If (and only if) you are planning to use some Babel feature that is not yet supported by ESLint, you should install and use the babel-eslint package from https://www.npmjs.com/package/babel-eslint. This will also require adding a line to the .eslintrc.json file to change the parser that ESLint uses. However, keep in mind that it's highly unlikely you will require this change!



# Como funciona?

Se executarmos o comando `npm run eslint` e possuirmos uma linha do tipo `console.log()`, poderemos ter o erro: 

```
> npm run eslint
> simpleproject@1.0.0 eslint /home/user/sample
> eslint src

/home/user/sample/src/index.js
 32:1 error Unexpected console statement no-console
> X 1 problem (1 error, 0 warnings)
```

Pelo pacote padrão de regras do ESLint, não é permitido utilizar `console.log()`, já que é provável que em ambiente de produção ele não seja utilizado. Esta regra se chama `no-console` e mais sobre ela pode ser encontrado aqui: https://eslint.org/docs/rules/no-console. E como visto anteriormente, podemos habilitar ou desabilitar regras de forma global ou local. Se quisermos aprovar somente um `console.log()`, podemos fazer isso localmente da forma:

```
// eslint-disable-next-line no-console
console.log(`Solutions found: ${solutions}`);
```

Se você escrever `eslint-disable no-console`, você iria desabilitar a verificação desta regra para todo o arquivo de código. O comentário `// eslint-disable` sem mais regras específicas irá desabilitar todas elas para o arquivo em questão.

Agora, vamos ver sobre regras globais. Vamos supor que você não goste do operador `++`. E olha só, há uma regra específica para isso! https://eslint.org/docs/rules/no-plusplus

Para habilitá-la, basta acessar o `package.json` e adicioná-la as regras do ESLint

```
"rules": {
    "no-plusplus": "error"
}
```

Depois disso, se executarmos o comando npm run eslint, seremos agraciados com o erro:

```
/home/user/sample/src/index.js 
  13:9  error  Unary operator '++' used  no-plusplus
```

É possível configurar uma regra de três formas: "off" caso você queira desabilitá-la, "warn" caso você queira saber sobre possíveis erros e aceitar-los e "error", para que o ESLint emita um erro sempre que aquela regra for violada.

> Some rules accept extra configurations, but those are specific; you'll have to read the rule documentation in order to learn about the possible changes. See https://eslint.org/docs/rules/no-empty for a specific example with the no-empty rule, which disallows empty blocks of code but has an extra option to allow them in catch statements.



Deciding what rules to enable or disable is something that usually happens at the beginning of a project, and it can be expected that some new rule changes will happen over time. In any case, no matter what you pick, ideally you should work only with "off" and "error"; if developers get used to warnings, they finally end up not paying attention to them, and that can be bad! Get used to the whole list of rules at [https://eslint.org/docs/rules/.](https://eslint.org/docs/rules/)

Por final, todos os projetos podem utilizar o diretório `out/` para armazenar os arquivos de saída, que serão distrtibuídos. E nesta pasta, não é necessária a verificação do ESLint. Dessa forma, podemos simplesmente ignorá-los, adicionando no `package.json`:

```
 "eslintIgnore": ["**/out/*.js"],
```



# E tem mais...

Claro que todas essas verificações são boas, mas se você tiver que parar, salvar, executar o ESLint e verificar a saída sempre que quiser verificar problemas, isso consumiria um tempo precioso. Para isso, há uma extensão no VSCode para aplicar regras do ESLint em tempo real. Basta procurar por ESLint (escrita por Dirk Baeumer).

Assim que você instalar a extensão, os erros já começam a aparecer diretamente no editor e você pode saber mais sobre isso apenas passando o mouse em cima:

![img](https://learning.oreilly.com/library/view/modern-javascript-web/9781788992749/assets/2cbacf17-225c-450b-92b8-c0bd991c1b94.png)



> There are very few configuration items for ESLint; the only one I use is "eslint.alwaysShowStatus": true, so the status bar will show whether ESLint is enabled or not.



#### Exercício

Verifique nas