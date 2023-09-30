using static GameElement;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

public class Program
{
    int turno, maxTurnos; //Turno actual y máximo de turnos.
    bool end; //bool para saber si ha terminado o no.
    GameElement[] items; //Matriz de objetos de juego.
    Character player; //Objeto de jugador.

    void CheckTurn(Vector2 posActual, GameElement item) //Función para comprobar si estamos en la misma casilla que un elemento de juego.
 {
 Vector2 v = new Vector2();
 //Comprobamos distancia con el item.
 if (v.Distance(posActual.vector, item.pos.vector)<=2)
 {
//Feedback para el usuario.
 Console.WriteLine("Ves una " + item.name + " cerca,está a " + v.Distance(posActual.vector, item.pos.vector).ToString() +
" distancia.");
}
 //Comprobamos si está en la misma casilla y su nombre.
 if(v.Distance(posActual.vector, item.pos.vector)== 0 &&
item.name == "Gema")
 {
    end = true;
}
if (v.Distance(posActual.vector, item.pos.vector) == 0 &&
item.name == "Trampa")
{
    player.isDeath = true;
}
 }
 static void Main(string[] args)
{
    //Inicializamos e instanciamos variables.
    Program p = new Program();
    p.player = new Character();
    p.items = new GameElement[2]; //Cantidad de objetos.
    p.items[0] = new GameElement.Trap(); //Tipo de objeto.
    p.items[0].name = "Trampa"; //Nombre de objeto.
    p.items[1] = new GameElement.Gem();
    p.items[1].name = "Gema";
    p.maxTurnos = 5;
    p.player.isDeath = false;
    p.end = false;
    p.player.pos = new Vector2();
    p.player.pos.vector[0] = 0;
 p.player.pos.vector[1] = 0;
    //Bucle para asignar la posición de los objetos, también se puede hacer que no puedan aparecer juntos la trampa y la gema.
 for (int i = 0; i < p.items.Length; i++)
    {
        //En base al número de turnos habrá más o menos casillas.
         Vector2 holder = p.items[i].SetRandomPos(p.maxTurnos);
        //Si la posición del jugador y del objeto coinciden volvemos a darle el valor y restamos una iteración.
 if (holder == p.player.pos)
        {
            holder = p.items[i].SetRandomPos(p.maxTurnos);
            i--;
        }
        else
        {
            p.items[i].pos = holder;
        }
    }
    //Pista feedback para el usuario.
    Console.WriteLine("La pista de la gema en posición X es: "
   + p.items[1].pos.vector[1].ToString() /*+
p.items[1].pos.vector[0].ToString()*/);
    //Bucle de control de movimiento
    for (p.turno = 0; p.turno < p.maxTurnos; p.turno++)
    {
        //Feedback para el usuario.
        Console.WriteLine("Es el turno " + p.turno);
    Console.WriteLine("Estas en la posición " +
p.player.pos.vector[0].ToString() + ", " +
p.player.pos.vector[1].ToString());
        Console.WriteLine("Escribe la siguiente posición, escribe 1, 0 o - 1 para avanzar o retroceder, solo puedes avanzar de uno en uno");
        //Input de usuario acerca del movimiento.
        int x, y;
        Console.WriteLine("X");
        if (int.TryParse(Console.ReadLine(), out x))
        {
            //Si el valor x introducido es 1,-1 o 0.
            if (x == 1 || x == -1 || x == 0)
            {
                Console.WriteLine("Y");
                if (int.TryParse(Console.ReadLine(), out y))
                {
                    //Si el valor y introducido es 1,-1 o 0.
                    if (y == 1 || y == -1 || y == 0)
                    {
                        //Movemos al jugador si los valores se pueden admitir.
 p.player.Move(x, y);
                    }
                    else //Input no valido.
                    {
                        Console.WriteLine("Escribe '1','0' o'-1'");
                       
                        p.turno--;
                        continue;
                    }
                }
                else //Input no valido.
                {
                    Console.WriteLine("Escribe '1','0' o '-1'");
                   
                    p.turno--;
                    continue;
                }
            }
            else //Input no valido.
            {
                Console.WriteLine("Escribe '1','0' o '-1'");
                p.turno--;
                continue;
            }
        }
        else //Input no valido.
        {
            Console.WriteLine("Escribe '1','0' o '-1'");
            p.turno--;
            continue;
        }
        //Bucle de comprobación de posición respecto a los items.
    for (int i = 0; i < p.items.Length; i++)
        {
            p.CheckTurn(p.player.pos, p.items[i]);
        }
    //Comprobación de final.
 if (p.end)
        {
            Console.WriteLine("Has encontrado la gema, enhorabuena.");
 return;
        }
        else if (p.player.isDeath)
        {
            Console.WriteLine("Has caído en una trampa, has perdido.");
        return;
        }
    }
}
 }
 //Clase de posición, dos valores enteros, X e Y.
 class Vector2
{
    internal int[] vector = { 0, 0 };
    internal float Distance(int[] a, int[] b)
    {
        float r = MathF.Sqrt(MathF.Pow((b[0] - a[0]), 2) +
       MathF.Pow((b[1] - a[1]), 2));
        return r;
    }
}
 //Clase jugador, contiene una posición, un control de movimiento y un bool para saber si ha muerto.
 class Character
{
    public Vector2 pos;
    public bool isDeath;
    public virtual void Move(int a, int b)
    {
        pos.vector[0] += a;
        pos.vector[1] += b;
    }
}
//Elementos de juego, contiene una posición y un método para asignar una posición aleatoria en base a los turnos máximos.
 class GameElement
{
    public Vector2 pos;
    public string name;
    public Vector2 SetRandomPos(int posMax)
    {
        Random r = new Random();
        int x = r.Next(-posMax, posMax);
        int y = r.Next(-posMax, posMax);
        Vector2 pos = new Vector2();
        pos.vector[0] = x;
        pos.vector[1] = y;
        return pos;
    }
 //Clases de objetos
 public class Trap : GameElement
    { }
    public class Gem : GameElement
    { }
}