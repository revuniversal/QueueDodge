# QueueDodge
I hate queueing into cheaters so I made this site.
I don't like people accusing me of harvesting IP addresses so I made it open source.

[![Join the chat at https://gitter.im/NickolasAcosta/QueueDodge](https://badges.gitter.im/NickolasAcosta/QueueDodge.svg)](https://gitter.im/NickolasAcosta/QueueDodge?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

**Stack**: C#.NET, SQL, Angular2, Typescript  
**Server**: Kestrel    (Cross platform! Woot!)  
**Reverse Proxy**: Nginx  

# Getting Started

1. [Get ASP.NET Core RC1](https://docs.asp.net/en/latest/)
2. [Get your favorite Entity Framework 7 compatible database.](http://ef.readthedocs.org/en/latest/providers/)
3. Clone this repository.
4. There are three projects:  
   a. **QueueDodge**:  The library  
   b. **QueueDodge.Api**:  The api  
   b. **QueueDodge.Site**:  The client  

In each of these projects, restore packages and build.

    dnu restore
    dnu build


### Secret Stuff
ASP.NET manages "Secrets" in a special way.  This project uses secrets to keep connection strings and battle.net api keys private.  You can read about secrets [here](https://docs.asp.net/en/latest/security/app-secrets.html#accessing-user-secrets-via-configuration)

Install the secret manager:

    dnu commands install Microsoft.Extensions.SecretManager


In QueueDodge and QueueDodge.Api:

    user-secret set apiKey "Your Battle.Net api key here."
    user-secret set connection "User ID=Queuedodge; password=queuedodge;Host=localhost;5432;Database=QueueDodge;Pooling=true;Connection Lifetime=0;"

## Database
Your database will need an Entity Framework 7 provider.  [Providers are listed here](http://docs.efproject.net/en/latest/providers/index.html)

If you aren't using PostgreSQL there are a couple changes to make.  First, you will need to reference the appropriate library in your project.JSON file.

In QueueDodge/project.json:

    "dependencies": {
    ...
    "EntityFramework7.Npgsql": "3.1.0-rc1-3",   // Change this according to your database provider.
    ...
    }

In QueueDodge/Startup.cs:

    services.AddEntityFramework()
    .AddNpgsql() // Change this and the next line according to your database provider.
    .AddDbContext<QueueDodgeDB>(options => options.UseNpgsql(Configuration["connection"]));


in QueueDodge/Data/QueueDodgeDB:

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    // Change this according to your database provider.
    optionsBuilder.UseNpgsql(options.connection);
    }

Navigate to the "QueueDodge" folder and run the following commands.

    dnx ef migrations add init
    dnx ef database update


## Running the api and site.
To run QueueDodge.Api, navigate to QueueDodge.Api and run:

    dnx web --server.urls "http://localhost:5001"

To run QueueDodge.Site, navigate to QueueDodge.Site and run:

    dnx web

In the EXTRAS folder there are a couple files for windows users:

 - **StartApi** : Runs QueueDodge.Api and hosts it on localhost:5001  
 - **StartSite**:  Runs QueueDodge.Site and hosts it on localhost:5000  
 - **3v3**:  An infinite loop that will get 3v3 ladder information every 30 seconds.  

## Nginx
Get it [here](http://nginx.org/)

Making requests from port 5000 to port 5001 makes web browsers cry, so I use NGINX as a reverse proxy to deal with this for now.

A config file for NGINX is provided in this repo in the EXTRAS folder.

Using the provided configuration file should allow you to access the site using the url **https://localhost**


## These docs are a work in progress.