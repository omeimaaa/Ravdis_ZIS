using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Login_Form_application
{
    public partial class trip : Form
    {
        public trip()
        {

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

        }

        private void trip_Load(object sender, EventArgs e)
        {

            CreateColumns1();
            CreateColumns2();
            CreateColumns3();
            String connectionString = @"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SqlCommand cmd = new SqlCommand("SELECT Message FROM MessageLog", connection);
              //  SqlCommand cmd = new SqlCommand("SELECT * FROM Trip", connection);
              //  SqlDataReader reader = cmd.ExecuteReader();

                /*while (reader.Read())
                {
                    string encryptedMessage = reader["Message"].ToString();
                  
                    string Time = reader["Timestamp"].ToString();


                }*/
            }
        }

        private void CreateColumns1()
        {
            SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");

            con.Open();
            string querry = "SELECT * FROM Trip";
            SqlCommand command = new SqlCommand(querry, con);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[7]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
            }
            reader.Close();
            con.Close();
            foreach (string[] s in data)

                dataGridView1.Rows.Add(s);
        }

        private void CreateColumns2()
        {
            SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");

            con.Open();
            string querry = "SELECT * FROM Passenger ";
            SqlCommand command = new SqlCommand(querry, con);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[2]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                
            }
            reader.Close();
            con.Close();
            foreach (string[] s in data)

                dataGridView2.Rows.Add(s);
        }

        private void CreateColumns3()
        {
            SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");

            con.Open();
            string querry = "SELECT * FROM Pass_in_trip";
            SqlCommand command = new SqlCommand(querry, con);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                
            }
            reader.Close();
            con.Close();
            foreach (string[] s in data)

                dataGridView3.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
