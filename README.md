# CharacterVaultApi
CharacterDataTool API is an ASP.NET Core Web API that manages game character data and persists it to a JSON file. It provides REST endpoints to create, read, update, and delete characters, and includes Swagger for easy testing and exploration of the API.


## Tech Stack
- ASP.NET Core (.NET 9)
- C#
- Swagger / OpenAPI
- JSON file persistence
- Dependency Injection


# This project demonstrates:
-	Building a Web API with ASP.NET Core
-	Working with JSON file persistence
-	CRUD operations (Create, Read, Update, Delete)
-	Dependency Injection
-	Swagger / OpenAPI integration
  

# Features
- Create, read, update, and delete characters
- Data stored in a JSON file (Data/characters.json)
- Swagger UI for interactive API testing
- Clean project structure with separation of concerns:
- Models
- Services (repository)
- Endpoints



# A character is stored in JSON like this:
    {
      "id": 1,
      "name": "Aether",
      "class": "Warrior",
      "level": 10
    }


# Getting Started

Prerequisites
	•	.NET SDK (9 or 10, depending on your setup)
	•	VS Code or any C# editor

Run the API

# From the project folder:
    dotnet run

# You should see output similar to:
    Now listening on: http://localhost:5xxx

# Open your browser and go to:
    http://localhost:5xxx/swagger
  This opens the Swagger UI, where you can test all endpoints.


## Get 
  - Gets all characters
## Get 
  - Get all characters by id
## Post 
  - Create a character
## Put 
  - Update a character
## Delete 
  - Delete a character

# Data Storage
- Character data is stored in:
  Data/characters.json
  
- In development, the API reads and writes to the project’s Data folder.
- In a published build, the API can be configured to store data next to the executable.


# Purpose

This project was built to demonstrate core backend development skills using .NET, including API design, JSON serialization, file-based persistence, and basic application architecture.
