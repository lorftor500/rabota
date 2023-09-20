using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOTA3
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            

        }
        class1 db;

        private void button1_Click(object sender, EventArgs e)
        {
            String EmailUser = textBox1.Text;
            String PasswordUser = textBox2.Text;

            if (EmailUser != string.Empty
            && PasswordUser != string.Empty)
            {
                checkAccount(EmailUser, PasswordUser);
            }
            else
            {
                MessageBox.Show("Введите данные", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkAccount(string Login, string password)
        {
            // Подключение к БД
            db = new class1();
            db.getConnection();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(db.connection))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();

                    // Команда для БД
                    string query = @"SELECT *  FROM avtoreg WHERE login=
                        '" + Login + "' " +
                        "AND password='" + password + "'";

                    int count = 0;
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    object result = cmd.ExecuteScalar();

                    SQLiteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        count++;
                    }

                    if (count == 1)
                    {
                 
                        

                      
                        
                            MessageBox.Show("Вы вошли", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form1 gen = new Form1();
                            gen.Show();
                            gen.FormClosed += new FormClosedEventHandler(form_FormClosed2);
                            this.Hide();

                            void form_FormClosed2(object sender, FormClosedEventArgs e)
                            {
                                this.Close();
                            }
                        
                    }
                    else
                        MessageBox.Show("Неверный логин или пароль", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Авторизация");
            }
        }


    }
}
