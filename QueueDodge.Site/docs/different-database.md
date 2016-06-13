
# Choosing a different database provider

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
