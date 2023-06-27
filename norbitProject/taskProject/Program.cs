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
var mapgroup = app.MapGroup("task/");

//Добавление задания
mapgroup.MapPost("create/", (Tasks task) =>
{
    using (taskContext db = new taskContext())
    {
        var tasks = db.Tasks.ToList();
        if (task == null)
        {
            Results.BadRequest();
        }
        else
        {
            if (tasks.Count != 0)
            {
                task.Id = db.Tasks.Max(x => x.Id) + 1;
                db.Tasks.Add(task);
                db.SaveChanges();
                Results.Ok(task);
            }
            else
            {
                task.Id = 0;
                db.Tasks.Add(task);
                db.SaveChanges();
                Results.Ok(task);
            }
        }
    }
});
//Вывод всех заданий
mapgroup.MapGet("all/", () =>
{
    using (taskContext db = new taskContext())
    {
        var tasks = db.Tasks.ToList();
        return Results.Ok(tasks);
    }
});

app.Run();
