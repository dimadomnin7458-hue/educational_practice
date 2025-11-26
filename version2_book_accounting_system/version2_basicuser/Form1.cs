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

namespace version2_basicuser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\\\192.168.3.250\\Veda\\3 курс\\ИСП 34\\Разработка кода ИС\\Домнин Дмитрий\\git_project_education\\version2_book_accounting_system\\version2_basicuser\\bin\\x64\\Debug\\git.accdb";
        private string tableName = "users";
        private string loginColumn = "login";
        private string passwordColumn = "password";

        private bool AuthenticateUser(string login, string password)
        {
            bool isAuthenticated = false;
            string query = $"SELECT COUNT(*) FROM [{tableName}] WHERE [{loginColumn}] = @p1 AND [{passwordColumn}] = @p2";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@p1", login);
                        command.Parameters.AddWithValue("@p2", password);

                        connection.Open();
                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            isAuthenticated = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isAuthenticated = false;
            }

            return isAuthenticated;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (AuthenticateUser(login, password))
            {
                MessageBox.Show("Вход выполнен!");
                searchform newForm4 = new searchform();
                newForm4.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
