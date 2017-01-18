# Overview (where do I start?)

## Content

* Executable for the WebServer (OWIN Self-Host) & Website

## Prerequisites

### Back-End
* .net Framework 4.6.1
* Nuget to get the dependencies
* Git to get the code (optional)
* Visual Studio 2013 or 2015 with F# and C# support to open and edit it (optional)

### Front-End
* Internet connection (in order to connect Bootstrap & JQuery CDNs)
* node & npm to get the dependencies and run the dev-server (optional)

## Run 

### The quick way

1. Start the WebServer : /dist/server/EquationsSolver.WebApi.exe (port can be changed in the app.config)
2. Run the front end : open in your favorite browser /dist/client/index.html (port can be changed in the app.config)

If the post is already taken, it can be changed in the server/app.config. It also must be change on the client in client/dist/bundle.js, as the JS is minified, just do a find an replace for the token *http://localhost:12321/resolve/*

### The long way

All the optional prerequisites mentioned above must be met

* Server
1. Open the solution in VS2015
2. Restore the nuget packages
3. Run EquationSolver.WebApi in debug mode

* Client
1. open a command line tool (cmd.exe), go to the project directory /EquationSolver.Web
2. restore npm packages - npm install
3. start webpack dev server - npm run start
4. Website will be available on localhost:8080

```
npm install
npm run start 
```

![Capture.PNG](https://bitbucket.org/repo/6LeLqA/images/2453946028-Capture.PNG)


## Tech Stack 

- WebServer Middleware : **OWIN** (SelfHosted)
- WebService/REST WebApi : **NancyFx** (similar to Asp.net WebApi2 but lightweight)
- Math Expression Helper : **Math.Net** (F#) 
- DI : **Ninject** 
- Front End Libraries : **React** (to design the web components), **JQuery** (only for ajax), **Bootstrap**

## Project Structure

The solution contains 4 projects :

- EquationsCore : F# project for resolving equations and dealing with math expressions.
- EquationsSolver.WebApi : Contains a self-hosted OWIN webserver implemented with NancyFX and the equation resolver service (wrapper around EquationsCore)
- EquationsSolver.Tests : unit tests for EquationsCore & EquationsSolver.WebApi
- EquationsSolver.Web : Singe Page App containing mainly a ReactJS standalone component to solve equations and display the result (by using the WebApi)
