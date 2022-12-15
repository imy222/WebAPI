FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /src
COPY ./WebAPI.sln .
COPY JokeAPI/JokeAPI.csproj ./JokeAPI/
RUN dotnet restore JokeAPI/JokeAPI.csproj
COPY JokeAPI/. ./JokeAPI/

FROM base AS build
RUN dotnet publish JokeAPI -c Release -o output --no-restore /restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish
WORKDIR /app
COPY --from=build /src/output .
EXPOSE 80
ENTRYPOINT ["dotnet", "JokeAPI.dll"]

