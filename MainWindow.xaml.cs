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
using OrdenDetalle.UI.Registro;
using OrdenDetalle.UI.Consultas;

namespace OrdenDetalle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void rPedidosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPedidos ventana = new rPedidos();
            ventana.Show();
        }
        private void cPedidosMenuItem_Click(object sender, RoutedEventArgs e)
        {
             cOrdenes ventana = new cOrdenes();
             ventana.Show();
        }

        
        private void cSuplidoresMenuItem_Click(object sender, RoutedEventArgs e)
        {
             cSuplidores ventana = new cSuplidores();
             ventana.Show();
        }

        private void cProductosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cProductos ventana = new cProductos();
            ventana.Show();
        }

    }
    


}
