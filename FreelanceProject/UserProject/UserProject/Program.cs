using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserDomain;
using UserInfrastructure;

var builder = WebApplication.CreateBuilder(args);

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<UserContext>(b => b.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);

var usersGroup = app.MapGroup("user/");
//Вывод всех пользователей
usersGroup.MapGet("/all", (UserContext db) =>
{
    var users = db.Users.ToList();
    return Results.Ok(users);
});
//Добавление пользователя
usersGroup.MapPost("create/", (User user, UserContext db) =>
{
        var users = db.Users.ToList();
        if (user == null)
        {
            return Results.BadRequest();
        }

        if (users.Count != 0)
        {
            user.Id = db.Users.Max(x => x.Id) + 1;
            db.Users.Add(user);
            db.SaveChanges();
            return Results.Ok(user);
        }

        db.Users.Add(user);
        db.SaveChanges();
        return Results.Ok(user);
});
//Удаление пользователя
usersGroup.MapDelete("delete/", (int userId, UserContext db) =>
{
        var user = db.Users.FirstOrDefault(x => x.Id == userId);
        db.Users.Remove(user);
        db.SaveChanges();
        Results.Ok("Deleted!");
});
//Поиск по id
usersGroup.MapGet("getid/", (int userId, UserContext db) =>
{
        var user = db.Users.FirstOrDefault(x => x.Id == userId);
        return Results.Ok(user);
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.Run();
