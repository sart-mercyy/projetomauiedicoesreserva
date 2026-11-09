namespace MauiAppHotel.Views;

public partial class HospedagemContratada : ContentPage
{
    public HospedagemContratada()
    {
        InitializeComponent();
    }

    private async void OnNovaReservaClicked(object? sender, EventArgs e)
        => await Navigation.PopAsync();

    private async void OnVerReservasClicked(object? sender, EventArgs e)
        => await Navigation.PushAsync(new ListaReservas());
}
