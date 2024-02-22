using System.ComponentModel.DataAnnotations.Schema;

namespace RinhaBackend.Api.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Transacao> Transacoes { get; set; }
    public double Limite { get; set; }
    public double SaldoInicial { get; set; }

    [NotMapped] public double Saldo => SaldoInicial + Transacoes.Sum(t => t.Tipo == 'd' ? -t.Valor : t.Valor);

    public bool AdicionarTransacao(Transacao transacao)
    {
        if (transacao == null)
            throw new ArgumentNullException(nameof(transacao));

        return transacao.Tipo != 'd' || Saldo - transacao.Valor >= -Limite;
    }

    public Cliente(string nome, double limite, double saldoInicial)
    {
        if (nome.Length is > 32 or < 1)
            throw new ArgumentOutOfRangeException(nameof(nome));

        if (limite < 0)
            throw new ArgumentOutOfRangeException(nameof(limite));

        Nome = nome;
        Limite = limite;
        SaldoInicial = saldoInicial;
    }
}

public record CreateCliente(string Nome, double Limite, double SaldoInicial);