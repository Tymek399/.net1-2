using System;
using System.Windows.Forms;

namespace DzielenieLiczbApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDziel_Click(object sender, EventArgs e)
        {
            try
            {
                // Sprawdź, czy dzielnik nie jest zerem
                if (textBoxDzielnik.Text == "0")
                {
                    throw new DivideByZeroException("Nie można dzielić przez zero.");
                }

                // Dzielenie liczb i wyświetlanie wyniku
                double dzielna = double.Parse(textBoxDzielna.Text);
                double dzielnik = double.Parse(textBoxDzielnik.Text);
                double wynik = dzielna / dzielnik;
                textBoxWynik.Text = wynik.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadzono nieprawidłowe dane.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DivideByZeroException ex)
            {
                
                System.Diagnostics.EventLog.WriteEntry("Application", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Wystąpił błąd podczas dzielenia przez zero.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.EventLog.WriteEntry("Application", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                MessageBox.Show("Wystąpił nieoczekiwany błąd.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
