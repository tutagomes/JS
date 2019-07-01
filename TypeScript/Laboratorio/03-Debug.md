# Debugging TypeScript

Visual Studio Code supports TypeScript debugging through its built-in [Node.js debugger](https://code.visualstudio.com/docs/nodejs/nodejs-debugging) and also through [extensions](https://code.visualstudio.com/docs/editor/extension-gallery) like [Debugger for Chrome](https://marketplace.visualstudio.com/items?itemName=msjsdiag.debugger-for-chrome) to support client-side TypeScript debugging.



## JavaScript source map support

TypeScript debugging supports JavaScript source maps. To generate source maps for your TypeScript files, compile with the `--sourcemap` option or set the `sourceMap` property in the `tsconfig.json` file to `true`.

In-lined source maps (a source map where the content is stored as a data URL instead of a separate file) are also supported, although in-lined source is not yet supported.

For a simple example of source maps in action, see the [TypeScript tutorial](https://code.visualstudio.com/docs/typescript/typescript-tutorial), which shows debugging a simple "Hello World" Node.js application using the following `tsconfig.json` and VS Code default Node.js debugging configuration.

```
{
    "compilerOptions": {
        "target": "es5",
        "module": "commonjs",
        "outDir": "out",
        "sourceMap": true
    }
}
```

For more advanced debugging scenarios, you can create your own debug configuration `launch.json`file. To see the default configuration, go to the Debug view (⇧⌘D) and press the gear icon to **Configure or Fix 'launch.json'**. If you have other debugger extensions installed (such as the Debugger for Chrome), you should select **Node.js** from the drop down.

![configure launch.json](https://code.visualstudio.com/assets/docs/typescript/debugging/configure-debugging.png)

This will create a `launch.json` file in a `.vscode` folder with default values detected in your project.

```
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "type": "node",
            "request": "launch",
            "name": "Launch Program",
            "program": "${workspaceFolder}/helloworld.ts",
            "preLaunchTask": "tsc: build - tsconfig.json",
            "outFiles": [
                "${workspaceFolder}/out/**/*.js"
            ]
        }
    ]
}
```

VS Code has determined the program to launch, `helloworld.ts`, included the build as a `preLaunchTask`, and told the debugger where to find the generated JavaScript files.

There is full IntelliSense with suggestions and information for `launch.json` to help you learn about other debug configuration options. You can also add new debug configurations to `launch.json` with the **Add Configuration** button in the lower right.

![launch.json IntelliSense](https://code.visualstudio.com/assets/docs/typescript/debugging/launch-json-intellisense.png)

Also see [Node.js Debugging](https://code.visualstudio.com/docs/nodejs/nodejs-debugging) for examples and further explanations.



## Mapping the output location

If generated (transpiled) JavaScript files do not live next to their source, you can help the VS Code debugger locate them by setting the `outFiles` attribute in the launch configuration. Whenever you set a breakpoint in the original source, VS Code tries to find the generated source by searching the files specified by glob patterns in `outFiles`.