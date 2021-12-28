using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LakDeranaHotel.View
{
    public partial class Splash : Form
    {
        int stratPoint = 1;
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            stratPoint += 1;
            ProgressIndicator1.Start();
            if (stratPoint > 30)
            {
                Login login = new Login();
                ProgressIndicator1.Stop();
                timer1.Stop();
                this.Hide();
                login.Show();
            }
        }
    }
}
