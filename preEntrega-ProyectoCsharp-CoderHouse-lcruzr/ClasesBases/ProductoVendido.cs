using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preEntrega.ClasesBases
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int IdVenta { get; set; }

        public ProductoVendido()
        {
            Id = 0;
            IdProducto = 0;
            Stock = 0;
            IdVenta = 0;
        }
    }
}
