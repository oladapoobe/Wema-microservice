# Wema-microservice



## Application Architecture

The my sample application is build based on the microservices architecture. There are serveral advantages in building a application using Microservices architecture like Services can be developed, deployed and scaled independently.The below diagram shows the high level design of Back-end architecture.


## Design of Microservice
This diagram shows the internal design of the Transaction Microservice. The business logic and data logic related to transaction service is written in a seperate transaction processing framework. The framework receives input via Web Api and process those requests based on some simple rules. The transaction data is stored up in SQL database.

## Technologies
- C#.NET
- ASP.NET WEB API Core
- SQL Server

## Opensource Tools Used
- Automapper (For object-to-object mapping)
- Entity Framework Core (For Data Access)
- Swashbucke (For API Documentation)
- XUnit (For Unit test case)
- Ocelot (For API Gateway Aggregation)

## Cloud Platform Services
- Azure App Insights (For Logging and Monitoring)
- Azure SQL Database (For Data store)

- **Gateway.WebApi** 
    - Validates the incoming Http request by checking for authorized JWT token in it.
    - Reroute the Http request to a downstream service.
- 
## Exception Handling
A Middleware is written to handle the exceptions and it is registered in the startup to run as part of http request. Every http request, passes through this exception handling middleware and then executes the Web API controller action method. 

* If the action method is successfull then the success response is send back to the client. 
* If any exception is thrown by the action method, then the exception is caught and handled by the Middleware and appropriate response is sent back to the client.


public async Task InvokeAsync(HttpContext context, RequestDelegate next)
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        var message = CreateMessage(context, ex);
        _logger.LogError(message, ex);

        await HandleExceptionAsync(context, ex);
    }
}
```
## Db Concurrency Handling

Db concurrency is related to a conflict when multiple transactions trying to update the same data in the database at the same time. In the below diagram, if you see that Transaction 1 and Transaction 2 are against the same account, one trying to deposit amount into account and the other system tring to Withdraw amount from the account at the same time. The framework contains two logical layers, one handles the Business Logic and the other handles the Data logic. 

When a data is read from the DB and when business logic is applied to the data, at this context, there will be three different states for the values relating to the same record.

- **Database values** are the values currently stored in the database.
- **Original values** are the values that were originally retrieved from the database
- **Current values** are the new values that application attempting to write to the database.

The state of the values in each of the transaction produces a conflict when the system attempts to save the changes and identifies using the concurrency token that the values being updated to the database are not the Original values that was read from the database and it throws DbUpdateConcurrencyException.


The general approach to handle the concurrency conflict is:

1. Catch **DbUpdateConcurrencyException** during SaveChanges
2. Use **DbUpdateConcurrencyException.Entries** to prepare a new set of changes for the affected entities.
3. **Refresh the original values** of the concurrency token to reflect the current values in the database.
4. **Retry the process** until no conflicts occur.

## Azure AppInsights: Logging and Monitoring

Azure AppInsights integrated into the "Transaction Microservice" for collecting the application Telemetry.

```
public void ConfigureServices(IServiceCollection services)
{           
   services.AddApplicationInsightsTelemetry(Configuration);           
}
```

AppInsights SDK for Asp.Net Core provides an extension method AddApplicationInsights on ILoggerFactory to configure logging. All transactions related to Deposit and Withdraw are logged through ILogger into AppInsights logs.

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
{
   log.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);     
}
```

To use AppInsights, you need to have a Azure account and  create a AppInsights instance in the Azure Portal for your application, that will give you an instrumentation key which should be configured in the appsettings.json

```
 "ApplicationInsights": {
    "InstrumentationKey": "<Your Instrumentation Key>"
  },
```
---
## Swagger: API Documentation

Swashbuckle Nuget package added to the "Transaction Microservice" and Swagger Middleware configured in the startup.cs for API documentation. when running the WebApi service, the swagger UI can be accessed through the swagger endpoint "/swagger".

```
public void ConfigureServices(IServiceCollection services)
{            
     services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new Info { Title = "Simple Transaction Processing", Version = "v1" });
     });
}
```

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
{           
     app.UseSwagger();
     app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Transaction Processing v1");
     });           
}
```


## How to run the application


1. Open the solution (.sln) in Visual Studio 2017 or later version
2. Configure the SQL connection string in Transaction.WebApi -> Appsettings.json file
3. Configure the AppInsights Instrumentation Key in Transaction.WebApi -> Appsettings.json file. If you dont  have a key or don't require logs then comment the AppInsight related code in Startup.cs file 
4. Check the Identity.WebApi -> UserService.cs file for Identity info. User details are hard coded for few accounts in Identity service which can be used to run the app. Same details shown in the below table.
5. Run the following projects in the solution
    - Customer.WebApi
    - Gateway.WebApi
6. Gateway host and port should be configured correctly in the ConsoleApp
7. Idenity and Transaction service host and port should be configured correctly in the gateway -> configuration.json 


## Console App - Gateway Client
