namespace AppConHilos2;

class Program
{
    static void Main(string[] args)
    {
        //Obtenemos el hilo actual e imprimimos algunas propiedades
        Thread currentThread = Thread.CurrentThread;
        currentThread.Name = "Hilo principal";

        //Establecemos su prioridad
        currentThread.Priority = ThreadPriority.Lowest;

        //Estableccemos si corre en segundo plano o no
        currentThread.IsBackground = false;

        //Imprimimos sus propiedades
        Console.WriteLine("Thread Id: {0}", currentThread.ManagedThreadId);
        Console.WriteLine("Thread Name: {0}", currentThread.Name);
        Console.WriteLine("Thread State: {0}", currentThread.ThreadState);
        Console.WriteLine("Es un thread background: {0}", currentThread.IsBackground);
        Console.WriteLine("Priority: {0}", currentThread.Priority);
        Console.WriteLine("Culture: {0}", currentThread.CurrentCulture.Name);
        Console.WriteLine("UI Culture: {0}", currentThread.CurrentUICulture.Name);
        Console.WriteLine();


        Thread workerThread = new Thread(new ParameterizedThreadStart(Print));
        workerThread.Name = "Hilo de Print";

        //Crear el token source
        CancellationTokenSource cts = new CancellationTokenSource();

        workerThread.Start(cts.Token);

        for(int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Principal thread: {i}");
            Thread.Sleep(200);
        }

        if(workerThread.IsAlive)
        {
            cts.Cancel();
        }
    }

    /// <summary>
    /// Este código es ejecutado por un hilo secundario
    /// </summary>
    static void Print(object? obj)
    {
        if(obj == null)
            return;

        CancellationToken token = (CancellationToken)obj;

        //Obtenemos el hilo actual e imprimimos algunas propiedades
        Thread currentThread = Thread.CurrentThread;

        //Establecemos su prioridad
        currentThread.Priority = ThreadPriority.Highest;

        //Estableccemos si corre en segundo plano o no
        currentThread.IsBackground = false;

        //Imprimimos sus propiedades
        Console.WriteLine("Thread Id: {0}", currentThread.ManagedThreadId);
        Console.WriteLine("Thread Name: {0}", currentThread.Name);
        Console.WriteLine("Thread State: {0}", currentThread.ThreadState);
        Console.WriteLine("Es un thread background: {0}", currentThread.IsBackground);
        Console.WriteLine("Priority: {0}", currentThread.Priority);
        Console.WriteLine("Culture: {0}", currentThread.CurrentCulture.Name);
        Console.WriteLine("UI Culture: {0}", currentThread.CurrentUICulture.Name);
        Console.WriteLine();

        for(int i = 11; i < 20; i++)
        {
            if(token.IsCancellationRequested)
            {
                Console.WriteLine("En la íteración {0}. la cancelación ha sido solicitada...");
                //Termina la operación for
                break;
            }

            Console.WriteLine($"Print thread: {i}");
            Thread.Sleep(1000);
        }
    }
}
