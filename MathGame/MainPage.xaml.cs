namespace MathGame
{
    public partial class MainPage : ContentPage
    {
        int iLB1 = 0;
        int iLB2 = 0;
        int iLB3 = 0;

        float fR = 0.0f;

        int iAcertouCount = 0;
        int iErrouCount = 0;

        Random rand = new Random();

        public MainPage()
        {
            InitializeComponent();
            GerarJogo();
        }

        private async void btOK_Clicked(object sender, EventArgs e)
        {
            float fResult = 0.0f;

            try
            {
                fResult = Convert.ToSingle(txR.Text);
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("ERRO", "Digite um número válido!", "OK");
                return;
            }

            // Se o usuário acertou a resposta...
            if(fResult == fR)
            {
                iAcertouCount++;
                lbAcertou.Text = $"Acertos: {iAcertouCount}";
                imR.Source = "win.png";
            }
            // Se o usuário errou a resposta...
            else
            {
                iErrouCount++;
                lbErrou.Text = $"Erros: {iErrouCount}";
                imR.Source = "loose.png";
            }

            await Task.Delay(2000);
            imR.Source = "question.png";

            GerarJogo();
        }

        public void GerarJogo()
        {
            iLB1 = rand.Next(1, 10);
            iLB2 = rand.Next(1, 5);
            iLB3 = rand.Next(1, 10);

            lb1.Text = Convert.ToString(iLB1);
            lb3.Text = Convert.ToString(iLB3);

            switch (iLB2)
            {
                case 1:
                    {
                        fR = (iLB1 + iLB3);
                        lb2.Text = "+";
                        break;
                    }
                case 2:
                    {
                        fR = (iLB1 - iLB3);
                        lb2.Text = "-";
                        break;
                    }
                case 3:
                    {
                        fR = (iLB1 * iLB3);
                        lb2.Text = "*";
                        break;
                    }
                case 4:
                    {
                        fR = (Convert.ToSingle(iLB1) / Convert.ToSingle(iLB3));
                        lb2.Text = "÷";
                        break;
                    }
            } // fim do switch
            txR.Text = "";
        } // fim da função GerarJogo()
    }
}
