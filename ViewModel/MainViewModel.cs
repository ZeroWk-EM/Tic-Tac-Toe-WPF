using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
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
        private string[] _gameGrid = new string[9];
        private readonly Player _player1 = new();
        private readonly Player _player2 = new();
        private int _counterClick = 0;
        public event PropertyChangedEventHandler? PropertyChanged;
        public DelegateCommand FillCellCommand { get; }

        public MainViewModel()
        {
            FillCellCommand = new DelegateCommand(FillCell, IsFillebleCell);
            _player1.Symbol = Symbol.X;
            _player2.Symbol = Symbol.O;
        }

        private void SetNextPlayer()
        {
            _counterClick++;
            if (_counterClick != 9)
            {
                if (_currentPlayer == Symbol.X)
                {
                    if (gm.IterativeCheckWinner(_player1))
                    {
                        MessageBox.Show($"Ha Vinto {_currentPlayer}", "VITTORIA");
                        App.Current.Shutdown();
                    }
                    _currentPlayer = Symbol.O;
                    OnPropertyChanged(nameof(CurrentPlayer));
                }
                else
                {
                    if (gm.IterativeCheckWinner(_player2))
                    {
                        MessageBox.Show($"Ha Vinto {_currentPlayer}", "VITTORIA");
                        App.Current.Shutdown();
                    }
                    _currentPlayer = Symbol.X;
                    OnPropertyChanged(nameof(CurrentPlayer));
                }
            }
            else
            {
                MessageBox.Show($"E' stato raggiunto un pareggio", "PARITA'");
                App.Current.Shutdown();
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

        public void FillCell(object? o)
        {
            int row = int.Parse(o!.ToString()!.Split(",")[0]);
            int col = int.Parse(o!.ToString()!.Split(",")[1]);
            try
            {
                gm.Grid.InsertSymbol(CurrentPlayer, row, col);

                var index = col + (row * 3);
                GameGrid[index] = CurrentPlayer.ToString();
                OnPropertyChanged(nameof(GameGrid));

                SetNextPlayer();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Errore");
            }
        }


        public bool IsFillebleCell(object? o)
        {
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
