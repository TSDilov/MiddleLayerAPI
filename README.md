# MiddleLayerAPI

## Overview

This project is a full-stack web application that integrates ASP.NET Web API, GraphQL, MongoDB, and React. It provides authentication using JWT tokens and allows users to fetch data from external APIs, save it in a MongoDB database, and perform CRUD operations via GraphQL.

## Features

- JWT token-based authentication using ASP.NET Identity and PostgreSQL database for user management.
- Integration of GraphQL server for fetching data from external APIs and performing CRUD operations on MongoDB.
- Front-end implemented in React, providing a home page to fetch data and login/register pages for user authentication.
- Utilizes various libraries for back-end and front-end development, including Microsoft.AspNetCore, Npgsql.EntityFrameworkCore.PostgreSQL, HotChocolate, GraphQL-Request, Redux, React Router, etc.

## Installation


# Clone the repository
git clone <repository-url>

# Install dependencies for the ASP.NET Identity project
cd Back-end\MiddleLayer.API\MiddleLayer.Identity
dotnet restore

# Install dependencies for the GraphQL server project
cd Back-end\MiddleLayer.API\MiddleLayer.Infrastructure
dotnet restore

```bash
# Setting up Database

1. Install PostgreSQL on your system if you haven't already or create docker container
docker run --name postgres-container -p 1433:5432 -e POSTGRES_PASSWORD=<your password> -d postgres

2. Install MongoDb tool like Compas on your system if you haven't already or create docker container or you can chose to work with cloud based service like Atlas.

# Run the GraphQL server
dotnet run

# Install dependencies for the React front-end project
cd Front-end\client-for-middle-layer
npm install

# Start the React development server
npm start

# Technologies Used

- **ASP.NET Web API**: Used for building RESTful APIs and providing authentication services.
- **GraphQL**: Implemented GraphQL for querying and mutating data.
- **MongoDB**: NoSQL database used for storing and managing data fetched from external APIs.
- **PostgreSQL**: Relational database used for storing user authentication data.
- **React**: JavaScript library used for building the user interface of the front-end application.
- **Redux**: State management library used for managing application state in React.
- **GraphQL-Request**: Used for sending GraphQL queries and mutations from the front-end to the server.
- **React Router**: Used for declarative routing in the React application, enabling navigation between different pages/components.