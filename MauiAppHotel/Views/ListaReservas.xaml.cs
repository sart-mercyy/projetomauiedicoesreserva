using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ListaReservas : ContentPage
{
    public ListaReservas()
    {
        InitializeComponent();
        Atualizar();
    }

    private void Atualizar()
    {
        colReservas.ItemsSource = null;
        colReservas.ItemsSource = Reservas.Lista;
        lblTotal.Text = $"{Reservas.Lista.Count} reserva(s) · total {Reservas.TotalFormatado}";
    }

    private async void OnCancelarClicked(object? sender, EventArgs e)
    {
        if (sender is not Button botao || botao.CommandParameter is not Hospedagem reserva)
            return;

        bool confirmar = await DisplayAlert("Cancelar reserva",
            $"Cancelar a reserva de {reserva.Hospede}?", "Sim", "Não");
        if (!confirmar) return;

        Reservas.Lista.Remove(reserva);
        Atualizar();
    }
}
