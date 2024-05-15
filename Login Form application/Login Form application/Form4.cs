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
using md5_sql_hash;
using System.IO;

namespace Login_Form_application
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            CreateColumns();
        }
        private void CreateColumns()
        {
            SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");

            con.Open();
            string querry = "SELECT * FROM Login_new";
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

                dataGridView1.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
