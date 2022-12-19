FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /src

COPY ./WebAPI.sln .
COPY JokeAPI/*.csproj ./JokeAPI/
RUN dotnet restore JokeAPI/JokeAPI.csproj
COPY JokeAPI/. ./JokeAPI/

FROM base AS build
#build application as release and save to /app/JokeAPI/output
RUN dotnet publish JokeAPI -c Release -o output --no-restore /restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish
WORKDIR /app
#configure the new directory containing .dll and runtime image
COPY --from=build /src/output .
EXPOSE 80
ENTRYPOINT ["dotnet", "JokeAPI.dll"]

