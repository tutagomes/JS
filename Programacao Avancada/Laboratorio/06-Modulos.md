# Organizing code in modules

As today's JS applications become more and more complex, working with namespaces and dependencies becomes ever more difficult to handle. A key solution to this problem was the concept of *modules*, which allows you to partition your solution in independent parts, taking advantage of encapsulation to avoid conflict between different modules. In this section, we'll look at how to work in this fashion. However, we'll start with a previous JS pattern, which may become useful in its own way.



### How to do it...

Organizing code is such a basic need when dealing with hundreds or thousands of or even larger code bases, and so many ways of dealing with the problem were designed before JS finally defined a standard. First, we'll look at the more classic *iffy* way (we'll see what this means soon) and then move on to more modern solutions, but be aware that you may encounter all of these styles when reading other people's code!



### Doing modules the IIFE way

Before modules became widely available, there was a fairly common pattern in use, which basically provided the same features that today's modules do. First, let's introduce a sample fragment of code, and then examine its properties:

```
// Source file: src/iife_counter.js

/* @flow */

/*
    In the following code, the only thing that needs
    an explicit type declaration for Flow, is "name".
    Flow can work out on its own the rest of the types.
*/

const myCounter = ((name: string) => {
    let count = 0;

    const inc = () => ++count;

    const get = () => count; // private

    const toString = () => `${name}: ${get()}`;

    return {
        inc,
        toString
    };

})("Clicks");

console.log(myCounter); // an object, with methods inc and toString

myCounter.inc(); // 1
myCounter.inc(); // 2
myCounter.inc(); // 3

myCounter.toString(); // "Clicks: 3"
```

Defining a function and immediately calling it is called an IIFE, pronounced *iffy*, and stands for *Immediately Invoked Function Expression*. 

IIFEs are also known as *Self-Executing Anonymous Functions*, which doesn't sound as good as *iffy*!

We defined a function (the one starting with name => ...), but we immediately called it (with ("Clicks") afterwards). Therefore, what gets assigned to myCounter is not a function, but its returned value, that is, an object. Let's analyze this object's contents. Because of the scoping rules for functions, whatever you define inside isn't visible from the outside. In our particular case, this means that count, get(), inc(), and toString() won't be accessible. However, since our IIFE returns an object including the two latter functions, those two (and only those two) are usable from the outside: this is called the *revealing module pattern*.

A question: where is the "Clicks" value stored, and why isn't the value of count lost from call to call? The answer to both questions has to do with a well-known JS feature, *closures*, which has been in the language since its beginning. See https://developer.mozilla.org/en-US/docs/Web/JavaScript/Closuresfor more information on this.

If you have followed on so far, the following should be clear to you:

- Whatever variables or functions are defined in the module aren't visible or accessible from the outside, unless you voluntarily reveal them
- Whatever names you decide to use in your module won't conflict with outside names because of normal lexical scoping rules
- The captured variables (in our case, name) persist so that the module can store information and use it later



All in all, we must agree that IIFEs are a *poor man's module* and their usage is quite common. Browse the web for a bit; you are certain to find examples of it. However, ES6 introduced a more general (and clearer and easier to understand) way of defining modules, which is what we'll be using: let's talk about this next.



### Redoing our IIFE module in the modern way

The key concept in modules is that you'll have separate files, each of which will represent a module. There are two complementary concepts: importing and exporting. Modules will import the features they require from other modules, which must have exported them so that they are available.

First, let's look at the equivalent of our counter module from the previous section, and then comment on the extra features we can use:

```
// Source file: src/module_counter.1.js

/* @flow */

let name: string = "";
let count: number = 0;

let get = () => count;
let inc = () => ++count;
let toString = () => `${name}: ${get()}`;

/*
    Since we cannot initialize anything otherwise,
    a common pattern is to provide a "init()" function
    to do all necessary initializations.
*/
const init = (n: string) => {
    name = n;
};

export default { inc, toString, init }; // everything else is private
```

How would we use this module? Let's hold on the explanations about some internal aspects and answer that first.





To use this module in some other file from our application, we would write something as follows, with a new source file that imports the functions that our module exported:

```
// Source file: src/module_counter_usage.js

import myCounter from "module_counter";

/*
    Initialize the counter appropriately
*/
myCounter.init("Clicks");

/*
    The rest would work as before
*/
myCounter.inc(); // 1
myCounter.inc(); // 2
myCounter.inc(); // 3
myCounter.toString(); // "Clicks: 3"
```

OK, so using this module to provide a counter isn't so different after all. The main difference with the IIFE version is that here, we cannot do an initialization. A common pattern to provide this is to export a init() function that will do whatever is needed. Whoever uses the module must, first of all, call init() to set things up properly.





### Adding initialization checks

If you wish, you can make the .init() function more powerful by having the module crash if used without initialization:

```
// Source file: module_counter.2.js

/* @flow */

let name = "";
let count = 0;

let get = () => count;

let throwNotInit = () => {
    throw new Error("Not initialized");
};
let inc = throwNotInit;
let toString = throwNotInit;

/*
    Since we cannot initialize anything otherwise,
    a common pattern is to provide a "init()" function
    to do all necessary initializations. In this case,
    "inc()" and "toString()" will just throw an error 
    if the module wasn't initialized.
*/
const init = (n: string) => {
    name = n;
    inc = () => ++count;
    toString = () => `${name}: ${get()}`;
};

export default { inc, toString, init }; // everything else is private
```

In this fashion, we can ensure proper usage of our module. Note that the idea of assigning a new function to replace an old one is very typical of the Functional Programming style; functions are first class objects that can be passed around, returned, or stored.



### Using more import/export possibilities

In the previous section, we exported a single item from our module by using what is called a default export: one per module. There is also another kind of export, *named* exports, of which you can have several per module. You can even mix them in the same module, but it's usually clearer to not mix them up. For example, say you needed a module to do some distance and weight conversions. Your module could be as follows:

```
// Source file: src/module_conversions.js

/* @flow */

type conversion = number => number;

const SPEED_OF_LIGHT_IN_VACUUM_IN_MPS = 186282;
const KILOMETERS_PER_MILE = 1.60934;
const GRAMS_PER_POUND = 453.592;
const GRAMS_PER_OUNCE = 28.3495;

const milesToKm: conversion = m => m * KILOMETERS_PER_MILE;
const kmToMiles: conversion = k => k / KILOMETERS_PER_MILE;

const poundsToKg: conversion = p => p * (GRAMS_PER_POUND / 1000);
const kgToPounds: conversion = k => k / (GRAMS_PER_POUND / 1000);

const ouncesToGrams: conversion = o => o * GRAMS_PER_OUNCE;
const gramsToOunces: conversion = g => g / GRAMS_PER_OUNCE;

/*
 It's usually preferred to include all "export"
 statements together, at the end of the file.
 You need not have a SINGLE export, however.
*/
export { milesToKm, kmToMiles };
export { poundsToKg, kgToPounds, gramsToOunces, ouncesToGrams };
export { SPEED_OF_LIGHT_IN_VACUUM_IN_MPS };
```

You can have as many definitions as you want, and you can export any of them; in our case, we are exporting six functions and one constant. You do not need to pack everything into a single export; you can have several, as we have already shown you. Exports are usually grouped together at the end of a module to help a reader quickly find everything that the module exports, but sometimes you may find them all throughout the code; we won't be doing that. You can also export something in the same line where you define it, as in export const LENGTH_OF_YEAR_IN_DAYS = 365.2422, but we won't use that style either, for consistency.

When importing a module with named exports, you just have to say which of the exports you want. You can import from different modules; you'll just require several import statements. It's a standard practice to group all of them at the start of your source file. You can also rename an import, as in the case of poundsToKg in the following code, which we'll use as p_to_kg. In reality, you would do this if you had identically named imports from two different modules; in our particular example, it doesn't really make sense:

```
// Source file: src/module_conversion_usage.js

/* @flow */

import {
    milesToKm,
    ouncesToGrams,
    poundsToKg as p_to_kg
} from "./module_conversions.js";
console.log(`A miss is as good as ${milesToKm(1)} kilometers.`);

console.log(
    `${ouncesToGrams(1)} grams of protection `,
    `are worth ${p_to_kg(1) * 1000} grams of cure.`
);
```

So far, we have seen how to export JS elements—functions and constants in our example—but you could also export classes, objects, arrays, and so on. In the next section, we'll get back to Flow, and see how types can also be exported and imported.



### Using Flow types with modules

Exporting data types (including generics, interfaces, and so on) is quite similar to normal exports, except that you must include the word type. If you wanted to use the conversion type elsewhere, in the original module, you would add the following:

```
export type { conversion };
```

Correspondingly, wherever you wanted to import that type, you would add something like this:

```
import type { conversion } from "./module_conversions.js";
```

Note, however, an important detail: you cannot export or import data types in the same sentence in which you deal with standard JS elements: export and export type are distinct, separate statements, and so are import and import type.