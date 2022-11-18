#grab relevant image from docker hub
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnetsdk 

# make and use as source directory
WORKDIR /src

#copy from local solution to docker. Copy takes two arguments, file to copy from and to copy to
COPY ./WebAPI.sln .
COPY ./JokeAPI/JokeAPI.csproj ./JokeAPI/
COPY ./JokeAPITests/JokeAPITests.csproj ./JokeAPITests/
RUN ["dotnet", "restore"]
# run restores all dependency used by project as listed in csproj files.

# copy .. copies all files in the repo to /src folder in docker environment
# why not just use line 16, instead of seperating parts in line 9-10 and line 16
# answer : CACHING. Docker chaches the product of each individeual command in thisf file. Then time an image is 
# built again, if there are no changes, docker uses the cache file. So for files that are not
# changed often, put in its own copy line for better performance.
COPY . .

FROM dotnetsdk AS test
# entrypoint tells docker when docker container using this immage starts, execute dotnet test immediately
# when tests all passed, tests will have exit code 0. Buildkite will then use this exit code to determine if 
# this stage in pipeline is a success or failure.
ENTRYPOINT ["dotnet", "test"]