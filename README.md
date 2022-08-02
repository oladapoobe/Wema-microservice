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
## Azure AppInsights: Logging and Monitoring

Azure AppInsights integrated into the "Customer Microservice" for collecting the application Telemetry.

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


## How to run the application


1. Open the solution (.sln) in Visual Studio 2017 or later version
2. Configure the SQL connection string in Customer.WebApi -> Appsettings.json file
3. Configure the AppInsights Instrumentation Key in Transaction.WebApi -> Appsettings.json file. If you dont  have a key or don't require logs then comment the AppInsight related code in Startup.cs file 

4. Run the following projects in the solution
    - Customer.WebApi
    - Gateway.WebApi
5. Gateway host and port should be configured correctly in the ConsoleApp
6. Idenity and Transaction service host and port should be configured correctly in the gateway -> configuration.json 
7. Attached is the sql query to run for your database , which attached as wema script 


## Console App - Gateway Client
