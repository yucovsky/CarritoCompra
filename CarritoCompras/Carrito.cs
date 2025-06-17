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

        public bool eliminarItem(int codigoProducto, int cantidad)
        {
            var item = items.FirstOrDefault(i => i.producto.codigo == codigoProducto);
            if (item != null)
            {
                if (cantidad >= item.cantidad)
                {
                    items.Remove(item);
                    return true;
                }
                else
                {
                    item.cantidad -= cantidad;
                    return false;
                }
            }
            return false;
        }

        public decimal calcularTotal()
        {
            return items.Sum(item => item.calcularSubtotal()) * 1.21m;
        }

        public void finalizarCompra()
        {
            foreach (var item in items)
            {
                item.producto.stock -= item.cantidad;
            }
            items.Clear();
        }
    }
}
