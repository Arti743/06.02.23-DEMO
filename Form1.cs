using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Обработчик события нажатия кнопки "Войти".
        private void button1_Click(object sender, EventArgs e)
        {
            //Задаем адрес подключения к нашему Sql Server.
            //Data Source - Имя(ip) сервера.
            //Initial Catalog - Имя базы данных к которой мы подключаемся.
            //Integrated Security - проверка подлинности. В нашем случае проверка подлинности Windows.
            string connectionString = @"Data Source=192.168.0.3, 3306; Initial Catalog=Demo; Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            bool succes = false;
            try
            {
                //Указываем в каких поля содержится логин и пароль.
                const string comand = "SELECT * FROM users WHERE user_log=@user_log AND user_pwd=@user_pwd";
                SqlCommand check = new SqlCommand(comand, conn);
                //Даем ссылку на наши textbox'ы, которые будут вводится данные аккаунта.
                check.Parameters.AddWithValue("@user_log", textBox1.Text);
                check.Parameters.AddWithValue("@user_pwd", textBox2.Text);
                conn.Open();

                using (var dataReader = check.ExecuteReader())
                {
                    succes = dataReader.Read();
                }
            }
            finally
            {
                conn.Close();
            }
            //Если введенные данные верны, осуществляем переход на форму с таблицей.
            if (succes)
            {
                Dostavka Win = new Dostavka();
                Win.Show();
                this.Hide();
            }
            //Если данные НЕ верны, выводим сообщение об этом.
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }
        //Обработчик события закрытия формы через "крестик".
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
