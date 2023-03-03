[![Build status](https://badge.buildkite.com/4cbb00adb8bbd7cd0ad50b0a4af870ccba825dd73a6110bdd7.svg)](https://buildkite.com/myob/imay-webapi)

# Basic Web Application Kata
___
## About the Kata

---
This project is a standalone Joke API which maintains a list of Pokemon related jokes. 
This project is developed based on the requirements of the Phase 2 Basic Web Application Kata.
The objective of the Kata is to develop a REST API project that:

* Is well documented and uses industry standards
* Automatically tests and builds your project artefacts
* Automatically deploys your project into a production-ready environment
* Has security built-in

Full details of the Kata can be viewed at:</br>
https://github.com/MYOB-Technology/General_Developer/blob/main/katas/kata-phase-2/kata-basic-web-application.md

---
## Accessing the deployed application

https://imay-webapi.svc.platform.myobdev.com/

---
## API 
### GET Requests
Using Postman or curl, making a GET request to https://imay-webapi.svc.platform.myobdev.com/joke path will return the collection of jokes, each with its own ID, question, punchline and category ID. 

A GET request to return a selected joke by ID can be made by appending the id number to the url above. </br>
For example https://imay-webapi.svc.platform.myobdev.com/joke/3 will return
```
{"id":3,"question":"What is the Dracula's favourite Pokemon?","punchline":"Koffin'"}
```
### POST Requests
To post a new joke, the Post endpoint can be reached by either using Postman or curl.
An example of a curl command would be:

>curl -X 'POST' \
'https://imay-webapi.svc.platform.myobdev.com/joke' \
-H 'accept: text/plain' \
-H 'Content-Type: application/json' \
-d '{
"Id" : 6,
"Question" : "What can Pikachu play with a baby?",
"Punchline" : "Pika-Boo!"}
'

### DELETE Requests

The DELETE endpoint takes in the ID number to indicate which joke to delete.
An example of a curl command to delete joke with ID no 6 would be:
```
curl -X 'DELETE' \
  'https://imay-webapi.svc.platform.myobdev.com/joke/6' \
  -H 'accept: text/plain'
```

### PUT Requests
The PUT endpoint takes in the ID number to indicate which joke to update.
An example of a curl command to update joke with ID no 4 would be:

```
curl -X 'PUT' \
  'https://imay-webapi.svc.platform.myobdev.com/joke/4' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "Id" : 4,
        "Question" : "What do you call Meowth's reflection?",
        "Punchline" : "TESTING"
}'
```


### Error Handling
Note that at this stage, error handling have not been implemented. This will be one of the next enhancements to this project. By deploying the project with a basic webapi, I hope to fully appreciate the benefits or CI/CD.

---
## Running the project locally

### Installing .NET6
You will need to have the .NET6 SDK installed on your Mac.
You can download the .NET SDK suitable for your local environment [here](https://learn.microsoft.com/en-us/dotnet/core/install/).

### Set up

First clone this repository to your local machine:
```
gh repo clone myob-fma/Imay_WebAPI
$ cd <your chosen folder>
$ dotnet restore
```

To start up the API locally,  while on  `WebAPI/` directory, run
```
dotnet run --project JokeAPI
```
Launch localhost on the browser and you will see the following:

>>😈 It's not a bug. It's an undocumented feature! 😈


### Running the tests
Staying in the folder called WebAPI, enter dotnet test in your CLI to run the unit tests in the solution

```
$ dotnet test
```
___
## Dependencies

Testing Framework : [xUnit Framework](https://xunit.net/).
