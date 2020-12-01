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
    public partial class ManageEntriesForm : Form
    {
        ManageEntryForm manageEntry;

        public ManageEntriesForm()
        {
            InitializeComponent();

            manageEntry = new ManageEntryForm();

            ReloadEntries();
        }

        protected int GetSelectedIndex()
        {
            if(listBox1.SelectedIndices.Count > 0)
            {
                int index = listBox1.SelectedIndices[0];
                return index;
            }
            return -1;
        }

        protected void ReloadEntries()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(AutoTypeEntryManager.Instance.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(manageEntry.ShowDialog() == DialogResult.OK)
            {
                AutoTypeEntry ate = new AutoTypeEntry(manageEntry.EntryName, manageEntry.EntryValue);
                AutoTypeEntryManager.Instance.Add(ate);
                AutoTypeEntryManager.Instance.Save();

                ReloadEntries();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            AutoTypeEntry ate = AutoTypeEntryManager.Instance[index];

            if(ate != null)
            {
                manageEntry.EntryName = ate.Name;
                manageEntry.EntryValue = ate.Decrypt(ate.EncryptedPassword);

                if(manageEntry.ShowDialog() == DialogResult.OK)
                {
                    ate.Name = manageEntry.EntryName;
                    ate.EncryptedPassword = ate.Encrypt(manageEntry.EntryValue);

                    AutoTypeEntryManager.Instance[index] = ate;
                    AutoTypeEntryManager.Instance.Save();

                    ReloadEntries();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();

            listBox1.Items.RemoveAt(index);
            AutoTypeEntryManager.Instance.RemoveAt(index);
            AutoTypeEntryManager.Instance.Save();

            ReloadEntries();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
