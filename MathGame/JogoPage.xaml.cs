namespace MathGame
{
    public partial class JogoPage : ContentPage
    {
        int iLB1 = 0;
        int iLB2 = 0;
        int iLB3 = 0;

        float fR = 0.0f;

        int iAcertouCount = 0;
        int iErrouCount = 0;

        int modo = 0;
        int tempoRestante = 30;
        bool timer = true;

        int pontuacao = 0;
        int contagem = 1;

        Random rand = new Random();

        public JogoPage(int modo)
        {
            InitializeComponent();
            this.modo = modo;
            GerarJogo();
            IniciarTimer();
        }

        private async void btOK_Clicked(object sender, EventArgs e)
        {
            float fResult = 0.0f;

            try
            {
                fResult = Convert.ToSingle(txR.Text);
                timer = false;
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("ERRO", "Digite um número válido!", "OK");
                return;
            }

            // Se o usuário acertou a resposta...
            if (fResult == fR)
            {
                switch (modo)
                {
                    case 0:
                        {
                            pontuacao += 10;
                            break;
                        }
                    case 1:
                        {
                            pontuacao += 20;
                            break;
                        }
                    case 2:
                        {
                            pontuacao += 30;
                            break;
                        }
                }
                if (tempoRestante > 10)
                {
                    pontuacao += 5;
                }
                else if (tempoRestante > 20)
                {
                    pontuacao += 10;
                }

                iAcertouCount++;
                lbAcertou.Text = $"Acertos: {iAcertouCount}";
                lbPontuacao.Text = $"Pontuação: {pontuacao}";
                imR.Source = "win.png";
            }
            // Se o usuário errou a resposta...
            else
            {
                iErrouCount++;
                lbErrou.Text = $"Erros: {iErrouCount}";
                lbPontuacao.Text = $"Pontuação: {pontuacao}";
                imR.Source = "loose.png";
            }

            btOK.IsEnabled = false;
            await Task.Delay(2000);
            btOK.IsEnabled = true;

            contagem++;
            if (contagem > 10)
            {
                await DisplayAlertAsync("Fim de Jogo", $"Você acertou {iAcertouCount} questões e errou {iErrouCount} questões.\nSua pontuação final é: {pontuacao}", "OK");
                await Navigation.PopAsync();
                return;
            }

            lbQuestao.Text = $"Questão: {contagem}/10";
            imR.Source = "question.png";
            timer = true;
            GerarJogo();
            IniciarTimer();
        }

        public void GerarJogo()
        {
            if (modo == 0)
            {
                iLB1 = rand.Next(1, 10);
                iLB2 = rand.Next(1, 5);
                iLB3 = rand.Next(1, 10);
            }
            else if (modo == 1)
            {
                iLB1 = rand.Next(1, 50);
                iLB2 = rand.Next(1, 5);
                iLB3 = rand.Next(1, 50);
            }
            else if (modo == 2)
            {
                iLB1 = rand.Next(1, 100);
                iLB2 = rand.Next(1, 5);
                iLB3 = rand.Next(1, 100);
            }

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
            tempoRestante = 30;
            lbTimer.TextColor = Colors.White;
        } // fim da função GerarJogo()

        private async void IniciarTimer()
        {
            while (timer == true)
            {
                lbTimer.Text = $"Tempo: {tempoRestante}";
                await Task.Delay(1000);
                tempoRestante--;
                if (tempoRestante <= 10)
                {
                    lbTimer.TextColor = Colors.Red;

                }
                
                if (tempoRestante < 0)
                {
                    iErrouCount++;
                    lbTimer.Text = "Tempo: Esgotado";
                    lbErrou.Text = $"Erros: {iErrouCount}";
                    imR.Source = "loose.png";
                    await Task.Delay(2000);
                    imR.Source = "question.png";
                    GerarJogo();
                }
            }
        }
    }
}
