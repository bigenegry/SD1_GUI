using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SD1_GUI
{
    public partial class Form1 : Form
    {
        private string encryptionKey = "";

        public Form1()
        {
            InitializeComponent();
        }

        // ================= FILE → OPEN =================
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMain.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        // ================= FILE → SAVE AS =================
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, txtMain.Text);
            }
        }

        // ================= FILE → EXIT =================
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ================= TOOLS → KEY =================
        private void keyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (KeyForm keyForm = new KeyForm())
            {
                if (keyForm.ShowDialog() == DialogResult.OK)
                {
                    encryptionKey = keyForm.KeyValue;
                    MessageBox.Show(
                        "Encryption key set successfully.",
                        "Key",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        // ================= TOOLS → ENCRYPT =================
        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                MessageBox.Show("Please set the encryption key first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMain.Text))
            {
                MessageBox.Show("Nothing to encrypt.");
                return;
            }

            txtMain.Text = EncryptText(txtMain.Text, encryptionKey);
        }

        // ================= TOOLS → DECRYPT =================
        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(encryptionKey))
            {
                MessageBox.Show("Please set the encryption key first.");
                return;
            }

            try
            {
                txtMain.Text = DecryptText(txtMain.Text, encryptionKey);
            }
            catch
            {
                MessageBox.Show(
                    "Decryption failed.\nMake sure the same key was used.",
                    "Decrypt Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ================= ENCRYPT / DECRYPT CORE =================
        private string EncryptText(string input, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] result = new byte[inputBytes.Length];

            for (int i = 0; i < inputBytes.Length; i++)
            {
                result[i] = (byte)(inputBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Convert.ToBase64String(result);
        }

        private string DecryptText(string input, string key)
        {
            byte[] inputBytes = Convert.FromBase64String(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] result = new byte[inputBytes.Length];

            for (int i = 0; i < inputBytes.Length; i++)
            {
                result[i] = (byte)(inputBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Encoding.UTF8.GetString(result);
        }

        // ================= HELP → ABOUT =================
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "SD1 version 1.0\n" +
                "Creator: Ugochukwu Eric\n" +
                "Organization: VVK",
                "About SD1",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
