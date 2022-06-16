using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika interakcji dla klasy CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : Window
    {
        /// <summary>
        /// Konstruktor okna anulowania rezerwacji
        /// </summary>
        public CancelReservation()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        // Metoda usuwająca rekordy z tabel "rezerwacje" oraz "zarezerwowane miejsca"
        private void DeleteReservation(int iD, int number)
        {
            using (KinoEntities context = new KinoEntities())
            {
                var reservation = context.rezerwacje.Where(r => r.id_rezerwacji == iD).Single();

                if (Int32.Parse(reservation.nr_telefonu) == number)
                {
                    context.zarezerwowane_miejsca.RemoveRange(context.zarezerwowane_miejsca.Where(z => z.id_rezerwacji == iD));

                    context.rezerwacje.Remove(reservation);

                    context.SaveChanges();
                }
                else
                    throw new Exception();

            }
        }

        // Wywołuje metodę usuwającą rezerwację
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Content = "";

            int id;
            int number;

            if (IdBox.Text.Length != 0 && NrBox.Text.Length == 9 && Int32.TryParse(IdBox.Text, out id) && Int32.TryParse(NrBox.Text, out number))
            {
                try
                {
                    DeleteReservation(id, number);
                }
                catch (Exception)
                {
                    Info infoBox = new Info("Nie znaleziono rezerwacji !");
                    infoBox.ShowDialog();
                    this.Close();
                    return;
                }
                Info infoBox2 = new Info("Twoja rezerwacja zostałą anulowana !");
                infoBox2.ShowDialog();
                this.Close();
            }
            else
                ErrorLabel.Content = "Wypełnij wszystkie pola !";
        }
    }
}
