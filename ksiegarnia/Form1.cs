using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.BC;
namespace ksiegarnia
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect();
        }
        private void connect()
        {
            string connect = "wpisz swoj serwer :p";
            connection = new MySqlConnection(connect);
            {
                try
                {
                   
                    connection.Open();
                    refresh();
                }         
                catch(Exception ex) 
                { 
                      MessageBox.Show("nie udalo sie xd" + ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nwm = numer.Text;
            int id = Convert.ToInt32(nwm);
            string sql2 = $"UPDATE ksiazka SET wypozyczona = true WHERE id = {id};";
            MySqlCommand query = new MySqlCommand(sql2, connection);
            MySqlDataReader reader = query.ExecuteReader();
            MessageBox.Show("Twoja ksiazka zostala wypozyczona");
            reader.Close();
            refresh();
        }
        private void refresh()
        {
            string sql = "Select * from ksiazka";
            MySqlCommand query = new MySqlCommand(sql, connection);
            MySqlDataReader reader = query.ExecuteReader();
            if (reader.HasRows)
            {
                StringBuilder ksiazka = new StringBuilder();
                while (reader.Read())
                {
                    // Pobieranie informacji o książce
                    int id = reader.GetInt32(3);
                    string tytul = reader.GetString(0);
                    string autor = reader.GetString(2);
                    bool wyp = reader.GetBoolean(1);

                    // Dodawanie informacji o książce do StringBuildera
                    ksiazka.AppendLine($"{id}. ,,{tytul}'' {autor} - {(wyp ? "Wypożyczona" : "Wolna")}");
                }
                textBox2.Text = ksiazka.ToString();
                reader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nwm = numer.Text;
            int id = Convert.ToInt32(nwm);
            string sql2 = $"UPDATE ksiazka SET wypozyczona = false WHERE id = {id};";
            MySqlCommand query = new MySqlCommand(sql2, connection);
            MySqlDataReader reader = query.ExecuteReader();
            MessageBox.Show("Zwrociles ksiazke :)");
            reader.Close();
            refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tytul = textBox1.Text;
            string autor = textBox3.Text;
            if(tytul.Length == 0 || autor.Length == 0)
            {
                MessageBox.Show("Nie mozesz dodac pustej ksiazki");
            }
            else
            {
                string sql3 = $"INSERT INTO ksiazka (nazwa,autor) VALUES ('{tytul}','{autor}');";
                MySqlCommand query = new MySqlCommand( sql3, connection);
                MySqlDataReader reader = query.ExecuteReader();
                MessageBox.Show("Dodales ksiazke do systemu");
                reader.Close();
                refresh();   
            }
        }
    }
}
