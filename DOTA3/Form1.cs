using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DOTA3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class1 db;

        private void button2_Click(object sender, EventArgs e)
        {
          
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            db = new class1();
            db.getConnection();


            SQLiteConnection connection = new SQLiteConnection(db.connection);


            string query = "SELECT * FROM info12";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dtFines = new DataTable();
            adapter.Fill(dtFines);

            dataGridView1.DataSource = dtFines;

            // добавляем столбы для datagridview, для их отображения

            // dataGridView1.Columns["name"].Visible = false; // Прячем эту колонку
            //dataGridView1.Columns["oldDate"].HeaderText = "ФИО";
            // dataGridView1.Columns["info"].HeaderText = "ОПИСАНИЕ";

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form fGeneral = new Form2();
            fGeneral.Show();
            fGeneral.FormClosed += new FormClosedEventHandler(form_FormClosed1);
            this.Hide();
        }
        void form_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
           
        }
    }
}

