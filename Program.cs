using System;
using System.Text;
using System.Threading;

namespace _2._4
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            Smartphone s = new Smartphone();
            while (true)
            {
                if (s.locked)
                {
                    Console.WriteLine("Smartphone is locked!");
                    Console.Write("Please Enter a pin: ");
                    byte[] pin = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine());
                    if(!s.unlock_phone(pin)) counter++;
                    if(counter >= 3){ 
                        Console.WriteLine("Smartphone is locked. Try again in 5 s.");
                        Thread.Sleep(5000);
                    }
                }
                else
                {
                    Console.WriteLine("Smartphone is unlocked!");
                    Console.WriteLine("Do you want to lock? ");
                    switch (Console.ReadLine())
                    {
                        case "y": s.lock_phone(); break;
                        case "q": return;
                        default: Console.WriteLine("You can only lock, unlock or turn off the Smartphone!"); break;
                    }
                }
            }
        }
    }
}
