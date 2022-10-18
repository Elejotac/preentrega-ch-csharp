

using preEntrega;
using preEntrega.ClasesBases;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


class Program
{
    static void Main(string[] args)
    {
        // Interfaz de cónsola.

        Console.WriteLine("Seleccione una opcion para ejecutar");
        Console.WriteLine("Opción A: Traer Usuario\r\nOpción B: Traer Producto\r\nOpción C: Traer Productos Vendidos\r\nOpción D: Traer Ventas\r\nOpcion E: Iniciar Sesion\r\nOpcion F: SALIR");
        Console.WriteLine("Elección: ");
        string inputletra = Console.ReadLine();

        while (inputletra.ToUpper() != "F")
        {

            switch (inputletra.ToUpper())
            {
                case "A" or "a":
                    Console.WriteLine("Ingrese el nombre de Usuario:");
                    var nombre = Console.ReadLine();
                    var resultado = GetUserByName(nombre);

                    Console.WriteLine("Id: " + resultado.Id.ToString());
                    Console.WriteLine("Nombre: " + resultado.Nombre);
                    Console.WriteLine("Apellido: " + resultado.Apellido);
                    Console.WriteLine("Nombre de Usuario: " + resultado.NombreUsuario);
                    Console.WriteLine($"Contraseña: {resultado.Contraseña}");
                    Console.WriteLine("Mail: " + resultado.Mail);
                    Console.WriteLine();
                    break;

                case "B" or "b":
                    Console.WriteLine("Ingrese el id de usuario:");
                    var id = Console.ReadLine();
                    GetItemsByUser(Convert.ToInt32(id));
                    break;

                case "C" or "c":
                    Console.WriteLine("Ingrese el id de Usuario del cual quiere saber los productos vendidos:");
                    var idu = Console.ReadLine();
                    GetProductosVendidosByIdUsuario(Convert.ToInt32(idu));
                    break;

                case "D" or "d":
                    Console.WriteLine("Ingrese el id de usuario del que quiere saber sus ventas:");
                    var idv = Console.ReadLine();
                    GetVentasByIdUsuario(Convert.ToInt32(idv));
                    break;

                case "E" or "e":

                    Console.WriteLine("Ingrese el nombre de usuario:");

                    var nombreUsuario = Console.ReadLine();

                    Console.WriteLine("Ingrese su contraseña:");
                    var Contraseña = Console.ReadLine();

                    loginByUserAndPassword(nombreUsuario, Contraseña);

                    Console.WriteLine("Ingreso Satisfactorio");

                    break;



                default:
                    Console.WriteLine("-Opción no válida, intente otra entre la A y la E");
                    break;
            }

            Console.WriteLine("Seleccione una opcion para ejecutar");
            Console.WriteLine("Opción A: Para traer el usuario\r\nOpción B: Para traer el producto\r\nOpción C: Para traer productos vendidos\r\nOpción D: Traer Ventas\r\nOpción E: Iniciar Sesión\r\nOpción F: SALIR");
            Console.WriteLine("Ingrese su opción: ");

            inputletra = Console.ReadLine();
            Console.Clear();
        }
    }

    // (A) Método para traer usuario a través del nombre.

    public static Usuario GetUserByName(string nombre)
    {
        SqlConnectionStringBuilder connectionStringBuilder = new();
        connectionStringBuilder.DataSource = "LJ-PC";
        connectionStringBuilder.InitialCatalog = "SistemaGestion";
        connectionStringBuilder.IntegratedSecurity = true;
        var cs = connectionStringBuilder.ConnectionString;

        var u = new Usuario();

        using (SqlConnection connection = new SqlConnection(cs))
        {
            connection.Open();

            string sql = $"Select * from usuario where NombreUsuario = '{nombre}'";

            SqlCommand cmd = new SqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                u.Id = Convert.ToInt32(reader["Id"]);
                u.Nombre = reader["Nombre"].ToString();
                u.Apellido = reader["Apellido"].ToString();
                u.NombreUsuario = reader["NombreUsuario"].ToString();
                u.Contraseña = reader["Contraseña"].ToString();
                u.Mail = reader["Mail"].ToString();


            }
            reader.Close();
        }
        return u;
    }

    // (B) Metodo para traer los productos cargados por un usuario en particular.

    public static void GetItemsByUser(int id)
    {
        SqlConnectionStringBuilder connectionStringBuilder = new();
        connectionStringBuilder.DataSource = "LJ-PC";
        connectionStringBuilder.InitialCatalog = "SistemaGestion";
        connectionStringBuilder.IntegratedSecurity = true;
        var cs = connectionStringBuilder.ConnectionString;

        var productos = new List<Producto>();

        using (SqlConnection connection = new SqlConnection(cs))
        {
            connection.Open();

            string sql = $"Select * from Producto where IdUsuario = {id}";

            SqlCommand cmd = new SqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var p = new Producto();
                p.Id = Convert.ToInt32(reader.GetValue(0));
                p.Descripciones = reader.GetValue(1).ToString();
                p.Costo = Convert.ToDouble(reader.GetValue(2));
                p.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                p.Stock = Convert.ToInt32(reader.GetValue(4));
                p.IdUsuario = Convert.ToInt32(reader.GetValue(5).ToString());

                productos.Add(p);

            }

            reader.Close();
        }

        Console.WriteLine("-----Productos-----");
        foreach (var p in productos)
        {
            Console.WriteLine("*Id = " + p.Id);
            Console.WriteLine("*descripciones = " + p.Descripciones);
            Console.WriteLine("*costo = " + p.Costo);
            Console.WriteLine("*precioVenta = " + p.PrecioVenta);
            Console.WriteLine("*stock = " + p.Stock);
            Console.WriteLine("*idUsuario = " + p.IdUsuario);

            Console.WriteLine("________________");
        }

    }

    // (C) Método para traer los productos vendidos por un usuario.
    public static void GetProductosVendidosByIdUsuario(int id)
    {
        SqlConnectionStringBuilder connectionStringBuilder = new();
        connectionStringBuilder.DataSource = "LJ-PC";
        connectionStringBuilder.InitialCatalog = "SistemaGestion";
        connectionStringBuilder.IntegratedSecurity = true;
        var cs = connectionStringBuilder.ConnectionString;

        var productos = new List<ProductoConnect>();

        using (SqlConnection connection = new SqlConnection(cs))
        {
            connection.Open();

            string sql = $"select p.Id,p.Descripciones,v.Stock from Producto p inner join ProductoVendido v on v.IdProducto = p.Id where v.IdVenta = {id}";

            SqlCommand cmd = new SqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var p = new ProductoConnect();
                p.Id = Convert.ToInt32(reader.GetValue(0));
                p.Descripciones = reader.GetValue(1).ToString();
                p.Stock = Convert.ToInt32(reader.GetValue(2));

                productos.Add(p);

            }

            reader.Close();
        }
        Console.WriteLine("-----Productos Vendidos por el usuario-----");
        foreach (var p in productos)
        {
            Console.WriteLine("*Id = " + p.Id);
            Console.WriteLine("*Descripciones = " + p.Descripciones);
            Console.WriteLine("*Stock = " + p.Stock);

            Console.WriteLine("________________");
        }
    }

    // (D) Método para traer ventas por un id de un usuario.

    public static void GetVentasByIdUsuario(int id)
    {
        SqlConnectionStringBuilder connectionStringBuilder = new();
        connectionStringBuilder.DataSource = "LJ-PC";
        connectionStringBuilder.InitialCatalog = "SistemaGestion";
        connectionStringBuilder.IntegratedSecurity = true;
        var cs = connectionStringBuilder.ConnectionString;

        var ventas = new List<Venta>();

        using (SqlConnection connection = new SqlConnection(cs))
        {
            connection.Open();

            string sql = $"Select * from Venta where IdUsuario = {id}";

            SqlCommand cmd = new SqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var venta = new Venta();
                venta.Id = Convert.ToInt32(reader.GetValue(0));
                venta.Comentarios = reader.GetValue(1).ToString();
                venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));

                ventas.Add(venta);

            }

            reader.Close();
        }

        Console.WriteLine("-----VENTAS----");
        foreach (var v in ventas)
        {
            Console.WriteLine("*Id = " + v.Id);
            Console.WriteLine("*descripciones = " + v.Comentarios);
            Console.WriteLine("*idUsuario = " + v.IdUsuario);
            Console.WriteLine("________________");
        }

    }

    // (E) Método de Login.

    public static Usuario loginByUserAndPassword(string nombreUsuario, string Contraseña)
    {
        SqlConnectionStringBuilder connectionStringBuilder = new();
        connectionStringBuilder.DataSource = "LJ-PC";
        connectionStringBuilder.InitialCatalog = "SistemaGestion";
        connectionStringBuilder.IntegratedSecurity = true;
        var cs = connectionStringBuilder.ConnectionString;

        var u = new Usuario();
        
        bool estaLog = false;

        using (SqlConnection connection = new SqlConnection(cs))
        {
            connection.Open();

            string sql = $"Select * from usuario where NombreUsuario = '{nombreUsuario}' and Contraseña = '{Contraseña}'";

            SqlCommand cmd = new SqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    u.Id = Convert.ToInt32(reader.GetValue(0));
                    u.Nombre = reader.GetValue(1).ToString();
                    u.Apellido = reader.GetValue(2).ToString();
                    u.NombreUsuario = reader.GetValue(3).ToString();
                    u.Contraseña = reader.GetValue(4).ToString();
                    u.Mail = reader.GetValue(5).ToString();

                    estaLog = true;
                }
            }
            reader.Close();
            return u;
        }
    }
   
}



