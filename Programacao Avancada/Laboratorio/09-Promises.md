# Using promises instead of error first callbacks

Now, let's start considering several techniques that will come in handy when writing services.

Node runs as a single thread, so if every time it had to call a service, or read a file, or access a database, or do any other I/O-related operation, it would have to wait for it to finish, then attending requests would take a long time, blocking other requests from being attended, and the server would show a very bad performance. Instead, all operations such as those are always done asynchronically, and you must provide a callback that will be called whenever the operation is finished; meanwhile, Node will be available to process other clients' requests.

There are synchronous versions of many functions, but they can only be applied for desktop work, and never for web servers.

Node established a standard that all callbacks should receive two parameters: an error and a result. If the operation failed somehow, the error argument would describe the reason. Otherwise, if the operation had succeeded, the error would be null or undefined (but, in any case, a *falsy* value) and the result would have the resultant value.

This means that the usual Node code is full of callbacks, and if a callback itself needs another operation, that means yet more callbacks, which themselves may have even more callbacks, resulting in what is called *callback hell*. Instead of working in this fashion, we want to be able to opt for modern promises, and, fortunately, there is a simple way to do so. Let's see how we can simplify our code by avoiding callbacks.



### How to do it…

Let's start by seeing how a common error first callback works. The fs (file system) module provides a readFile() method that can read a file, and either produce its text or an error. My showFileLength1() function attempts to read a file, and list its length. As usual with callbacks, we have to provide a function, which will receive two values: a possible error, and a possible result.

This function must check whether the first argument is null or not. If it isn't null, it means there was a problem, and the operation wasn't successful. On the other hand, if the first argument is null, then the second argument has the file read operation result. The following code highlights the usual programming pattern used with Node callbacks; the lines in bold are the key ones:

```
// Source file: src/promisify.js

/* @flow */
"use strict";

const fs = require("fs");

const FILE_TO_READ = "/home/fkereki/MODERNJS/chapter03/src/promisify.js"; // its own source!

function showFileLength1(fileName: string): void {
    fs.readFile(fileName, "utf8", (err, text) => {
        if (err) {
            throw err;
        } else {
            console.log(`1. Reading, old style: ${text.length} bytes`);
        }
    });
}
showFileLength1(FILE_TO_READ);

// continues...
```

This style of coding is well-known, but doesn't really fit modern development, based on promises and, even better, async/await. So, since version 8 of Node, there has been a way to automatically transform an error-first callback function into a promise: util.promisify(). If you apply that method to any old-style function, it will turn into a promise, which you can then work in simpler ways.



### How it works…

The util module is standard with Node, and all you have to do to use it is the following:

```
const util = require("util");
```



Using util.promisify(), we can make fs.readFile() return a promise, which we'll process with the .then() and .catch() methods:

```
// ...continued

function showFileLength2(fileName: string): void {
    fs.readFile = util.promisify(fs.readFile);

    fs
        .readFile(fileName, "utf8")
        .then((text: string) => {
            console.log(`2. Reading with promises: ${text.length} bytes`);
        })
        .catch((err: mixed) => {
            throw err;
        });
}
showFileLength2(FILE_TO_READ);

// continues...
```

You could have also written const { promisify } = require("util"), and then it would have been fs.readFile = promisify(fs.readFile). 

This also allows us the usage of async and await; I'll be using an arrow async function, just for variety:

```
// ...continued

const showFileLength3 = async (fileName: string) => {
    fs.readFile = util.promisify(fs.readFile);

    try {
        const text: string = await fs.readFile(fileName, "utf8");
        console.log(`3. Reading with async/await: ${text.length} bytes`);
    } catch (err) {
        throw err;
    }
};
showFileLength3(FILE_TO_READ);
```