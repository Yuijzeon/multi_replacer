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
            string replaceRule = "";
            int n = 0;
            //當取代條件為 null 時，跳出迴圈
            while (dataGridView1[0, n].Value != null
                | dataGridView1[1, n].Value != null)
            {
                if (dataGridView1[0, n].Value == null
                    & dataGridView1[1, n].Value != null)
                {
                    replaceRule += "\t";
                    replaceRule += dataGridView1[1, n].Value.ToString();
                    replaceRule += "\r\n";
                }
                else if (dataGridView1[0, n].Value != null 
                    & dataGridView1[1, n].Value == null)
                {
                    replaceRule += dataGridView1[0, n].Value.ToString();
                    replaceRule += "\t";
                    replaceRule += "\r\n";
                }
                else
                {
                    replaceRule += dataGridView1[0, n].Value.ToString();
                    replaceRule += "\t";
                    replaceRule += dataGridView1[1, n].Value.ToString();
                    replaceRule += "\r\n";
                }
                n = n + 1;
            }
            MessageBox.Show(replaceRule);
            StreamWriter sw = new StreamWriter("Replace.conf");
            sw.Write(replaceRule);
            sw.Close();
            MessageBox.Show("Saved!");
            replaceRule = "";
        }
    }
}
