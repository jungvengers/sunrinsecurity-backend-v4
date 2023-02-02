using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using sunrinsecurity_backend_v4.Data;
using sunrinsecurity_backend_v4.Models;
namespace sunrinsecurity_backend_v4.Endpoints;

public static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Application").WithTags(nameof(Application));

        group.MapGet("/", async (sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Application.ToListAsync();
        })
        .WithName("GetAllApplications")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Application>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Application.FindAsync(id)
                is Application model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetApplicationById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Application application, sunrinsecurity_backend_v4Context db) =>
        {
            var foundModel = await db.Application.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(application);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateApplication")
        .WithOpenApi();

        group.MapPost("/", async (Application application, sunrinsecurity_backend_v4Context db) =>
        {
            db.Application.Add(application);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Application/{application.ID}",application);
        })
        .WithName("CreateApplication")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Application>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            if (await db.Application.FindAsync(id) is Application application)
            {
                db.Application.Remove(application);
                await db.SaveChangesAsync();
                return TypedResults.Ok(application);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteApplication")
        .WithOpenApi();
    }
}
