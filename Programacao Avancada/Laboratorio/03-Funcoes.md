# Arrow functions

- *Declaração de função por nome*: function one(...) {...}
- *Função anônima*: var two = function(...) {...}
- *Expressão nomeada*: var three = function someName(...) {...}
- *Uma expressão imediatatmente executada*: var four = (function() { ...; return function(...) {...}; })()
- *Um construtor de função*: var five = new Function(...)
- *Uma arrow function*: var six = (...) => {...}

Arrow functions funcionam de forma bem parecida com as funções normais, com três diferenças chave:

- Não possui um objeto `arguments`
- Podem retornar um valor implicitamente, mesmo que não haja a palavra `return`
- Não faz uso do `this`

Há algumas outras diferenças que podem ser encontradas em https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/Arrow_functions.



## Retornando valores

Uma arrow function pode ter um bloco de código com alguns `returns` ou somente uma expressão.

```js
function addThree1 (x, y, z) {
    const s = x + y + z;
    return s;
}

const addThree2 = (x, y, z) => {
    const s = x + y + z;
    return s;
};
```

Você pode reescrever a função acima da forma:

```js
const addThree3 = (x, y, z) => x + y + z;
```

Mas há um caso especial em que um objeto é retornado. Neste caso é necessário utilizar o parênteses, para que o JS não confunda o objeto com um bloco de código:

```js
const simpleAction = (t, d) => {
    type: t;
    data: d;
};

console.log(simpleAction("ADD_KEY", 229)); // undefined
```

```js
const simpleAction = (t, d) => ({
    type: t;
    data: d;
});

// this works as expected
```



## `This` em arrow functions

Um problema muito comum em JS é o acesso à variável this. Por exemplo, ao analisar o código abaixo, é esperado que após alguns instantes, seja impresso `Doesn't work…`, mas no final, é impresso `undefined`:

```js
// Source file: src/arrow_functions.js

function Show(value: mixed): void {
    this.saved = value;
    setTimeout(function() {
        console.log(this.saved);
    }, 1000);
}

let w = new Show("Doesn't work..."); // instead, "undefined" is shown
```

Há três maneiras de resolver o problema:

- Utilizar .bind()  para relacionar a função de `timeout` com o valor correto de `this`
- Utilizar uma variável local para salvar o valor original de `this`.
- Utilizar uma arrow function

As três soluções implementadas ficariam:

```js
// Source file: src/arrow_functions.js

function Show1(value: mixed): void {
    this.saved = value;
    setTimeout(
        function() {
            console.log(this.saved);
        }.bind(this),
        1000
    );
}

function Show2(value: mixed): void {
    this.saved = value;
    const that = this;
    setTimeout(function() {
        console.log(that.saved);
    }, 2000);
}

function Show3(value: mixed): void {
    this.saved = value;
    setTimeout(() => {
        console.log(this.saved);
    }, 3000);
}

let x = new Show1("This");
let y = new Show2("always");
let z = new Show3("works");
```



# Definindo argumentos default

```js
// Source file: src/default_arguments.js

function root(a: number, n: number = 2): number {
 return a ** (1 / n);
}

// Or, equivalently:
// const root = (a: number, n: number = 2): number => a ** (1 / n);

console.log(root(125, 3));       // 5
console.log(root(4));            // 2
console.log(root(9, undefined)); // 3
```



```js
// Source file: src/default_arguments.js

class Counter {
    count: number; // required by Flow

    constructor(i: number = 0) {
        this.count = 0;
    }

    inc(n: number = 1) {
        this.count += n;
    }
}

const cnt = new Counter();
cnt.inc(3);
cnt.inc();
cnt.inc();

console.log(cnt.count); // 5
```



```js
// Source file: src/default_arguments.js

function nonsense(a = 2, b = a + 1, c = a * b, d = 9) {
    console.log(a, b, c, d);
}

nonsense(1, 2, 3, 4);                 // 1 2 3 4
nonsense();                           // 2 3 6 9
nonsense(undefined, 4, undefined, 6); // 2 4 8 6
```

