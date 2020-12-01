using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTyper
{
    public partial class ManageEntryForm : Form
    {
        public string EntryName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string EntryValue
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public ManageEntryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
