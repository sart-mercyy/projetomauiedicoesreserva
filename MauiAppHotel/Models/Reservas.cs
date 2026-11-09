using System.Globalization;

namespace MauiAppHotel.Models;

// Guarda as reservas em memória (List de objetos Hospedagem).
public static class Reservas
{
    public static List<Hospedagem> Lista { get; } = new();

    public static double TotalGeral => Lista.Sum(h => h.ValorTotal);

    public static string TotalFormatado => TotalGeral.ToString("C", new CultureInfo("pt-BR"));
}
