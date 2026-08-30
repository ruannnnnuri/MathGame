namespace MathGame
{
    public partial class MainPage : ContentPage
    {

        int modo = 0;

        Random rand = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Modo_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (rbFacil.IsChecked)
            {
                modo = 0;
            }
            else if (rbMedio.IsChecked)
            {
                modo = 1;
            }
            else if (rbDificil.IsChecked)
            {
                modo = 2;
            }
        }

        private async void btJogar_Clicked(object sender, EventArgs e)
        {
            var jogoPage = new JogoPage(modo);

            // Navegar para a tela do jogo
            await Navigation.PushAsync(jogoPage);
        }
    }
}
