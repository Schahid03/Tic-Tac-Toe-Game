namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {

        public enum Player
        {
            X, O
        }

        Player currentPlayer;
        Random random = new Random();
        int playerWinCount = 0;
        int CPUWinCount = 0;
        List<Button> buttons;

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void CPUmove(object sender, EventArgs e)
        {
            if (buttons.Count > 0)
            {
                Button bestMove = GetBestMove(); // Optimalen Zug holen
                if (bestMove != null)
                {
                    bestMove.Text = "O";
                    bestMove.Enabled = false;
                    bestMove.BackColor = Color.DarkSalmon;
                    buttons.Remove(bestMove); // Zug entfernen
                    CheckGame(); // Spielstand prüfen
                }
                CPUTimer.Stop();
            }
        }


        private void PLayerClickButton(object sender, EventArgs e)
        {
            var button = (Button)sender;

            currentPlayer = Player.X;
            button.Text = currentPlayer.ToString();
            button.Enabled = false;
            button.BackColor = Color.Cyan;
            buttons.Remove(button);
            CheckGame();
            CPUTimer.Start();
        }

        private void RestartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void CheckGame()
        {
            if ((button1.Text == "X" && button2.Text == "X" && button3.Text == "X") ||
                (button4.Text == "X" && button5.Text == "X" && button6.Text == "X") ||
                (button7.Text == "X" && button8.Text == "X" && button9.Text == "X") ||
                (button1.Text == "X" && button4.Text == "X" && button7.Text == "X") ||
                (button2.Text == "X" && button5.Text == "X" && button8.Text == "X") ||
                (button3.Text == "X" && button6.Text == "X" && button9.Text == "X") ||
                (button1.Text == "X" && button5.Text == "X" && button9.Text == "X") ||
                (button3.Text == "X" && button5.Text == "X" && button7.Text == "X"))
            {
                CPUTimer.Stop();
                MessageBox.Show("Player Wins!");
                playerWinCount++;
                label1.Text = "Player Wins: " + playerWinCount;
                RestartGame();
                
            }

            else if ((button1.Text == "O" && button2.Text == "O" && button3.Text == "O") ||
                (button4.Text == "O" && button5.Text == "O" && button6.Text == "O") ||
                (button7.Text == "O" && button8.Text == "O" && button9.Text == "O") ||
                (button1.Text == "O" && button4.Text == "O" && button7.Text == "O") ||
                (button2.Text == "O" && button5.Text == "O" && button8.Text == "O") ||
                (button3.Text == "O" && button6.Text == "O" && button9.Text == "O") ||
                (button1.Text == "O" && button5.Text == "O" && button9.Text == "O") ||
                (button3.Text == "O" && button5.Text == "O" && button7.Text == "O"))
            {
                CPUTimer.Stop();
                MessageBox.Show("CPU Wins!");
                CPUWinCount++;
                label2.Text = "CPU Wins: " + CPUWinCount;
                RestartGame();
                
            }
            // Prüfen, ob ein Unentschieden vorliegt
            if (buttons.Count == 0)
            {
                MessageBox.Show("Unentschieden!");
                RestartGame();
            }
        }

        private void RestartGame()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 }; 

            foreach (Button x in buttons) 
            {
                x.Enabled = true;
                x.Text = "?";
                x.BackColor = DefaultBackColor;
            }

        }

        // Änderung:
        private int Minimax(bool isMaximizing)
        {
            // 1. Bewertung des Spiels
            if (IsWinner("O")) return 10; // CPU gewinnt
            if (IsWinner("X")) return -10; // Spieler gewinnt
            if (buttons.All(b => b.Text != "?")) return 0; // Unentschieden

            // 2. Maximierer (CPU)
            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                foreach (Button btn in buttons.Where(b => b.Text == "?"))
                {
                    btn.Text = "O"; // Simuliere CPU-Zug
                    int score = Minimax(false); // Simuliere nächsten Zug
                    btn.Text = "?"; // Rückgängig machen
                    bestScore = Math.Max(score, bestScore);
                }
                return bestScore;
            }
            else // Minimierer (Spieler)
            {
                int bestScore = int.MaxValue;
                foreach (Button btn in buttons.Where(b => b.Text == "?"))
                {
                    btn.Text = "X"; // Simuliere Spielerzug
                    int score = Minimax(true); // Simuliere nächsten Zug
                    btn.Text = "?"; // Rückgängig machen
                    bestScore = Math.Min(score, bestScore);
                }
                return bestScore;
            }
        }

        private Button GetBestMove()
        {
            Button bestMove = null;
            int bestScore = int.MinValue;

            // Gehe alle verfügbaren Züge durch
            foreach (Button btn in buttons.Where(b => b.Text == "?"))
            {
                btn.Text = "O"; // Simuliere CPU-Zug
                int score = Minimax(false); // Bewerte diesen Zug
                btn.Text = "?"; // Rückgängig machen

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = btn;
                }
            }

            return bestMove;
        }

        // Hilfsmethode zur Überprüfung, ob ein Spieler gewonnen hat
        private bool IsWinner(string player)
        {
            return ((button1.Text == player && button2.Text == player && button3.Text == player) ||
                    (button4.Text == player && button5.Text == player && button6.Text == player) ||
                    (button7.Text == player && button8.Text == player && button9.Text == player) ||
                    (button1.Text == player && button4.Text == player && button7.Text == player) ||
                    (button2.Text == player && button5.Text == player && button8.Text == player) ||
                    (button3.Text == player && button6.Text == player && button9.Text == player) ||
                    (button1.Text == player && button5.Text == player && button9.Text == player) ||
                    (button3.Text == player && button5.Text == player && button7.Text == player));
        }


    }
}
// Bisherige Änderungen: - CPUTimer auf 10 reduziert damit die CPU schneller ihren Zug ausführt
//                       - Prüfen ob ein Unentschieden vorliegt
//                       - CPU spielt perfekt und maximal Unentschieden ansosnten Gewinn