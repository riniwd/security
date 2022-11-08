using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Data.SQLite;


namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        
        
        public Form1()
        {
            Form2 form = new Form2();
            
            InitializeComponent();
            ConnectDB();
            dataGridView1.Columns.Remove(rin);
            MaximizeBox = false;
            form.Show();
            //System.Windows.Forms.Application.Run(new Form2());
        }
        

        private SQLiteConnection SQLiteConn;
        

        private void button1_Click(object sender, EventArgs e)
        {           
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Column1", "Column1");
            dataGridView1.Columns.Add("Column1", "Column1");
            dataGridView1.Columns.Add("Column1", "Column1");
            //if (dataGridView1.Columns.Count > 3) { dataGridView1.Columns.Remove(rin); }
           
            shifr();
            
            transp();
            string SQL = "INSERT INTO шифр VALUES(@Шифр, @Расшифровка) ";
            //SQL += textBox2.Text;
            SQLiteCommand cmd = new SQLiteCommand(SQL);
            cmd.Connection = SQLiteConn;

            //cmd.CommandText = SQL;
            //cmd.ExecuteNonQuery() ;
            cmd.Parameters.AddWithValue("Шифр", textBox2.Text);
            cmd.Parameters.AddWithValue("Расшифровка", textBox1.Text);
            cmd.ExecuteNonQuery();
        }

        public void transp()
        {
            textBox2.Clear();
            int i = 0;
            int j = 0;
            int N = Convert.ToInt32(textBox1.Text.Length);
            for (j = 0;j < dataGridView1.Columns.Count; j++)
            {
                for(i = 0; i < Convert.ToInt32(Math.Round(Convert.ToDouble(N / 3.0), MidpointRounding.ToEven)); i++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null) continue;
                    textBox2.Text += dataGridView1.Rows[i].Cells[j].Value.ToString(); 
                }
            }
        }
        public void transp1()
        {
            textBox2.Clear();
            int i = 0;
            int j = 0;
            int N = Convert.ToInt32(textBox1.Text.Length);
            
            for (j = 0; j < Convert.ToInt32(Math.Round(Convert.ToDouble(N / 3.0), MidpointRounding.ToEven)); j++)
            {
                
                for (i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    
                    if (dataGridView1.Rows[i].Cells[j].Value == null) continue;
                    textBox2.Text += dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
                
            }
        }
        public int shifr()
        {   
            int N = Convert.ToInt32(textBox1.Text.Length);
            string[,] mass = new string[N, N];
            int j = 0;
            int jn = 0;
            for (int i = 0; i < Convert.ToInt32(Math.Round(Convert.ToDouble(N/3.0), MidpointRounding.ToEven)); i++)
            {
                dataGridView1.Rows.Add();
                

                int G = N - jn;
               //MessageBox.Show(Convert.ToString(jn));
                int r = N - G;
                
              // MessageBox.Show(Convert.ToString(textBox1.Text[4]));
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    mass[i, j] = Convert.ToString(textBox1.Text[r]);                  
                    dataGridView1.Rows[i].Cells[j].Value = mass[i,j];
                   r++;
                    if (r == N) return 0;
                }
               jn = jn + j;
               
            }
            return 0;
        }
       
        public int shifr1()
        {
            int N = Convert.ToInt32(textBox1.Text.Length);
            string[,] mass = new string[N, N];
            int j = 0;
            int jn = 0;
            int num = 0;
            
            for (int i = 0; i < 3; i++)
            { 
                dataGridView1.Rows.Add();
                  
                int G = N - jn;
                //MessageBox.Show(Convert.ToString(jn));
                int r = N - G;

                // MessageBox.Show(Convert.ToString(textBox1.Text[4]));
               
                for (j = 0; j < Convert.ToInt32(Math.Round(Convert.ToDouble(N / 3.0 ), MidpointRounding.ToEven)); j++)
                {
                    
                    if (j >=3 )dataGridView1.Columns.Add("rin", "колонка" + $"{num}"); 
                    mass[j, i] = Convert.ToString(textBox1.Text[r]);
                    dataGridView1.Rows[i].Cells[j].Value = mass[j, i];
                    r++;
                    num++;
                    if (r == N) return 0;
                }
                jn = jn + j;
            }
            return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            dataGridView1.Rows.Clear();
            //if (dataGridView1.Columns.Count > 3) { while (i < dataGridView1.Columns.Count) { dataGridView1.Columns.RemoveAt(4); i++; } }

            //if (dataGridView1.Columns.Count > 3) { dataGridView1.Columns.Remove(rin); }
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Column1", "Column1");
            dataGridView1.Columns.Add("Column1", "Column1");
            dataGridView1.Columns.Add("Column1", "Column1");
            shifr1();
            transp1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            string filepathe = fileDialog.FileName.ToString();

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc = app.Documents.Open(filepathe);
            string data = doc.Content.Text;
            textBox1.Text = data;
            app.Quit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            dataGridView2.Rows.Clear();
            //ShowTable(SQL_AllTable());
            //GetTableColumns();
            // dataGridView2.Rows.Add();
            GetTableNames();
        }
        
        private void GetTableColumns()
        {
            string SQLQuery = "PRAGMA table_info(\"" + "шифр"+ "\");";
            SQLiteCommand command = new SQLiteCommand(SQLQuery, SQLiteConn);
            SQLiteDataReader read = command.ExecuteReader();
        }
        private void GetTableNames()
         {
             string SQLQuery = "SELECT name FROM sqlite_master WHERE type ='table' ORDER BY name;";
             SQLiteCommand command = new SQLiteCommand(SQLQuery, SQLiteConn);
             SQLiteDataReader reader = command.ExecuteReader();
             SQLiteCommand command1 = SQLiteConn.CreateCommand();
             command1.CommandText = "SELECT Шифр, Расшифровка FROM шифр;";
             SQLiteDataReader reader1 = command1.ExecuteReader();
             //Image img = null;
             while (reader1.Read())
             {
                dataGridView2.Rows.Add(reader1[0].ToString(), reader1[1].ToString());
               
            }
         }
        private string SQL_AllTable()
        {
            return "SELECT * FROM [шифр] order by 1;";
        }

        private void ConnectDB()
            {
                SQLiteConn = new SQLiteConnection("Data Source=D:\\СГУГиТ\\WindowsFormsApp1\\История шифров.db;Version=3;");
                SQLiteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = SQLiteConn;
            }
    }
}
