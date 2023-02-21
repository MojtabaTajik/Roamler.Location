FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

RUN apt-get update
RUN apt-get install -y curl
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY . .
RUN dotnet restore "Roamler.API.csproj"
RUN dotnet build "Roamler.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roamler.API.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roamler.API.dll"]
