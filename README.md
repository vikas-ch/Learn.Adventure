# Learn.Adventure
A back-end for a simple web application which allows a player to choose their own adventure by picking from multiple choices in order to progress to the next set of choices, until they get to one of the endings.

The solution contains the core API project and a test project.

The API project is written in `.NET 5` and has `Docker` support.

`MongoDb` is used as the backend datastore.

The `docker-compose.yml` file contains the script to create a `MongoDb` instance, a `mongo-express` instance (used as a GUI to view the `MongoDb` collections) and the application itself (via the `Dockerfile`).

All the containers can be run using `docker compose up -d`. Once the container are started and running, the API `Swagger` page is accessible at `http://localhost:5100/swagger`.
