using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version1_admin.gitDataSetTableAdapters;

namespace Version1_admin
{
    public partial class usersBd : Form
    {
        public usersBd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 newForm2 = new Form1();
            newForm2.Show();
        }

        private void usersBd_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "gitDataSet1.пользователи". При необходимости она может быть перемещена или удалена.
            this.пользователиTableAdapter.Fill(this.gitDataSet1.пользователи);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "gitDataSet.пользователи". При необходимости она может быть перемещена или удалена.
            this.пользователиTableAdapter.Fill(this.gitDataSet.пользователи);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            main1 newForm3 = new main1();
            newForm3.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы и так находитесь на этой базе данных"  );
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.пользователиBindingSource1.EndEdit();

                // Здесь вызываем обновление
                int result = this.пользователиTableAdapter.Update(this.gitDataSet.пользователи);

                // Проверяем результат операции
                if (result > 0)
                {
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.пользователиTableAdapter.Fill(this.gitDataSet.пользователи);
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Конфликт при обновлении данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Ошибка связи с базой данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;

                    пользователиBindingSource1.RemoveAt(selectedIndex);

                    int result = пользователиTableAdapter.Update(gitDataSet.пользователи);

                    // Проверяем результат операции
                    if (result > 0)
                    {
                        MessageBox.Show("Данные успешно удалены!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (DBConcurrencyException ex)
                {
                    MessageBox.Show("Конфликт при удалении данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("Ошибка связи с базой данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выделите строку для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                пользователиBindingSource1.Filter = $"`Имя` LIKE '%{searchText}%' OR `Фамилия` LIKE '%{searchText}%' OR `Отчество` LIKE '%{searchText}%' OR `Взятые книги` LIKE '%{searchText}%'";
            }
            else
            {
                пользователиBindingSource1.Filter = "";
            }
        }
    }
    
}
