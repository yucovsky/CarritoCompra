using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    public class Producto
    {
        private static int ultimoCodigo = 0;

        public int codigo { get; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public Categoria categoria { get; set; }

        public Producto()
        {
            codigo = ++ultimoCodigo;
        }
    }
}
