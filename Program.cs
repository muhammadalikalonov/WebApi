using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Настройка контекста базы данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=123;Database=postgres"));

// Добавление контроллеров
builder.Services.AddControllers();

// Добавление Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация репозиториев в контейнере зависимостей
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Store>, StoreRepository>();
builder.Services.AddScoped<IRepository<Device>, DeviceRepository>();

var app = builder.Build();

// Включение Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Другие настройки...
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
