# Interview task for Tamás György

## task

Create a RESTful API in C# to retrieve and manage a list of users. The API should be able to list, add, update and delete users from a NoSQL database like MongoDB. The API should be self-documenting and contain full test coverage.
The users dataset we would like you to use can be downloaded from https://jsonplaceholder.typicode.com/users
Note: If you are selected for interview, you will be asked to show your application working, using a tool of your choice to drive the API, and to step through it using a debugger.

## run code

### 1. firebase secret keys

Create a new folder called "keys". Copy the secret keys to this folder.

### 2. build

`docker build -t wannabe -f Dockerfile .`

### 3. run

`docker run -p 8080:80 wannabe`

### 4. test

`dotnet test /p:CollectCoverage=true`

It run all tests and measure the coverage of them.

## can be improved

I ignored **4 warning** messages from the unit test classes. Needs more time to fix this.

I regretted not using MongoDB.