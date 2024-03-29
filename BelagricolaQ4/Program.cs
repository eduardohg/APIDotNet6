using BelagricolaQ4.Data;
using BelagricolaQ4.Repositorio;
using BelagricolaQ4.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BelagricolaQ4DBContex>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
);
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IClienteContatoRepositorio, ClienteContatoRepositorio>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
