using System;

class Program
{
    static void Main(string[] args)
    {
        var tienda = new Tienda();
        tienda.inicializarDatos();
        tienda.mostrarMenu();
    }
}
