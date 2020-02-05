using System;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace typeFall
{
    public partial class HomeForm : Form
    {
        private const int gravity = 10;
        public HomeForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const float framerate = 12;
            var timer = new Timer();
            timer.Tick += TimerTickProcessor;
            timer.Interval = (int) (1000f / framerate);
            timer.Start();
        }

        private void TimerTickProcessor(object sender, EventArgs e)
        {
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

    }
}
