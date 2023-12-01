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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\EmpDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            this.Hide();
            log.ShowDialog();
            clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string conpass = textBox3.Text;


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try 
            {
                Con.Open();
                string Query = "SELECT COUNT(*) FROM Login WHERE uname = @Username";
                using (SqlCommand cmd = new SqlCommand(Query, Con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int existingUserCount = (int)cmd.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        MessageBox.Show("Username already exists. Choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Login (uname, pass, ConPass ) VALUES (@Username, @Password, @ConPass)";
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, Con))
                {
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Password", password);
                    insertCommand.Parameters.AddWithValue("@ConPass", conpass);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the textboxes after successful registration
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
           
        }
    }
}
