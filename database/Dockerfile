FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY ./src/UserManagementService/*.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY ./src/UserManagementService/. ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "UserManagementService.dll"]