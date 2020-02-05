using System;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace typeFall
{
    public partial class HomeForm : Form
    {
        private const int gravity = 2;

        private Random random = new Random();
        public HomeForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const float framerate = 60;
            var timer = new Timer();
            timer.Tick += TimerTickProcessor;
            timer.Interval = (int) (1000f / framerate);
            timer.Start();
        }

        private void TimerTickProcessor(object sender, EventArgs e)
        {
            setRandomText();
            updatePositions();
        }

        private void updatePositions()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                var pos = button.Location;
                pos.Offset(0, gravity);
                button.Location = pos;
            }
        }

        private void setRandomText()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                if (button.Text.Any()) 
                    continue;

                var number = random.Next(0, 256); // 0-255, 00-FF
                button.Text = number.ToString();
            }
        }
    }
}
