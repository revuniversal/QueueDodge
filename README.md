# QueueDodge
I hate queueing into cheaters so I made this site.
I don't like people accusing me of harvesting IP addresses so I made it open source.

[![Join the chat at https://gitter.im/NickolasAcosta/QueueDodge](https://badges.gitter.im/NickolasAcosta/QueueDodge.svg)](https://gitter.im/NickolasAcosta/QueueDodge?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/8nxyi7dli2mrukxb?svg=true)](https://ci.appveyor.com/project/NickolasAcosta/queuedodge)
[![Build Status](https://travis-ci.org/NickolasAcosta/QueueDodge.svg?branch=master)](https://travis-ci.org/NickolasAcosta/QueueDodge)

**Stack**: C#/.NET, SQL, Angular2, Typescript  
**Server**: Kestrel  
**Reverse Proxy**: Nginx

# Getting Started

1. [Get ASP.NET Core RC2](https://www.microsoft.com/net/core#windows)
2. [Choose your favorite Entity Framework 7 compatible database.](http://ef.readthedocs.org/en/latest/providers/)
3. Clone this repository  


    git clone https://github.com/NickolasAcosta/QueueDodge.git  
    cd QueueDodge  
    ./build  


## Set these environment variables

    queueDodgeApi_apiKey        // Your Battle.net API key.
    queueDodgeApi_connection    // Your connection string.

## Create the database.

    cd QueueDodge
    dotnet ef migrations add init
    dotnet ef database update

## Running a project

    cd ../QueueDodge.Api
    dotnet run

## Populate database (temporary sketchy measure)

    http://localhost/api/seed

## Nginx
Get it [here](http://nginx.org/)

Making requests from port 5000 to port 5001 makes web browsers cry, so I use NGINX as a reverse proxy to deal with this for now.

A config file for NGINX is provided in this repo in the EXTRAS folder.

Using the provided configuration file should allow you access **https://localhost**


