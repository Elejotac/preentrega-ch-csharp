using preEntrega;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace preEntrega
{
    public class ProductoConnect
{
    public int Id { get; set; }
    public string Descripciones { get; set; }
    public int Stock { get; set; }
}
}