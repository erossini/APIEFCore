using Microsoft.EntityFrameworkCore;
using APIEFCore.Domain;
using APIEFCore.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace APIEFCore.Endpoints;

public static class ChannelEndpoints
{
    public static void MapChannelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Channel").WithTags(nameof(Channel));

        group.MapGet("/", async (MyDbContext db) => { return await db.Channels.ToListAsync(); })
            .WithName("GetAllChannels")
            .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Channel>, NotFound>> (long id, MyDbContext db) =>
            {
                return await db.Channels.AsNoTracking()
                        .FirstOrDefaultAsync(model => model.Id == id)
                    is Channel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
            })
            .WithName("GetChannelById")
            .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (long id, Channel channel, MyDbContext db) =>
            {
                var affected = await db.Channels
                    .Where(model => model.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(m => m.Id, channel.Id)
                        .SetProperty(m => m.Name, channel.Name)
                    );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateChannel")
            .WithOpenApi();

        group.MapPost("/", async (Channel channel, MyDbContext db) =>
            {
                db.Channels.Add(channel);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Channel/{channel.Id}", channel);
            })
            .WithName("CreateChannel")
            .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (long id, MyDbContext db) =>
            {
                var affected = await db.Channels
                    .Where(model => model.Id == id)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteChannel")
            .WithOpenApi();
    }
}