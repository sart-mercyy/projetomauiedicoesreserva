namespace MauiAppHotel.Models;

public class Quarto
{
    private string _descricao = "";
    private double _valorDiaria;

    public string Descricao
    {
        get { return _descricao; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("A descrição do quarto não pode ser vazia.");
            _descricao = value.Trim();
        }
    }

    public double ValorDiaria
    {
        get { return _valorDiaria; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("O valor da diária deve ser maior que zero.");
            _valorDiaria = value;
        }
    }

    public Quarto(string descricao, double valorDiaria)
    {
        Descricao = descricao;
        ValorDiaria = valorDiaria;
    }

    // Lista (array dinâmico) de objetos Quarto usada no Picker.
    public static List<Quarto> Catalogo() => new()
    {
        new Quarto("Standard", 150),
        new Quarto("Luxo", 250),
        new Quarto("Suíte Master", 400),
    };
}
