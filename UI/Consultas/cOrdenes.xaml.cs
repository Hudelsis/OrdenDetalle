using OrdenDetalle.BLL;
using OrdenDetalle.Entidades;
using System.Collections.Generic;
using System.Windows;

namespace OrdenDetalle.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cOrdenes.xaml
    /// </summary>
    public partial class cOrdenes : Window
    {
        public cOrdenes()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ordenes>();
            listado = OrdenesBLL.GetList(c => true);
            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}