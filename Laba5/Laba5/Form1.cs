using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Laba5
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
       
        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

            sqlConnection.Open();

            SqlDataAdapter dataAd = new SqlDataAdapter("SELECT * FROM Applicants", sqlConnection);

            DataSet DB = new DataSet();

            dataAd.Fill(DB);

            dataGridView2.DataSource = DB.Tables[0];
            dataGridView3.DataSource = DB.Tables[0];
            dataGridView4.DataSource = DB.Tables[0];

            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connection complete!");
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand("INSERT INTO [Applicants] (Name, Surname, Species, Birthday, PhysicsScores, MathScores, RusScores, CompScienceScores, AverageScore, Allowance) VALUES (@Name, @Surname, @Species, @Birthday, @PhysicsScores, @MathScores, @RusScores, @CompScienceScores, @AverageScore, @Allowance)", sqlConnection);

            DateTime date = DateTime.Parse(textBox4.Text);

            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.Parameters.AddWithValue("Surname", textBox2.Text);
            command.Parameters.AddWithValue("Species", textBox3.Text);
            command.Parameters.AddWithValue("Birthday", $"{date.Month}/{date.Day}/{date.Year}");
            command.Parameters.AddWithValue("PhysicsScores", textBox5.Text);
            command.Parameters.AddWithValue("MathScores", textBox6.Text);
            command.Parameters.AddWithValue("RusScores", textBox7.Text);
            command.Parameters.AddWithValue("CompScienceScores", textBox8.Text);
            command.Parameters.AddWithValue("AverageScore", ((float.Parse(textBox5.Text) + float.Parse(textBox6.Text) + float.Parse(textBox7.Text) + float.Parse(textBox8.Text)) / 4));

            if ((int.Parse(textBox5.Text) + int.Parse(textBox6.Text) + int.Parse(textBox7.Text) + int.Parse(textBox8.Text)) >= int.Parse(textBox9.Text))
            {
                command.Parameters.AddWithValue("Allowance", "Допущен в ИжГТУ");
            }
            else
            {
                command.Parameters.AddWithValue("Allowance", "Недопущен в ИжГТУ");
            }


            MessageBox.Show(command.ExecuteNonQuery().ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(textBox10.Text, sqlConnection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            SqlDataReader dataReader = null;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Name, Surname, Species, Birthday FROM Applicants", sqlConnection);

                dataReader = sqlCommand.ExecuteReader();

                ListViewItem item = null;

                while (dataReader.Read())
                {
                    item = new ListViewItem(new string[] { Convert.ToString(dataReader["Name"]), Convert.ToString(dataReader["Surname"]), Convert.ToString(dataReader["Species"]), DateTime.Parse(Convert.ToString(dataReader["Birthday"])).ToShortDateString() });

                    listView1.Items.Add(item);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Name LIKE '%{textBox11.Text}%'";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Allowance LIKE 'Допущен в ИжГТУ'";
                    break;

                case 1:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Allowance LIKE 'Недопущен в ИжГТУ'";
                    break;

                case 2:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"AverageScore >= 80";
                    break;

                case 3:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"AverageScore >= 51 AND AverageScore < 80";
                    break;

                case 4:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"AverageScore <= 50";
                    break;

                case 5:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"Species LIKE 'Хоббит'";
                    break;

                case 6:
                    (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"id > 0";
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataSet DB = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM Applicants WHERE id = {int.Parse(textBox12.Text)}", sqlConnection);
                cmd.ExecuteNonQuery();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void button5_Click_1(object sender, EventArgs e)
        {
            SqlDataAdapter dataAd = new SqlDataAdapter("SELECT * FROM Applicants", sqlConnection);

            DataSet DB = new DataSet();

            dataAd.Fill(DB);

            dataGridView2.DataSource = DB.Tables[0];
            dataGridView3.DataSource = DB.Tables[0];
            dataGridView4.DataSource = DB.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dataAd = new SqlDataAdapter("SELECT * FROM Applicants", sqlConnection);

            DataSet DB = new DataSet();

            dataAd.Fill(DB);

            dataGridView2.DataSource = DB.Tables[0];
            dataGridView3.DataSource = DB.Tables[0];
            dataGridView4.DataSource = DB.Tables[0];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                if (textBox13.Text == Convert.ToString(dataGridView4.Rows[i].Cells[0].Value.ToString()))
                { 

                    textBox14.Text = dataGridView4.Rows[i].Cells[1].Value.ToString();
                    textBox15.Text = dataGridView4.Rows[i].Cells[2].Value.ToString();
                    textBox16.Text = dataGridView4.Rows[i].Cells[3].Value.ToString();
                    textBox17.Text = DateTime.Parse(dataGridView4.Rows[i].Cells[4].Value.ToString()).ToShortDateString();
                    textBox18.Text = dataGridView4.Rows[i].Cells[5].Value.ToString();
                    textBox19.Text = dataGridView4.Rows[i].Cells[6].Value.ToString();
                    textBox20.Text = dataGridView4.Rows[i].Cells[7].Value.ToString();
                    textBox21.Text = dataGridView4.Rows[i].Cells[8].Value.ToString();
                    break;
                }
          
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int Q = (int.Parse(textBox21.Text) + int.Parse(textBox20.Text) + int.Parse(textBox19.Text) + int.Parse(textBox18.Text)) / 4;

            string Al;

            DateTime DATE = DateTime.Parse(textBox17.Text);

            SqlCommand update1 = new SqlCommand($"UPDATE [Applicants] SET [Name]=N'{textBox14.Text}', [Surname]=N'{textBox15.Text}', [Species]=N'{textBox16.Text}',[Birthday]=N'{DATE.Month}/{DATE.Day}/{DATE.Year}',[PhysicsScores]=N'{textBox18.Text}',[MathScores]=N'{textBox19.Text}',[RusScores]=N'{textBox20.Text}',[CompScienceScores]=N'{textBox21.Text}',[AverageScore]=N'{Q}' WHERE id = '{textBox13.Text}'", sqlConnection);

            update1.ExecuteNonQuery();

            if ((int.Parse(textBox21.Text) + int.Parse(textBox18.Text) + int.Parse(textBox19.Text) + int.Parse(textBox20.Text)) >= int.Parse(textBox9.Text))
            {
                Al = "Допущен в ИжГТУ";
                SqlCommand update3 = new SqlCommand($"UPDATE [Applicants] SET [Allowance] = N'{Al}' WHERE id = {int.Parse(textBox13.Text)}", sqlConnection);
                update3.ExecuteNonQuery();

            }
            else
            {
                Al = "Недопущен в ИжГТУ";
                SqlCommand update3 = new SqlCommand($"UPDATE [Applicants] SET [Allowance] = N'{Al}' WHERE id = {int.Parse(textBox13.Text)}", sqlConnection);
                update3.ExecuteNonQuery();
            }

            SqlDataAdapter dataAd = new SqlDataAdapter("SELECT * FROM Applicants", sqlConnection);

            DataSet DB = new DataSet();

            dataAd.Fill(DB);

            dataGridView2.DataSource = DB.Tables[0];
            dataGridView3.DataSource = DB.Tables[0];
            dataGridView4.DataSource = DB.Tables[0];
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox10.Text == "Введите команду...")
            {
                textBox10.Clear();
            }
        }

        private void textBox11_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox11.Text == "Поиск...")
            {
                textBox11.Clear();
            }
        }

        private void textBox12_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox12.Text == "Введите id абитуриента для удаления всей информации о нём...")
            {
                textBox12.Clear();
            }
            
        }

        private void textBox13_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox13.Text == "Введите id абитуриента, чтоб выписать всю информацию о нём...")
            {
                textBox13.Clear();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
