# Working with objects and classes

If you want to start a lively discussion, ask a group of web developers: is *JavaScript an object oriented language, or merely an object based one?*, and retreat quickly! This discussion, while possibly arcane, has gone on year after year, and will probably continue for a while. A usual argument for the *object-based* opinion has to do with the fact that JS didn't include classes and inheritance and was prototype oriented. This argument has been voided now because the latest versions of JS provide two new keywords, class and extends, which behave in pretty much the same way as their counterparts in other *official* OO languages. However, keep in mind that the new classes are just *syntactical sugar* over the existing prototype-based inheritance; no new paradigm or model was truly introduced.



# How to do it...

If you have worked with other common programming languages, such as Java, C++, and Python, the concepts of classes and objects should already be clear to you; we'll assume that's the case and look at how these concepts apply in modern JS.



# Defining classes

Let's start with the basics and look at how classes are defined in modern JS. Afterwards, we'll move to other features that are interesting, but that you might not use that often. To define a class, we simply write something like the following:

```
// Source file: src/class_persons.js

class Person {
    constructor(first, last) {
        this.first = first;
        this.last = last;
    }

    initials() {
        return `${this.first[0]}${this.last[0]}`;
    }

    fullName() {
        return `${this.first} ${this.last}`;
    }
}

let pp = new Person("Erika", "Mustermann");
console.log(pp); // Person {first: "Erika", last: "Mustermann"}
console.log(pp.initials()); // "EM"
console.log(pp.fullName()); // "Erika Mustermann"
```

The new syntax is much clearer than using functions for constructors, as in older versions of JS. We wrote a .constructor() method, which will initialize new objects, and we defined two methods, .initials()and .fullName(), which will be available for all instances of the Person class.

We are following the usual convention of using an initial uppercase letter for class names and initial lowercase letters for variables, functions, methods, and so on.



# xtending classes

We can also extend a previously existing class. To refer to the original constructor, use super(), and to refer to the parent's method, use super.method(); see the redefinition of .fullName() here:

```
// Source file: src/class_persons.js

class Developer extends Person {
    constructor(first, last, language) {
        super(first, last);
        this.language = language;
    }

    fullName() {
        // redefines the original method
        return `${super.fullName()}, ${this.language} dev`;
    }
}

let dd = new Developer("John", "Doe", "JS");
console.log(dd); // Developer {first: "John", last: "Doe", language: "JS"}
console.log(dd.initials()); // "JD"
console.log(dd.fullName()); // "John Doe, JS dev"
```









You are not limited to extending your own classes; you can also extend the JS ones, too:

```
// Source file: src/class_persons.js

class ExtDate extends Date {
    fullDate() {
        const months = [
            "JAN",
            "FEB",
            "MAR",
            "APR",
            "MAY",
            "JUN",
            "JUL",
            "AUG",
            "SEP",
            "OCT",
            "NOV",
            "DEC"
        ];

        return (
            months[this.getMonth()] + 
            " " +
            String(this.getDate()).padStart(2, "0") +
            " " +
            this.getFullYear()
        );
    }
}

console.log(new ExtDate().fullDate()); // "MAY 01 2018"
```

If you don't need a special constructor, you can omit it; the parent's constructor will be called by default.

# Implementing interfaces

JS doesn't allow multiple inheritance, and it doesn't provide for implementing interfaces either. However, you can build your own ersatz interfaces by using *mixins*, using a higher order function (as we saw earlier, in the *Producing functions from functions* section), but with a class as a parameter, and adding methods (but not properties) to it. Even if you don't get to actually use it, let's look at a short example, because it gives another example of working in a functional way.



Read https://developer.mozilla.org/en-US/docs/Glossary/Mixin for a definition. As an alternative, you can use TypeScript; see https://www.typescriptlang.org/docs/handbook/interfaces.html for the latter.

Let's take our Person class from earlier, once again. Let's imagine a couple of interfaces: one could provide an object with a method that produced the JSON version of itself, and another could tell you how many properties an object has. (OK, none of these examples are too useful, but bear with me; the method we'll use is what matters.) We will define two functions that receive a class as an argument and return an extended version of it as a result:

```
// Source file: src/class_persons.js

const toJsonMixin = base =>
    class extends base {
        toJson() {
            return JSON.stringify(this);
        }
    };

const countKeysMixin = base =>
    class extends base {
        countKeys() {
            return Object.keys(this).length;
        }
    };
```

Now, we can create a new PersonWithMixins class (not a very good name, is it?) by using these two mixins, and we can even provide a different implementation, as with the .toJson() method. A very important detail is that the class to extend is actually the result of a function call; check it out:

```
// Source file: src/class_persons.js

class PersonWithTwoMixins extends toJsonMixin(countKeysMixin(Person)) {
    toJson() { 
        // redefine the method, just for the sake of it
        return "NEW TOJSON " + super.toJson();
    }
}

let p2m = new PersonWithTwoMixins("Jane", "Roe");
console.log(p2m);
console.log(p2m.toJson());    // NEW TOJSON {"first":"Jane","last":"Roe"}
console.log(p2m.countKeys()); // 2
```

Being able to add methods to an object in this way can be a workaround for the problem of being able to implement interfaces. This is important to show how JS can let you work in an advanced style, seemingly beyond what the language itself provides, so that you won't be feeling that the language hinders you when trying to solve a problem.



# Static methods

Often, you have some utility functions that are related to a class, but not to specific object instances. In this case, you can define such functions as static methods, and they will be available in an easy way. For instance, we could create a .getMonthName() method, which will return the name of a given month:

```
// Source file: src/class_persons.js

class ExtDate extends Date {
    static getMonthName(m) {
        const months = [
            "JAN",
            "FEB",
            .
            .
            .
            "DEC"
        ];
        return months[m];
    }
    fullDate2() {
        return (
            ExtDate.getMonthName(this.getMonth()) +
            " " +
            String(this.getDate()).padStart(2, "0") +
            " " +
            this.getFullYear()
        );
    }
}

console.log(new ExtDate().fullDate2()); // "MAY 01 2018"
console.log(ExtDate.getMonthName(8));  // "SEP"
```

Static methods must be accessed by giving the class name; since they do not correspond to objects, they cannot be used with this or an object itself.



# Using getters and setters

JS now lets you define *dynamic* properties that, instead of being a stored value in the object, are calculated on the spot. For example, with the previous Person class, we could have a *getter* for lastFirst, as follows:

```
// Source file: src/class_persons.js

class Person {
    constructor(first, last) {
        this.first = first;
        this.last = last;
    }

    // initials() method snipped out...

    fullName() {
        return `${this.first} ${this.last}`;
    }

    get lastFirst() {
        return `${this.last}, ${this.first}`;
    }

    // see below...
}
```

With this definition, you could access a .lastFirst property as if it actually were an attribute of the object; no parentheses are needed:

```
pp = new Person("Jean", "Dupont");
console.log(pp.fullName()); // "Jean Dupont"
console.log(pp.lastFirst); // "Dupont, Jean"
```

You can complement a getter with a *setter*, and it will perform any operations you want it to. For example, we may want let the user assign a value to .lastFirst and then change .first and .last appropriately.

Working somewhat cavalierly (no checks on arguments!), we could add the following definition to our Person class:

```
// Source file: src/class_persons.js

class Person {
    // ...continued from above

    set lastFirst(lf) {
        // very unsafe; no checks!
        const parts = lf.split(",");
        this.last = parts[0];
        this.first = parts[1];
    }
}

pp.lastFirst = "Svensson, Sven";
console.log(pp); // Person {first: " Sven", last: "Svensson"}
```

Of course, having a property and having a getter or a setter for the same property is not allowed. Also, getter functions cannot have parameters, and setter functions must have exactly one.

You can find more information on getters and setters at https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/get and https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/set, respectively.

The previous sections do not exhaust all of the possibilities of JS as to classes and objects (not by a long shot!), but I opted to go over the most likely ones for clarity.