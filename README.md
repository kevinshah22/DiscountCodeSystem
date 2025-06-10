Created 3 projects inside the main solution file.
    • DiscountCodeService: which was generated using .net 8.0 framework with gRPC service framework, we configure the endpoints to run on port 5000 from program.cs file and add configure dbcontext and connectionstring. 
        ◦ It has protos for discount with different messages for code generation, code generate response, user code request message and user code usage response which will be exported as DiscountService
        ◦ Created two service classes one is “CodeStore”, which will be used to perform generating and using the code and interact with the Data layer project for CRUD operation another service “DiscountServiceImpl” which will be use in the client application to send the requested models and inherit the “DiscountService” protos to send appropriate message/response to the client according the method being called.
        ◦ Modified appsettings.json for the connection string of the database, we have used MSSQL database for the project for quick implementation but can use SQLite or PostgreSQL as well. 
    • DiscountCodeSystem.Data: is the data layer project for the database operation, in the project there are two files
        ◦ DiscountCode is the model file for the table structure of SQL database table.
        ◦ DiscountDBContext is the db context file use for performing CRUD operation against the DbSet of DiscountCode model.
    • CodeClientApp: Created using Blazor server application which will have grpc..net.client, Google.protobuf and Grpc.tools nuget packages which will be useful to call GRPC service endpoints from the client application. In client applications we didn’t implement UI, just used the base configuration of Blazor application and used Home component to create codes and option to use the codes once its used. 

Note: We have also provided a script file to create a table in the SQL database table for the discountCode.
    • Due to time constraint we haven’t implemented logging using serilog, and unit test project which we usually preffere xUnit for the test case creation to verify the business logic. 
    • Also this was a simple project and does not have business logic requirement so we haven’t created business layer between gRPC and data layer to handle the business configuration/logic. 
