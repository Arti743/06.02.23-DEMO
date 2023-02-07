using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Demo
{
    public partial class Dostavka : Form
    {
        DataSet data;
        SqlDataAdapter adap;
        //Задаем адрес подключения к нашему Sql Server.
        //Data Source - Имя(ip) сервера.
        //Initial Catalog - Имя базы данных к которой мы подключаемся.
        //Integrated Security - проверка подлинности. В нашем случае проверка подлинности Windows.
        string connectionString = @"Data Source=192.168.0.3, 3306; Initial Catalog=Demo; Integrated Security=True";
        //Вывод таблицы через команду Sql.
        string sql = "SELECT * FROM delivery";
        public static bool Sett;

        public Dostavka()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            //Осуществляем подключение к серверу и вывод данных в dataGridView.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    adap = new SqlDataAdapter(sql, connection);

                    data = new DataSet();
                    adap.Fill(data);
                    dataGridView1.DataSource = data.Tables[0];
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.AllowUserToAddRows = false;
                }
                //При отсутсвии подключения к базе данных(среверу), будет выводиться сообщение об этом.
                catch
                {
                    MessageBox.Show("Отсутсвует подключение к серверу. Повторите попытку позже", "Ошибка подключения");
                    Application.Restart();
                }
            }
        }
        //При нажатии на строку в dataGridView будет выходить сообщение с данными выбранной строки.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                sb.Append(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[i].Value + " | ");
            }
            MessageBox.Show(sb.ToString());
        }

        //Обработчик события закрытия формы через "крестик".
        private void Dostavka_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //При нажатии кнопки "Выйти из аккаунта" будет осуществляться переход на окно авторизации.
        private void button1_Click(object sender, System.EventArgs e)
        {
            Form1 Win = new Form1();
            Win.Show();
            this.Hide();
        }
    }
}
