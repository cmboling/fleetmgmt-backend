# Fleet Management System Backend

![Fleet Management API](https://github.com/abhiroop43/fleetmgmt-backend/workflows/Fleet%20Management%20API/badge.svg)

Backend Web API for the Fleet Management System.

Uses **ASP.NET Core v3** and **IdentityServer4** for authentication.

1. Run the IdentityServer first, then the backend server.
2. To run the identity server navigate to the **FleetMgmt.IdentityServer** directory and run the commands:

   _dotnet ef database update --context ConfigurationDbContext_

   _dotnet ef database update --context ApplicationDbContext_

   _dotnet run /seed_

   The **seed** flag updates the database with the clients, the Identity resources and the API resources.

3. Navigate to the Data directory **FleetMgmt.Web** and run the command:

   _dotnet ef database update --context FmDbContext_

   This will populate the database with the tables.

4. Navigate to the web server directory **FleetMgmt.Web** and run the command _dotnet run_

5. To register the user, you may use the following request:

   URL: http://localhost:5000/Account/RegisterUser

   Request Type: POST

   Sample Request:

   {
   "email": "jane.doe@example.com",
   "password": "MySecuredPassword@123",
   "confirmPassword": "MySecuredPassword@123"
   }

6. To get the login token, you may use the following request:

   URL: http://localhost:5000/connect/token

   Request Type: POST

   Headers: {"Content-Type": "multipart/form-data"}

   Request Body:

   client_id:ro.client
   client_secret:secret
   grant_type:password
   username:john.doe@example.com
   password:Abcd@1234
   scope:fleetMgmt

##### Please Note: This solution (both the Web API and the IdentityServer) uses SQL Local DB. You may change the connection string from the **_appsettings.json_** file.

## Updating from a previous version

If updating from a previous version, first create a new migration, to handle the breaking changes introduced by EF Core update.

1. Navigate to the **FleetMgmt.IdentityServer** directory and run the commands:

_dotnet ef migrations add FrameworkUpdate_

_dotnet ef migrations add FrameworkUpdate --context ConfigurationDbContext_

Now, you may seed the database again using:

_dotnet run /seed_

2. Navigate to the Data directory **FleetMgmt.Data** and run the commands:

_dotnet ef migrations add FrameworkUpdateWeb --context FmDbContext -s ..\\FleetMgmt.Web\\_

_dotnet ef database update --context FmDbContext -s ..\\FleetMgmt.Web\\_

3. Now navigate to the web server directory **FleetMgmt.Web** and run the command _dotnet run_
