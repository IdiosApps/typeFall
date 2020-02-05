using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace typeFall
{
    public partial class HomeForm : Form
    {
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
            firstButton.Text += "a";
        }
    }
}
