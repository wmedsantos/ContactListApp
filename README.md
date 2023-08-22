# ContactList App

This is a monorepo project for the ContactList application, consisting of a C# .NET Core backend and an Angular frontend.

## Backend (C# .NET Core)

- The backend is built using C# .NET Core.
- It follows the Clean Architecture pattern for modularity and maintainability.
- Redis is used as the database for storing contact information.
I was using MongoDB and redis for cache, but had to quit mongo after it did not start in my local machine docker.

### Setup

1. Clone the repository.

2. Navigate to the `backend` directory:
   ```shell
   cd backend

3. Configure MongoDB and Redis connection settings in appsettings.json.

4. Install required NuGet packages:

    ```shell
        Copy code
        dotnet restore
        Run the application:

    ```shell
        Copy code
        dotnet run

The API should be available at http://localhost:5000.

API Endpoints
- GET /api/contacts: Retrieve a list of contacts.
- POST /api/contacts: Create a new contact.
- PUT /api/contacts/{id}: Update an existing contact.
- DELETE /api/contacts/{id}: Delete a contact.

## Frontend (Angular)

The frontend is built using Angular with Angular Material for UI components.
It consumes the RESTful API provided by the backend to manage contacts.

### Setup

1. Navigate to the frontend directory:

    ```shell
        Copy code
        cd frontend
        Install dependencies:

    ```shell
        Copy code
        npm install
        Start the development server:

    ```shell
        Copy code
        ng serve

The frontend should be available at http://localhost:4200.

    Features
    - View a list of contacts.
    - Add new contacts.
    - Edit existing contacts.
    - Delete contacts.
    - Optional: Client-side caching with Redis.

# Contributing

    If you'd like to contribute to this project, please follow our Contributing Guidelines.

# License

    This project is licensed under the MIT License.