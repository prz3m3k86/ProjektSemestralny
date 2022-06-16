using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Data.Entity;

namespace ProjektSemestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        private Dictionary<int, string> sits = new Dictionary<int, string>();

        private void LoadFilms()
        {
            try
            {
                using (KinoEntities context = new KinoEntities())
                {
                    FilmsCombo.ItemsSource = context.filmy.ToList();
                    FilmsCombo.DisplayMemberPath = "tytul";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public Reservation()
        {
            InitializeComponent();

            MiejscaList.SelectionMode = SelectionMode.Multiple;

            try
            {
                LoadFilms();
            }
            catch
            {
                Info infobox = new Info("Nie można wczytać danych!");
                infobox.ShowDialog();
                return;
            }

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void FilmsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeanseList.Items.Clear();
            using (KinoEntities context = new KinoEntities())
            {
                var item = FilmsCombo.SelectedItem as filmy;
                var seanse = context.seanse.Where(s => s.id_filmu == item.id_filmu).Where(s => s.czas_rozpoczecia > DateTime.Now).ToList();
                foreach (var s in seanse)
                {
                    SeanseList.Items.Add(s.ToString());
                }
                TextDirector.Visibility = Visibility.Visible;
                TextDuration.Visibility = Visibility.Visible;
                TextLanguage.Visibility = Visibility.Visible;
                DirectorLabel.Content = item.nazwisko_rezysera;
                DurationLabel.Content = item.czas_trwania_minuty + " min";
                LanguageLabel.Content = item.jezyk;
            }
        }
        private void SeanseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (KinoEntities context = new KinoEntities())
            {
                if (SeanseList.SelectedItem != null)
                {
                    MiejscaList.Items.Clear();
                    sits.Clear();
                    NoSitsLabel.Content = "";

                    var curItem = SeanseList.SelectedItem.ToString().Split(' ');

                    int id_sali = Int32.Parse(curItem[5].ToString());
                    int id_seansu = Int32.Parse(curItem[2].ToString());

                    var miejsca = context.miejsca.Where(m => m.id_sali == id_sali).ToList();

                    foreach (var m in miejsca)
                    {
                        if (!context.zarezerwowane_miejsca.Where(o => o.id_seansu == id_seansu).Any(z => z.id_miejsca == m.id_miejsca))
                            sits.Add(m.id_miejsca, m.ToString());
                    }

                    foreach (var s in sits)
                    {
                        MiejscaList.Items.Add(s.Value);
                    }

                    if (MiejscaList.Items.Count == 0)
                    {
                        NoSitsLabel.Content = "Przepraszamy ! Brak wolnych miejsc !";
                    }
                }
                else
                    MiejscaList.Items.Clear();
            }
        }
        private void MakeReservation()
        
        {
            var list = MiejscaList.SelectedItems;

            var curItem = SeanseList.SelectedItem.ToString().Split(' ');

            using (KinoEntities context = new KinoEntities())
            {
                var reservation = new rezerwacje()
                {
                    id_seansu = Int32.Parse(curItem[2].ToString()),
                    typ_rezerwacji = "internetowa",
                    imie_klienta = ImieText.Text,
                    nazwisko_klienta = NazwiskoText.Text,
                    nr_telefonu = NrTelefonuText.Text,
                    czy_oplacone = false,
                    data_dokonania_rezerwacji = DateTime.Now
                };
                context.rezerwacje.Add(reservation);
                context.SaveChanges();

                var id_rezerwacji = context.rezerwacje.Max(r => r.id_rezerwacji);

                int i = 0;

                foreach (var l in list)
                {
                    var reservationSeats = new zarezerwowane_miejsca()
                    {
                        id_rezerwacji = id_rezerwacji,
                        id_seansu = Int32.Parse(curItem[2].ToString()),
                        id_miejsca = sits.FirstOrDefault(s => s.Value.Contains(l.ToString())).Key
                    };
                    context.zarezerwowane_miejsca.Add(reservationSeats);
                    i++;
                }

                context.SaveChanges();
            }
        }

        private void DokonajRezerwacjiButton_Click(object sender, RoutedEventArgs e)
        {
            if (MiejscaList.SelectedItems.Count != 0 && ImieText.Text.Length != 0 && NazwiskoText.Text.Length != 0 && NrTelefonuText.Text.Length == 9 && Int32.TryParse(NrTelefonuText.Text, out int num))
            {
                try
                {
                    MakeReservation();
                }
                catch (Exception)
                {
                    Info infobox = new Info("Błąd ! Rezerwacja nie powiodła się !");
                    infobox.ShowDialog();
                    this.Close();
                    return;
                }

                ErrorLabel.Content = "";
                using (KinoEntities context = new KinoEntities())
                {
                    Info infobox = new Info($"Sukces ! Twój numer rezerwacji to: {context.rezerwacje.Max(r => r.id_rezerwacji)}");
                    infobox.ShowDialog();
                }
                this.Close();
            }
            else
                ErrorLabel.Content = "Wypełnij poprawnie wszystkie pola!";
        }
    }
}
