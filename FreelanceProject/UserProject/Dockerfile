FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

WORKDIR /app

COPY *.sln .

COPY UserProject/*.csproj ./UserProject/
COPY UserDomain/*.csproj ./UserDomain/
COPY UserInfrastructure/*.csproj ./UserInfrastructure/

RUN dotnet restore -r alpine-x64

COPY UserProject/. ./UserProject/
COPY UserDomain/. ./UserDomain/
COPY UserInfrastructure/. ./UserInfrastructure/

RUN dotnet build

RUN dotnet publish -c Release -o /out -r alpine-x64

FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine AS publish

WORKDIR /app
COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["./UserProject"]