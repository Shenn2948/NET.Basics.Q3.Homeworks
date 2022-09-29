using System.Windows.Forms;

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

        private static string GetOutputMessage(string userName)
        {
            return $"Hello, {userName}";
        }

        private void InputTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (!(sender is TextBox textBox))
            {
                return;
            }

            string message = string.IsNullOrWhiteSpace(textBox.Text) ? OutPutPlaceholder : GetOutputMessage(textBox.Text);
            outputTextbox.Text = message;
        }
    }
}