using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoTyper
{
    public partial class Form1 : Form
    {
        ManageEntriesForm manageEntries;

        const int SW_RESTORE = 9;

        public Form1()
        {
            InitializeComponent();

            AutoTypeEntryManager.Instance.Init();

            RefreshOpenWindows();
            ReloadEntries();

            manageEntries = new ManageEntriesForm();
        }

        protected void RefreshOpenWindows()
        {
            comboBox2.Items.Clear();

            Process[] processes = Process.GetProcesses().Where(p => p.MainWindowTitle != string.Empty).ToArray();
            
            foreach(Process p in processes)
            {
                comboBox2.Items.Add(string.Format("{0} [{1}]", p.MainWindowTitle, p.ProcessName));
            }
        }

        protected void ReloadEntries()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(AutoTypeEntryManager.Instance.ToArray());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(manageEntries.ShowDialog() != DialogResult.Cancel)
            {
                ReloadEntries();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshOpenWindows();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoTypeEntry ate = (AutoTypeEntry)comboBox1.SelectedItem;
            string processInfo = comboBox2.Text;

            if(ate != null)
            {
                int start = processInfo.IndexOf("[");
                int end = processInfo.IndexOf("]");
                int length = (end - start);
                string processName = processInfo.Substring(start, length);
                processName = processName.Replace('[', ' ');
                processName = processName.Replace(']', ' ');
                processName = processName.Trim();

                Process[] pList = Process.GetProcessesByName(processName);

                if(pList.Length > 0)
                {
                    Process selectedProcess = null;

                    foreach(Process p in pList)
                    {
                        if(processInfo.Contains(p.MainWindowTitle))
                        {
                            selectedProcess = p;
                            break;
                        }
                    }

                    if(selectedProcess != null)
                    {
                        string decryptedStr = ate.Decrypt(ate.EncryptedPassword);

                        ShowWindow(selectedProcess.MainWindowHandle, SW_RESTORE);
                        SetForegroundWindow(selectedProcess.MainWindowHandle);

                        SendKeys.Send(decryptedStr);
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
