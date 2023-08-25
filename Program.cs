using Microsoft.EntityFrameworkCore;
using UserStore.Data;
using Microsoft.OpenApi.Models;
using UserStore.Models;

// snippet ALLOW CORS
const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("users") ?? "Data Source=users1.db";

builder.Services.AddDbContext<UserDb>(options => options.UseSqlite(connectionString));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users API", Description = "Users user", Version = "v1" });
});

// snippet allow CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          // use specific origins
          //builder.WithOrigins("*"); 

          // use all origins
          builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
      });
});


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API V1");
});

// middleware to allow CORS
app.UseCors(MyAllowSpecificOrigins);


app.MapGet("/", () => "Users API working!");

app.MapGet("/user", async (UserDb db) => await db.Users.ToListAsync());

app.MapPost("/user", async (UserDb db, User user) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", user);
});


app.MapPut("/user/{id}", async (UserDb db, User updateUser, int id) =>
{
    var userItem = await db.Users.FindAsync(id);
    if (userItem is null) return Results.NotFound();
    userItem.Firstname = updateUser.Firstname;
    userItem.Lastname = updateUser.Lastname;
    userItem.About = updateUser.About;
    userItem.Username = updateUser.Username;
    userItem.Usernumber = updateUser.Usernumber;
    userItem.City = updateUser.City;
    userItem.Province = updateUser.Province;
    userItem.Country = updateUser.Country;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/user/{id}", async (UserDb db, int id) =>
{
    var todo = await db.Users.FindAsync(id);
    if (todo is null)
    {
        return Results.NotFound();
    }
    db.Users.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.Run();
