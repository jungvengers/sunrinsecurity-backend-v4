using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using sunrinsecurity_backend_v4.Data;
using sunrinsecurity_backend_v4.Models;
namespace sunrinsecurity_backend_v4.Endpoints;

public static class ProjectEndpoints
{
    public static void MapProjectEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Project").WithTags(nameof(Project));

        group.MapGet("/", async (sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Project.ToListAsync();
        })
        .WithName("GetAllProjects")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Project>, NotFound>> (string id, sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Project.FindAsync(id)
                is Project model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetProjectById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Project project, sunrinsecurity_backend_v4Context db) =>
        {
            var foundModel = await db.Project.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(project);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateProject")
        .WithOpenApi();

        group.MapPost("/", async (Project project, sunrinsecurity_backend_v4Context db) =>
        {
            db.Project.Add(project);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Project/{project.ID}",project);
        })
        .WithName("CreateProject")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Project>, NotFound>> (string id, sunrinsecurity_backend_v4Context db) =>
        {
            if (await db.Project.FindAsync(id) is Project project)
            {
                db.Project.Remove(project);
                await db.SaveChangesAsync();
                return TypedResults.Ok(project);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteProject")
        .WithOpenApi();
    }
}
