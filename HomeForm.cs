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
        private int totalKills;
        private int framesToNextBlock;
        private int maxBlocksOnScreen = 3;
        private readonly Dictionary<string, Button> buttonMap = new Dictionary<string, Button>();

        private int difficultyLevel = 1;
        private readonly Dictionary<int, int> difficultyLevels = new Dictionary<int, int>
        {
            {1, 15},
            {2, 40},
            {3, 80},
            {4, 150},
            {5, 255}
        };
        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeFormLoad(object sender, EventArgs e)
        {
            textBox.BackColor = Color.FromArgb(247, 183, 99);
            textBox.TextAlign = HorizontalAlignment.Center;

            ActiveControl = textBox;

            timer.Tick += update;
            timer.Interval = (int) (1000f / framerate);
            timer.Start();
        }

        private void update(object sender, EventArgs e)
        {
            checkTextForMatch();
            tryIncreaseDifficulty();

            updatePositions();

            if (canCreateBlock() && framesToNextBlock <= 0)
            {
                makeNewBlock();
                var next = (int) framerate * random.Next(1, 3);
                framesToNextBlock += next;
            }

            if (playerIsDead())
            {
                timer.Stop();
            }

            framesToNextBlock--;
        }

        private void tryIncreaseDifficulty()
        {
            if (totalKills % 5 == 0 && totalKills > 0)
                difficultyLevel++;
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
            totalKills++;
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

            newButton.BackColor = Color.FromArgb(96, 147, 172);
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
            return Controls.OfType<Button>().Count() < maxBlocksOnScreen;
        }

        private int getConstrainedX(float widthPercent)
        {
            var maxX = Width * (1 - widthPercent);
            var randomX = Width * getRandomProbability();
            return (int) Math.Min(maxX, randomX); // constrain x so that button is fully on-screen
        }

        private bool playerIsDead()
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
            while (buttonMap.ContainsKey(number.ToString("X")))
            {
                number = random.Next(0, difficultyMaxRandom);
            }

            return number.ToString();
        }
    }
}