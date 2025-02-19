using Microsoft.EntityFrameworkCore;
using APIEFCore.Domain;
using APIEFCore.Persistence;
using AutoMapper;
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
                        .Include(c => c.Channels)
                        .FirstOrDefaultAsync(model => model.Id == id)
                    is Domain.Client model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
            })
            .WithName("GetClientById")
            .WithOpenApi();

        group.MapPut("/{id}",
                async Task<Results<Ok<Domain.Client>, NotFound>> (long id, Domain.Client client, MyDbContext db,
                    IMapper mapper) =>
                {
                    var localClient = await db.Clients
                        .Include(c => c.Channels)
                        .FirstAsync(model => model.Id == id);
                    mapper.Map(client, localClient);

                    var updatedChannelIds = client.Channels.Select(c => c.Id).ToList();
                    var currentChannelIds = localClient.Channels.Select(c => c.Id).ToList();
                    var channelIdsToAdd = updatedChannelIds.Except(currentChannelIds);
                    var channelIdsToRemove = currentChannelIds.Except(updatedChannelIds);

                    if (channelIdsToRemove.Any())
                    {
                        var channelsToRemove =
                            localClient.Channels.Where(c => channelIdsToRemove.Contains(c.Id));
                        foreach (var channel in channelsToRemove)
                            localClient.Channels.Remove(channel);
                    }

                    if (channelIdsToAdd.Any())
                    {
                        var channelsToAdd = db.Channels.Where(c => channelIdsToAdd.Contains(c.Id));
                        foreach (var channel in channelsToAdd)
                            localClient.Channels.Add(channel);
                    }

                    await db.SaveChangesAsync();
                    return TypedResults.Ok(localClient);
                })
            .WithName("UpdateClient")
            .WithOpenApi();

        group.MapPost("/", async (Domain.Client client, MyDbContext db) =>
            {
                if (client.Channels != null && client.Channels.Count > 0)
                {
                    var list = client.Channels;
                    client.Channels = new List<Channel>();
                    foreach (var c in list)
                    {
                        var channel = await db.Channels.FirstOrDefaultAsync(ch => ch.Id == c.Id);
                        if (channel != null)
                            client.Channels.Add(channel);
                    }
                }

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