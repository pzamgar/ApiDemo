FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app 
EXPOSE 80 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build 
COPY *.sln ./
WORKDIR /build/src
COPY src/ApiBuildDemo.Api/ApiBuildDemo.Api.csproj ApiBuildDemo.Api/
COPY src/ApiBuildDemo.Core/ApiBuildDemo.Core.csproj ApiBuildDemo.Core/
COPY src/ApiBuildDemo.Infrastructure/ApiBuildDemo.Infrastructure.csproj ApiBuildDemo.Infrastructure/
WORKDIR /build/test
COPY test/ApiBuildDemo.UnitTests/ApiBuildDemo.UnitTests.csproj ApiBuildDemo.UnitTests/
COPY test/ApiBuildDemo.FunctionalTests/ApiBuildDemo.FunctionalTests.csproj ApiBuildDemo.FunctionalTests/
WORKDIR /build
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /build/src/ApiBuildDemo.Api
RUN dotnet build -c Release -o /app
WORKDIR /build/src/ApiBuildDemo.Core
RUN dotnet build -c Release -o /app
WORKDIR /build/src/ApiBuildDemo.Infrastructure
RUN dotnet build -c Release -o /app
WORKDIR /build/test/ApiBuildDemo.UnitTests
RUN dotnet build -c Release -o /app
WORKDIR /build/test/ApiBuildDemo.FunctionalTests
RUN dotnet build -c Release -o /app

FROM build AS unittest
WORKDIR /build/test/ApiBuildDemo.UnitTests
RUN dotnet test

FROM build AS publish
WORKDIR /build/src/ApiBuildDemo.Api
RUN dotnet publish -c Release -o /app
WORKDIR /build/src/ApiBuildDemo.Core
RUN dotnet publish -c Release -o /app
WORKDIR /build/src/ApiBuildDemo.Infrastructure
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiBuildDemo.Api.dll"]