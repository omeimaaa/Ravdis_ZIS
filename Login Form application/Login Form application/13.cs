using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Form_application
{
    public partial class _13 : Form
    {

        public _13()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.DrawItem += new DrawItemEventHandler(listBox2_DrawItem);

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

        private void _13_Load(object sender, EventArgs e)
        {
            /*String connectionString = @"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True";

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


                    richTextBox.AppendText(decryptedMessage);
                    richTextBox.AppendText(Time);
                }
            }*/
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
                    string date = reader["Timestamp"].ToString();


                    listBox1.Items.Add(decryptedMessage);
                    listBox1.Items.Add(DateTime.Parse(date));

                }
            }
        }






        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void parseButton_Click(object sender, EventArgs e)
        { /*
            string searchText = "Form";
             
            string highlightText = "Равдис.О.А.";
           


            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string itemText = listBox1.Items[i].ToString();
                if (itemText.Contains(searchText))
                {
                    int index = itemText.IndexOf(searchText);
                 
                   
                    listBox1.Items[i] = itemText.Insert(index + searchText.Length, " (" + highlightText + ")");
                    
                    listBox1.SetSelected(i, true);
                    listBox1.SetSelected(i, false);

                  
                }
            }*/

        // string startDate = dateTimePickerStart.Value.ToString("dd.MM.yyyy");
          //  string endDate = dateTimePickerEnd.Value.ToString("dd.MM.yyyy");
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            string searchText = @"\w*form(\w*)"; // Используем регулярное выражение для поиска словоформы "form"
            string highlightText = "Равдис.О.А.";



                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    string itemText = listBox2.Items[i].ToString();
                    MatchCollection matches = Regex.Matches(itemText, searchText, RegexOptions.IgnoreCase);

                    foreach (Match match in matches)
                    {
                        int ind = match.Index;
                        int length = match.Length;
                        itemText = itemText.Insert(ind + length, " (" + highlightText + ")");
                        listBox2.Items[i] = itemText;
                        listBox2.Invalidate();
                    }

                }



            
                    
                
           


           

            


        }

        


            private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
            {
                e.DrawBackground();
                ListBox listBox = sender as ListBox;

                if (e.Index >= 0)
                {
                    string itemText = listBox.Items[e.Index].ToString();
                    string highlightText = "Равдис.О.А.";

                    if (itemText.Contains(highlightText))
                    {
                        int startIndex = itemText.IndexOf(highlightText);
                        int length = highlightText.Length;

                        using (Brush brush = new SolidBrush(Color.Green))
                        {
                            e.Graphics.DrawString(itemText, e.Font, brush, e.Bounds);
                            // e.Graphics.DrawString(highlightText, e.Font, Brushes.Green, e.Bounds.X + TextRenderer.MeasureText(itemText.Substring(0, startIndex), e.Font).Width, e.Bounds.Y);
                        }
                    }
                    else
                    {
                        e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds);
                    }
                }
            }

            private void button2_Click(object sender, EventArgs e)
            {
                dat f = new dat();
                f.ShowDialog();
            }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;

            string connectionString = @"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Message, Timestamp FROM MessageLog WHERE Timestamp BETWEEN @StartDate AND @EndDate";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string encryptedMessage = reader["Message"].ToString();
                    string message = DecryptMessage(encryptedMessage);

                    //string message = reader.GetString(0);
                    DateTime date = reader.GetDateTime(1);
                    listBox2.Items.Add($"Сообщение: {message}, Дата: {date}");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }
    }

}
 















