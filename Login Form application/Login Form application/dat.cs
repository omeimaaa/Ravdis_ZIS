using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace Login_Form_application
{
    public partial class dat : Form
    {
        public dat()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
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

        private void button1_Click(object sender, EventArgs e)


        {
            //string text = "Sample text for word search.";
            string text = "";
            foreach (var item in listBox1.Items)
            {
                text += item.ToString() + " ";
            }
            string regexPattern = @"\b\w+\b";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] foundWords = WordSearchRegex.FindWordsByRegex(text, regexPattern);
            foreach (string word in foundWords)
            {
                listBox2.Items.Add(word);
            }

            int wordCount = WordSearchHelper.CountWords(text);
            MessageBox.Show($"Количество слов в тексте: {wordCount}");
            stopwatch.Stop();
            MessageBox.Show($"Время выполнения поиска слов: {stopwatch.ElapsedMilliseconds} мс");
        }

        private void dat_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True";
            string query = "SELECT * FROM MessageLog";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {


                    string message = reader["Message"].ToString();
                    string decryptedMessage = DecryptMessage(message);
                    string date = reader["Timestamp"].ToString();

                    listBox1.Items.Add(date + " - " + decryptedMessage);
                }

                reader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach (var item in listBox1.Items)
            {
                text += item.ToString() + " ";
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] foundForms = WordSearchForm1.FindFormsByRegex(text);
            foreach (string form in foundForms)
            {
                listBox2.Items.Add(form);
            }

            int formCount = foundForms.Length;
            MessageBox.Show($"Количество Форм авторизации: {formCount}");

            stopwatch.Stop();
            MessageBox.Show($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

            string text = "";
            foreach (var item in listBox1.Items)
            {
                text += item.ToString() + " ";
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] foundForms = WordSearchWatchlog.FindFormsByRegex(text);
            foreach (string form in foundForms)
            {
                listBox2.Items.Add(form);
            }

            int formCount = foundForms.Length;
            MessageBox.Show($"Количество слов 'Форм Просмотра логов: {formCount}");

            stopwatch.Stop();
            MessageBox.Show($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        
    }

        private void button4_Click(object sender, EventArgs e) // главная 
        {
            string text = "";
            foreach (var item in listBox1.Items)
            {
                text += item.ToString() + " ";
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] foundForms = WordSearchmain.FindFormsByRegex(text);
            foreach (string form in foundForms)
            {
                listBox2.Items.Add(form);
            }

            int formCount = foundForms.Length;
            MessageBox.Show($"Количество Форм Информационных: {formCount}");

            stopwatch.Stop();
            MessageBox.Show($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }

        private void button5_Click(object sender, EventArgs e) //учетные
        {
            string text = "";
            foreach (var item in listBox1.Items)
            {
                text += item.ToString() + " ";
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string[] foundForms = WordSearchUser.FindFormsByRegex(text);
            foreach (string form in foundForms)
            {
                listBox2.Items.Add(form);
            }

            int formCount = foundForms.Length;
            MessageBox.Show($"Количество Форм Учетные данные: {formCount}");

            stopwatch.Stop();
            MessageBox.Show($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }

        private void button6_Click(object sender, EventArgs e)
        {

            listBox2.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    }
    /*
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.ClearSelected();

            foreach (string item in listBox1.Items)
            {
                string[] parts = item.Split(new string[] { " - " }, StringSplitOptions.None);
                if (parts.Length == 2 && DateTime.TryParse(parts[0], out DateTime date))
                {
                    if (date >= startDate && date <= endDate)
                    {
                        int index = listBox1.FindStringExact(item);
                        if (index != ListBox.NoMatches)
                        {
                            int ind = listBox1.Items.IndexOf(item);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                            listBox1.SetSelected(ind, true);
                        }

                    }
                }
            }
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


       


       

      

    }
}
*/