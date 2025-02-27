FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /app

COPY ./ ./

WORKDIR /app/ApiRestCampoDijital/

RUN dotnet restore

RUN dotnet publish -c release -o ./../build

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=builder /app/build ./

EXPOSE 5000

ENTRYPOINT ["dotnet","ApiRestCampoDijital.dll"]