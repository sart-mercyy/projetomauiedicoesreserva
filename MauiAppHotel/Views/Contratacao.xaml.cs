using System.Globalization;
using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class Contratacao : ContentPage
{
    private static readonly CultureInfo PtBr = new("pt-BR");

    private readonly bool carregado;
    private int adultos = 1;
    private int criancas = 0;

    public Contratacao()
    {
        InitializeComponent();

        pckQuarto.ItemsSource = Quarto.Catalogo();

        dtpCheckIn.MinimumDate = DateTime.Today;
        dtpCheckIn.Date = DateTime.Today;
        dtpCheckOut.MinimumDate = DateTime.Today.AddDays(1);
        dtpCheckOut.Date = DateTime.Today.AddDays(1);

        carregado = true;
        AtualizarResumo();
    }

    // Prévia sem validação, só para mostrar noites e valor na tela.
    private void AtualizarResumo()
    {
        if (!carregado) return;

        lblAdultos.Text = $"Adultos: {adultos}";
        lblCriancas.Text = $"Crianças: {criancas}";

        int noites = Math.Max((dtpCheckOut.Date.Date - dtpCheckIn.Date.Date).Days, 0);
        if (pckQuarto.SelectedItem is Quarto q)
        {
            double total = noites * q.ValorDiaria * (adultos + 0.5 * criancas);
            lblResumo.Text = $"{noites} noite(s) · {total.ToString("C", PtBr)}";
        }
        else
        {
            lblResumo.Text = $"{noites} noite(s)";
        }
    }

    private void OnAdultosMenos(object? sender, EventArgs e) { if (adultos > 1) adultos--; AtualizarResumo(); }
    private void OnAdultosMais(object? sender, EventArgs e) { if (adultos < 4) adultos++; AtualizarResumo(); }
    private void OnCriancasMenos(object? sender, EventArgs e) { if (criancas > 0) criancas--; AtualizarResumo(); }
    private void OnCriancasMais(object? sender, EventArgs e) { if (criancas < 4) criancas++; AtualizarResumo(); }

    private void OnAlterouValor(object? sender, EventArgs e) => AtualizarResumo();

    private void OnAlterouData(object? sender, DateChangedEventArgs e) => AtualizarResumo();

    // Validação do DatePicker: o check-out sempre fica depois do check-in.
    private void OnCheckInSelecionado(object? sender, DateChangedEventArgs e)
    {
        var minimoSaida = e.NewDate.AddDays(1);
        if (dtpCheckOut.Date < minimoSaida)
            dtpCheckOut.Date = minimoSaida;
        dtpCheckOut.MinimumDate = minimoSaida;
        AtualizarResumo();
    }

    private async void OnContratarClicked(object? sender, EventArgs e)
    {
        try
        {
            // Cada set faz sua própria validação e lança ArgumentException se o valor for inválido.
            var hospedagem = new Hospedagem();
            hospedagem.Hospede = txtNome.Text ?? "";
            hospedagem.Quarto = pckQuarto.SelectedItem as Quarto;
            hospedagem.Adultos = adultos;
            hospedagem.Criancas = criancas;
            hospedagem.CheckIn = dtpCheckIn.Date;
            hospedagem.CheckOut = dtpCheckOut.Date;

            Reservas.Lista.Add(hospedagem);

            // Envia os dados para a próxima tela por meio do BindingContext.
            await Navigation.PushAsync(new HospedagemContratada { BindingContext = hospedagem });
        }
        catch (ArgumentException ex)
        {
            await DisplayAlert("Dados inválidos", ex.Message, "OK");
        }
    }

    private async void OnReservasClicked(object? sender, EventArgs e)
        => await Navigation.PushAsync(new ListaReservas());

    private async void OnSobreClicked(object? sender, EventArgs e)
        => await Navigation.PushAsync(new Sobre());
}
