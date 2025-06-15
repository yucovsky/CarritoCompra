using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tienda
{
    public List<Categoria> categorias { get; } = new List<Categoria>();
    public List<Producto> productos { get; } = new List<Producto>();

    public void inicializarDatos()
    {
        // agregar categorias
        var electronica = new Categoria { nombre = "Electrónica", descripcion = "Productos electrónicos" };

        categorias.Add(electronica);
        productos.Add(new Producto { nombre = "Smartphone", precio = 500, stock = 10, categoria = electronica });
    }

    public void mostrarMenu()
    {
        Console.WriteLine("MENU DE OPCIONES:");
        //yuco: requisitos (mostrar productos, agregar al carrito, etc)
    }
}