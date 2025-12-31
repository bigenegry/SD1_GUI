using System;
using System.Windows.Forms;

namespace SD1_GUI
{
    public partial class KeyForm : Form
    {
        public KeyForm()
        {
            InitializeComponent();
        }

        // Expose the entered key to Form1
        public string KeyValue
        {
            get { return txtKey.Text; }
        }
    }
}
