using System.ComponentModel.DataAnnotations.Schema;

namespace RinhaBackend.Api.Models;

public class Cliente
{
    public int Id { get; set; }
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
}

public class CreateCliente(double limite, double saldoInicial)
{
    public required double Limite
    {
        get => limite;
        init
        {
            if (value < 0)
                throw new ArgumentException("Limite inválido");
            limite = value;
        }
    }

    public required double SaldoInicial
    {
        get => saldoInicial;
        init
        {
            if (value < 0)
                throw new ArgumentException("Saldo inicial inválido");
            saldoInicial = value;
        }
    }
}