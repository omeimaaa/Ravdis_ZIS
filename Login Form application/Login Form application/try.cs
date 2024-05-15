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
    public partial class @try : Form

    {
        private Color currentHighlightColor = Color.Blue; 
        public @try()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
           
            comboBox1.Items.Add("Form1");
            comboBox1.Items.Add("Form2");
            comboBox1.Items.Add("Form3");
            comboBox1.Items.Add("Form4");
            comboBox1.Items.Add("Form5");
            comboBox1.Items.Add("Menuform");
            comboBox1.Items.Add("Watch_log");
        }

        private void @try_Load(object sender, EventArgs e)
        {

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

                    

                    listBox1.Items.Add(decryptedMessage);
                    listBox1.Items.Add(Time);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            richTextBox.Clear();
            // int count = 0;

            foreach (var item in listBox1.Items)
            {
                if (item.ToString().Contains(selectedValue))
                {
                   // richTextBox.SelectionColor = selectedValue.StartsWith("цветок") ? Color.Green : Color.Black;
                   // richTextBox.SelectionColor = Color.Black; // Сначала устанавливаем черный цвет для всего текста
                   // richTextBox.AppendText(selectedValue + Environment.NewLine); // Добавляем значение из ComboBox
                    richTextBox.AppendText(item.ToString() + Environment.NewLine);
                    int startIndex = richTextBox.Text.LastIndexOf(selectedValue); // Находим индекс начала добавленного текста
                    richTextBox.Select(startIndex, selectedValue.Length); // Выделяем только что добавленный текст
                    richTextBox.SelectionColor = Color.Green; // 

                   // richTextBox.AppendText(item.ToString() + Environment.NewLine);
                    //richTextBox.AppendText(selectedValue + Environment.NewLine);
                    //count += CountWordOccurrence(item.ToString(), "Form1");
                }
            }
            if (richTextBox.Lines.Length>0)
              
            { 
            richTextBox.AppendText(Environment.NewLine + $"Количество исключений: {richTextBox.Lines.Length - 1}");
            }
            else
                richTextBox.AppendText(Environment.NewLine + $"Количество исключений: {richTextBox.Lines.Length}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*  private int CountWordOccurrence(string text, string word)
          {
              string[] words = text.Split(' ');
              int count = 0;

              foreach (string w in words)
              {
                  if (w.ToLower() == word.ToLower())
                  {
                      count++;
                  }
              }

              return count;
          }*/
    }
}

       /* private void ComboBoxSignatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSignature = comboBox1.SelectedItem.ToString();
            
            foreach (var item in listBox1.Items)
            {
                if (item.ToString() == selectedSignature)
                {
                    richTextBox.AppendText(item.ToString() + Environment.NewLine);
                    if (!string.IsNullOrEmpty(selectedSignature))
                    {
                        richTextBox.SelectionStart = 0;
                        richTextBox.SelectionLength = richTextBox.TextLength;
                        richTextBox.SelectionColor = Color.Black; // Сброс цвета выделения
                        currentHighlightColor = GetRandomColor(); // Генерация случайного цвета для новой сигнатуры
                        HighlightSelectedSignature();
                        richTextBox.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            
        }

        private void HighlightSelectedSignature()
        {
            string selectedSignature = comboBox1.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedSignature))
            {
                string pattern = @"\b" + Regex.Escape(selectedSignature) + @"\b";
                MatchCollection matches = Regex.Matches(richTextBox.Text, pattern);

                foreach (Match match in matches)
                {
                    richTextBox.Select(match.Index, match.Length);
                    richTextBox.SelectionColor = currentHighlightColor;
                }
            }

            else
            {
                richTextBox.SelectionColor = Color.Black;
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }

        }

        private Color GetRandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

     
       */ 

   