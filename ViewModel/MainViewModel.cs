using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Tic_tac_Toe_WPF.Commands;
using TicTacToeLibrary;
using TicTacToeLibrary.Enum;
using TicTacToeLibrary.Models;

namespace Tic_tac_Toe_WPF.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        GameLogic gm = new();
        private Symbol _currentPlayer = Symbol.X;
        private string[] _gameGrid = new string[9] { "", "", "", "", "", "", "", "", "", };
        private readonly Player _player1 = new();
        private readonly Player _player2 = new();
        private int _counterClick = 0;
        private bool _tie = false;
        public event PropertyChangedEventHandler? PropertyChanged;
        public DelegateCommand FillCellCommand { get; }

        public MainViewModel()
        {
            FillCellCommand = new DelegateCommand(FillCell);
            _player1.Symbol = Symbol.X;
            _player2.Symbol = Symbol.O;
        }

        private void SetNextPlayer()
        {
            if (_currentPlayer == Symbol.X)
            {
                if (gm.IterativeCheckWinner(_player1))
                {
                    PlayerOneWin++;
                    OpenEndGameBox();
                }
                else
                {
                    _counterClick++;
                    _currentPlayer = Symbol.O;
                    OnPropertyChanged(nameof(CurrentPlayer));
                    OnPropertyChanged(nameof(CounterClick));
                }
            }
            else
            {
                if (gm.IterativeCheckWinner(_player2))
                {
                    PlayerTwoWin++;
                    OpenEndGameBox();
                }
                else
                {
                    _counterClick++;
                    _currentPlayer = Symbol.X;
                    OnPropertyChanged(nameof(CurrentPlayer));
                    OnPropertyChanged(nameof(CounterClick));
                }
            }
        }
        public Symbol CurrentPlayer
        {
            get { return _currentPlayer; }
        }
        public string[] GameGrid
        {

            get { return _gameGrid; }
            set { _gameGrid = value; }
        }
        public int CounterClick
        {
            get { return _counterClick; }
        }

        public int PlayerOneWin
        {
            get
            {
                return _player1.CounterWin;
            }
            set
            {
                _player1.CounterWin = value;
            }
        }

        public int PlayerTwoWin
        {
            get
            {
                return _player2.CounterWin;
            }
            set
            {
                _player2.CounterWin = value;
            }
        }

        public void FillCell(object? o)
        {
            int row = int.Parse(o!.ToString()!.Split(",")[0]);
            int col = int.Parse(o!.ToString()!.Split(",")[1]);
            var index = col + (row * 3);
            try
            {
                gm.Grid.InsertSymbol(CurrentPlayer, row, col);
                GameGrid[index] = CurrentPlayer.ToString();
                OnPropertyChanged(nameof(GameGrid));
                SetNextPlayer();
                if (_counterClick == 9)
                {
                    _tie = true;
                    OpenEndGameBox();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); ;
            }
        }

        public void ResetGame()
        {
            _gameGrid = new string[9] { "", "", "", "", "", "", "", "", "", };
            _counterClick = 0;
            _currentPlayer = Symbol.X;
            gm.Grid.ResetGrid();
            OnPropertyChanged(nameof(GameGrid));
            OnPropertyChanged(nameof(PlayerOneWin));
            OnPropertyChanged(nameof(PlayerTwoWin));
            OnPropertyChanged(nameof(CounterClick));
            OnPropertyChanged(nameof(CurrentPlayer));
        }

        public void OpenEndGameBox()
        {

            var result = MessageBox.Show($"{(_tie ? "Tie" : ("Congratulations won " + $"{CurrentPlayer}"))}\nDo you want to play again?", $"{(_tie ? "Tie" : "Win")}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ResetGame();
                    _tie = false;
                    break;
                case MessageBoxResult.No:
                    CloseGame();
                    break;
            }
        }

        public static void CloseGame()
        {
            App.Current.Shutdown();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
