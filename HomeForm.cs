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
        private const int gravity = 2;
        const float framerate = 60;
        private readonly Random random = new Random();
        private Timer timer = new Timer();
        private int difficultyLevel = 1;
        private int totalKills = 0;
        private int framesToNextBlock = 0;
        private int maxBlocksOnScreen = 3;
        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeFormLoad(object sender, EventArgs e)
        {
            timer.Tick += update;
            timer.Interval = (int) (1000f / framerate);
            timer.Start();
        }

        private void update(object sender, EventArgs e)
        {
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

        private void makeNewBlock()
        {
            var newButton = new Button();

            var widthPercent = getRandomProbability(20, 30);
            newButton.Width = (int)(Width * widthPercent);
            newButton.Height = (int)(Height * getRandomProbability(5, 10));

            var x = getConstrainedX(widthPercent);
            newButton.Location = new Point(x, 0);

            setRandomText(newButton);

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

        private void setRandomText(Button button)
        {
            var number = random.Next(0, 256); // 0-255, 00-FF
            button.Text = number.ToString();
        }
    }
}