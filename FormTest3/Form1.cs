using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTest3
{
    public partial class Form1 : Form
    {
        Label
            firstClicked = null,
            secondClicked = null;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        // used for choosing random icons for the squares
        Random random = new Random();

        // each of these is an interesting webdings font, each icon appears twice
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // assign each icon from the list to a square
        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // stop the timer
            timer1.Stop();

            // hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // reset both so we know when it's the first click
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            // go through all labels to check if matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // if the loop didnt return the user won
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

        private void label_Click(object sender, EventArgs e)
        {
            // ignore clicks if the timer is running
            if (timer1.Enabled)
                return;

            Label clickedLabel = sender as Label;

            // if the player has clicked twice the game has not reset so ignore the click
            if (secondClicked != null)
                return;

            if (clickedLabel != null)
            {
                // ignore the click as it's already been revealed
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // handle first one in pair
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    clickedLabel.ForeColor = Color.Black;
                    return;
                }

                // if the player is here this must be the second click so change its color and start the clock
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // check for a winner
                CheckForWinner();

                // if there are two matching icons, keep them black and reset the label pointers
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // if they are here that means two non pairs are up so start the clock
                timer1.Start();
            }
        }
    }
}
