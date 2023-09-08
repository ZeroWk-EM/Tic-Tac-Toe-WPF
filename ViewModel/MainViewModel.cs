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
        private Symbol[]? _gameGrid = null;
        private Player _player1 = new Player();
        private Player _player2 = new Player();
        public event PropertyChangedEventHandler? PropertyChanged;
        public DelegateCommand FillCellCommand { get; }

        public MainViewModel()
        {
            FillCellCommand = new DelegateCommand(FillCell, IsFillebleCell);
            _gameGrid = new Symbol[9];
            _player1.Symbol = Symbol.X;
            _player2.Symbol = Symbol.O;
        }

        private void SetNextPlayer()
        {
            if (_currentPlayer == Symbol.X)
            {
                /* if (gm.IterativeCheckWinner(_player1))
                 {
                     MessageBox.Show($"Ha Vinto {_currentPlayer}");
                     App.Current.Shutdown();
                 }*/
                _currentPlayer = Symbol.O;
            }
            else
            {
                /*  if (gm.IterativeCheckWinner(_player2))
                  {
                      MessageBox.Show($"Ha Vinto {_currentPlayer}");
                      App.Current.Shutdown();
                  }*/
                _currentPlayer = Symbol.X;
            }
        }

        public Symbol[]? GameGrid
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
                gm.Grid.InsertSymbol(_currentPlayer, row, col);

                var index = col + (row * 3);
                if (GameGrid != null)
                {
                    GameGrid[index] = _currentPlayer;
                }
                SetNextPlayer();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Errore");
            }
            {
                OnPropertyChanged();
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
