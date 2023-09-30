using System.Collections;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace EjercicioPractico3
{

    //la clase program se encarga de la parte visual del ejercicio
    public class Program
    {

        static void Main(string[] args)
        {
               double cartera = 0;
               int id = 1;
               Program p = new Program();
           MenuTienda:
               Console.WriteLine("¿Que quieres hacer en la tienda?");
               Console.WriteLine("1 - Dar de alta un producto en a tienda");
               Console.WriteLine("2 - Ver los productos de la tienda");
               Console.WriteLine("3 - Comprar algo de la tienda");
               Console.WriteLine("4 - Lista de articulos comprados");
               Console.WriteLine("5 - Cerrar tienda");
               Console.WriteLine("");
               int menu = 0;
               if (!int.TryParse(Console.ReadLine(), out menu))
               {
                   Console.WriteLine("");
                   Console.WriteLine("Introduce el numero de kilos correctamente con un formato numerico");
                   Console.WriteLine("");
                   goto MenuTienda;
               }
               Console.WriteLine("");
               switch (menu)       //Menu del programa
               {
                   case 1:             //Añadir un producto a la tienda
                       bool flag = true;
                       while (flag)
                       {
                           String nombre = "";
                           double precio = 0.00;
                           bool flagPesoUnidad = true;// boolean para trabajar los output de consola de este producto
                           int unidades = 0;
                           double peso = 0.00;

                       SeleccionTipoProducto:
                           Console.WriteLine("Que clase de producto quieres añadir");
                           Console.WriteLine("1 - Snacks");
                           Console.WriteLine("2 - Frutos Secos");
                           Console.WriteLine("3 - Verduras/Frutas");
                           Console.WriteLine("4 - Cereales/Pan");
                           Console.WriteLine("5 - Bebidas");
                           int seleccion;
                           Console.WriteLine("");
                           if (!int.TryParse(Console.ReadLine(), out seleccion)) // control de errrores
                           {
                               Console.WriteLine("");
                               Console.WriteLine("Introduce un numero entero que corresponda a alguno de los productos");
                               Console.WriteLine();
                               goto SeleccionTipoProducto;
                           }
                           switch (seleccion)
                           {
                               case 1:
                               case 4:
                               case 5:
                                   flagPesoUnidad = true;
                                   break;
                               case 2:
                               case 3:
                                   flagPesoUnidad = false;
                                   break;
                               default:
                                   Console.WriteLine("No has introducido ninguna respuesta valida");
                                   Console.WriteLine();
                                   goto SeleccionTipoProducto;
                           }
                           // guardamos la seleccion para mas tarde crear un tipo de objeto correspondiente a la seleccion
                           //   y tener en cuenta si usamos peso o unidades

                           //por ahora guardaremos las variables comunes nombre y precio

                           nombre = p.NombreProducto();

                           precio = p.PrecioProducto();

                           //ahora usando la seleccion anterior solicitaremos peso o unidades dependiendo del tipo de producto seleccionado.

                           if (flagPesoUnidad)
                           {
                               unidades = p.UnidadesProducto();
                           }
                           else if (!flagPesoUnidad)
                           {
                               peso = p.PesoProducto();
                           }

                           // procedemos ha hacer una revision de los datos introducidos del producto
                           Console.WriteLine("");
                           Console.WriteLine("////Revision del producto////");
                           Console.WriteLine("Nombre: " + nombre);
                           Console.WriteLine("Precio: " + precio);
                           if (!flagPesoUnidad) { Console.WriteLine("Peso: " + peso); }
                           else { Console.WriteLine("Unidades: " + unidades); }
                           Console.WriteLine("");
                           Console.WriteLine("------------------");
                           Console.WriteLine("");
                           confirmacionAdicionTienda:
                           Console.WriteLine("¿Quieres añadir este producto a la tienda? s/si/n/no");
                           Console.WriteLine("");
                           string confirmacion = Console.ReadLine().ToLower().ToString();
                           switch (confirmacion)
                           {
                               case "s":
                               case "si":
                                   ListaTienda.AñadirProductoTienda(seleccion, nombre, id, precio, peso, unidades);
                                   id++;
                                   break;
                               case "n":
                               case "no":
                                   continue;
                                   break;
                               default:
                                   Console.WriteLine("No has introducido ninguna respuesta valida");
                                   Console.WriteLine();
                                   goto confirmacionAdicionTienda;
                           }

                           //Consutamos si añadmos mas productos, en caso contrario, mostramos la lista de la tienda
                           seguirComprando:
                           Console.WriteLine("");
                           Console.WriteLine("¿Quieres añadir mas productos? s/si/n/no");
                           string answer = Console.ReadLine().Trim().ToString();
                           Console.WriteLine("");
                           Console.WriteLine("---------------------");
                           switch (answer)
                           {
                               case "s":
                               case "si":
                                   continue;
                               case "n":
                               case "no":
                                   flag = false;
                                   break;
                               default:
                                   Console.WriteLine("No has introducido ninguna respuesta valida");
                                   Console.WriteLine();
                                   goto seguirComprando;
                           }
                       }
                       goto MenuTienda;
                   case 2:             //ver los productos de la tienda actuales
                       Console.WriteLine("");
                       Console.WriteLine("Visualizado de productos");
                       ListaTienda.RecorreListaProductos();
                       goto MenuTienda;
                   case 3:             //compra de productos
                       if (true) { 

                           //añadimos dinero a la cartera

                           DineroCartera:
                           Console.WriteLine("¿Cuanto dinero tienes en la cartera?");
                           Console.WriteLine("");

                           cartera = 0;     
                           if (!double.TryParse(Console.ReadLine(), out cartera))
                           {
                               Console.WriteLine("");
                               Console.WriteLine("Introduce el que tienes en la cartera con un formato numerico");
                               Console.WriteLine("");
                               goto DineroCartera;
                           }
                           ShopTime:
                           Console.WriteLine("");
                           Console.WriteLine("Tienes "+cartera+" euros en la cartera.");

                           //miramos que comprar en la tienda

                           Console.WriteLine("--------------------");
                           Console.WriteLine("¿Que producto deseas comprar?.");
                           ListaTienda.RecorreListaProductos();
                           Console.WriteLine("Selecciona la id del producto.");
                           int idItem = 0;
                           if (!int.TryParse(Console.ReadLine(), out idItem))
                           {
                               Console.WriteLine("");
                               Console.WriteLine("Introduce la Id del producto correctamente con un formato numerico");
                               Console.WriteLine("");
                               goto ShopTime;
                           }
                           cartera = cartera - ListaTienda.ComprarProductos(idItem, cartera);
                           Console.WriteLine();
                           Console.WriteLine("Te quedan "+cartera+" euros en la cartera.");    //Notifica lo que queda en la cartera
                           Console.WriteLine();
                           keepShoping:
                           Console.WriteLine("¿Quieres seguir comprando? s/si/n/no");
                           string a = Console.ReadLine().ToLower().ToString();
                           switch (a)
                           {
                               case "s":
                               case "si":
                                   goto ShopTime;
                                   break;
                               case "n":
                               case "no":
                                   goto MenuTienda;
                               default:
                                   Console.WriteLine("");
                                   Console.WriteLine("No has introducido ninguna respuesta valida");
                                   Console.WriteLine();
                                   goto keepShoping;

                           }
                       }
                       break;
                   case 4:                             //Lista de articulos comprados
                       Console.WriteLine("");
                       ListaTienda.RecorrerListaCompra();
                       goto MenuTienda;
                   case 5:                                      //cerrado de tienda
                       Console.WriteLine("");
                       Console.WriteLine("Cerrando Tienda");
                       break;
                   default:
                       Console.WriteLine("");
                       Console.WriteLine("No has introducido ninguna respuesta valida");
                       Console.WriteLine();
                       goto MenuTienda;
               }

            
        }
        public string NombreProducto()
        {
            bool flagname = true;
            string name = "";
            while (flagname)
            {
                Console.WriteLine("");
                Console.WriteLine("Escribe el nombre del producto / numeros, letras y caracteres especiales permitidos().");
                Console.WriteLine("Ejemplo: Patatas fritas, Vino reserva del 67, Pocion de Vida ++ ");
                name = Console.ReadLine().ToLower().ToString();
                name = char.ToUpper(name[0]) + name.Substring(1);
                if (name == null || name == "" || name == " ")
                {
                    Console.WriteLine();
                    Console.WriteLine("Escribe el nombre de algun producto correctamente.");
                    continue;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("El nombre del producto sera " + name + ".");
                    Console.WriteLine("");
                    Console.WriteLine("---------------------");
                    flagname = false;
                }
            }
            return name;
        }

        public double PrecioProducto()
        {
            double price = 0.00;
            bool flagPrecio = true;
            while (flagPrecio)
            {
                Console.WriteLine("");
                Console.WriteLine("Establezcamos ahora el precio del producto. Utiliza ',' en lugar de '.', por favor. ");
                Console.WriteLine("");
                if (!double.TryParse(Console.ReadLine(), out price)) // control de errrores
                {
                    Console.WriteLine("");
                    Console.WriteLine("Introduce un precio correcto para el producto");
                    Console.WriteLine();
                    continue;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("El precio del producto sera " + price + " euros.");
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------");
                    flagPrecio = false;
                }
            }
            return price;
        }

        public int UnidadesProducto()
        {
            int unit = 0;
            bool flagUnidades = true;
            while (flagUnidades)
            {
                Console.WriteLine("");
                Console.WriteLine("Cuantas unidades del producto va a introducir.");
                Console.WriteLine("");
                if (!int.TryParse(Console.ReadLine(), out unit)) // control de errrores
                {
                    Console.WriteLine("");
                    Console.WriteLine("Introduce el numero de unidades que habra del producto");
                    Console.WriteLine();
                    continue;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("la cantidad del producto sera " + unit + " unidades.");
                    Console.WriteLine("");
                    Console.WriteLine("-----------------");
                    flagUnidades = false;

                }
            }
            return unit;
        }

        public double PesoProducto()
        {
            double weight = 0.00;
            bool flagPeso = true;
            while (flagPeso)
            {
                Console.WriteLine("");
                Console.WriteLine("Cuantos kilos del producto vas a introducir a la tienda.");
                if (!double.TryParse(Console.ReadLine(), out weight)) // control de errrores
                {
                    Console.WriteLine("");
                    Console.WriteLine("Introduce el numero de kilos que habra del producto corectamente");
                    Console.WriteLine();
                    continue;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("la cantidad del producto sera " + weight + " kilos.");
                    Console.WriteLine("");
                    Console.WriteLine("------------------");
                    flagPeso = false;
                }
            }
            return weight;
        }

    }

    //-------------------------------------------------------------------------------

    //las siguientes clases se encargan de la parte logica


    //Jerarquia de los objetos Items
    public abstract class ItemG
    {
        public abstract string nombre { get; set; }
        public abstract int id { get; set; }
        public abstract double precio { get; set; }
        public abstract double peso { get; set; }
        public abstract int unidades { get; set; }
        public ItemG(String name, int id, double price, double weight, int unit)
        {
            nombre = name;
            this.id = id;
            precio = price;
            peso = weight;
            unidades = unit;
        }
        public abstract void SetItemNameId(string name, int id);
        public abstract void AñadirProducto(string name, int id, double pesoUnidades, double precio);


        public class ItemsAPeso : ItemG
        {


            public override double precio { get; set; }
            public override string nombre { get; set; }
            public override int id { get; set; }
            public override double peso { get; set; }
            public override int unidades { get; set; }
            public ItemsAPeso(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
            {
                nombre = name;
                this.id = id;
                precio = price;
                peso = weight;  
            }
            public void SetPesoPrecio(double precio, double peso)
            {
                this.precio = precio;
                this.peso = peso;
            }
            public double CalcularPrecioAPeso(double peso, double precio)
            {
                return peso * precio;
            }

            public override void SetItemNameId(string name, int id)
            {
                this.nombre = name;
                this.id = id;
            }
            public override void AñadirProducto(string name, int id, double pesoUnidades, double precio)
            {
                SetItemNameId(name, id);
                SetPesoPrecio(precio, pesoUnidades);
            }

            public class FrutaVerdura : ItemsAPeso
            {
                public FrutaVerdura(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
                {
                    nombre = name;
                    this.id = id;
                    precio = price;
                    peso = weight;
                    unidades = unit;
                }
            }
            public class FrutosSecos : ItemsAPeso
            {
                public FrutosSecos(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
                {
                    nombre = name;
                    this.id = id;
                    precio = price;
                    peso = weight;
                    unidades = unit;
                }
            }
        }

        public class ItemsAUnidad : ItemG
        {


            //variables heredadas
            public override double precio { get; set; }
            public override string nombre { get; set; }
            public override int id { get; set; }
            public override double peso { get; set; }
            public override int unidades { get; set; }
            public ItemsAUnidad(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
            {
                nombre = name;
                this.id = id;
                precio = price;
                unidades = unit;
            }

            public void SetUnidadPrecio(double precio, int unidades)
            {
                this.precio = precio;
                this.unidades = unidades;
            }

            public double CalcularPrecioAUnidad(int unidades, double precio)
            {
                return unidades * precio;
            }

            public override void SetItemNameId(string name, int id)
            {
                this.nombre = name;
                this.id = id;
            }

            public override void AñadirProducto(string name, int id, double pesoUnidades, double precio)
            {
                SetItemNameId(name, id);
                int Unidades = (int)Math.Round(pesoUnidades);
                SetUnidadPrecio(precio, Unidades);
            }

            public class Snacks : ItemsAUnidad
            {
                public Snacks(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
                {
                    nombre = name;
                    this.id = id;
                    precio = price;
                    peso = weight;
                    unidades = unit;
                }
            }
            public class Cereales : ItemsAUnidad
            {
                public Cereales(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
                {
                    nombre = name;
                    this.id = id;
                    precio = price;
                    peso = weight;
                    unidades = unit;
                }
            }
            public class Bebidas : ItemsAUnidad
            {
                public Bebidas(string name, int id, double price, double weight, int unit) : base(name, id, price, weight, unit)
                {
                    nombre = name;
                    this.id = id;
                    precio = price;
                    peso = weight;
                    unidades = unit;
                }
            }
        }
    }

    //Lista de Items de la tienda
    public static class ListaTienda
    {
        public static List<ItemG> itemsTienda = new List<ItemG>();
        public static List<ItemG> listaCompra = new List<ItemG>();

        public static void AñadirProductoTienda(int seleccion, String name, int id, double price, double weight, int unit)
        {
            switch (seleccion)
            {
                case 1:
                    itemsTienda.Add(new ItemG.ItemsAUnidad.Snacks(name, id, price, weight, unit));
                    break;
                case 2:
                    itemsTienda.Add(new ItemG.ItemsAPeso.FrutosSecos(name, id, price, weight, unit));
                    break;
                case 3:
                    itemsTienda.Add(new ItemG.ItemsAPeso.FrutaVerdura(name, id, price, weight, unit));
                    break;
                case 4:
                    itemsTienda.Add(new ItemG.ItemsAUnidad.Cereales(name, id, price, weight, unit));
                    break;
                case 5:
                    itemsTienda.Add(new ItemG.ItemsAUnidad.Bebidas(name, id, price, weight, unit));
                    break;
            }
        }
        public static void RecorreListaProductos()
        {
            foreach (var Items in itemsTienda)
            {

                Console.WriteLine("-----------------");
                Console.WriteLine("Producto: " + Items.id);
                Console.WriteLine("Nombre: " + Items.nombre);
                Console.WriteLine("Precio: " + Items.precio);
                if (Items.peso == 0) { Console.WriteLine("Unidades: " + Items.unidades); }
                else { Console.WriteLine("Peso: " + Items.peso); }
                Console.WriteLine("");
            }
        }
        public static void RecorrerListaCompra()
        {
            Console.WriteLine("Lista de la Compra");
            foreach (var Items in listaCompra)
            {

                Console.WriteLine("-----------------");
                Console.WriteLine("Producto: " + Items.id);
                Console.WriteLine("Nombre: " + Items.nombre);
                if (Items.peso == 0) { Console.WriteLine("Unidades: " + Items.unidades); }
                else { Console.WriteLine("Peso: " + Items.peso); }
                Console.WriteLine("");
            }
        }
        public static double ComprarProductos(int id, double cartera)
        {
            double coste = 0;
            foreach (var Items in itemsTienda)
            {
                if (Items.id == id)
                {
                    if (Items.unidades == 0)    //compra porducto kilos
                    {
                    CompraKilos:
                        Console.WriteLine("¿Cuantos Kilos de "+ Items.nombre+" vas comprar?");
                        double kilos = 0;
                        if (!double.TryParse(Console.ReadLine(), out kilos))    //control errores
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Introduce el numero de kilos correctamente con un formato numerico");
                            Console.WriteLine("");
                            goto CompraKilos;
                        }
                        else if (kilos > Items.peso)    //revision existencias
                        {
                            Console.WriteLine("No tenemos tantos kilos para comprar, el maximo es: " + Items.peso);
                            goto CompraKilos;
                        }
                        else if (kilos*Items.precio >cartera)           //revision de dinero suficiente
                        {
                            double dineroFaltante = (kilos * Items.precio) - cartera;
                            Console.WriteLine("");
                            Console.WriteLine("No tienes dinero suficiente para comprar esa cantidad");
                            Console.WriteLine("te faltan "+ dineroFaltante+" euros para comprar la cantidad seleccionada");
                            goto CompraKilos;
                        }
                        else if (kilos == 0)  // si eligues no comprar nada te lleva al menu de compra de nuevo
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Decides no comprar nada al final");
                        }
                        else    // si todo esta correcto ejecuta la compra y lo guarda en la lista de la compra realizada
                        {
                            Console.WriteLine("");
                            Items.peso -= kilos;
                            Console.WriteLine("has comprado "+kilos+" kilos de "+Items.nombre+".");
                            Console.WriteLine("");
                            listaCompra.Add(new ItemG.ItemsAPeso(Items.nombre, Items.id,Items.precio,kilos,Items.unidades));
                            coste = kilos * Items.precio;
                        }
                    }
                    else                    //compra producto unidades
                    {
                    CompraUnidades:
                        Console.WriteLine("");
                        Console.WriteLine("¿Cuantas unidades de "+ Items.nombre+ " vas a comprar?");
                        int unidadesCompra = 0;
                        if (!int.TryParse(Console.ReadLine(), out unidadesCompra))
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Introduce el numero de unidades correctamente con un formato numerico");
                            Console.WriteLine("");
                            goto CompraUnidades;
                        }
                        if (unidadesCompra > Items.unidades) //reviso el numero de existencias
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No tenemos tantas unidades para comprar, el maximo es: " + Items.unidades);
                            goto CompraUnidades;
                        }
                        else if (unidadesCompra * Items.precio > cartera) // reviso el dinero de la cartera
                        {
                            Console.WriteLine("");
                            double dineroFaltante = (unidadesCompra * Items.precio) - cartera;
                            Console.WriteLine("No tienes dinero suficiente para comprar esa cantidad");
                            Console.WriteLine("te faltan " + dineroFaltante + " euros para comprar la cantidad seleccionada");
                            Console.WriteLine("");
                            goto CompraUnidades;
                        }
                        else if (unidadesCompra == 0) // si eligues no comprar nada te lleva al menu de compra de nuevo
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Decides no comprar nada al final");
                        }
                        else    // si todo esta correcto ejecuta la compra y lo guarda en la lista de la compra realizada
                        {
                            Items.unidades -= unidadesCompra;
                            Console.WriteLine("");
                            Console.WriteLine("has comprado " + unidadesCompra + " unidades de " + Items.nombre + ".");
                            listaCompra.Add(new ItemG.ItemsAPeso(Items.nombre, Items.id, Items.precio, Items.peso, unidadesCompra));
                            Console.WriteLine(Items.unidades +" unidades restantes en la tienda");
                            coste = unidadesCompra * Items.precio;
                            Console.WriteLine("");
                        }

                    }
                    
                }
            }
            return coste;
        }
    }

}
