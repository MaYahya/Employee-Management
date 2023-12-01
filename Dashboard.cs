using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Employee_Management
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\EmpDB.mdf;Integrated Security=True;Connect Timeout=30");



        private int panelIndex = 50;
        private int labelIndex = 50;
        private int pictureBoxIndex = 50;
        int y = 50;

        private void panelCreate(string empname, string empgen, string empadd, string emppos, string emplvl, string empmob, byte[] empPic)
        {
            Panel panel = new Panel();

            // Set the properties of the Panel.
            panel.Location = new Point(860, y);
            panel.Size = new Size(610, 100);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Anchor = AnchorStyles.Right;
            panel.BackColor = Color.Transparent;
            // Add the Panel to the form.
            flowLayoutPanel1.Controls.Add(panel);

            Label label1 = new Label();

            // Set the properties of the Label.
            label1.Location = new Point(118, 14);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            label1.Size = new Size(140, 25);
            label1.Text = empname;
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;

            // Add the Label to the Panel.
            panel.Controls.Add(label1);

            Label label2 = new Label();

            label2.Location = new Point(297, 20);
            label2.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            label2.Size = new Size(100, 21);
            label2.Text = empgen;
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;

            // Add the Label to the Panel.
            panel.Controls.Add(label2);

            Label label3 = new Label();

            label3.Location = new Point(419, 20);
            label3.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            label3.Size = new Size(170, 21);
            label3.Text = empadd;
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;

            // Add the Label to the Panel.
            panel.Controls.Add(label3);

            Label label4 = new Label();

            label4.Location = new Point(129, 62);
            label4.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            label4.Size = new Size(80, 21);
            label4.Text = emppos;
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;

            // Add the Label to the Panel.
            panel.Controls.Add(label4);

            Label label5 = new Label();

            label5.Location = new Point(267, 62);
            label5.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            label5.Size = new Size(80, 21);
            label5.Text = emplvl;
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;

            // Add the Label to the Panel.
            panel.Controls.Add(label5);

            Label label6 = new Label();

            label6.Location = new Point(419, 62);
            label6.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            label6.Size = new Size(100, 21);
            label6.Text = empmob;
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;

            // Add the Label to the Panel.
            panel.Controls.Add(label6);

            PictureBox pictureBox = new PictureBox();

            // Set the properties of the PictureBox.
            pictureBox.Location = new Point(6, 0);
            pictureBox.Size = new Size(100, 97);
            pictureBox.BackColor = Color.Transparent;
            if (empPic != null && empPic.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(empPic))
                {
                    pictureBox.Image = Image.FromStream(ms);
                }
            }
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Name = "pictureBox" + (pictureBoxIndex + 11).ToString();
            pictureBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            // Add the PictureBox to the Panel.
            panel.Controls.Add(pictureBox);

            y += 69;
            pictureBoxIndex++;
            labelIndex += 6;
            panelIndex++;
           
        }

        string profile;
        private void picbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file name and display it
               profile = openFileDialog.FileName.ToString();
                string fileName = Path.GetFileName(profile);
                piclbl.Text = fileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] images = null;
            FileStream str = new FileStream(profile, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(str);
            images = brs.ReadBytes((int)str.Length);


            if (nametxt.Text == "" || addtxt.Text == "" || mobtxt.Text == "" || postxt.SelectedIndex == -1 ||
              gentxt.SelectedIndex == -1 || lvltxt.SelectedIndex == -1)
            {
                MessageBox.Show("Both textboxes must be filled out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmpReg(EmpName,EmpGen,EmpDOB,EmpAdd,EmpPhn,EmpLevel,EmpApp,EmpPosition,EmpDegree,EmpPic) values(@EN,@EG,@ED,@EA,@EPH,@EL,@EAP,@EPS,@EDG,@EPIC)", Con);
                    cmd.Parameters.AddWithValue("@EN", nametxt.Text);
                    cmd.Parameters.AddWithValue("@EG", gentxt.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", dobtxt.Value.Date);
                    cmd.Parameters.AddWithValue("@EA", addtxt.Text);
                    cmd.Parameters.AddWithValue("@EPH", mobtxt.Text);
                    cmd.Parameters.AddWithValue("@EL", lvltxt.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EAP", Apptxt.Value.Date);
                    cmd.Parameters.AddWithValue("@EPS", postxt.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EDG", degtxt.Text);
                    cmd.Parameters.AddWithValue("@EPIC",images );
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration Success");
                    Con.Close();

                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
           
        }


       void clear()
        {
            degtxt.Clear(); mobtxt.Clear(); addtxt.Clear();  nametxt.Clear();
            postxt.SelectedIndex = -1; lvltxt.SelectedIndex = -1;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            flowLayoutPanel1.Controls.Clear();

            showdata();

        }

        private void showdata()
        {
            try
            {
                Con.Open();

                // Retrieve all user data from the database
                SqlCommand retrieveAllCmd = new SqlCommand("SELECT * FROM EmpReg ORDER BY EmpId DESC", Con);
                SqlDataReader reader = retrieveAllCmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader["EmpName"].ToString();
                    string gen = reader["EmpGen"].ToString();
                    string add = reader["EmpAdd"].ToString();
                    string pos = reader["EmpPosition"].ToString();
                    string level = reader["EmpLevel"].ToString();
                    string mob = reader["EmpPhn"].ToString();
                    byte[] empPict = (byte[])reader["EmpPic"];

                    // Call panelCreate for each user
                    panelCreate(name, gen, add, pos, level, mob, empPict);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            showdata();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Contact contact =new Contact();
            contact.Show();
            this.Hide();

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
