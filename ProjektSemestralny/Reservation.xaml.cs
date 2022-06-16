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
        public Reservation()
        {
            InitializeComponent();
            using (KinoEntities context = new KinoEntities())
            { 
                FilmsCombo.ItemsSource = context.filmy.ToList();
                FilmsCombo.DisplayMemberPath = "tytul";

            }
        }

        private void FilmsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeanseList.Items.Clear();
            using (KinoEntities context = new KinoEntities())
            {
                var item = FilmsCombo.SelectedItem as filmy;
                var seanse = context.seanse.Where(s => s.id_filmu == item.id_filmu).ToList();
                foreach (var s in seanse)
                {
                    SeanseList.Items.Add(s.ToString());
                }
            }
        }
        private void SeanseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (KinoEntities context = new KinoEntities())
            {

                if (SeanseList.SelectedItem != null)
                {
                    MiejscaList.Items.Clear();
                    string curItem = SeanseList.SelectedItem.ToString();

                    int id = Int32.Parse(curItem[24].ToString());
                    var miejsca = context.miejsca.Where(m => m.id_sali == id).ToList();

                    foreach (var m in miejsca)
                    {
                        MiejscaList.Items.Add(m.id_miejsca);
                    }
                }
                else
                    MiejscaList.Items.Clear();
            }
        }
    }
}
