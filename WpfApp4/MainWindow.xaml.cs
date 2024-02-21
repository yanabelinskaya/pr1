using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        Random random = new Random();
        Button[] buttons;
        bool isPlayerTurn = true;
        int turnCount = 0;
        bool gameStarted = false; // Переменная, указывающая, началась ли игра

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!gameStarted) return; // Если игра еще не началась, не обрабатываем нажатия кнопок крестиков-ноликов

            Button clickedButton = (Button)sender;
            if (clickedButton.Content == null)
            {
                if (isPlayerTurn)
                {
                    clickedButton.Content = "X";
                }
                else
                {
                    clickedButton.Content = "O";
                }
                clickedButton.IsEnabled = false;
                turnCount++;

                if (CheckForWin(clickedButton.Content.ToString()))
                {
                    MessageBox.Show($"{clickedButton.Content} выиграл!");
                    ResetGame();
                }
                else if (turnCount == 9)
                {
                    MessageBox.Show("Ничья!");
                    ResetGame();
                }
                else
                {
                    isPlayerTurn = !isPlayerTurn;
                    if (!isPlayerTurn)
                    {
                        Computer();
                    }
                }
            }
        }

        private void Computer()
        {
            int randomIndex = random.Next(0, 9);
            while (buttons[randomIndex].Content != null)
            {
                randomIndex = random.Next(0, 9);
            }

            buttons[randomIndex].Content = "O";
            buttons[randomIndex].IsEnabled = false;
            turnCount++;

            if (CheckForWin("O"))
            {
                MessageBox.Show("Выиграл компьютер!");
                ResetGame();
            }
            else if (turnCount == 9)
            {
                MessageBox.Show("Ничья!");
                ResetGame();
            }
            else
            {
                isPlayerTurn = !isPlayerTurn;
            }
        }

        private bool CheckForWin(string symbol)
        {
            // Проверка горизонтальных и вертикальных комбинаций 
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i].Content?.ToString() == symbol && buttons[i + 3].Content?.ToString() == symbol && buttons[i + 6].Content?.ToString() == symbol)
                {
                    return true;
                }
                if (buttons[i * 3].Content?.ToString() == symbol && buttons[i * 3 + 1].Content?.ToString() == symbol && buttons[i * 3 + 2].Content?.ToString() == symbol)
                {
                    return true;
                }
            }

            // Проверка диагональных комбинаций 
            if (buttons[0].Content?.ToString() == symbol && buttons[4].Content?.ToString() == symbol && buttons[8].Content?.ToString() == symbol)
            {
                return true;
            }
            if (buttons[2].Content?.ToString() == symbol && buttons[4].Content?.ToString() == symbol && buttons[6].Content?.ToString() == symbol)
            {
                return true;
            }

            return false;
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = null;
                button.IsEnabled = false; // Блокируем кнопки после окончания игры
            }
            isPlayerTurn = true;
            turnCount = 0;
            gameStarted = false; // Сбрасываем переменную, указывающую, началась ли игра
        }

        private void newbutton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            gameStarted = true; // Устанавливаем переменную, указывающую, что игра началась
            foreach (Button button in buttons)
            {
                button.IsEnabled = true; // Разблокируем кнопки для нажатия после начала новой игры
            }
        }
    }
}