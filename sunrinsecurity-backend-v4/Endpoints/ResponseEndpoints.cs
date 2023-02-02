using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using sunrinsecurity_backend_v4.Data;
using sunrinsecurity_backend_v4.Models;
namespace sunrinsecurity_backend_v4.Endpoints;

public static class ResponseEndpoints
{
    public static void MapResponseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Response").WithTags(nameof(Response));

        group.MapGet("/", async (sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Response.ToListAsync();
        })
        .WithName("GetAllResponses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Response>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Response.FindAsync(id)
                is Response model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetResponseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Response response, sunrinsecurity_backend_v4Context db) =>
        {
            var foundModel = await db.Response.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(response);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateResponse")
        .WithOpenApi();

        group.MapPost("/", async (Response response, sunrinsecurity_backend_v4Context db) =>
        {
            db.Response.Add(response);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Response/{response.ID}",response);
        })
        .WithName("CreateResponse")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Response>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            if (await db.Response.FindAsync(id) is Response response)
            {
                db.Response.Remove(response);
                await db.SaveChangesAsync();
                return TypedResults.Ok(response);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteResponse")
        .WithOpenApi();
    }
}
