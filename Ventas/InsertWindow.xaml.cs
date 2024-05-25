using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ventas
{
    /// <summary>
    /// Lógica de interacción para InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        public InsertWindow()
        {
            InitializeComponent();
            DataAccess dataAccess = new DataAccess();
            cboProductos.ItemsSource = dataAccess.GetProductos();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            DataAccess dataAccess = new DataAccess();
            Ventas ventas = new Ventas
            {
                Cliente = txtCliente.Text,
                IdProducto = int.Parse(cboProductos.SelectedValue?.ToString() ?? "0"),
                Cantidad = txtcantidad.Text,
                Monto = txtmonto.Text,
                Id = Convert.ToInt32(txtId.Text)
            };
            int resultado = dataAccess.Create(ventas);
            if (resultado > 0)
            {
                MessageBox.Show("Venta guardada correctamente");
            }
            this.Close();

        }


    }
}
