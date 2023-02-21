FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Roamler.API/Roamler.API.csproj", "Roamler.API/"]
RUN dotnet restore "Roamler.API/Roamler.API.csproj"
COPY . .
WORKDIR "/src/Roamler.API"
RUN dotnet build "Roamler.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roamler.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roamler.API.dll"]
