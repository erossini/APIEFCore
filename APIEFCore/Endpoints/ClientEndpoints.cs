using Microsoft.EntityFrameworkCore;
using APIEFCore.Domain;
using APIEFCore.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace APIEFCore.Endpoints;

public static class ClientEndpoints
{
    public static void MapClientEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Client").WithTags(nameof(Client));

        group.MapGet("/", async (MyDbContext db) => { return await db.Clients.ToListAsync(); })
            .WithName("GetAllClients")
            .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Domain.Client>, NotFound>> (long id, MyDbContext db) =>
            {
                return await db.Clients.AsNoTracking()
                        .FirstOrDefaultAsync(model => model.Id == id)
                    is Domain.Client model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
            })
            .WithName("GetClientById")
            .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (long id, Domain.Client client, MyDbContext db) =>
            {
                var affected = await db.Clients
                    .Where(model => model.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(m => m.Id, client.Id)
                        .SetProperty(m => m.FirstName, client.FirstName)
                        .SetProperty(m => m.LastName, client.LastName)
                    );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateClient")
            .WithOpenApi();

        group.MapPost("/", async (Domain.Client client, MyDbContext db) =>
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Client/{client.Id}", client);
            })
            .WithName("CreateClient")
            .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (long id, MyDbContext db) =>
            {
                var affected = await db.Clients
                    .Where(model => model.Id == id)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteClient")
            .WithOpenApi();
    }
}