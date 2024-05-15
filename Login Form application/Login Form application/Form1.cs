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
using md5_sql_hash;

namespace Login_Form_application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True");
        //
        //public class MessageLogger
       // {
         //   private string connectionString = @"Data Source = INBOOK_Y2_PLUS; Initial Catalog = Demo; Integrated Security = True";

           // public void LogMessage(string message)
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
             //  {
               //     connection.Open();

                    // Записываем сообщение в базу данных
                 //   string query = "INSERT INTO MessageLog (Message, Timestamp) VALUES (@Message, GETDATE())";
                   // SqlCommand command = new SqlCommand(query, connection);
                   // command.Parameters.AddWithValue("@Message", message);
                   // command.ExecuteNonQuery();
               // }
            //}
        //}

        //


        private void Form1_Load(object sender, EventArgs e)
        {
            txt_username.MaxLength = 50;
            txt_password.MaxLength = 50;
        }



        private void button_login_Click(object sender, EventArgs e)
        {
            String username, user_password;

            username = txt_username.Text;
            user_password = txt_password.Text;
            txt_password.UseSystemPasswordChar = true;

            string hashedPassword = HashPassword(user_password);


            using (SqlConnection con = new SqlConnection(@"Data Source=INBOOK_Y2_PLUS;Initial Catalog=Demo;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT password FROM Login_new WHERE username = @username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);

                string dbHashedPassword = cmd.ExecuteScalar() as string;

                if (dbHashedPassword != null && dbHashedPassword.Equals(hashedPassword))
                {
                    Menuform form2 = new Menuform();
                    form2.Show();
                    this.Hide();

                    string message = ("Вход в систему под логином: " + username + ". Form1: Форма авторизации");

                    string encryptedMessage = EncryptMessage(message);

                    // Сохраняем зашифрованное сообщение в базу данных
                    SaveToDatabase(encryptedMessage);

                    // MessageLogger messageLogger = new MessageLogger();
                    //messageLogger.LogMessage("Вход в систему под логином: " + username);
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.");
                    // Записываем сообщение в базу данных

                    // MessageLogger messageLogger = new MessageLogger();
                    //messageLogger.LogMessage("Неверный логин или пароль при попытке входа " + username);
                    string message = ("Неверный логин или пароль при попытке входа под логином: "+ username + ".Form1: Форма авторизации");


                    string encryptedMessage = EncryptMessage(message);

                    // Сохраняем зашифрованное сообщение в базу данных
                    SaveToDatabase(encryptedMessage);

                   // MessageBox.Show(message);
                }
            }


             string EncryptMessage(string message)
            {
                string key = "mySecretKey12345"; // Ключ для шифрования
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

            string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }
}