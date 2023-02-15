[![Build status](https://badge.buildkite.com/4cbb00adb8bbd7cd0ad50b0a4af870ccba825dd73a6110bdd7.svg)](https://buildkite.com/myob/imay-webapi)

# Basic Web Application Kata
___
## About the Kata

---
This project is a standalone Joke API which maintains a list of Pokemon related jokes. 
This project is developed based on the requirements of the Phase 2 Basic Web Application Kata.
The objective of the Kata is to develop a REST API that offers CRUD functionality:

Create a new entry<br>
Read a list of all existing entries<br>
Read an existing entry<br>
Update an existing entry<br>
Delete an existing entry<br>

Full details of the Kata can be viewed at:</br>
https://github.com/MYOB-Technology/General_Developer/blob/main/katas/kata-phase-2/kata-basic-web-application.md

---
## Accessing the deployed application

https://imay-webapi.svc.platform.myobdev.com/

---
## Usage
### GET Requests
Using Postman or curl, making a GET request to https://imay-webapi.svc.platform.myobdev.com/joke path will return the collection of jokes, each with its own ID, question, punchline and category ID. 

A GET request to return a selected joke by ID can be made by appending the id number to the url above. </br>
For example https://imay-webapi.svc.platform.myobdev.com/joke/3 will return
```
{"id":3,"question":"What is the Dracula's favourite Pokemon?","punchline":"Koffin'","categoryId":2}
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
"Punchline" : "Pika-Boo!",
"CategoryId" : 1
'

### DELETE Requests

The DELETE endpoint takes in the ID number to indicate which joke to delete.
An example of a curl command to delete joke with ID no 2 would be:
```
curl -X 'DELETE' \
  'https://imay-webapi.svc.platform.myobdev.com/joke/2' \
  -H 'accept: text/plain'
```

### PUT Requests
The PUT endpoint takes in the ID number to indicate which joke to update.
An example of a curl command to delete joke with ID no 4 would be:

```
curl -X 'PUT' \
  'https://imay-webapi.svc.platform.myobdev.com/joke/4' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
        "Id" : 4,
        "Question" : "What do you call Meowth'\''s reflection?",
        "Punchline" : "TESTING",
        "CategoryId" : 2
}'
```


### Error Handling
Note that at this stage, error handling have not been implemented. This will be the next enhancement to this project. By deploying the project with a basic webapi, I hope to fully appreciate the benefits or CI/CD.

---

## Running the project

### Installing .NET6
You will need to have the .NET6 SDK installed on your Mac.
Install the .NET SDK using homebrew on the command line
```
brew install --cask dotnet-sdk
```
Alternatively, you can download the .NET SDK [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

### Set up

First clone this repository to your local machine:
```
gh repo clone myob-fma/Imay_WebAPI
$ cd <your chosen folder>
$ dotnet restore
```

To start up the API locally, use the command `dotnet run` from the CLI when inside the `WebAPI/` directory. You would see the following: </br>
```
😈 It's not a bug. It's an undocumented feature! 😈
```

### Running the tests
Staying in the folder called WebAPI, enter dotnet test in your CLI to run the unit tests in the solution

```
$ dotnet test
```
___
## Dependencies

Testing Framework : [xUnit Framework](https://xunit.net/).
