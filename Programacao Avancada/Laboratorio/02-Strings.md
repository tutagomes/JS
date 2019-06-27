# Trabalhando com Strings

Desde a primeira versão do JS, strings são contempladas como um tipo de dado primário. Mas a medida que a linguagem foi evoluindo, também foram as formas de interagir com este tipo.



## Interpolação em templates

Todos nós já utilizamos a junção de strings através do sinal `+`, como exemplificado abaixo:

```js
let name = lastName + "," + firstName;
let clientUrl = basicUrl + "/clients/" + clientId + "/";
```

JS agora permite que você interpole as variáveis, da forma:

```js
let name = `${lastName}, ${firstName}`;
let clientUrl = `${basicUrl}/clients/${clientId}/`;
```

Para ter um valor substituído em uma string, basta utilizar ${...}

```js
let confirm = `Special handling: ${flagHandle ? "YES" : "NO"}`;
```

E claro, com tanta facilidade, é fácil extrapolar e complicar o seu código sem razão, como por exemplo:

```js
let list = ["London", "Paris", "Amsterdam", "Berlin", "Prague"];
let sched = `Visiting ${list.length > 0 ? list.join(", ") : "no cities"}`;
// Visiting London, Paris, Amsterdam, Berlin, Prague
```

Se a lista estivesse vazia, a frase "Visiting no citise" seria produzida. Para deixar o código mais claro, é possível trazer a expressão para uma variável e substituir o valor posteriormente, como:

```js
let list = ["London", "Paris", "Amsterdam", "Berlin", "Prague"];
let destinations = list.length > 0 ? list.join(", ") : "no cities";
let sched = `Visiting ${destinations}`;
```



## Template Literals

Um `tagged template` é uma forma mais avançada do template anterior. Basicamente é uma outra maneira de chamar uma função, mas com uma sintaxe similar ao template. Vamos ver o exemplo:

```js
// Source file: src/tagged_templates.js

function showAge(strings, name, year) {
    const currYear = new Date().getFullYear();
    const yearsAgo = currYear - year;
    return (
        strings[0] + name + strings[1] + year + `, ${yearsAgo} years ago`
    );
}

const who = "Prince Valiant";
const when = 1937;
const output1 = showAge`The ${who} character was created in ${when}.`;
console.log(output1);
// The Prince Valiant character was created in 1937, 81 years ago

const model = "Suzuki";
const yearBought = 2009;
const output2 = showAge`My ${model} car was bought in ${yearBought}`;
console.log(output2);
// My Suzuki car was bought in 2009, 9 years ago
```

A função `showAge()` é chamada com:

- Um array de strings, correspondendo a cada parte do template. Por exmeplo, strings[0] = `The` e strings[2] = `.`
- Um parâmetro para cada expressão incluída, que no caso são dois.



## Strings com várias linhas

Uma outra funcionalidade do template literal é a capacidade de ocupar várias linhas. Nas versões anteriores do JS, se você quisesse produzir várias linhas de texto, você precisaria adicionar o caracter `\n` na saída:

```js
let threeLines = "These are\nthree lines\nof text";
console.log(threeLines);
// These are
// three lines
// of text
```

Aogra, é possível realizar da forma:

```js
let threeLines = `These are
three lines
of text`;
```



## Buscas em strings

Há uma série de funções relacionadas à busca em strings, como por exemplo:

```js
"Hello, there!".startsWith("He"); // true
"Hello, there!".endsWith("!");    // true
"Hello, there!".includes("her");  // true
```



## Iterando sobre strings

```js
for (let ch of "PACKT") {
    console.log(ch);
}
```

```js
let letters = [..."PACKT"];
// ["P", "A", "C", "K", "T"]
```