﻿namespace AppConHilos3;

class Program
{
    static void Main(string[] args)
    {
        int workers, ports;

        ThreadPool.GetMaxThreads(out workers, out ports);
        Console.WriteLine($"Máximos hilos de trabajo: {workers}");
        Console.WriteLine($"Máximos puertos para hilos: {ports}");

        ThreadPool.GetMinThreads(out workers, out ports);
        Console.WriteLine($"Mínimos hilos de trabajo: {workers}");
        Console.WriteLine($"Mínimos puertos (completion port) para hilos: {ports}");

        ThreadPool.GetAvailableThreads(out workers, out ports);
        Console.WriteLine($"Hilos de trabajo disponibles: {workers}");
        Console.WriteLine($"Hilos de puerto (completion port) disponibles: {ports}");
    
        int processCount = Environment.ProcessorCount;
        Console.WriteLine($"No. de procesadores disponibles en el sistema: {processCount}");
        Console.WriteLine("------------------------");

        ThreadPool.QueueUserWorkItem(TareaDeFondo);

        ThreadPool.GetAvailableThreads(out workers, out ports);
        Console.WriteLine($"Hilos de trabajo disponibles despues del hilo 1: {workers}");

        Persona p = new Persona("César Lezama", 20, "Hombre");
        ThreadPool.QueueUserWorkItem(TareaDeFondoConParametro, p);

        ThreadPool.GetAvailableThreads(out workers, out ports);
        Console.WriteLine($"Hilos de trabajo disponibles al final: {workers}");

        Console.ReadKey();
    }

    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }

        public Persona(string nombre, int edad, string sexo)
        {
            this.Nombre = nombre;
            this.Edad = edad;
            this.Sexo = sexo;
        }
    }

    static void TareaDeFondo(Object? stateInfo)
    {
        Console.WriteLine($"Hilo 1: Hola soy un hilo sin uso de parametros desde ThreadPool.");
        Thread.Sleep(1500);
    }

    static void TareaDeFondoConParametro(Object? stateInfo)
    {
        if(stateInfo == null)
            return;
        
        Persona data = (Persona)stateInfo;
        Console.WriteLine($"Hilo 2: Hola {data.Nombre}, tu edad es {data.Edad}");
        Thread.Sleep(500);
    }

}

