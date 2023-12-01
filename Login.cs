using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Management
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
     
            
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\EmpDB.mdf;Integrated Security=True;Connect Timeout=30");


        private void button2_Click(object sender, EventArgs e)
        {
            User user = new User();
            this.Hide();
            user.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Con.Open();
                string query = "SELECT COUNT(*) FROM Login WHERE uname = @uname AND pass = @pass";
                using (SqlCommand command = new SqlCommand(query, Con))
                {
                    command.Parameters.AddWithValue("@uname", username);
                    command.Parameters.AddWithValue("@pass", password);

                    int userCount = (int)command.ExecuteScalar();

                    if (userCount > 0)
                    {
                        Dashboard dash = new Dashboard();
                        this.Hide();
                        dash.ShowDialog();

                        textBox1.Clear();
                        textBox2.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
