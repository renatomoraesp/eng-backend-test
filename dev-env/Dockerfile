FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /app

COPY ./src/UserManagementService/*.csproj ./
RUN dotnet restore

COPY ./src/UserManagementService/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "UserManagementService.dll"]