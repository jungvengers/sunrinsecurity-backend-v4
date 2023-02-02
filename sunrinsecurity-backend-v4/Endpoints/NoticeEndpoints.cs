using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using sunrinsecurity_backend_v4.Data;
using sunrinsecurity_backend_v4.Models;
namespace sunrinsecurity_backend_v4.Endpoints;

public static class NoticeEndpoints
{
    public static void MapNoticeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Notice").WithTags(nameof(Notice));

        group.MapGet("/", async (sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Notice.ToListAsync();
        })
        .WithName("GetAllNotices")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Notice>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            return await db.Notice.FindAsync(id)
                is Notice model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetNoticeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Notice notice, sunrinsecurity_backend_v4Context db) =>
        {
            var foundModel = await db.Notice.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(notice);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateNotice")
        .WithOpenApi();

        group.MapPost("/", async (Notice notice, sunrinsecurity_backend_v4Context db) =>
        {
            db.Notice.Add(notice);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Notice/{notice.ID}",notice);
        })
        .WithName("CreateNotice")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Notice>, NotFound>> (int id, sunrinsecurity_backend_v4Context db) =>
        {
            if (await db.Notice.FindAsync(id) is Notice notice)
            {
                db.Notice.Remove(notice);
                await db.SaveChangesAsync();
                return TypedResults.Ok(notice);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteNotice")
        .WithOpenApi();
    }
}
