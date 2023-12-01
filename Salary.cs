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

    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
            GetEmpId();
            ShowSalary();
            textBox1.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            label5.Visible = false;
            textBox4.Visible = false;
            textBox3.Visible = false;
            textBox5.Visible = false; label11.Visible = false;
            label8.Visible = false; textBox8.Visible = false;
            label7.Visible = false;
            label6.Visible = false; label9.Visible = false; label10.Visible = false; textBox6.Visible = false; textBox7.Visible = false;
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\EmpDB.mdf;Integrated Security=True;Connect Timeout=30");


        private void label1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
                dash.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Contact cont = new Contact();
            cont.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Salary sal = new Salary();
                this.Show();
        }

        private void GetEmpId()
        {
            Con.Open();

            SqlCommand cmd = new SqlCommand("Select * from EmpReg", Con);
            SqlDataReader Rdr = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Columns.Add("EmpId", typeof(int));
            td.Load(Rdr);
            comboBox1.ValueMember = "EmpId";

            comboBox1.DataSource = td;
            comboBox1.SelectedIndex = -1;
            Con.Close();
        }
        private void GetEmpName()
        {
            Con.Open();
            string Query = "Select * from EmpReg where EmpId=" + comboBox1.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox1.Text = dr["EmpName"].ToString();
            }


            Con.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                textBox1.Visible = true;
                label3.Visible = true; textBox2.Visible = true;
                label5.Visible = true; textBox4.Visible = true; textBox3.Visible = true; textBox5.Visible = true;
                label8.Visible = true; textBox8.Visible = true;
                label7.Visible = true; label6.Visible = true; label11.Visible = true;

            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetEmpName();
        }

        private void ShowSalary()
        {
            try
            {
                Con.Open();
                string Query = "Select * from EmpSalaryTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var empdata = new DataSet();
                sda.Fill(empdata);
                DataGridView1.DataSource = empdata.Tables[0];
                DataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close(); // Ensure the connection is closed even in case of an exception.
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex == -1 ||
               textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                int leave = int.Parse(textBox5.Text);
                double bsal = double.Parse(textBox2.Text);
                double ded = bsal - (leave * 800);
                textBox6.Text = ded.ToString();
                double all = double.Parse(textBox3.Text);
                double bonus = double.Parse(textBox4.Text);
                double tax = double.Parse(textBox8.Text);
                double net = (ded + all + bonus);
                double taxam = net * tax / 100;
                double dedect = taxam + (leave * 800);
                double netsalary = net - taxam;
                textBox7.Text = net.ToString();
                
                textBox6.Visible = true; textBox7.Visible = true; label9.Visible = true; label10.Visible = true; 
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmpSalaryTbl(EmpId,EmpName,EmpBSal,EmpAllow,EmpBonus,EmpAtt,EmpTax,EmpDed,EmpNet) values(@EI,@EN,@EBS,@EAL,@EB,@EAT,@ETX,@ED,@ENET)", Con);
                    cmd.Parameters.AddWithValue("@EI", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EBS", bsal);
                    cmd.Parameters.AddWithValue("@EAL", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EB", textBox4.Text);
                    cmd.Parameters.AddWithValue("@EAT", leave);
                    cmd.Parameters.AddWithValue("@ETX", tax.ToString());
                    cmd.Parameters.AddWithValue("@ED", dedect.ToString());
                    cmd.Parameters.AddWithValue("@ENET", netsalary.ToString());


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration Success");
                    Con.Close();
                    ShowSalary();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex == -1
               || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please Insert Information");
            }

            else
            {
                int leave = int.Parse(textBox5.Text);
                double bsal = double.Parse(textBox2.Text);
                double ded = bsal - (leave * 800);
                textBox6.Text = ded.ToString();
                double all = double.Parse(textBox3.Text);
                double bonus = double.Parse(textBox4.Text);
                double tax = double.Parse(textBox8.Text);
                double net = (ded + all + bonus);
                double taxam = net * tax / 100;
                double dedect = taxam + (leave * 800);
                double netsalary = net - taxam;
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmpSalaryTbl set EmpId=@EI,EmpName=@EN,EmpBSal=@EBS,EmpAllow=@EAL,EmpBonus=@EB,EmpAtt=@EAT,EmpTax=@ETX,EmpDed=@ED,EmpNet=@ENET where SalaryId=@EmpSalKey", Con);
                    cmd.Parameters.AddWithValue("@EI", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EBS", bsal);
                    cmd.Parameters.AddWithValue("@EAL", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EB", textBox4.Text);
                    cmd.Parameters.AddWithValue("@EAT", leave);
                    cmd.Parameters.AddWithValue("@ETX", tax.ToString());
                    cmd.Parameters.AddWithValue("@ED", dedect.ToString());
                    cmd.Parameters.AddWithValue("@ENET", netsalary.ToString());
                    cmd.Parameters.AddWithValue("@EmpSalKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Edited");
                    Con.Close();
                    ShowSalary();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmpSalaryTbl where SalaryId=@EmpSalKey", Con);
                    cmd.Parameters.AddWithValue("@EmpSalKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted");
                    Con.Close();
                    ShowSalary();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear(); textBox7.Clear();
            textBox5.Clear(); textBox8.Clear();
            comboBox1.SelectedIndex = -1;
            Key = 0;


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("SLIATE-ATI", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Red, new Point(220, 80));
            e.Graphics.DrawString("BATTICALOA", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Red, new Point(220, 100));
            e.Graphics.DrawString("SALARY MANAGEMENT SYSTEM 2.0 ", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Red, new Point(160, 750));

            if (DataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = DataGridView1.SelectedRows[0];
                string EmpId = selectedRow.Cells["EmpId"].Value.ToString();
                string EmpName = selectedRow.Cells["EmpName"].Value.ToString();
                string EmpSal = selectedRow.Cells["EmpBSal"].Value.ToString();
                string Allowance = selectedRow.Cells["EmpAllow"].Value.ToString();
                string bonus = selectedRow.Cells["EmpBonus"].Value.ToString();
                string Leave = selectedRow.Cells["EmpAtt"].Value.ToString();
                string Tax = selectedRow.Cells["EmpTax"].Value.ToString();
                string ded = selectedRow.Cells["EmpDed"].Value.ToString();
                string net = selectedRow.Cells["EmpNet"].Value.ToString();
                


                            string[] headers = {
                    "--------------------------------------------------------",
                    "Employee Id",
                    "Employee Name",
                    "Basic Salary",
                    "Total Allowance",
                    "Total Bonus",
                    "Total Leave",
                    "Tax Percentage",
                    "Total Deduction",
                    "--------------------------------------------------------",
                    "Net Salary",
        
                };

                            string[] data = {
                    "----------",
                    EmpId,
                    EmpName,
                    EmpSal,
                    Allowance,
                    bonus,
                    Leave,
                    Tax,
                    ded,
                    "---------",
                    net

                };




                int yPos = 250;

                for (int i = 0; i < headers.Length; i++)
                {
                    // Draw header
                    e.Graphics.DrawString(headers[i], new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Blue, new Point(50, yPos));
                    yPos += 30;

                }
                int xPos = 250;
                for (int i = 0; i < data.Length; i++)
                {
                    // Draw header
                    e.Graphics.DrawString(data[i], new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Blue, new Point(300, xPos));
                    xPos += 30;

                }

            }

        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridView1.Rows[e.RowIndex].Selected = true;
                DataGridView1.Refresh();

                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Letter", 500, 800);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = DataGridView1.Rows[e.RowIndex];


                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                comboBox1.SelectedItem = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[3].Value.ToString();
                textBox3.Text = selectedRow.Cells[4].Value.ToString();
                textBox4.Text = selectedRow.Cells[5].Value.ToString();
                textBox5.Text = selectedRow.Cells[6].Value.ToString();
                textBox8.Text = selectedRow.Cells[7].Value.ToString();
                textBox6.Text = selectedRow.Cells[8].Value.ToString();
                textBox7.Text = selectedRow.Cells[9].Value.ToString();
                //textBox7.Text = selectedRow.Cells[8].Value.ToString();
                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
