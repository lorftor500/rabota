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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        class1 db;

        private void button1_Click(object sender, EventArgs e)
        {
            db = new class1();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);

            // Команда для БД
            string query = @"SELECT Photo_dotera_2, Photo_dotera_3 FROM photo";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable datatable = new DataTable();

            adapter.Fill(datatable);

            dataGridView1.DataSource = datatable;


            dataGridView1.Columns["Photo_dotera_3"].HeaderText = "Цена";
            dataGridView1.Columns["Photo_dotera_3"].HeaderText = "статус";

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            db = new class1();
            db.getConnection();

            MessageBox.Show("вы успеешно заказали игрушку");

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = "нет в наличии";

                DataGridViewRow selectRow = dataGridView1.Rows[e.RowIndex];
                int recordID = Convert.ToInt32(selectRow.Cells["SpecialistId"].Value);

                Update(recordID, "нет в наличии");
            }

            void Update(int recordid, string value)
            {
                db = new class1();
                db.getConnection();

                using (SQLiteConnection connection = new SQLiteConnection(db.connection))
                {
                    connection.Open();

                    string quyre = "UPDATE Specialist SET status = @Value WHERE SpecialistId = @RecordID";

                    SQLiteCommand command = new SQLiteCommand(quyre, connection);

                    command.Parameters.AddWithValue("@Value", value);
                    command.Parameters.AddWithValue("@RecordID", recordid);

                    command.ExecuteNonQuery();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form fGeneral = new Form1();
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
