!bin/bash

base=$(pwd)
workspaceroot=$base/$1
targetframework=netcoreapp2.2

echo $base
echo $workspaceroot

# Create boilerplate
mkdir $workspaceroot
mkdir $workspaceroot/src
mkdir $workspaceroot/src/$1.Api
mkdir $workspaceroot/src/$1.Core
mkdir $workspaceroot/src/$1.Infrastructure

mkdir $workspaceroot/test
mkdir $workspaceroot/test/$1.UnitTests

cd $workspaceroot/src/$1.Api
dotnet new webapi

cd $workspaceroot/src/$1.Core
dotnet new classlib

cd $workspaceroot/src/$1.Infrastructure
dotnet new classlib

cd $workspaceroot/test/$1.UnitTests
dotnet new xunit

cd $workspaceroot

# Create solution and add projects inside
dotnet new sln
dotnet sln ./$1.sln add ./src/$1.Api/$1.Api.csproj
dotnet sln ./$1.sln add ./src/$1.Core/$1.Core.csproj
dotnet sln ./$1.sln add ./src/$1.Infrastructure/$1.Infrastructure.csproj
dotnet sln ./$1.sln add ./test/$1.UnitTests/$1.UnitTests.csproj

# Add reference projects
cd $workspaceroot/src/$1.Api
dotnet add reference ../$1.Core/$1.Core.csproj
dotnet add reference ../$1.Infrastructure/$1.Infrastructure.csproj

cd $workspaceroot/src/$1.Infrastructure
dotnet add reference ../$1.Core/$1.Core.csproj

cd $workspaceroot/test/$1.UnitTests
dotnet add reference ../../src/$1.Api/$1.Api.csproj
dotnet add reference ../../src/$1.Core/$1.Core.csproj

cd $workspaceroot

dotnet restore
dotnet build


