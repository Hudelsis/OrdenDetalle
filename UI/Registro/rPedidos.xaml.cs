using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OrdenDetalle.Entidades;
using OrdenDetalle.BLL;
using OrdenDetalle.DAL;
using Microsoft.EntityFrameworkCore;

namespace OrdenDetalle.UI.Registro
{
    public partial class rPedidos : Window
    {
        private Ordenes Ordene = new Ordenes();

        public rPedidos()
        {
            InitializeComponent();
            IniciarComboboxes();
        }
        private void IniciarComboboxes()
        {
            SuplidorComboBox.ItemsSource = SuplidoresBLL.GetList();
            SuplidorComboBox.SelectedValuePath = "SuplidorId";
            SuplidorComboBox.DisplayMemberPath = "Nombres";

            ProductoComboBox.ItemsSource = ProductosBLL.GetList();
            ProductoComboBox.SelectedValuePath = "ProductoId";
            ProductoComboBox.DisplayMemberPath = "Descripcion";
            Limpiar();
            MontoTextBox.Text = "0";
        }
        private void Cargar()
        {
            SuplidorComboBox.SelectedIndex = Ordene.SuplidorId;
            this.DataContext = null;
            this.DataContext = Ordene;
        }
        private void Limpiar()
        {
            this.Ordene = new Ordenes();
            this.DataContext = Ordene;
        }
        private bool ValidarAgregar()
        {
            var prod  = ProductosBLL.Buscar(Convert.ToInt32(ProductoComboBox.SelectedValue));
            bool esValido = true;
            if (ProductoComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Seleccione un Producto", "Aviso",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (CantidadTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Introduce la cantidad", "Aviso",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (CostoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Introduce el costo", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private bool ValidarGuardar()
        {
            bool esValido = true;
            if (OrdenesDataGrid.Items.Count == 0)
            {
                esValido = false;
                MessageBox.Show("Debe Agregar ordenes", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return esValido;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes encontrado = OrdenesBLL.Buscar(Ordene.OrdenId);

            if (encontrado != null)
            {
                Ordene = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("No existe en la base de datos", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;
                Ordene.Monto += Convert.ToSingle(CostoTextBox.Text)*Convert.ToSingle(CantidadTextBox.Text);
                Ordene.Detalle.Add(new OrdenesDetalle(Ordene.OrdenId,
                Convert.ToInt32(ProductoComboBox.SelectedValue.ToString()),
                Convert.ToSingle(CantidadTextBox.Text), 
                Convert.ToSingle(CostoTextBox.Text)));
            Cargar();
            
            CostoTextBox.Clear();
            CantidadTextBox.Clear();
            
            
        }
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdenesDataGrid.Items.Count >= 1 && OrdenesDataGrid.SelectedIndex <= OrdenesDataGrid.Items.Count - 1)
            {
                OrdenesDetalle d = (OrdenesDetalle)OrdenesDataGrid.SelectedValue;
                Ordene.Monto -= d.Costo*d.Cantidad;
                Ordene.Detalle.RemoveAt(OrdenesDataGrid.SelectedIndex);
                Cargar();
            }
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Ordenes esValido = OrdenesBLL.Buscar(Ordene.OrdenId);

            return (esValido != null);
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarGuardar())
                return;
            bool paso = false;

            if (Ordene.OrdenId == 0)
                paso = OrdenesBLL.Guardar(Ordene);
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = OrdenesBLL.Guardar(Ordene);
                }
                    
                else
                    MessageBox.Show("No existe en la base de datos", "Información");
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transacciones exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transacciones  Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes existe = OrdenesBLL.Buscar(Ordene.OrdenId);

            if (existe == null)
            {
                MessageBox.Show("No existe el orden  en la base de datos", "Mensaje",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                OrdenesBLL.Eliminar(Ordene.OrdenId);
                MessageBox.Show(" Registro Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
        private void ProductoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var busc = ProductosBLL.Buscar(Convert.ToInt32(ProductoComboBox.SelectedValue));
            if(busc != null)
                CostoTextBox.Text = busc.Costo.ToString();
        }
    }
}
