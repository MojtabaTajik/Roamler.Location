
<br/>
<p align="center">
 <img width="50%" src="https://raw.githubusercontent.com/MojtabaTajik/Roamler.Location/master/Resources/Roamler-Logo.png">
</p>

<br/>
<p align="center">
    <a href="https://github.com/MojtabaTajik/Roamler.Location/actions" target="_blank">
        <img src="https://github.com/MojtabaTajik/Roamler.Location/workflows/build-dot-net/badge.svg?branch=main">
    </a>
    <a href="https://github.com/MojtabaTajik/Roamler.Location/pulse" target="_blank">
        <img src="https://img.shields.io/github/last-commit/MojtabaTajik/Roamler.Location">
    </a>  
    <a href="https://github.com/MojtabaTajik/Roamler.Location/graphs/commit-activity" target="_blank">
        <img src="https://img.shields.io/github/commit-activity/w/MojtabaTajik/Roamler.Location">
    </br>
    </a>
        <a href="docker.com/" target="_blank">
        <img src="https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white">
    </a>
    </a>
        <a href="https://redis.io/" target="_blank">
        <img src="https://img.shields.io/badge/redis-%23DD0031.svg?style=for-the-badge&logo=redis&logoColor=white">
    </a>
    </a>
        <a href="https://dotnet.microsoft.com/en-us/" target="_blank">
        <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white">
    </a>
</p>
<br/>

This API let the client add location(s) to configured store and search for locatipns near the given location.

## 📖&nbsp; Architecture
The project developed based on Clean Architecture and has four layers: 
  
  * **API** entry point of the project, endpoint exposure, Swagger config and DI registration
  * **Application** The whole logic of the program following CQRS model
  * **Domain** The domain models
  * **Infrastructure** Services which communicating with external API/Tools/Files
  
## ✅&nbsp; Requirements
**[Docker](https://docs.docker.com/get-docker)** and **[Docker Compose](https://docs.docker.com/compose/compose-file)** required to deploy the project.

## 🚀&nbsp; Deploy

To run the project use terminal and go to project directory whoch docker-compose.yml is there:

    cd ~/Roamler.Location/src/
    docker compose up -d

Docker scripts automatically handle all required resources and dependencies, and after the command runs successfully, you can verify the project is running using the below command:

    docker ps

You should see three Docker containers running and the state of them should be **Healthy**:

- **roamler_loc_api** => Project container that host the API
- **roamler_loc_db** => Main DB which used in production
- **roamler_test_db** => DB which used in integration test

Now, the project is ready to use, just browse below URL using your browser:

    http://localhost:8040/swagger/index.html

## 📫&nbsp; Have a question? Ran into a problem?

Feel free to reach me at [Mojtaba Tajik](mailto:mojtabatajik@hotmail.com).