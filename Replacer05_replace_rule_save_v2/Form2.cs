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
        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string replaceRule = ""; //新建一ㄍ空字串
            int n = dataGridView1.Rows.Count - 1; //有 n 列取代條件
            int y = 0; //While迴圈計數器
            while (y < n)
            {
                if (dataGridView1[0, y].Value != null)
                {
                    replaceRule += dataGridView1[0, y].Value.ToString();
                }
                replaceRule += "\t";
                if (dataGridView1[1, y].Value != null)
                {
                    replaceRule += dataGridView1[1, y].Value.ToString();
                }
                replaceRule += "\r\n";
                y = y + 1;
            }
            StreamWriter sw = new StreamWriter("Replace.conf");
            sw.Write(replaceRule);
            sw.Close();
            MessageBox.Show(replaceRule, "Rule Saved!");
            replaceRule = "";
            y = 0;
        }
    }
}
