using System.Globalization;

namespace MauiAppHotel.Models;

public class Hospedagem
{
    private static readonly CultureInfo PtBr = new("pt-BR");

    private string _hospede = "";
    private int _adultos = 1;
    private int _criancas;
    private Quarto? _quarto;
    private DateTime _checkIn = DateTime.Today;
    private DateTime _checkOut = DateTime.Today.AddDays(1);

    public string Hospede
    {
        get { return _hospede; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Informe o nome do hóspede.");
            _hospede = value.Trim();
        }
    }

    public Quarto? Quarto
    {
        get { return _quarto; }
        set
        {
            if (value is null)
                throw new ArgumentException("Selecione o tipo de quarto.");
            _quarto = value;
        }
    }

    public int Adultos
    {
        get { return _adultos; }
        set
        {
            if (value < 1 || value > 4)
                throw new ArgumentException("A reserva deve ter de 1 a 4 adultos.");
            _adultos = value;
        }
    }

    public int Criancas
    {
        get { return _criancas; }
        set
        {
            if (value < 0 || value > 4)
                throw new ArgumentException("A reserva aceita de 0 a 4 crianças.");
            _criancas = value;
        }
    }

    public DateTime CheckIn
    {
        get { return _checkIn; }
        set
        {
            if (value.Date < DateTime.Today)
                throw new ArgumentException("O check-in não pode ser anterior a hoje.");
            _checkIn = value.Date;
        }
    }

    public DateTime CheckOut
    {
        get { return _checkOut; }
        set
        {
            if (value.Date <= _checkIn)
                throw new ArgumentException("O check-out deve ser posterior ao check-in.");
            _checkOut = value.Date;
        }
    }

    // Somente leitura (calculadas)
    public int Noites => (_checkOut - _checkIn).Days;

    // Diária cobrada por adulto; crianças pagam 50%.
    public double ValorTotal =>
        _quarto is null ? 0 : Noites * _quarto.ValorDiaria * (_adultos + 0.5 * _criancas);

    public string QuartoDescricao => _quarto?.Descricao ?? "-";

    public string Periodo =>
        $"{_checkIn:dd/MM/yyyy} a {_checkOut:dd/MM/yyyy} · {Noites} noite(s)";

    public string Ocupacao => $"{_adultos} adulto(s), {_criancas} criança(s)";

    public string ValorFormatado => ValorTotal.ToString("C", PtBr);
}
