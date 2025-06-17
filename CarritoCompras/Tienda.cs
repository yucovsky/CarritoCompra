using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    public class Tienda
    {
        public List<Categoria> categorias { get; } = new List<Categoria>();
        public List<Producto> productos { get; } = new List<Producto>();

        public void inicializarDatos()
        {
            var celulares = new Categoria { nombre = "Celulares", descripcion = "Teléfonos móviles" };
            var notebooks = new Categoria { nombre = "Notebooks", descripcion = "Computadoras portátiles" };
            var tablets = new Categoria { nombre = "Tablets", descripcion = "Tablets" };
            var televisores = new Categoria { nombre = "Televisores", descripcion = "Televisores" };

            categorias.AddRange(new[] { celulares, notebooks, tablets, televisores });

            productos.Add(new Producto { nombre = "Nokia 1100", precio = 100, stock = 10, categoria = celulares });
            productos.Add(new Producto { nombre = "Samsung J2 Prime", precio = 300, stock = 10, categoria = celulares });

            productos.Add(new Producto { nombre = "MacBook Air M1", precio = 1200, stock = 8, categoria = notebooks });
            productos.Add(new Producto { nombre = "Samsung Galaxy Book 3", precio = 1000, stock = 5, categoria = notebooks });

            productos.Add(new Producto { nombre = "Lenovo Tab M10", precio = 600, stock = 15, categoria = tablets });
            productos.Add(new Producto { nombre = "iPad Mini", precio = 550, stock = 20, categoria = tablets });

            productos.Add(new Producto { nombre = "LED Phillips 32\"", precio = 1100, stock = 20, categoria = televisores });
            productos.Add(new Producto { nombre = "Smart TV Noblex 40\"", precio = 1400, stock = 18, categoria = televisores });
        }

        private int obtenerStockDisponible(Producto producto, Carrito carrito)
        {
            int enCarrito = carrito.items
                .Where(i => i.producto.codigo == producto.codigo)
                .Sum(i => i.cantidad);

            return producto.stock - enCarrito;
        }

        public void mostrarMenu()
        {
            var carrito = new Carrito();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n// MENÚ PRINCIPAL //");
                Console.WriteLine("1) Consultar categorías disponibles");
                Console.WriteLine("2) Consultar todos los productos");
                Console.WriteLine("3) Consultar productos por categoría");
                Console.WriteLine("4) Agregar producto al carrito");
                Console.WriteLine("5) Eliminar producto del carrito");
                Console.WriteLine("6) Ver mi carrito");
                Console.WriteLine("7) Ver total a pagar");
                Console.WriteLine("8) Finalizar compra");
                Console.WriteLine("9) Salir del programa");
                Console.Write("Elija una opción: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            mostrarCategorias();
                            break;
                        case 2:
                            mostrarProductos(carrito);
                            break;
                        case 3:
                            mostrarProductosPorCategoria();
                            break;
                        case 4:
                            agregarAlCarrito(carrito);
                            break;
                        case 5:
                            eliminarDelCarrito(carrito);
                            break;
                        case 6:
                            mostrarCarrito(carrito);
                            break;
                        case 7:
                            mostrarTotal(carrito);
                            break;
                        case 8:
                            finalizarCompra(carrito);
                            break;
                        case 9:
                            salir = true;
                            Console.WriteLine("Saliendo del programa...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor ingrese un número.");
                }
            }
        }

        private void mostrarCategorias()
        {
            Console.WriteLine("\n// CATEGORÍAS DISPONIBLES //");
            foreach (var categoria in categorias)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"{categoria.nombre}: {categoria.descripcion}");
                Console.WriteLine("----------------------------------------");
            }
        }

        private void mostrarProductos(Carrito carrito)
        {
            Console.WriteLine("\n// PRODUCTOS DISPONIBLES //");
            foreach (var producto in productos)
            {
                int disponible = obtenerStockDisponible(producto, carrito);
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"[{producto.codigo}] {producto.nombre} - ${producto.precio} - Disponible: {disponible}/{producto.stock} - Categoría: {producto.categoria.nombre}");
                Console.WriteLine("----------------------------------------");
            }
        }

        private void mostrarProductosPorCategoria()
        {
            Console.Write("\nIngrese el nombre de la categoría: ");
            string nombreCategoria = Console.ReadLine();

            var categoria = categorias.FirstOrDefault(c => c.nombre.Equals(nombreCategoria, StringComparison.OrdinalIgnoreCase));

            if (categoria != null)
            {
                var productosCategoria = productos.Where(p => p.categoria.nombre.Equals(categoria.nombre, StringComparison.OrdinalIgnoreCase));

                Console.WriteLine($"\n// PRODUCTOS DE {categoria.nombre.ToUpper()} //");
                foreach (var producto in productosCategoria)
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"[{producto.codigo}] {producto.nombre} - ${producto.precio} - Stock: {producto.stock}");
                    Console.WriteLine("----------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Categoría no encontrada.");
            }
        }

        private void agregarAlCarrito(Carrito carrito)
        {
            mostrarProductos(carrito);
            Console.Write("\nIngrese el código del producto: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                var producto = productos.FirstOrDefault(p => p.codigo == codigo);

                if (producto != null)
                {
                    Console.Write("Ingrese la cantidad: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                    {
                        int stockDisponible = obtenerStockDisponible(producto, carrito);

                        if (stockDisponible >= cantidad)
                        {
                            carrito.agregarItem(producto, cantidad);
                            Console.WriteLine("¡Producto agregado al carrito!");
                        }
                        else
                        {
                            Console.WriteLine($"No hay suficiente stock disponible. Stock actual: {stockDisponible}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cantidad no válida. Debe ser un número positivo.");
                    }
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Código no válido.");
            }
        }

        private void eliminarDelCarrito(Carrito carrito)
        {
            if (carrito.items.Count == 0)
            {
                Console.WriteLine("Carrito vacío.");
                return;
            }

            Console.WriteLine("\n// CARRITO DE COMPRAS //");
            foreach (var item in carrito.items)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"[{item.producto.codigo}] {item.producto.nombre} - Cantidad: {item.cantidad} - Precio unitario: ${item.producto.precio}");
                Console.WriteLine("----------------------------------------");
            }

            Console.Write("\nIngrese el código del producto a eliminar: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                var item = carrito.items.FirstOrDefault(i => i.producto.codigo == codigo);

                if (item != null)
                {
                    Console.Write($"Ingrese la cantidad a eliminar (actual: {item.cantidad}): ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                    {
                        if (cantidad > item.cantidad)
                        {
                            Console.WriteLine($"No puede eliminar más de {item.cantidad} unidades.");
                            return;
                        }

                        bool eliminadoCompleto = carrito.eliminarItem(codigo, cantidad);

                        if (eliminadoCompleto)
                        {
                            Console.WriteLine("Producto eliminado completamente del carrito.");
                        }
                        else
                        {
                            Console.WriteLine($"Se eliminaron {cantidad} unidades del producto.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cantidad no válida. Debe ser un número positivo.");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró un producto con ese código en el carrito.");
                }
            }
            else
            {
                Console.WriteLine("Código no válido.");
            }
        }

        private void mostrarCarrito(Carrito carrito)
        {
            if (carrito.items.Count == 0)
            {
                Console.WriteLine("\nCarrito vacío.");
                return;
            }

            Console.WriteLine("\n// CARRITO DE COMPRAS //");
            foreach (var item in carrito.items)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"[{item.producto.codigo}] {item.producto.nombre} - Cantidad: {item.cantidad} - Precio unitario: ${item.producto.precio} - Subtotal: ${item.calcularSubtotal()}");
                Console.WriteLine("----------------------------------------");
            }
        }

        private void mostrarTotal(Carrito carrito)
        {
            if (carrito.items.Count == 0)
            {
                Console.WriteLine("\nCarrito vacío.");
                return;
            }

            mostrarCarrito(carrito);
            Console.WriteLine($"\nTOTAL A PAGAR (IVA incluido): ${carrito.calcularTotal()}");
        }

        private void finalizarCompra(Carrito carrito)
        {
            if (carrito.items.Count == 0)
            {
                Console.WriteLine("\nCarrito vacío. No hay compra que finalizar.");
                return;
            }

            mostrarTotal(carrito);
            Console.WriteLine("\nConfirmando compra...");
            carrito.finalizarCompra();
            Console.WriteLine("¡Compra realizada con éxito!");
            Console.WriteLine("Gracias por su compra.\n");
        }
    }
}
