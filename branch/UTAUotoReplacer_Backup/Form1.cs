using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UTAUotoReplacer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            ReadRule("Replace.conf");
        }

        private void ReadRule(string RuleTittle)
        {
            TextReader txtReader = new StreamReader(RuleTittle);
            while (txtReader.Peek() != -1)
            {
                string[] parts = txtReader.ReadLine().Split('\t');
                dataGridView2.Rows.Add(parts);
            }
            txtReader.Close();
        }

        private void ReadOto(string otoTittle)
        {
            TextReader txtReader = new StreamReader(otoTittle);
            while (txtReader.Peek() != -1)
            {
                string[] parts = txtReader.ReadLine().Split('=', ',');
                dataGridView1.Rows.Add(parts);
            }
            txtReader.Close();
        }

        private void WriteOto(string otoTittle)
        {
            string s = ""; //新建一ㄍ空字串
            int datarows = dataGridView1.Rows.Count - 1; //有 n 列資料
            int counter = 0; //While迴圈計數器
            while (counter < datarows)
            {
                s += GetDataGrid(0, counter);
                s += "=";
                s += GetDataGrid(1, counter);
                s += ",";
                s += GetDataGrid(2, counter);
                s += ",";
                s += GetDataGrid(3, counter);
                s += ",";
                s += GetDataGrid(4, counter);
                s += ",";
                s += GetDataGrid(5, counter);
                s += ",";
                s += GetDataGrid(6, counter);
                s += "\r\n";
                counter = counter + 1;
            }
            StreamWriter sw = new StreamWriter(otoTittle);
            sw.Write(s);
            sw.Close();
            MessageBox.Show(s, "oto Saved!");
        }

        private string GetDataGrid(int x, int y)
        {
            if (dataGridView1[x, y].Value != null)
            {
                return dataGridView1[x, y].Value.ToString();
            }
            else
            {
                return "";
            }
        }

        private string GetDataGrid2(int x, int y)
        {
            if (dataGridView2[x, y].Value != null)
            {
                return dataGridView2[x, y].Value.ToString();
            }
            else
            {
                return "";
            }
        }

        private void AddDataGrid(string s, int x, int y)
        {
            if (dataGridView1[x, y].Value != null)
            {
                s += dataGridView1[x, y].Value.ToString();
            }
        }

        private void ReplaceSrting(string s)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Initial File|*.ini|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                ReadOto(openFileDialog.FileName.ToString());
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "oto";
            saveFileDialog.Filter = "Initial File|*.ini|All Files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WriteOto(saveFileDialog.FileName.ToString());
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter = 0;
            int otoRows = dataGridView1.Rows.Count - 1; //有 n 列 oto
            while (counter < otoRows)
            {
                string s = GetDataGrid(1, counter);
                int counter2 = 0;
                int replaceRows = dataGridView2.Rows.Count - 1; //有 n 列取代條件
                while (counter2 < replaceRows)
                {
                    s = s.Replace(GetDataGrid2(0, counter2), GetDataGrid2(1, counter2));
                    dataGridView1[1, counter].Value = s;
                    counter2 = counter2 + 1;
                }
                ReplaceSrting(s);
                dataGridView1[1, counter].Value = s;
                counter = counter + 1;
            }
            MessageBox.Show("Finished!");
        }
    }
}
