using System;
using System.Runtime.ConstrainedExecution;

namespace EjercicioPractico1
{ 
    internal class Program
    {
        private static void Main(string[] args)
        {
            Program instance = new Program();
            //instance.PuntoUno();
            //instance.PuntoDos();
            instance.PuntoTres();
        }
        public void PuntoUno()
        {
            Punto1 punto1 = new Punto1();
            Boolean flag=false;
        while (flag == false)
        {
            //parte1
            punto1.setNumbers();
            punto1.division();
            //parte2
            punto1.setNameScript();
            punto1.setCh(punto1.extractFirstLetterName(punto1.getName()));
            punto1.setPi(3.14f);
            punto1.finalMessage();
            Console.WriteLine();
            Console.WriteLine("Quieres cerrar el programa? ");
            string answer = Console.ReadLine();
            if (answer == "Si" || answer == "s" || answer == "si")//si el nombre es confirmado cambio en el boolean
            {
                    
                flag = true;
            }
        }
    }
        public void PuntoDos()
    {
            Punto2 punto2 = new Punto2();
            Boolean flag=false;
        while (flag == false)
        {
            //parte1
            punto2.asingRandomValues();
            punto2.changeValues();
            Console.WriteLine();
            Console.WriteLine("Quieres cerrar el programa? ");
            string answer = Console.ReadLine();
            if (answer == "Si" || answer == "s" || answer == "si")//si el nombre es confirmado cambio en el boolean
            {
                    
                flag = true;
            }
        }
    }
        public void PuntoTres()
        {
            Punto3 punto3 = new Punto3();
            Console.WriteLine("Introduce cuantas monedas tienes");
            punto3.startingGold(long.Parse(Console.ReadLine()));
            punto3.coinsCount();
            Console.WriteLine("Introduce cantidad de monedas a restar en valor de monedas de cobre");
            punto3.subtractCoins(long.Parse(Console.ReadLine()));
            Console.WriteLine("Introduce cantidad de monedas a sumar en valor de monedas de cobre");
            punto3.sumCoins(long.Parse(Console.ReadLine()));
        }

    }
        public class Punto1
        {
        //variables
            //parte 1
            float a, b;
            double ab;
            //parte 2
            static float pi;
            char ch;
            string name;

        //funciones
            //parte1
        public void setNumbers()
        {
            float numero1 = 0;
            float numero2 = 0;
            Console.WriteLine("Intruduce el valor de a");
            numero1 = float.Parse(Console.ReadLine());
            setNumberA(numero1);
            Console.WriteLine();

            Console.WriteLine("Intruduce el valor de b");
            numero2 = float.Parse(Console.ReadLine());
            setNumberB(numero2);
                
            Console.WriteLine();
        }
        public void setNumberA(float value)
        {
            a = value;
        }
        public void setNumberB(float value)
        {
            b = value;
        }
        public float getNumberA()
        {
            return a;
        }
        public float getNumberB()
        {
            return b;
        }
        public void division()
        {
            ab = a / b;
            ab = Math.Round(ab, 5);
            Console.WriteLine("Division de los numeros a y b limitado a 5 decimales = "+ab);
            Console.WriteLine();
        }
        //parte2
        public void setNameScript()
        {
            //variables
            string nameValue = "";
            Boolean flag = false;
            while (flag == false)//comienzo del bucle para escribir el nombre
            {
                Console.WriteLine("Introduce un nombre");
                nameValue = "";
                while (nameValue == "")
                {
                    nameValue = Console.ReadLine();
                    if (nameValue == "")//control de errores null
                    {
                        Console.WriteLine("No dejes el espacio vacio estoy seguro que tienes un nombre");
                    }
                    Console.WriteLine();
                }            
                Console.WriteLine("El nombre elegido es = " + nameValue + " ,¿estas seguro?");//confirmacion del nombre
                Console.WriteLine("Si/No");
                string confirmacion = Console.ReadLine();
                Console.WriteLine();
                if (confirmacion == "Si" || confirmacion == "s" || confirmacion == "si")//si el nombre es confirmado cambio en el boolean
                {
                    setName(nameValue);
                    flag = true;
                }
            }
        }
        public char extractFirstLetterName(String value)
        {
            value = value.ToUpper();
            ch = value.ToCharArray()[0];
            return ch;
        }
        /*public void setNumberPiScript()
        {
            string piValue="";
            Console.WriteLine("escribe el numero de Pi con dos decimales");
            while (piValue != "3.14" || piValue!="3,14")
            {
                piValue = Console.ReadLine(); 
                if (piValue != "3.14" )
                {
                    Console.WriteLine("No sabes el numero? shhh.. te lo dire, es 3.14, intenta escribirlo de nuevo");
                }
                else
                {
                    setPi(3.14f);
                }
            }
            Console.WriteLine(getPi());
        }*/
        public void setName(string value)
        {
            name = value;
        }
        public string getName()
        {
            return name;
        }
        public void setPi(float value)
        {
            pi = value;
        }
        public float getPi() 
        {
            return pi;
        }
        public void setCh(char value)
        {
            ch = value;
        }
        public char getCh()
        {
            return ch;
        }
        public void finalMessage()
        {
            Console.WriteLine(" Mensaje final: ");
            Console.WriteLine();
            Console.WriteLine(" Numero a = " + getNumberA() + " Numero b = " +getNumberB());
            Console.WriteLine(" Resultado division ab con cinco decimales =" +ab);
            Console.WriteLine();
            Console.WriteLine(" Nombre = " + getName());
            Console.WriteLine();
            Console.WriteLine(" Pi ="+getPi());
            Console.WriteLine();
            Console.WriteLine(" Mensaje de "+getCh()+". "+getName());
        }
    }

        public class Punto2
        {
        //variables
            int a, b;

        //funciones
        
        public void asingRandomValues()
        {
            Random rnd = new Random();
            a = rnd.Next(0, 20);
            b = rnd.Next(0, 20);    
        }
        public void changeValues()
        {
            Console.WriteLine("los actuales valores de a y be son: a= "+a+" b= "+b);
            int[] numerosAux = new int[] {a,b};
            a =numerosAux[1];
            b =numerosAux[0];
            Console.WriteLine("los nuevos valores de a y be son: a= " + a + " b= " + b);
        }
        }

        public class Punto3
    {
        //variables
        long coins;
        readonly long g = 100, s = 10, c = 1;
        long gold
        {
            get
            {
                return coins / g;
            }
        }
        long silver
        {
            get
            {
                return coins % g /10;
            } 
        }
        long copper
        {
            get
            {
                return coins % g % s;
            }
        }
    
        //funciones
        public void startingGold(long value)
        {
            coins = value;
        }
        public void coinsCount()
        {
            Console.WriteLine();
            Console.WriteLine("Monedas // Gold: " + gold + " | silver: " + silver + " | copper: " + copper);
            Console.WriteLine();
        }
        public void subtractCoins(long value)
        {
            coins -= value;
            coinsCount();
        }
        public void sumCoins(long value)
        {
            coins += value;
            coinsCount();
        }
    }
}
