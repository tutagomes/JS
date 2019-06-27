# Running your Node code with Nodemon

With the work we have done so far, after each and every change, running our updated Node code would require that we perform the following:

1. Stop the current version of the code, if it's still running.
2. Rerun the build process to update the out directory.
3. Run the new version of the code.

Doing all of this, for every single small change, can quickly become boring and tiresome. But, there is a solution: we can install a watcher, that will monitor our files for changes and do everything mentioned here by itself, freeing us from the repetitive chore. Let's then see how we can set a tool to watch out for changes, and do all the steps shown on its own.



### How to do it...

We will want to install and configure nodemon, which will take care of everything for us, running updated code as necessary. First, obviously, we must install the mentioned package. You could do it globally with npm install nodemon -g, but I'd rather do it locally:

```
npm install nodemon --save-dev
```

Then, we'll need to add a couple of scripts:

- npm start will build the application and run our main file
- npm run nodemon will start the monitoring

```
"scripts": {
    "build": "flow-remove-types src/ -d out/",
    "buildWithMaps": "flow-remove-types src/ -d out/ --pretty --
     sourcemaps",
    "start": "npm run build && node out/doroundmath.js",
    "nodemon": "nodemon --watch src --delay 1 --exec npm start",
    .
    .
    .  
},
```

Now, we are ready to monitor our application for changes, and restart it as needed!

# How it works...

The command most interesting for us is the second one. When you run it, nodemon will start monitoring, meaning it will watch whatever directory you selected (out, in this case) and whenever it detects some file change, it will wait one second (to make sure, for example, that all files are saved) and then it will rerun the application. How did I do this?



Initially, I started nodemon. When you do npm run nodemon, the project is built and then run, and nodemon keeps waiting for any changes.

Afterwards, I just added a simple console.log() line, so a file would be changed.

That's all there is to it. The application will be rebuilt and restarted automatically, without us having to manually rerun npm start each and every time; a big help!