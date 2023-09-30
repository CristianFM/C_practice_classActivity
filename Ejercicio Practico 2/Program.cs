using System.Diagnostics;

enum Dia { Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo}

interface ILibre
{
    public bool diaLibre { get; set; }
}

public class ToDoList : ILibre
{
    public bool diaLibre { get; set; }
    Dia dia;
    string[] dias = new string[7];
    string[] tareas = new string[7];

    public void SaveDaysArray()//extraccion de los string del enum 
    {
        int i = 0;
        foreach ( string day in Enum.GetNames(typeof(Dia)))
        {
            dias[i] = day;    
            i++;
        }
    }

    public void AsignarTareaArray(int dia, string tareasAsignar)
    {
        //La idea es pasar el dia como posicion del array de list, e ir añadiendo a la list valores según sea necesario
        tareas[dia] = tareasAsignar;
    }

    public void MostrarTareas()
    {
        for (int i = 0; i < tareas.Length; i++)
        {
            if (tareas[i] != null) 
            {
                Console.WriteLine("El "+dias[i]+" tienes las siguientes tareas: ");
                Console.WriteLine();
                string[] tareaDiaria = tareas[i].Split('¡');
                for (int x = 0; x < tareaDiaria.Length - 1; x++)
                { 
                    Console.WriteLine(tareaDiaria[x]);
                }
                Console.WriteLine();
                Console.WriteLine("-----------------");
                Console.WriteLine();
            }
            else 
            {
                Console.WriteLine("El " + dias[i] + " es festivo. ");
                Console.WriteLine();
                Console.WriteLine("-----------------");
                Console.WriteLine();
                continue; 
            }
        }
    }

    public String AñadirTareas(int cantTareas)
    {
        string tareasDiarias = ""; //una string donde meter todas las tareas concatenadas
        for (int x = 0; x < cantTareas; x++)// iniciamos un bucle en el cual vamos a introducir tantas strings como tareas en un string concatenado
        {
            Boolean aux = true;
            double number;
            while (aux)
            {
                Console.WriteLine();
                Console.WriteLine("Escribe la tarea a realizar y pulsa 'Enter' para pasar a la siguiente");
                string data = Console.ReadLine().ToString(); //Control de errrores
                if (data == null || data == "" || data == " ")
                {
                    Console.WriteLine();
                    Console.WriteLine("Escribe alguna tarea no lo dejes en blanco");
                    continue;
                }
                else if (double.TryParse(data, out number))
                {
                    Console.WriteLine();
                    Console.WriteLine("No escribas numeros, escribe la tarea de este dia");
                    continue;
                }
                else
                {
                    tareasDiarias += data + "¡"; // confirmacion y adicion de strings a la cadena
                    aux = false;
                }
            }
        }
        return tareasDiarias;

    }

     static void Main(string[] args)
     {
        //Comienzo de la ejecucion del programa
        //cargamos variables e instancias
        ToDoList toDoList = new ToDoList(); 
        Console.WriteLine("Planea tu semana:");
        Console.WriteLine();
        //creo una array con los dias de la semana a partir del enum
        toDoList.SaveDaysArray();

        //inicio del bucle de asignacion de tareas semanales
        for(int i=0;i<toDoList.dias.Length;i++ ) { 
            diaFestivo:
            Console.WriteLine("El dia " + toDoList.dias[i] + " es fiesta?");
            Console.WriteLine("Si/No");
            string answer = Console.ReadLine();
            switch (answer)
            {
                case "Si":
                case "si":
                case "s":
                    Console.WriteLine();
                    break;
                case "No":
                case "no":
                case "n":
                    Console.WriteLine();
                    AsignacionTareas:
                    //selecionamos cuantas tareas hay ese dia
                    Console.WriteLine("Cuantas tareas tienes el " + toDoList.dias[i] + "?");
                    int cantTareas;
                    if (!int.TryParse(Console.ReadLine(), out cantTareas)) // control de errrores
                        {
                        Console.WriteLine("Introduce un numero entero para asignar el numero de tareas de este dia");
                        Console.WriteLine();
                        goto AsignacionTareas;
                    }
                    
                    String tareasDiarias = toDoList.AñadirTareas(cantTareas);


                    Console.WriteLine();
                    confirmacionTareas:
                    Console.WriteLine("Estas conforme con la asignacion de tareas?"); // confirmacion de las tareas
                    Console.WriteLine("Si/No");
                    string flag = Console.ReadLine().ToString();
                    switch (flag)
                    {
                        case "Si":
                        case "si":
                        case "s":
                            // una vez confirmado se envia el string concatenado para almecenarlo en un array par su posterior uso
                            Console.WriteLine(); 
                            toDoList.AsignarTareaArray(i, tareasDiarias);
                            break;
                        case "No":
                        case "no":
                        case "n":
                            //si no estas conforme repites el proceso
                            goto AsignacionTareas;
                        default:
                            Console.WriteLine("No has introducido ninguna respuesta valida");
                            Console.WriteLine();
                            goto confirmacionTareas;
                    }
                    break;
                default:
                    Console.WriteLine("No has introducido ninguna respuesta valida");
                    Console.WriteLine();
                    goto diaFestivo;
            }
        }
        toDoList.MostrarTareas(); //visualizacion final de la tarea
    }
}
