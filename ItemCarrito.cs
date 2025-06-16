using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    public class ItemCarrito
    {
        public Producto producto { get; set; }
        public int cantidad { get; set; }

        public decimal calcularSubtotal()
        {
            return producto.precio * cantidad;
        }
    }
}
