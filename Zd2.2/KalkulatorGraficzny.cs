using System;
using System.Windows.Forms;

namespace KalkulatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            textBoxWynik.Text += button.Text;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxWynik.Text = "";
        }

        private void buttonRownaSie_Click(object sender, EventArgs e)
        {
            try
            {
                var wynik = new System.Data.DataTable().Compute(textBoxWynik.Text, null);
                textBoxWynik.Text = wynik.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
