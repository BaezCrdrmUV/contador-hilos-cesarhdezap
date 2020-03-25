using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Interfaz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int ContadorManual = 0;
        public int ContadorAutomatico = 0;
        Thread HiloContadorManual;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonContadorManual_Click(object sender, RoutedEventArgs e)
        {
            ContadorManual++;
            LabelContadorManual.Content = ContadorManual.ToString();
        }

        private void ButtonContadorAutomatico_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CheckboxUtilizarHilos.IsChecked)
            {
                HiloContadorManual = new Thread(new ThreadStart(HiloAutomatico));
                HiloContadorManual.Start();
            }
            else
            {
                while (!(bool)CheckboxUtilizarHilos.IsChecked)
                {
                    ContadorAutomatico++;
                    LabelContadorAutomatico.Content = ContadorAutomatico.ToString();
                }
            }
        }

        private void CheckboxUtilizarHilos_Checked(object sender, RoutedEventArgs e)
        {
            if (HiloContadorManual != null)
            {
                if (HiloContadorManual.IsAlive && !(bool)CheckboxUtilizarHilos.IsChecked)
                {
                    HiloContadorManual.Abort();
                }
            }
        }

        public void HiloAutomatico()
        {
            try
            {
                
                while (true)
                {
                    Thread.Sleep(100);
                    LabelContadorAutomatico.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => this.AumentarContadorAutomatico()));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public void AumentarContadorAutomatico()
        {
            ContadorAutomatico++;
            LabelContadorAutomatico.Content = ContadorAutomatico.ToString();
        }
    }
}
