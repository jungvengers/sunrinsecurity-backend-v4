using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using sunrinsecurity_backend_v4.Data;
using sunrinsecurity_backend_v4.Models;
namespace sunrinsecurity_backend_v4.Endpoints;

public static class ClubEndpoints
{
    public static void MapClubEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Club").WithTags(nameof(Club));

        group.MapGet("/", async (sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Club.ToListAsync();
        })
        .WithName("GetAllClubs")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Club>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Club.FindAsync(id)
                is Club model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetClubById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Club club, sunrinsecurity_backend_v4Context db) =>
        {
            var foundModel = await db.Club.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(club);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateClub")
        .WithOpenApi();

        group.MapPost("/", async (Club club, sunrinsecurity_backend_v4Context db) =>
        {
            db.Club.Add(club);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Club/{club.ID}",club);
        })
        .WithName("CreateClub")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Club>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            if (await db.Club.FindAsync(id) is Club club)
            {
                db.Club.Remove(club);
                await db.SaveChangesAsync();
                return TypedResults.Ok(club);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteClub")
        .WithOpenApi();
    }
}
