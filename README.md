# User Management Service

The User Management Service is a .NET Core Web API that provides endpoints for managing user data. It follows a microservices architecture and utilizes a PostgreSQL database for data persistence.

## Features

-   Create new users
-   Update user information
-   Delete users
-   Retrieve a list of all users
-   Retrieve a single user by ID
-   Filter users by active status, search term, and birthdate range
-   Sort users by specified columns and order

## Prerequisites

-   Docker
-   Docker Compose
-   .NET SDK (for running tests locally)

## Getting Started

### Running Locally (with Docker)

1.  Clone the repository:
    `git clone https://github.com/renatomoraesp/eng-backend-test.git`
    
2.  Navigate to the  `dev-env`  directory:
    `cd eng-backend-test/dev-env`
    
3.  Build and run the Docker containers:
    `docker-compose up --build`
    
4.  The User Management Service should now be running and accessible at  `http://localhost:8000`.

## API Endpoints

### Create a User

-   URL:  `POST /api/users`
-   Request Body:
    `{   "name":  "John Doe", "birthdate":  "1990-01-01" }`
    

### Update a User

-   URL:  `PUT /api/users/{id}`
-   Request Body:
    `{   "name":  "Updated Name", "birthdate":  "1995-05-05", "active":  false }`
    

### Delete a User

-   URL:  `DELETE /api/users/{id}`

### Get All Users

-   URL:  `GET /api/users`
-   Query Parameters:
    -   `active`: Filter users by active status (e.g.,  `true`  or  `false`)
    -   `searchTerm`: Search users by name or active status
    -   `initialBirthdate`: Filter users born after the specified date (e.g.,  `1990-01-01`)
    -   `finalBirthdate`: Filter users born before the specified date (e.g.,  `2000-12-31`)
    -   `sort`: Sort users by a specific column (e.g.,  `name`,  `birthdate`)
    -   `order`: Sorting order (`ASC`  for ascending,  `DESC`  for descending)

### Get a Single User

-   URL:  `GET /api/users/{id}`

## Running Tests

1.  Ensure you have the .NET SDK installed on your machine.
2.  Navigate to the  `UserManagementService.Tests`  directory:
    `cd eng-backend-test/UserManagementService.Tests`
    
3.  Run the tests using the  `dotnet test`  command:
    `dotnet test`
    
    The test results will be displayed in the console.