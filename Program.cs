using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.EfCore;
using TrilhaApiDesafio.Interfaces;
using TrilhaApiDesafio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<ITarefa, TarefaEfCore>();
builder.Services.AddTransient<IFuncionario, FuncionarioEfCore>();
builder.Services.AddTransient<IHistoricoTarefa, HistoricoTarefaEfCore>();
builder.Services.AddTransient<ITarefaService, TarefaService>();
builder.Services.AddTransient<IFuncionarioService, FuncionarioService>();
builder.Services.AddTransient<IHistoricoTarefaService, HistoricoTarefaService>();

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

app.Run();
