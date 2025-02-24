﻿using Assignment2.Model;
using System.Windows;

namespace Assignment2.View
{
    public partial class GameWindow : Window
    {
        private GameManager gameManager;
        public GameWindow()
        {
            InitializeComponent();
        }

        // Event handler for the "New Game" button click
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the SetUpGameDialog to collect player names and types
            SetUpGameDialog setUpDialog = new SetUpGameDialog();
            bool? result = setUpDialog.ShowDialog();  // Show dialog and wait for result

            if (result == true)  // If OK was clicked (DialogResult = true)
            {
                // Retrieve player names and player types
                string player1Name = setUpDialog.Player1Name;
                string player2Name = setUpDialog.Player2Name;
                bool isPlayer2Computer = setUpDialog.IsPlayer2Computer;

                // Update the status section with the player names
                Player1Label.Text = "Player 1: " + player1Name;
                Player2Label.Text = "Player 2: " + player2Name;

                // Set the score to 0
                Player1Score.Text = "Black Disks: 0";
                Player2Score.Text = "White Disks: 0";

                // Create an 8x8 board and initialize it with empty disks
                Disk[,] boardState = new Disk[8, 8];

                // Initialize the pieces in the starting configuration
                boardState[3, 3] = Disk.White;
                boardState[3, 4] = Disk.Black;
                boardState[4, 3] = Disk.Black;
                boardState[4, 4] = Disk.White;

                // Update the board with the initial state
                GameGridControl.UpdateBoard(boardState);  // Call UpdateBoard on the GameGridControl


                /* Creates 3 instances for the if statement. player 1 is always human 
                   so i always creater player1 as HumanPlayer and the if determines if it's computer or human.*/
                Player player1 = new HumanPlayer(player1Name, Disk.White);
                Player player2;
                if (isPlayer2Computer)
                {
                    player2 = new ComputerPlayer(player2Name, Disk.Black);
                }
                else
                {
                    player2 = new HumanPlayer(player2Name, Disk.Black);
                }


                // Create the GameManager instance.
                GameManager game = new GameManager(player1, player2);
                game.StartGame(GameGridControl);

            }
            else
            {
                // If the user cancels the setup
                MessageBox.Show("Game setup was canceled.", "Canceled");
            }
        }

        // Event handler for the "Exit Game" button click
        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }
    }
}
