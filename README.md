# German Articles API
To run download and install https://dotnet.microsoft.com/download/dotnet-core/3.1 on your machine.


From your terminal/console go to the folder where you cloned/downloaded the code and type `dotnet ef database update`. That will create the SQLite database.

If you are missing ef run `dotnet tool install --global dotnet-ef` and then repeat the above step.

When the database is created, use `dotnet run` to run the API. It is run on port 5000.