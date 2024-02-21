using System.Text.Json.Serialization;
using RinhaBackend.Api.Models;

namespace RinhaBackend.Api.Responses;

public class Extrato
{
    [JsonPropertyName("saldo")] public Saldo Saldo { get; set; }
    [JsonPropertyName("ultimas_transacoes")] public List<TransacaoResponse> Transacoes { get; set; }

    public static Extrato FromCliente(Cliente cliente) =>
        new()
        {
            Saldo = new Saldo
            {
                Total = cliente.Saldo,
                Limite = cliente.Limite
            },
            Transacoes = cliente.Transacoes
                .OrderByDescending(t => t.Data)
                .Take(10)
                .Select(s => new TransacaoResponse()
                {
                    Valor = s.Valor,
                    Data = s.Data,
                    Descricao = s.Descricao,
                    Tipo = s.Tipo
                })
                .ToList()
        };
}

public record SaldoTransacao(double Saldo, double Limite);