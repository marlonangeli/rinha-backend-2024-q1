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

            var response = new SaldoTransacao(cliente.Saldo, cliente.Limite);

            return Results.Created($"/clientes/{id}/transacoes/{novaTransacao.Id}", response);
        }).WithName("AdicionarTransacao").WithOpenApi();

        app.MapGet("/clientes/{id:int}/extrato", async (
            [FromRoute] int id,
            [FromServices] RinhaBackendContext context) =>
        {
            var cliente = await context.Clientes
                .Include(c => c.Transacoes)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return Results.NotFound();

            var extrato = Extrato.FromCliente(cliente);

            return Results.Ok(extrato);
        }).WithName("Extrato").WithOpenApi();
    }
}