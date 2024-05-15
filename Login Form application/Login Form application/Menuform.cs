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
using System.Security.Cryptography;

namespace Login_Form_application
{
    
    public partial class Menuform : Form
    {
        private DateTime startTime;
        public Menuform()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            statusStrip1.Items.Add("Время процесса: ");
            /*CreateColumns();*/
   
        }

       // public class MessageLogger
       // {
         //   private string connectionString = @"Data Source = INBOOK_Y2_PLUS; Initial Catalog = Demo; Integrated Security = True";

           // public void LogMessage(string message)
            //{
              //  using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                  //  connection.Open();

                    // Записываем сообщение в базу данных
                    //string query = "INSERT INTO MessageLog (Message, Timestamp) VALUES (@Message, GETDATE())";
                    //SqlCommand command = new SqlCommand(query, connection);
                    //command.Parameters.AddWithValue("@Message", message);
                  //  command.ExecuteNonQuery();
                //}
           // }
       // }
        /* private void CreateColumns()
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
          } */

        private void Menuform_Load(object sender, EventArgs e)
        {

        } 

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор - Равдис Омейма Азизовна. Группа 3335М.");
            // MessageLogger messageLogger = new MessageLogger();
            //messageLogger.LogMessage("Пользователь запросил информацию об авторе");
            string message = ("Пользователь запросил информацию об авторе. Форма главного меню Menuform");

            string encryptedMessage = EncryptMessage(message);

            // Сохраняем зашифрованное сообщение в базу данных
            SaveToDatabase(encryptedMessage);
        }

        private void посторетьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Учебный проект. Выполнен по дисциплине Защищенные информационные системы. Язык С#. Версия Visual Studio 2019");
            string message = ("Пользователь запросил информацию о проекте. Форма главного меню Menuform");

            string encryptedMessage = EncryptMessage(message);

            // Сохраняем зашифрованное сообщение в базу данных
            SaveToDatabase(encryptedMessage);
           // MessageLogger messageLogger = new MessageLogger();
           // messageLogger.LogMessage("Пользователь запросил информацию о проекте");
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = ".txt";
            saveFile.Filter = "Test files| *.txt";
            if(saveFile.ShowDialog()==System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length>0)
            {
                using(StreamWriter sw = new StreamWriter(saveFile.FileName,true))
                {
                    sw.WriteLine(richTextBox1.Text);
                    sw.Close();

                    string message = ("Изменение и сохранение файла. Форма главного меню Menuform");
                    string encryptedMessage = EncryptMessage(message);
                    SaveToDatabase(encryptedMessage);

                    // MessageLogger messageLogger = new MessageLogger();
                    //messageLogger.LogMessage("Изменение и сохранение файла");
                }
            }
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();

            string message = ("Выход из системы. Форма главного меню Menuform");
            string encryptedMessage = EncryptMessage(message);
            SaveToDatabase(encryptedMessage);

            // MessageLogger messageLogger = new MessageLogger();
            // messageLogger.LogMessage("Выход из системы");
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFile.FileName);

                string message = ("Открытие файла. Форма главного меню Menuform");
                string encryptedMessage = EncryptMessage(message);
                SaveToDatabase(encryptedMessage);

                // MessageLogger messageLogger = new MessageLogger();
                // messageLogger.LogMessage("Открытие файла");
            }
        }

        private void шифрованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();//хэш SHA256
            f.ShowDialog();
        }

        private void шифрованиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {   
            Form2 f = new Form2(); //MD5
            f.ShowDialog();
        }

        private void учетныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();

            string message = ("Просмотр учетных данных из таблицы базы данных. Form4: Учетные данные");
            string encryptedMessage = EncryptMessage(message);
            SaveToDatabase(encryptedMessage);


            // MessageLogger messageLogger = new MessageLogger();
            // messageLogger.LogMessage("Просмотр учетных данных из таблицы базы данных");
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            string plainText = inputTextBox.Text;
            int shift = 3; // Шаг сдвига для метода Цезаря

            string cipherText = CaesarCipher(plainText, shift);

            outputTextBox.Text = cipherText;

            TimeSpan elapsedTime = DateTime.Now - startTime;
            statusStrip1.Items[0].Text = "Время шифрования: " + elapsedTime.TotalMilliseconds.ToString() + " seconds";
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            string cipherText = inputTextBox.Text;
            int shift = 3; // Шаг сдвига для метода Цезаря

            string plainText = CaesarCipher(cipherText, -shift);

            outputTextBox.Text = plainText;

            TimeSpan elapsedTime = DateTime.Now - startTime;
            statusStrip1.Items[0].Text = "Время дешифрования: " + elapsedTime.TotalMilliseconds.ToString() + " seconds";
        }

        private string CaesarCipher(string text, int shift)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetter(chars[i]))
                {
                    char baseChar = char.IsUpper(chars[i]) ? 'A' : 'a';
                    chars[i] = (char)(((chars[i] + shift - baseChar + 26) % 26) + baseChar);
                }
            }

            return new string(chars);
        }



        string EncryptMessage(string message)
        {
            string key = "mySecretKey12345"; // Ключ для шифрования 16 символов
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // Инициализационный вектор

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(message);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }


        void SaveToDatabase(string encryptedMessage)
        {
            SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");
            string connectionString = @"Data Source = INBOOK_Y2_PLUS; Initial Catalog = Demo; Integrated Security = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand("INSERT INTO MessageLog (Message, Timestamp) VALUES (@encryptedMessage, GETDATE())", con);
                command.Parameters.AddWithValue("@encryptedMessage", encryptedMessage);
                command.ExecuteNonQuery();
            }
        }

        private void логжурналToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Watch_log f = new Watch_log(); 
            f.ShowDialog();

            string message = ("Просмотр лог-журнала. Form Watch_log: Лог-журнал");
            string encryptedMessage = EncryptMessage(message);
            SaveToDatabase(encryptedMessage);

            // MessageLogger messageLogger = new MessageLogger();
            //messageLogger.LogMessage("Просмотр лог-журнала");
        }

        private void парсингToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parsing f = new Parsing();
            f.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            trip f = new trip();
            f.ShowDialog();
        }
    }
}