using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace typeFall
{
    public partial class HomeForm : Form
    {
        private const int gravity = 1;
        private const float framerate = 60;
        private readonly Random random = new Random();
        private readonly Timer timer = new Timer();
        private int killsInDifficulty;
        private int killsPerLevel = 5;
        private int framesToNextBlock;
        private int maxBlocksOnScreen = 3;
        private readonly Dictionary<string, Button> buttonMap = new Dictionary<string, Button>();
        private int totalKills;
        private bool playerIsDead;

        private int difficultyLevel = 1;
        private readonly Dictionary<int, int> difficultyLevels = new Dictionary<int, int>
        {
            {1, 15},
            {2, 40},
            {3, 80},
            {4, 120},
            {5, 160},
            {6, 200},
            {7, 255}
        };

        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeFormLoad(object sender, EventArgs e)
        {
            textBox.BackColor = Color.FromArgb(247, 183, 99);
            textBox.TextAlign = HorizontalAlignment.Center;

            KeyPreview = true;
            showTutorial();
        }

        private void showTutorial()
        {
            var tutorialMessage = "Convert decimal to hexadecimal!\n\n" +
                               "Decimal | Hexadecimal\n" +
                               "         0   |   00\n" +
                               "         7   |   07\n" +
                               "         10  |   0A\n" +
                               "         15  |   0F\n" +
                               "         16  |   10\n" +
                               "         33  |   21\n" +
                               "         50  |   32\n" +
                               "         64  |   40\n" +
                               "         87  |   57\n\n" +
                               "80 = 16 x 5 ... 87 - 80 = 7 ... so 57!\n\n" +
                               "Press any key to begin";

            tutorialText.Text = tutorialMessage;
            tutorialText.Visible = true;
        }

        private void HomeForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!tutorialText.Visible) 
                return;
            
            tutorialText.Visible = false;
            scoreText.Visible = true;
            highscore.Visible = true;

            textBox.Visible = true;
            var x = (Width / 2) - (textBox.Width / 2);
            textBox.Location = new Point(x, Height - (Height / 10));

            timer.Tick += update;
            timer.Interval = (int)(1000f / framerate);
            timer.Start();
            ActiveControl = textBox;
        }
        
        private void restartGame()
        {
            highscore.Text = highscore.Text.Any() ? Math.Max(int.Parse(highscore.Text), totalKills).ToString() : totalKills.ToString();

            totalKills = 0;
            scoreText.Text = totalKills.ToString();

            killsInDifficulty = 0;
            difficultyLevel = 1;
            timer.Stop();

            playerIsDead = false;
            scoreText.Visible = false;
            textBox.Visible = false;
            showTutorial();
        }

        private void update(object sender, EventArgs e)
        {
            checkTextForMatch();

            if (playerHasDied())
                playerIsDead = true;

            if (playerIsDead)
            {
                if (getBlockCount() != 0) // player gets time to remove difficult remaining blocks  (must need more thought as they killed the player)
                {
                    var x = (Width / 2) - (textBox.Width / 2);
                    textBox.Location = new Point(x, Height / 10);
                    return;
                } 


                // all blocks are removed, can play again
                restartGame();
                return;
            }

            tryIncreaseDifficulty();
            updatePositions();

            if (canCreateBlock() && framesToNextBlock <= 0)
            {
                makeNewBlock();
                var next = (int) framerate * random.Next(1, 3);
                framesToNextBlock += next;
            }

            framesToNextBlock--;
        }

        private void tryIncreaseDifficulty()
        {
            if (killsInDifficulty < 5) 
                return;

            difficultyLevel++;
            killsPerLevel += difficultyLevel + 2; // more practice at harder levels
            BackColor = getRandomColor();
            killsInDifficulty = 0;
        }

        private void checkTextForMatch()
        {
            var text = textBox.Text;
            if (!buttonMap.ContainsKey(text))
                return;

            var button = buttonMap[text];
            Controls.Remove(button);
            buttonMap.Remove(text);
            textBox.Text = "";
            killsInDifficulty++;
            totalKills++;
            scoreText.Text = totalKills.ToString();
        }

        private void makeNewBlock()
        {
            var newButton = new Button();

            var widthPercent = getRandomProbability(20, 30);
            newButton.Width = (int)(Width * widthPercent);
            newButton.Height = (int)(Height * getRandomProbability(10, 15));

            var x = getConstrainedX(widthPercent);
            newButton.Location = new Point(x, 0);

            string randomText = getRandomText();
            newButton.Text = randomText;

            newButton.BackColor = getRandomColor(50); // don't let the background be too dark ('.' dark text)
            newButton.FlatStyle = FlatStyle.Flat;

            Font font = new Font("MS Sans Serif", 40, FontStyle.Bold);
            newButton.Font = font;

            // Store the hex of the block and the block in a map, so we can quickly find if the entered text has a match
            var randomInt = int.Parse(randomText);
            string hexString = randomInt.ToString("X2");
            buttonMap.Add(hexString, newButton);

            Controls.Add(newButton);
        }

        private bool canCreateBlock()
        {
            return getBlockCount() < maxBlocksOnScreen;
        }

        private int getBlockCount()
        {
            return Controls.OfType<Button>().Count() ;
        }

        private int getConstrainedX(float widthPercent)
        {
            var maxX = Width * (1 - widthPercent);
            var randomX = Width * getRandomProbability();
            return (int) Math.Min(maxX, randomX); // constrain x so that button is fully on-screen
        }

        private bool playerHasDied()
        {
            var bottomCount = Controls
                .OfType<Button>()
                .Count(button => (button.Location.Y + button.Height) > ClientRectangle.Height); // ClientRectangle accounts for app bar height

            return bottomCount > 0;
        }

        private float getRandomProbability(int start = 0, int end = 100)
        {
            return random.Next(start, end) / 100f;
        }
        
        private Color getRandomColor(int min = 0)
        {
            return Color.FromArgb(random.Next(min, 255), random.Next(min, 255), random.Next(min, 255));
        }

        private void updatePositions()
        {
            foreach (var button in Controls.OfType<Button>())
            {
                var pos = button.Location;
                pos.Offset(0, gravity);
                button.Location = pos;
            }
        }

        private string getRandomText()
        {
            var difficultyMaxRandom = difficultyLevels[difficultyLevel];
            var number = random.Next(0, difficultyMaxRandom); // 0-255, 00-FF;
            while (buttonMap.ContainsKey(number.ToString("X2")))
            {
                number = random.Next(0, difficultyMaxRandom);
            }

            return number.ToString();
        }
    }
}