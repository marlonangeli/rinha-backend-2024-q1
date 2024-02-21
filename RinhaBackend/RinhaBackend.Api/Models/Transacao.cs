using System.Text.Json.Serialization;

namespace RinhaBackend.Api.Models;

public class Transacao
{
    public int Id { get; set; }
    public char Tipo { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public Transacao() { }

    public Transacao(char tipo, double valor, DateTime data, string descricao, int clienteId)
    {
        if (tipo != 'c' && tipo != 'd')
            throw new ArgumentException("Tipo de transação inválido, deve ser 'c' ou 'd'");

        if (valor <= 0 || valor % 1 != 0)
            throw new ArgumentException("Valor inválido, deve ser positivo e inteiro");

        if (descricao.Length is < 1 or > 10)
            throw new ArgumentException("Descrição inválida, deve ter entre 1 e 10 caracteres");

        Tipo = tipo;
        Valor = valor;
        Data = data;
        Descricao = descricao;
        ClienteId = clienteId;
    }

    public Transacao(char tipo, double valor, string descricao, int clienteId) :
        this(tipo, valor, DateTime.UtcNow, descricao, clienteId) { }

    public Transacao(int clienteId, CreateTransacao transacao) :
        this(transacao.Tipo, transacao.Valor, DateTime.UtcNow, transacao.Descricao, clienteId) { }
}

public record CreateTransacao(char Tipo, double Valor, string Descricao);

public class TransacaoResponse
{
    [JsonPropertyName("valor")]
    public double Valor { get; set; }
    [JsonPropertyName("realizada_em")]
    public DateTime Data { get; set; }
    [JsonPropertyName("descricao")]
    public string Descricao { get; set; }
    [JsonPropertyName("tipo")]
    public char Tipo { get; set; }
}