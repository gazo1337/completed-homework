using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserDomain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
//Вывод всех пользователей
app.MapGet("user/", () =>
{
    using (userContext db = new userContext())
    {
        var users = db.Users.ToList();
        return Results.Ok(users);
    }
});
//Добавление пользователя
app.MapPost("user/create/", (User user) =>
{
    using (userContext db = new userContext())
    {
        var users = db.Users.ToList();
        if (user == null)
        {
            Results.BadRequest();
        }
        else
        {
            if (users.Count != 0)
            {
                user.Id = db.Users.Max(x => x.Id) + 1;
                db.Users.Add(user);
                db.SaveChanges();
                Results.Ok(user);
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();
                Results.Ok(user);
            }
        }
    }
});
//Удаление пользователя
app.MapDelete("user/delete/", (int userId) =>
{
    using (userContext db = new userContext())
    {
        var user = db.Users.FirstOrDefault(x => x.Id == userId);
        db.Users.Remove(user);
        db.SaveChanges();
        Results.Ok("Deleted!");
    }
});
//Поиск по id
app.MapGet("user/getid/", (int userId) =>
{
    using (userContext db = new userContext())
    {
        var user = db.Users.FirstOrDefault(x => x.Id == userId);
        return Results.Ok(user);
    }
});
app.Run();
