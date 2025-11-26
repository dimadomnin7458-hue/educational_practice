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

namespace Version1_admin
{
    public partial class main1 : Form
    {
        public main1()
        {
            InitializeComponent();
        }

        private void main1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "gitDataSet.учет_книг". При необходимости она может быть перемещена или удалена.
            this.учет_книгTableAdapter.Fill(this.gitDataSet.учет_книг);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "gitDataSet.учет_книг". При необходимости она может быть перемещена или удалена.
            this.учет_книгTableAdapter.Fill(this.gitDataSet.учет_книг);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "gitDataSet.учет_книг". При необходимости она может быть перемещена или удалена.
            this.учет_книгTableAdapter.Fill(this.gitDataSet.учет_книг);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 newForm2 = new Form1();
            newForm2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            usersBd newForm4 = new usersBd();
            newForm4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.учетКнигBindingSource.EndEdit();

                // Здесь вызываем обновление
                int result = this.учет_книгTableAdapter.Update(this.gitDataSet.учет_книг);

                // Проверяем результат операции
                if (result > 0)
                {
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.учет_книгTableAdapter.Fill(this.gitDataSet.учет_книг);
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

                    учетКнигBindingSource.RemoveAt(selectedIndex);

                    int result = учет_книгTableAdapter.Update(gitDataSet.учет_книг);

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

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы и так находитесь на этой базе данных");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                учетКнигBindingSource.Filter = $"`Имя` LIKE '%{searchText}%' OR `Наименование` LIKE '%{searchText}%' OR `Автор` LIKE '%{searchText}%' OR `Фамилия` LIKE '%{searchText}%'";
            }
            else
            {
                учетКнигBindingSource.Filter = "";
            }
        }
    }
}
