# Getting started

Let's see both styles by using a simple example. This book was written in three different cities: Pune, India; London, England; and Montevideo, Uruguay, so let's do some work related to those cities. We will write code that will get weather information for those cities:

- For Montevideo alone
- For London and then for Pune, in series, so that the second call won't start until the first is done
- For the three cities in parallel, so that all three requests will be processed at the same time, gaining time by the overlap

We will not get into details such as using this or that API, getting a private key, and so on, and we'll just fake it by accessing the free *The Weather Channel* page. We will use the following definitions for all our coding, which we'll do in Node, using the axios module; don't worry about the details now:

```
// Source file: src/get_service_with_promises.js

const axios = require("axios");

const BASE_URL = "https://weather.com/en-IN/weather/today/l/";

// latitude and longitude data for our three cities
const MONTEVIDEO_UY = "-34.90,-56.16";
const LONDON_EN = "51.51,-0.13";
const PUNE_IN = "18.52,73.86";

const getWeather = coords => axios.get(`${BASE_URL}${coords}`);
```



The BASE_URL constant provides the basic web address, to which you must attach the coordinates (latitude, longitude) of the desired city. On its own, we would get a page like the one shown in the following screenshot:

![img](https://learning.oreilly.com/library/view/modern-javascript-web/9781788992749/assets/f27bb572-7800-4640-8d02-86e62e9c0190.png)

 we will be using Ajax to get weather information for cities

In real life, we would not be getting a web page but rather an API, and then process the returned results. In our case, since we don't actually care for the data, but for the methods we'll use to do the calls, we'll be content with just showing some banal information, such as how many bytes were sent back. Totally useless, I agree, but this is enough for our example!



# Doing Ajax calls with promises

The first way we can do web service calls is by using promises, and they were (up to the appearance of the more modern async/await statements, which we'll be seeing in the next section) the favorite method. Promises were available some time back (first around 2011 through jQuery's deferred objects, and afterwards by means of libraries such as BlueBird or Q), but in recent JS versions, they became native. Since promises cannot really be considered something new, let's just see some examples so that we can move on to more modern ways of working—no, we won't be even considering going further back than promises, and directly work with callbacks!

Do native promises imply that libraries won't be needed again? That's a tricky question! JS promises are quite basic, and most libraries add several methods that can simplify your coding. (See http://bluebirdjs.com/docs/api-reference.html or https://github.com/kriskowal/q/wiki/API-Reference for such features from Bluebird or Q.) Hence, while you may do perfectly well with native promises, in some circumstances, you may want to keep using a library. 

Getting the weather data for Montevideo is simple if we use the getWeather() function that we defined previously:

```
// Source file: src/get_service_with_promises.js

function getMontevideo() {
    getWeather(MONTEVIDEO_UY)
        .then(result => {
            console.log("Montevideo, with promises");
            console.log(`Montevideo: ${result.data.length} bytes`);
        })
        .catch(error => console.log(error.message));
}
```

The getWeather() function actually returns a promise; its .then() method corresponds to the success case and .catch() corresponds to any error situations.

Getting data for two cities in a row is also simple. We don't want to start the second request until the first one has been successful, and that leads to the following scheme:

```
// Source file: src/get_service_with_promises.js

function getLondonAndPuneInSeries() {
    getWeather(LONDON_EN)
        .then(londonData => {
            getWeather(PUNE_IN)
                .then(puneData => {
                    console.log("London and Pune, in series");
                    console.log(`London: ${londonData.data.length} b`);
                    console.log(`Pune: ${puneData.data.length} b`);
                })
                .catch(error => {
                    console.log("Error getting Pune...", error.message);
                });
        })
        .catch(error => {
            console.log("Error getting London...", error.message);
        });
}
```

This is not the only way to program such a series of calls, but since we won't actually be directly working with promises, let's just skip the alternatives.

Finally, in order to do calls in parallel and optimize time, the Promise.all() method will be used to build up a new promise out of the three individual ones for each city. If all calls succeed, the bigger promise will also do; should any of the three calls fail, then failure will also be the global result:

For more information on Promise.all(), check out https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/all. If you'd rather build a promise that succeeds when *any* (instead of *all*) of the involved promises succeeds, you should use Promise.race(); see https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/race.





```
// Source file: src/get_service_with_promises.js

function getCitiesInParallel() {
    const montevideoGet = getWeather(MONTEVIDEO_UY);
    const londonGet = getWeather(LONDON_EN);
    const puneGet = getWeather(PUNE_IN);

    Promise.all([montevideoGet, londonGet, puneGet])
        .then(([montevideoData, londonData, puneData]) => {
            console.log("All three cities in parallel, with promises");
            console.log(`Montevideo: ${montevideoData.data.length} b`);
            console.log(`London: ${londonData.data.length} b`);
            console.log(`Pune: ${puneData.data.length} b`);
        })
        .catch(error => {
            console.log(error.message);
        });
}
```

Note how we use a destructuring assignment to get the data for each city. The result of calling these functions may be as follows; I added some spacing for clarity:

```
Montevideo, with promises
Montevideo: 353277 bytes

London and Pune, in series
London: 356537 b
Pune: 351679 b

All three cities in parallel, with promises
Montevideo: 351294 b
London: 356516 b
Pune: 351679 b
```

Organizing web calls with promises is a straightforward method, but the usage of possibly nested .then()methods can become hard to understand, so we really should give a look to an alternative. We'll do just that in the next section.



# Doing Ajax calls with async/await

The second way, async/await, is more modern but, deep inside, actually also works with promises, but simplifyies the job. There are some important definitions that we should take into account:

- An async function will contain some await expressions, depending on promises
- await expressions pause the execution of the async function until the promise's resolution
- After the promise's resolution, processing is resumed, with the returned value
- If an error is produced, it can be caught with try ... catch
- await can only be used in async functions

How does this affect our coding? Let's review our three examples. Getting information for a single city is simple:

```
// Source file: src/get_service_with_async_await.js

async function getMontevideo() {
    try {
        const montevideoData = await getWeather(MONTEVIDEO_UY);
        console.log("Montevideo, with async/await");
        console.log(`Montevideo: ${montevideoData.data.length} bytes`);
    } catch (error) {
        console.log(error.message);
    }
}
```

We are still using a promise (the one returned by axios via the getWeather() call), but now the code looks more familiar: you wait for results to come, and then you process them—it almost looks as if the call were a synchronous one! 

Getting data for London and then Pune in sequence is also quite direct: you wait for the first city's data, then you wait for the second's, and then you do your final process; what could be simpler? Let's see the code:

```
// Source file: src/get_service_with_async_await.js

async function getLondonAndPuneInSeries() {
    try {
        const londonData = await getWeather(LONDON_EN);
        const puneData = await getWeather(PUNE_IN);
        console.log("London and Pune, in series");
        console.log(`London: ${londonData.data.length} b`);
        console.log(`Pune: ${puneData.data.length} b`);
    } catch (error) {
        console.log(error.message);
    }
}
```

Finally, getting all data in parallel also depends on the Promise.all() method we saw in the previous section:

```
// Source file: src/get_service_with_async_await.js

async function getCitiesInParallel() {
    try {
        const montevideoGet = getWeather(MONTEVIDEO_UY);
        const londonGet = getWeather(LONDON_EN);
        const puneGet = getWeather(PUNE_IN);

        const [montevideoData, londonData, puneData] = await Promise.all([
            montevideoGet,
            londonGet,
            puneGet
        ]);

        console.log("All three cities in parallel, with async/await");
        console.log(`Montevideo: ${montevideoData.data.length} b`);
        console.log(`London: ${londonData.data.length} b`);
        console.log(`Pune: ${puneData.data.length} b`);
    } catch (error) {
        console.log(error.message);
    }
}
```

The parallel call code is really quite similar to the pure promises' version: the only difference here is that you await results, instead of using .then(). 

We have seen two ways of dealing with asynchronous service calls. Both are very much in use, but in this text, we'll tend to favor async/await, given that the resulting code seems clearer, with less extra baggage.