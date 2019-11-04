using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hangman_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Paint += Form1_DrawingHangman;
            //Load += Form1_Load;
        }

        string[] words = {
        "active",
        "forum",
        "participation",
        "reward",
        "ratings"
        };

        Random r = new Random();
        Button[] alphabetButtons;

        List<Label> labels = new List<Label>();
        bool ignore;

        int stage = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            alphabetButtons = this.Controls.OfType<Button>().Except(new Button[] { ButtonNew }).ToArray();
            Array.ForEach(alphabetButtons, b => b.Click += btn_click);
            ButtonNew.PerformClick();
        }

        private void btn_click(object sender, EventArgs e)
        {
            if (ignore)
                return;
            Button b = (Button)sender;
            b.Enabled = false;

            Array.ForEach(labels.ToArray(), lbl => lbl.Text = lbl.Tag.ToString() == b.Text ? b.Text : lbl.Text);

            for (int x = 1; x <= labels.Count - 1; x++)
            {
                if (labels[x].Location.Y == labels[x - 1].Location.Y)
                {
                    labels[x].Left = labels[x - 1].Right;
                }
            }

            stage += !labels.Any(lbl => lbl.Text == b.Text) ? 1 : 0;
            ignore = labels.All(lbl => lbl.Text != " ") || stage == 10;

            this.Invalidate();
        }

        private void Form1_DrawingHangman(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (stage >= 1)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 85, 190, 210, 190);
            }
            if (stage >= 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 148, 190, 148, 50);
            }
            if (stage >= 3)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 148, 50, 198, 50);
            }
            if (stage >= 4)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 50, 198, 70);
            }
            if (stage >= 5)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 2), new Rectangle(188, 70, 20, 20));
            }
            if (stage >= 6)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 90, 198, 130);
            }
            if (stage >= 7)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 95, 183, 115);
            }
            if (stage >= 8)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 95, 213, 115);
            }
            if (stage >= 9)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 130, 183, 170);
            }
            if (stage >= 10)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2), 198, 130, 213, 170);
            }
        }

        private void ButtonNew_Click(object sender, EventArgs e)
        {

            Array.ForEach(this.Controls.OfType<Label>().ToArray(), lbl => lbl.Dispose());
            Array.ForEach(alphabetButtons, b => b.Enabled = true);
            labels = new List<Label>();
            int startX = 14;
            int startY = 250;
            CreationOfLabelForWords(startX, startY);
            CreationOfLabelForWords(startX, startY + 75);
            CreationOfLabelForWords(startX, startY + 150);


            ignore = false;
            stage = 0;
            this.Invalidate();
        }

        private string GenerateWordFromListOfWords()
        {
            string word = words[r.Next(0, words.Length)].ToUpper();
            return word;
        }

        private void CreationOfLabelForWords(int startX, int startY)
        {
            foreach (char c in GenerateWordFromListOfWords())
            {
                Label lbl = new Label();
                lbl.Text = " ";
                lbl.Font = new Font(this.Font.Name, 35, FontStyle.Underline);
                lbl.Location = new Point(startX, startY);
                lbl.Tag = c.ToString();
                lbl.AutoSize = true;
                this.Controls.Add(lbl);
                labels.Add(lbl);
                startX = lbl.Right;
            }
        }
    }
}
