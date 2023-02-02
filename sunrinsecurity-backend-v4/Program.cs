using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sunrinsecurity_backend_v4.Data;
using sunrinsecurity_backend_v4.Endpoints;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<sunrinsecurity_backend_v4Context>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("sunrinsecurity_backend_v4Context") ?? throw new InvalidOperationException("Connection string 'sunrinsecurity_backend_v4Context' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapNoticeEndpoints();

app.MapClubEndpoints();

app.MapProjectEndpoints();

app.MapResponseEndpoints();

app.MapApplicationEndpoints();

app.Run();
