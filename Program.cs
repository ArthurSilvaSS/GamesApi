using GamesAPI.Data;
using GamesAPI.Middlewares;
using GamesAPI.Services.Games;
using GamesAPI.Services.Platform;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//Entity Framework and MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 25)))
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions
        .ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddControllers().AddNewtonsoftJson();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "GamesAPI V1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseMiddleware<ExeptionHandlingMiddleware>();
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
