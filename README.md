# Clean Architecture Boilerplate

Clean Architecture for ASP.NET 6.0. Built with Onion/Hexagonal Architecture.

## About The Project

For ASP.NET 6.0 (WebApi)

# Technologies

- ASP.NET 6
- Entity Framework Core
- AutoMapper
- FluentValidation
- NUnit, FluentAssertions, Moq & Respawn
- Docker
- Serilog (ELK)
- Monitoring (ELK)

## Serilog

Elastic (http://localhost:9200)= elastic:changeme
Elastic Head: http://localhost:9100/?auth_user=elastic&auth_password=changeme
Kibana: http://localhost:5601

To start the components:
docker-compose up

add the -d option to run in the background

To scale the nodes:

docker-compose up --scale elasticsearch=3

To shutdown:

docker-compose down

## Monitoring - Elastic APM

- Elasticsearch and kibana setup from docker-compose.yml
- This tutorial install APM Server -> http://localhost:5601/app/home#/tutorial/apm
- Run ./apm-server e 
- Asp.net Core install package Elastic.Apm.NetCoreAll after startup.cs use app.UseAllElasticApm(_configuration); 

## Database Migrations

To use dotnet-ef for your migrations please add the following flags to your command (values assume you are executing from repository root)

--project src\Boilerplate.Infrastructure (optional if in this folder)
--startup-project src\Boilerplate.Api
--output-dir Persistence/Migrations
For example, to add a new migration from the root folder:

dotnet ef migrations add "EfCoreApplicationDbContext" --project src\Boilerplate.Infrastructure --startup-project src\Boilerplate.Api --output-dir Persistence\Migrations

## Unit Tests

- XUnit
- Moq
- FluentAssertions 
- AutoFixture

## Roadmap
If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing
State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment
Show your appreciation to those who have contributed to the project.

## License
For open source projects, say how it is licensed.

## Project status
If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing your project to keep going. You can also make an explicit request for maintainers.

