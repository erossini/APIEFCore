# Using Entity Framework Core via APIs with complex objects

I want to give a complete example of minimal APIs in Blazor with Entity Framework Core with complex objects. I always struggle to have a solution working when my model has dependencies with other object. Here I show my test and my code. The code is in [NET9](https://puresourcecode.com/category/dotnet/net9/). In the [Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/update-related-data?view=aspnetcore-9.0), there are some examples but it is not complex enough. A few days ago, [I posted about another problem](https://puresourcecode.com/dotnet/net9/pendingmodelchangeswarning-with-net9/) I had with NET9 and Entity Framework Core.

## Posts

This code is related to the following posts:

- [APIs with Entity Framework Core](https://puresourcecode.com/dotnet/net9/apis-with-entity-framework-core)
- [APIs with Entity Framework Core: POST](https://puresourcecode.com/dotnet/csharp/apis-with-entity-framework-core-post/)
- [APIs with Entity Framework Core: PUT](https://puresourcecode.com/dotnet/net7/apis-with-entity-framework-core-put/)

## Why this project

Let me show a real database I'm working on. As you can see I have a few tables but not all of them are coming strictly from the code. The tables `Clients`, `ClientAddresses` and `tbl_Channels` are created from the `Domain`. `ChannelClient` is a joined table that Entity Framework Core is creating automatically.

![image](https://github.com/user-attachments/assets/b8496939-fd06-4c00-a558-0582311dc89d)
