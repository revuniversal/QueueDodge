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
3. Clone this repository.


    git clone https://github.com/NickolasAcosta/QueueDodge.git
    cd QueueDodge
    ./build

Set these environment variables

    queueDodgeApi_apiKey        // Your Battle.net API key.
    queueDodgeApi_connection    // Your connection string.

Update the database.

    cd QueueDodge
    dotnet ef database update

Transpile the front end.

    cd ../QueueDodge.Site
    npm install
    gulp sass
    gulp dependencies
    tsc

## Running the api and site.
In the EXTRAS folder there are files to start the app:

 - **StartApi** : Runs QueueDodge.Api 
 - **StartSite**:  Runs QueueDodge.Site 
 - **3v3**:  An infinite loop that will get 3v3 ladder information every 30 seconds.

## Nginx
Get it [here](http://nginx.org/)

Making requests from port 5000 to port 5001 makes web browsers cry, so I use NGINX as a reverse proxy to deal with this for now.

A config file for NGINX is provided in this repo in the EXTRAS folder.

Using the provided configuration file should allow you access **https://localhost**

You will need your own certificate.

---

## Choosing a different database provider
Your database will need an Entity Framework 7 provider.  [Providers here](http://docs.efproject.net/en/latest/providers/index.html)

If you aren't using PostgreSQL there are a couple changes to make.  First, you will need to reference the library in your project.json file.

In QueueDodge/project.json:

    "dependencies": {
        ...
        // Change this according to your database provider.
        "Npgsql.EntityFrameworkCore.PostgreSQL": "1.0.0-rc2-release1",
        ...
    }

In QueueDodge/Startup.cs:

    // Change this according to your database provider.
    services.AddDbContext<QueueDodgeDB>(options => options.UseNpgsql(Configuration["connection"]));


in QueueDodge/Data/QueueDodgeDB:

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Change this according to your database provider.
        optionsBuilder.UseNpgsql(options.connection);
    }
