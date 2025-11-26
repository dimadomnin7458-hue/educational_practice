using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace version2_basicuser
{
    public partial class searchform : Form
    {
        public searchform()
        {
            InitializeComponent();
        }

        private void searchform_Load(object sender, EventArgs e)
        {
            this.пользователиTableAdapter.Fill(this.gitDataSet.пользователи);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cardNumber = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(cardNumber))
            {
                MessageBox.Show("Пожалуйста, введите номер библиотечной карточки.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                пользователиBindingSource.Filter = "";
                return;
            }

            try
            {

                пользователиBindingSource.Filter = $"[№ Библиотечной карточки] = {cardNumber}";

                if (пользователиBindingSource.Count == 0)
                {
                    MessageBox.Show("Пользователь с таким номером не найден.", "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Data.EvaluateException ex)
            {
                MessageBox.Show($"Ошибка типа данных: Проверьте, является ли столбец текстовым или числовым.\n{ex.Message}", "Ошибка фильтрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 newForm2 = new Form1();
            newForm2.Show();
        }
    }
}
