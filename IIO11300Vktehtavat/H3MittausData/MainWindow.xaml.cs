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
using System.Windows.Navigation;
using System.Windows.Shapes;
using JAMK.IT.IIO11300;

namespace H3MittausData
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
        //luodaanlista mittaus-oliota varten
        List<MittausData> mitatut;
    public MainWindow()
    {
      InitializeComponent();
      IniMyStuff();
    }
    private void IniMyStuff()
    {
      //omat ikkunaan liittyvät alustukset
      txtToday.Text = DateTime.Today.ToShortDateString();
            mitatut = new List<MittausData>();
    }

    private void btnSaveData_Click(object sender, RoutedEventArgs e)
    {
      //luodaan uusi mittausdata olio ja näytetään se käyttäjälle
      MittausData md = new MittausData(txtClock.Text, txtData.Text);
            //lbData.Items.Add(md); testausta varten
            mitatut.Add(md);
            ApplyChanges();
    }
        private void ApplyChanges()
        {
            lbData.ItemsSource = null;
            lbData.ItemsSource = mitatut;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MittausData.SaveToFile(txtFileName.Text, mitatut);
                MessageBox.Show("Tiedot tallennettu onnistuneesti " + txtFileName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            //haetaan käyttäjän antamasta tiedostosta mitatut arvot
            try
            {
                mitatut = MittausData.ReadFromFile(txtFileName.Text);
                ApplyChanges();
                MessageBox.Show("Tiedot haettu onnistuneesti tiedostosta" + txtFileName.Text);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnSerialize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JAMK.IT.IIO11300.Serialisointi.SerialisoiXml(txtFileName.Text, mitatut);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeSerialize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mitatut = JAMK.IT.IIO11300.Serialisointi.DeSerialisoiXml(txtFileName.Text);
                ApplyChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnSerializeBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JAMK.IT.IIO11300.Serialisointi.Serialisoi(txtFileName.Text, mitatut);
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnDeSerializeBin_Click(object sender, RoutedEventArgs e)
        {
            //kutsutaan deserialisoijaa , binäärideserialisoija metodi palauttaa viittauksen objektiin
            object obj = new object();
            try
            {
                JAMK.IT.IIO11300.Serialisointi.DeSerialisoi(txtFileName.Text, ref obj);
                // ja nyt koetaan castata object tyyppinen olio listaksi mittausdataa
                mitatut = (List<MittausData>)obj;
                ApplyChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
