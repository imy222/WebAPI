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

This application runs within a Docker container and is deployed via Jupiter. This application can be assessed at this link: </br>
https://imay-webapi.svc.platform.myobdev.com/

---
## About Joke API 

This API is used to track a collection of Pokemon Jokes. Each joke has three properties, an Id, a question and a punchline.
This API is built with ASP.NET Core 6.0 using Entity Framework ('EF'). The collection of joke is stored in an EF in-memory database. The rationale for this approach is because one of the four focus area for Phase 2 is on learning about APIs, creating and testing API endpoints and not on relational databases or database management and structure. I understand that changes to data in the in-memory database will not persists when the application is not running. However, such a database structure is sufficient to enable me to meet phase 2 objectives (API Design, Continuous Integration, Continuous Delivery/Deployment and Security).


## Endpoints

### GET Requests

- Get all jokes
- Get one joke by selected Id

Using Postman or curl, making a GET request to https://imay-webapi.svc.platform.myobdev.com/joke path will return the collection of jokes, each with its own ID, question and punchline. 

A GET request to return a selected joke by ID can be made by appending the id number to the url above. </br>
For example https://imay-webapi.svc.platform.myobdev.com/joke/3 will return
```
{"id":3,"question":"What is the Dracula's favourite Pokemon?","punchline":"Koffin'"}
```
### POST Requests

- Create a new joke

To post a new joke, a request body containing a Question and Punchline is required. An Id is not required in the request body as new Ids for new resources will be created by EF.  This endpoint can be reached by either using Postman or curl.
An example of a curl command would be:

>curl -X 'POST' \
'https://imay-webapi.svc.platform.myobdev.com/joke' \
-H 'accept: text/plain' \
-H 'Content-Type: application/json' \
-d '{
"Question" : "What can Pikachu play with a baby?",
"Punchline" : "Pika-Boo!"}
'
---

### PUT Requests
 
- Update an entire joke.

The PUT endpoint takes in the ID number to indicate which joke to update.
An example of a curl command to update joke with ID no 4 would be:

```
curl -X 'PUT' \
'https://imay-webapi.svc.platform.myobdev.com/joke/4' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "Id" : 4,
        "Question" : "Which sci-fi movie do Pokémon like the most?",
        "Punchline" : "Staryu Wars."
}'
```

### Patch Requests
The Patch endpoint can be used to update either the Question or Punchline of the joke. In the real world, it would not make sense to update just the Question or Punchline of one joke object, but I wanted to learn how to create a Patch Request, so I included this request on the controller. This Patch request requires an Id and a request body in the form of a Json Patch document. 
An example of a curl command to update one of the property of the joke with ID no 3 would be:

```
curl -X 'PATCH' \
'https://imay-webapi.svc.platform.myobdev.com/joke/3' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json-patch+json' \
  -d '[
  {
    "path": "/question",
    "op": "replace",
    "value": "This is where a new question will be entered"
  }
]
'
```

### DELETE Requests

The DELETE endpoint takes in the ID number to indicate which joke to delete.
An example of a curl command to delete joke with ID no 5 would be:
```
curl -X 'DELETE' \
  'https://imay-webapi.svc.platform.myobdev.com/joke/5' \
  -H 'accept: text/plain'
```

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
Launch [https://localhost:7149/index.html](https://localhost:7149/index.html) on the browser and the default page is the SwaggerUI.


### Running the tests
Staying in the folder called WebAPI, enter dotnet test in your CLI to run the unit tests in the solution

```
$ dotnet test
```
___

## Dependencies

Testing Framework : [xUnit Framework](https://xunit.net/).
