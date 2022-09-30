using System.Windows.Forms;
using NETStandard.Lib;

namespace NETFramework.WinFormsApp
{
    public partial class HelloWorldForm : Form
    {
        private const string OutPutPlaceholder = "please, provide a name";

        public HelloWorldForm()
        {
            InitializeComponent();
            outputTextbox.Text = OutPutPlaceholder;
        }

        private void InputTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (!(sender is TextBox textBox))
            {
                return;
            }

            string message = string.IsNullOrWhiteSpace(textBox.Text) ? OutPutPlaceholder : Utils.GetOutputMessage(textBox.Text);
            outputTextbox.Text = message;
        }
    }
}