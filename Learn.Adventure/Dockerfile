FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Learn.Adventure.csproj", "Learn.Adventure/"]
RUN dotnet restore "Learn.Adventure/Learn.Adventure.csproj"

WORKDIR "/src/Learn.Adventure"
COPY . .
RUN dotnet build "Learn.Adventure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Learn.Adventure.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Learn.Adventure.dll"]
