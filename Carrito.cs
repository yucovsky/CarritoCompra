using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    public class Carrito
    {
        public List<ItemCarrito> items { get; } = new List<ItemCarrito>();

        public void agregarItem(Producto producto, int cantidad)
        {
            var itemExistente = items.FirstOrDefault(i => i.producto.codigo == producto.codigo);

            if (itemExistente != null)
            {
                itemExistente.cantidad += cantidad;
            }
            else
            {
                items.Add(new ItemCarrito { producto = producto, cantidad = cantidad });
            }
        }

        public decimal calcularTotal()
        {
            // agregar iva
            return items.Sum(item => item.calcularSubtotal());
        }

        public void eliminarItem(int codigoProducto)
        {
            var item = items.FirstOrDefault(i => i.producto.codigo == codigoProducto);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}
