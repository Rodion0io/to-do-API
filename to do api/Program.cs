using to_do_api.Modules;
using Microsoft.EntityFrameworkCore;
using to_do_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// БД
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection));

// Регистрация сервиса ICardService
builder.Services.AddScoped<ICardService, CardService>();

// Добавляем CORS с политикой, разрешающей доступ с любого источника
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Инициализация БД
// using var serviceScope = app.Services.CreateScope();
// var dbContext = serviceScope.ServiceProvider.GetService<Context>();
// dbContext?.Database.Migrate(); // Миграция

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Добавляем middleware CORS с политикой AllowAllOrigins
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();