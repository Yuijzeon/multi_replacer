using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Replacer
{
    public partial class FormReplace : Form
    {
        public FormReplace()
        {
            InitializeComponent();
            ReadRule("Replace.conf");
        }

        private void ReadRule(string RuleTittle)
        {
            //新增 TextReader 讀取 RuleTittle 的文件
            //參考"Blogger:無聲的世界"的代碼
            TextReader txtReader = new StreamReader(RuleTittle);
            while (txtReader.Peek() != -1)
            {
                string[] parts = txtReader.ReadLine().Split('\t');
                dataGridView1.Rows.Add(parts);
            }
            txtReader.Close();
        }

        private void WriteRule(string RuleTittle)
        {
            string replaceRule = ""; //新建一ㄍ空字串
            int datarows = dataGridView1.Rows.Count - 1; //有 n 列取代條件
            int counter = 0; //While迴圈計數器
            while (counter < datarows)
            {
                if (dataGridView1[0, counter].Value != null)
                {
                    replaceRule += dataGridView1[0, counter].Value.ToString();
                }
                replaceRule += "\t";
                if (dataGridView1[1, counter].Value != null)
                {
                    replaceRule += dataGridView1[1, counter].Value.ToString();
                }
                replaceRule += "\r\n";
                counter = counter + 1;
            }
            StreamWriter sw = new StreamWriter(RuleTittle);
            sw.Write(replaceRule);
            sw.Close();
            MessageBox.Show(replaceRule, "Rule Saved!");
        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteRule("Replace.conf");
        }

        private void deleteAllRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Configuration File|*.conf|All Files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ReadRule(openFileDialog.FileName.ToString());
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Configuration File|*.conf|All Files(*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WriteRule(saveFileDialog.FileName.ToString());
            }

        }
    }
}
