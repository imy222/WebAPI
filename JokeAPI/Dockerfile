FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JokeAPI/JokeAPI.csproj", "JokeAPI/"]
RUN dotnet restore "JokeAPI/JokeAPI.csproj"
COPY . .
WORKDIR "/src/JokeAPI"
RUN dotnet build "JokeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JokeAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JokeAPI.dll"]
