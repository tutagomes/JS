# Adding authentication with JWT

For any server-based application, one challenge that must be solved is authentication, and our RESTful server therefore will need a solution for that. In traditional web pages, sessions and cookies may be used, but if you are using an API, there's no guarantee that requests will come from a browser; in fact, they may very well come from another server. Adding this to the fact that HTTP is stateless, and that RESTful services are also supposed to be so, we need another mechanism, and **JSON Web Tokens**(**JWT**) is an often used solution.

JWT is sometimes read aloud as *JOT*; see Section 1 of the RFC at https://www.rfc-editor.org/info/rfc7519.

The idea with JWT is that the client will first exchange valid credentials (such as username and password) with a server and get back a token, which will afterwards give them access to the server's resources. Tokens are created using cryptological methods, and are far longer and more obscure than usual passwords. However, tokens are small enough to be sent as body parameters or a HTTP header. 

Sending the token in the URL as a query parameter is a bad security practice! And, given that the token isn't actually a part of a request, putting it in the body also doesn't fit very well, so opt for a header; the recommended one is Authorization: Bearer.

After getting the token, it must be supplied with every API call, and the server will check it before proceeding. The token may include all information about the user so that the server won't have to query a database again to re-validate the request. In that sense, a token works like the security passes you are given at the front desk of a restricted building; you have to prove your identity once to the security officer, but afterwards you can move through the building by only showing the pass (which will be recognized and accepted) instead of having to go through the whole identification procedure again and again. 

Check out https://jwt.io/ for online tools that allow you to work with JWT, and also lots of information about tokens.



We won't be getting into the details of a JWT's creation, format, and so on; read the documentation if you are interested, because we will be working with libraries that will handle all such details for us. (We may just keep in mind that the token includes a *payload* with some *claims* related to the client or the token itself, like an expiration or issue date, and may include more information if we need to—but don't include secret data, because the token can be read.)

In this recipe, let's create a basic server that will be able to first issue a JWT to a valid user, and second check the presence of the JWT for specific routes.



### How to do it...

Let's look at how we can add authentication. To work with JWT, we'll be using jsonwebtoken from https://github.com/auth0/node-jsonwebtoken. Install it with the help of the following command:

```
npm install jsonwebtoken --save
```

Our code example for JWT will be larger than in previous examples, and it should be separated into many files. However, I avoided doing this in order to make it clearer. First, we'll need to make some declarations, and the key lines are in bold:

```js
// Source file: src/jwt_server.js

/* @flow */
"use strict";

const express = require("express");
const app = express();
const jwt = require("jsonwebtoken");
const bodyParser = require("body-parser");

const validateUser = require("./validate_user.js");

const SECRET_JWT_KEY = "modernJSbook";

app.use(bodyParser.urlencoded({ extended: false }));
```

Almost everything is standard, except for the validateUser() function and the SECRET_JWT_KEY string. The latter will be used to sign the tokens, and most definitely shouldn't be in the code itself! (If somebody could hack their way into the source code, your secret would be out; rather, set the key in an environment variable, and get the value from there.)

As for the function, checking if a user exists and if their password is correct is simple to do, and can be achieved in many ways, such as by accessing a database, active directory, service, and so on. Here, we'll just make do with a hardcoded version, which accepts only a single user. The validate_user.js source code is, then, quite simple:

```js
// Source file: src/validate_user.js

/* @flow */
"use strict";

/*
    In real life, validateUser could check a database,
    look into an Active Directory, call another service,
    etc. -- but for this demo, let's keep it quite
    simple and only accept a single, hardcoded user.
*/

const validateUser = (
    userName: string,
    password: string,
    callback: (?string, ?string) => void) => {
    if (!userName || !password) {
        callback("Missing user/password", null);
    } else if (userName === "usuario" && password === "password") {
        callback(null, "usuario"); // OK, send userName back
    } else {
        callback("Not valid user", null);
    }
};

module.exports = validateUser;
```

Let's get back to our server. After the initial definitions, we can place the routes that need no tokens. Let's have a /public route, and also a /gettoken route to get a JWT for later. In the latter, we'll see whether the POST included user and password values in its body, and if they are a valid user by means of the validateUser() function we showed in the preceding code. Any problems will mean a 401 status will be sent, while if the user is correct, a token will be created, expiring in one hour's time:

```js
// Source file: src/jwt_server.js

app.get("/public", (req, res) => {
    res.send("the /public endpoint needs no token!");
});

app.post("/gettoken", (req, res) => {
    validateUser(req.body.user, req.body.password, (idErr, userid) => {
        if (idErr !== null) {
            res.status(401).send(idErr);
        } else {
            jwt.sign(
                { userid },
                SECRET_JWT_KEY,
                { algorithm: "HS256", expiresIn: "1h" },
                (err, token) => res.status(200).send(token)
            );
        }
    });
});
```

Now that the unprotected routes are out of the way, let's add some middleware to verify that a token is present. We expect, according to the JWT RFC, to have an Authorization: Bearer somejwttoken header included, and it must be accepted. If no such header is present, or if it's not in the right format, a 401 status will be sent. If the token is present, but it's expired or has any other problem, a 403 status will be sent. Finally, if there's nothing wrong, the userid field will be extracted from the payload, and attached to the request object so that future code will be able to use it:

```js
// Source file: src/jwt_server.js

app.use((req, res, next) => {
    // First check for the Authorization header
    const authHeader = req.headers.authorization;
    if (!authHeader || !authHeader.startsWith("Bearer ")) {
        return res.status(401).send("No token specified");
    }

    // Now validate the token itself
    const token = authHeader.split(" ")[1];
    jwt.verify(token, SECRET_JWT_KEY, (err, decoded) => {
        if (err) {
            // Token bad formed, or expired, or other problem
            return res.status(403).send("Token expired or not valid");
        } else {
            // Token OK; get the user id from it
            req.userid = decoded.userid;
            // Keep processing the request
            next();
        }
    });
});
```

Now, let's have some protected routes (in fact, a single one, /private, just for this example), followed by error checking and setting up the whole server:

```js
// Source file: src/jwt_server.js

app.get("/private", (req, res) => {
    res.send("the /private endpoint needs JWT, but it was provided: OK!");
});

// eslint-disable-next-line no-unused-vars
app.use((err, req, res, next) => {
    console.error("Error....", err.message);
    res.status(500).send("INTERNAL SERVER ERROR");
});

app.listen(8080, () =>
    console.log("Mini JWT server ready, at http://localhost:8080/!")
);
```

We're done! Let's see how this all comes together.

# How it works...

We can start by testing the /public and /private routes, without any token. The former won't cause any problems, but the latter will be caught by our token testing code and rejected:

```js
> curl "http://localhost:8080/public"  
the /public endpoint needs no token!

> curl "http://localhost:8080/private" 
No token specified
```

Now, let's try to get a token. Check out the following code:

```js
> curl http://localhost:8080/gettoken -X POST -d "user=usuario&password=password"     
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiJma2VyZWtpIiwiaWF0IjoxNTI2ODM5MDEwLCJleHAiOjE1MjY4NDI2MTB9.cTwpL-x7kszn7C9OUXhHlkTGhb8Aa7oOGwNf_nhALCs
```

Another way of testing this would be going to https://jwt.io/ and creating a JWT, including userid:"fkereki" in the payload, and using modernJSbook as the secret key. You would have to calculate the expiration date (exp) by yourself, though.



Checking the token at [https://jwt.io](https://jwt.io/) shows the following payload:

```json
{
  "userid": "usuario",
  "iat": 1526839010,
  "exp": 1526842610
}
```

The iat attribute shows that the JWT was issued on 5/20/2018, close to 2:00 P.M. and the exp attributes show that the token is set to expire one hour (3,600 seconds) later. If we now repeat the curl request to /private, but adding the appropriate header, it will be accepted. However, if you wait (at least an hour!), the result will be different; the JWT checking middleware will detect the expired token, and a 403 error will be produced:

```json
> curl "http://localhost:8080/private" -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiJma2VyZWtpIiwiaWF0IjoxNTI2ODM5MDEwLCJleHAiOjE1MjY4NDI2MTB9.cTwpL-x7kszn7C9OUXhHlkTGhb8Aa7oOGwNf_nhALCs"
the /private endpoint needs JWT, but it was provided: OK!
```

With this code, we now have a way to add authentication to our RESTful server. If you want, you could go further and add specific authorization rules so that some users would get access to some features, while others would be restricted. 