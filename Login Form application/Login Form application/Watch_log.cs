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
    public partial class Watch_log : Form
    {
        public Watch_log()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Watch_log_Load(object sender, EventArgs e)
        {
            CreateColumns();
            String connectionString = @"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SqlCommand cmd = new SqlCommand("SELECT Message FROM MessageLog", connection);
                SqlCommand cmd = new SqlCommand("SELECT * FROM MessageLog", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string encryptedMessage = reader["Message"].ToString();
                    string decryptedMessage = DecryptMessage(encryptedMessage);
                    string Time = reader["Timestamp"].ToString();

                    
                    listBox.Items.Add(decryptedMessage);
                    listBox1.Items.Add(Time);
                }
            }
        }
        private void CreateColumns()
        {
            SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");

            con.Open();
            string querry = "SELECT * FROM MessageLog";
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

                dataGridView2.Rows.Add(s);
        }


        

        private string DecryptMessage(string encryptedMessage)
        {
            string key = "mySecretKey12345";
            byte[] cipherText = Convert.FromBase64String(encryptedMessage);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // Инициализационный вектор

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();


                        }
                    }
                }
            }
        }

        

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _13 f = new _13(); 
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            @try f = new @try();
            f.ShowDialog();
        }
    }
}
