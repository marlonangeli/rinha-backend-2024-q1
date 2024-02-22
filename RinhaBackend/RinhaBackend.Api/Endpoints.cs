using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Database.Context;
using RinhaBackend.Api.Models;
using RinhaBackend.Api.Responses;

namespace RinhaBackend.Api;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPost("/clientes", async (
            [FromBody] CreateCliente cliente,
            [FromServices] RinhaBackendContext context) =>
        {
            var novoCliente = new Cliente(cliente.Nome, cliente.Limite, cliente.SaldoInicial);
            await context.Clientes.AddAsync(novoCliente);
            await context.SaveChangesAsync();

            return Results.Created($"/clientes/{novoCliente.Id}", novoCliente);
        }).WithName("AdicionarCliente").WithOpenApi();

        app.MapPost("/clientes/{id:int}/transacoes", async (
            [FromRoute] int id,
            [FromBody] CreateTransacao transacao,
            [FromServices] RinhaBackendContext context) =>
        {
            var cliente = await context.Clientes
                .Include(c => c.Transacoes)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return Results.NotFound();

            var novaTransacao = new Transacao(id, transacao);
            if (!cliente.AdicionarTransacao(novaTransacao))
                return Results.BadRequest("Transação inválida");

            await context.Transacoes.AddAsync(novaTransacao);
            await context.SaveChangesAsync();

            var saldo = cliente.Saldo + (novaTransacao.Tipo == 'd' ? -novaTransacao.Valor : novaTransacao.Valor);

            var response = new SaldoTransacao(saldo, cliente.Limite);

            return Results.Created($"/clientes/{id}/transacoes/{novaTransacao.Id}", response);
        }).WithName("AdicionarTransacao").WithOpenApi();

        app.MapGet("/clientes/{id:int}/extrato", async (
            [FromRoute] int id,
            [FromServices] RinhaBackendContext context) =>
        {
            var extrato = await context.Clientes
                .Include(c => c.Transacoes.Where(t => t.ClienteId == id))
                .AsNoTracking()
                .AsSplitQuery()
                .Where(c => c.Id == id)
                .Select(s => new Extrato()
                {
                    Saldo = new Saldo()
                    {
                        Total = s.Saldo,
                        Limite = s.Limite
                    },
                    Transacoes = s.Transacoes.Select(t => new TransacaoResponse()
                    {
                        Valor = t.Valor,
                        Data = t.Data,
                        Descricao = t.Descricao,
                        Tipo = t.Tipo
                    }).ToList()
                }).SingleOrDefaultAsync();

            return extrato == null ? Results.NotFound() : Results.Ok(extrato);
        }).WithName("Extrato").WithOpenApi();
    }
}