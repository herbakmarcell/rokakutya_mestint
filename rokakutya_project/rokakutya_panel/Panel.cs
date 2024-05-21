using rokakutya_console.Interfaces;
using rokakutya_console.Solvers;
using rokakutya_console.StateRepresentation;

namespace rokakutya_panel
{
    public partial class Panel : Form
    {
        MiniMax solver;

        FoxCatchingState state;

        public Panel()
        {
            InitializeComponent();

            solver = new MiniMax(new FoxCatchingOperatorGenerator(), 0);

            state = new FoxCatchingState();
            labelResult.Text = "Válassz nehézséget!";
            SyncState();
            DisableButtons();
        }

        private void SyncState()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = Controls.OfType<Button>().FirstOrDefault(
                        b => b.Name == $"button{i}_{j}"
                        );

                    button.Text = state.Board[i, j].ToString();

                    if (state.Board[i, j] != 'F')
                        button.Enabled = false;
                    else
                        button.Enabled = true;
                }
            }
        }

        private void DisableButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = Controls.OfType<Button>().FirstOrDefault(
                       b => b.Name == $"button{i}_{j}"
                       );

                    button.Enabled = false;
                }
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            int difficulty = 0;

            string text = comboBoxDifficulty.Text;

            switch (text)
            {
                case "Könnyû":
                    difficulty = 3;
                    break;

                case "Normál":
                    difficulty = 5;
                    break;

                case "KÖNYÖRTELEN":
                    difficulty = 7;
                    break;

                default:
                    difficulty = 2;
                    break;
            }

            solver.Depth = difficulty;

            state = new FoxCatchingState();

            SyncState();
            selectingCharacter = true;
            labelResult.Text = "";
        }

        private int oldX = -1;
        private int oldY = -1;
        private bool selectingCharacter = true;

        private void UserMoves(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (selectingCharacter)
            {
                oldX = int.Parse(btn.Name.Split('_')[0].Last().ToString());
                oldY = int.Parse(btn.Name.Split('_')[1].Last().ToString());

                selectingCharacter = false;
                EnablePossibleMoves();
                return;
            }

            int x = int.Parse(btn.Name.Split('_')[0].Last().ToString());
            int y = int.Parse(btn.Name.Split('_')[1].Last().ToString());

            FoxCatchingOperator op = new FoxCatchingOperator(oldX, oldY, x, y, 'F');

            if (!op.IsApplicable(state))
            {
                MessageBox.Show("Ez a lépés nem lehetséges ebben az állapotban!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            state = op.Apply(state) as FoxCatchingState;

            SyncState();

            if (CheckStatus())
                return;

            DisableButtons();

            state = solver.NextMove(state) as FoxCatchingState;

            SyncState();

            CheckStatus();

            selectingCharacter = true;
        }

        private bool CheckStatus()
        {
            switch (state.GetStatus())
            {
                case Status.PLAYER1WINS:
                    labelResult.Text = "Elsõ játékos nyert!";
                    DisableButtons();
                    labelResult.ForeColor = Color.Green;
                    return true;

                case Status.PLAYER2WINS:
                    labelResult.Text = "Második játékos nyert!";
                    DisableButtons();
                    labelResult.ForeColor = Color.Red;
                    return true;
            }

            return false;
        }
        private void EnablePossibleMoves()
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 || j == 0)
                        continue;

                    int newRow = oldX + i;
                    int newColumn = oldY + j;

                    if (newRow >= 0 && newRow < 8 &&
                        newColumn >= 0 && newColumn < 8)
                    {
                        Button button = Controls.OfType<Button>().FirstOrDefault(
                       b => b.Name == $"button{newRow}_{newColumn}"
                       );
                        if (button.Text != "D")
                        {
                            button.Enabled = true;
                        }
                    }
                }
            }
        }
    }
}
