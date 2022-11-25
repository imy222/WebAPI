
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnetsdk 
#WORKDIR /src

#COPY ./WebAPI.sln .
#COPY ./JokeAPI/JokeAPI.csproj ./JokeAPI/
#COPY ./JokeAPITests/JokeAPITests.csproj ./JokeAPITests/
#RUN ["dotnet", "restore"]
#COPY . .

#FROM dotnetsdk AS test
# entrypoint tells docker when docker container using this immage starts, execute dotnet test immediately
# when tests all passed, tests will have exit code 0. Buildkite will then use this exit code to determine if 
# this stage in pipeline is a success or failure.
#ENTRYPOINT ["dotnet", "test"]


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnetsdk 
WORKDIR /src

COPY ./WebAPI.sln .
COPY ./JokeAPI/JokeAPI.csproj ./JokeAPI/
COPY ./JokeAPITests/JokeAPITests.csproj ./JokeAPITests/
COPY . .
#RUN ["dotnet", "restore"] 
#not necessary dotnet publish already run a dotnet restore
#dotnet restore uses NuGET to restore dependancies as well as project-specific
#tools that are specified in the project file
RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=dotnetsdk /src/output .
EXPOSE 80
ENTRYPOINT ["dotnet", "JokeAPI.dll"]
