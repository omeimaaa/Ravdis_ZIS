using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Login_Form_application
   
{
        public partial class Parsing: Form
        {
           // private string[] signatures = { "Цель и порядок выполнения работы", "Варианты индивидуальных заданий", "Методические рекомендации", "Контрольные вопросы" };    
            private Color currentHighlightColor = Color.Blue; // Цвет выделения по умолчанию

            public Parsing()
            {

                InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            //comboBox1.Items.AddRange(signatures);
            richTextBox.TextChanged += RichTextBoxText_TextChanged;
                comboBox1.SelectedIndexChanged += ComboBoxSignatures_SelectedIndexChanged;
                comboBox1.Items.Add("Цель");
                comboBox1.Items.Add("Варианты");
                comboBox1.Items.Add("Методические");
                comboBox1.Items.Add("Контрольные");
            }


       
        private void openfile_Click(object sender, EventArgs e)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string text = File.ReadAllText(openFileDialog.FileName);
                    richTextBox.Text = text;
                }
            }
        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Colo_Click(object sender, EventArgs e)
            {
                HighlightSelectedSignature();
            }

            private void RichTextBoxText_TextChanged(object sender, EventArgs e)
            {
                HighlightSelectedSignature();
            }

            private void ComboBoxSignatures_SelectedIndexChanged(object sender, EventArgs e)
            {
                string selectedSignature = comboBox1.SelectedItem?.ToString();
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

            private void HighlightSelectedSignature()
            {
                string selectedSignature = comboBox1.SelectedItem?.ToString();

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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = richTextBox.Text.ToLower(); // Получаем текст из RichTextBox и приводим к нижнему регистру

            if (searchText.Contains("Цель"))
            {
                comboBox1.SelectedItem = "Цель";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (searchText.Contains("цель"))
            {
                comboBox1.SelectedItem = "Цель";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }
            

            else if (searchText.Contains("Варианты"))
            {
                comboBox1.SelectedItem = "Варианты";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }

            else if (searchText.Contains("варианты"))
            {
                comboBox1.SelectedItem = "Варианты";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }


            else if (searchText.Contains("Методические"))
            {
                comboBox1.SelectedItem = "Методические";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (searchText.Contains("методические"))
            {
                comboBox1.SelectedItem = "Методические";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }



            else if (searchText.Contains("Контрольные"))
            {
                comboBox1.SelectedItem = "Контрольные";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }
            else if (searchText.Contains("контрольные"))
            {
                comboBox1.SelectedItem = "Контрольные";
                richTextBox.ForeColor = System.Drawing.Color.Black;
            }

            else
            {
                richTextBox.ForeColor = System.Drawing.Color.Black; // Сброс цвета шрифта
            }

        }
    }
    }



/*{ 
    public partial class Parsing : Form
    {
        private List<string> comboBoxValues = new List<string> { "Цель и порядок выполнения работы", "Варианты индивидуальных заданий", "Методические рекомендации", "Контрольные вопросы" };
        public Parsing()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            comboBox1.Items.AddRange(comboBoxValues.ToArray());
            comboBox1.SelectedIndex = 0;
        }

        private void HighlightText(string searchText)
        {
            string text = richTextBox.Text;
            richTextBox.SelectAll();
            richTextBox.SelectionColor = Color.Black; // Reset color

            if (!string.IsNullOrEmpty(searchText))
            {
                foreach (string value in comboBoxValues)
                {
                    string pattern = $@"\b{Regex.Escape(value)}\b";
                    MatchCollection matches = Regex.Matches(text, pattern);
                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;
                        int length = match.Length;
                        richTextBox.Select(startIndex, length);
                        richTextBox.SelectionColor = value == searchText ? Color.Blue : Color.Black; // Change color based on selected value
                    }
                }
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            HighlightText(comboBox1.SelectedItem.ToString());
        }

        private void openfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string text = File.ReadAllText(openFileDialog.FileName);
                richTextBox.Text = text;
                HighlightText(comboBox1.SelectedItem.ToString());
            }
        }

        private void Colo_Click(object sender, EventArgs e)
        {
            HighlightText(comboBox1.SelectedItem.ToString());
        }

        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

      
    }
}
*/
