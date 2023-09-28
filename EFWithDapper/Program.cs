using Dapper;
using EFWithDapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

connection.Execute(@"
        CREATE TABLE Produto (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Descricao TEXT,
            Preco DECIMAL(18, 2),
            Desconto DECIMAL(18, 2)
        )");


builder.Services.AddSingleton<IDbConnection>(connection);

builder.Services.AddScoped<IRepository<Produto>, EFRepository<Produto>>();
builder.Services.AddScoped<IDapperRepository, DapperRepository<Produto>>();

builder.Services.AddDbContext<ProdutoContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDatabaseProduto");
});

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

app.Run();
