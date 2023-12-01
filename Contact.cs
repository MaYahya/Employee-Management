using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Management
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nametxt.Text =="" || msgtxt.Text == "" || mailtxt.Text=="")
            {
                MessageBox.Show("Both textboxes must be filled out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Succesfully Sent","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Salary sal = new Salary();
            sal.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
