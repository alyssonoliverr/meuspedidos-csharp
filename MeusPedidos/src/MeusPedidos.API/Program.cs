using MeusPedidos.Application.UseCases.Pedidos.CancelarPedido;
using MeusPedidos.Application.UseCases.Pedidos.CriarPedido;
using MeusPedidos.Application.UseCases.Pedidos.ListarPedido;
using MeusPedidos.Application.UseCases.Pedidos.ObterPedidoPorId;
using MeusPedidos.Application.UseCases.Pedidos.PagarPedido;
using MeusPedidos.Domain.Interfaces;
using MeusPedidos.Infrastructure.Persistence;
using MeusPedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IFormaDePagamentoRepository, FormaDePagamentoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<CriarPedidoHandler>();
builder.Services.AddScoped<ListarPedidosHandler>();
builder.Services.AddScoped<ObterPedidoPorIdHandler>();
builder.Services.AddScoped<PagarPedidoHandler>();
builder.Services.AddScoped<CancelarPedidoHandler>();


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
