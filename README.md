# ContactList App

This is a monorepo project for the ContactList application, consisting of a C# .NET Core with Clean Achitecture in backend and an Angular with Material frontend. 
With docker-compose and gitHub actions to push the image to DockerHub and DigitalOcean

It is published in http://134.122.117.221/ for tests, and could be off to limit costs of Digital Ocean Cloud

It working but, its not done. Lacks all types of validations and security.
The Backend API initialy would use mongoDB and redis for cache. But I had to simplify, cause the mongoDB container did not start in my local docker.

## Backend (C# .NET Core)

- The backend is built using C# .NET Core.
- It follows the Clean Architecture pattern for modularity and maintainability.
- Redis is used as the database for storing contact information.
I was using MongoDB and redis for cache, but had to quit mongo after it did not start in my local machine docker.

### Setup

1. Clone the repository.
2. docker-compose up


The API should be available at http://localhost:3000.

API Endpoints
- GET /api/contacts: Retrieve a list of contacts.
- POST /api/contacts: Create a new contact.
- PUT /api/contacts/{id}: Update an existing contact.
- DELETE /api/contacts/{id}: Delete a contact.

## Frontend (Angular)

The frontend is built using Angular with Angular Material for UI components.
It consumes the RESTful API provided by the backend to manage contacts.


The frontend should be available at http://localhost

    Features
    - View a list of contacts.
    - Add new contacts.
    - Edit existing contacts.
    - Delete contacts.

# Contributing

    If you'd like to contribute to this project, please follow our Contributing Guidelines.

# License

    This project is licensed under the MIT License.