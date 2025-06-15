using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Carrito
{
    public List<ItemCarrito> items { get; } = new List<ItemCarrito>();

    public void agregarItem(Producto producto, int cantidad)
    {
        //stock
        items.Add(new ItemCarrito { producto = producto, cantidad = cantidad });
    }

    public decimal calcularTotal()
    {
        // iva y descuentos
        return items.Sum(item => item.producto.precio * item.cantidad);
    }
}