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
            var electronica = new Categoria { nombre = "Electrónica", descripcion = "Dispositivos electrónicos" };
            var ropa = new Categoria { nombre = "Ropa", descripcion = "Prendas de vestir" };

            categorias.AddRange(new[] { electronica, ropa });

            productos.Add(new Producto
            {
                nombre = "Smartphone",
                precio = 500,
                stock = 10,
                categoria = electronica
            });

            productos.Add(new Producto
            {
                nombre = "Pantalón",
                precio = 45,
                stock = 15,
                categoria = ropa
            });
        }

        public void mostrarMenu()
        {
            var carrito = new Carrito();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n/MENÚ PRINCIPAL/");
                Console.WriteLine("1. Ver categorías");
                Console.WriteLine("2. Ver productos");
                Console.WriteLine("3. Agregar al carrito");
                Console.WriteLine("4. Ver carrito");
                Console.WriteLine("5. Eliminar del carrito");
                Console.WriteLine("9. Salir");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            mostrarCategorias();
                            break;
                        case 2:
                            mostrarProductos();
                            break;
                        case 3:
                            agregarAlCarrito(carrito);
                            break;
                        case 4:
                            mostrarCarrito(carrito);
                            break;
                        case 5:
                            eliminarDelCarrito(carrito);
                            break;
                        case 9:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese un número válido");
                }
            }
        }

        private void mostrarCategorias()
        {
            Console.WriteLine("\n/CATEGORÍAS:/");
            foreach (var cat in categorias)
            {
                Console.WriteLine($"- {cat.nombre}: {cat.descripcion}");
            }
        }

        private void mostrarProductos()
        {
            Console.WriteLine("\n/PRODUCTOS:/");
            foreach (var prod in productos)
            {
                Console.WriteLine($"[{prod.codigo}] {prod.nombre} - ${prod.precio} (Stock: {prod.stock})");
            }
        }

        private void agregarAlCarrito(Carrito carrito)
        {
            mostrarProductos();
            Console.Write("\nIngrese código de producto: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                var producto = productos.FirstOrDefault(p => p.codigo == codigo);

                if (producto != null)
                {
                    Console.Write("Cantidad: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                    {
                        if (producto.stock >= cantidad)
                        {
                            carrito.agregarItem(producto, cantidad);
                            Console.WriteLine("¡Agregado al carrito!");
                        }
                        else
                        {
                            Console.WriteLine("No hay suficiente stock");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cantidad inválida");
                    }
                }
                else
                {
                    Console.WriteLine("Producto no encontrado");
                }
            }
            else
            {
                Console.WriteLine("Código inválido");
            }
        }

        private void mostrarCarrito(Carrito carrito)
        {
            if (carrito.items.Count == 0)
            {
                Console.WriteLine("\nCarrito vacío");
                return;
            }

            Console.WriteLine("\n/TU CARRITO:/");
            foreach (var item in carrito.items)
            {
                Console.WriteLine($"[{item.producto.codigo}] {item.producto.nombre} x{item.cantidad} = ${item.calcularSubtotal()}");
            }
            Console.WriteLine($"TOTAL: ${carrito.calcularTotal()}");
        }

        private void eliminarDelCarrito(Carrito carrito)
        {
            if (carrito.items.Count == 0)
            {
                Console.WriteLine("El carrito está vacío");
                return;
            }

            mostrarCarrito(carrito);
            Console.Write("\nIngrese código de producto a eliminar: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                carrito.eliminarItem(codigo);
                Console.WriteLine("Producto eliminado");
            }
            else
            {
                Console.WriteLine("Código inválido");
            }
        }
    }
}
