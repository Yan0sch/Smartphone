using System;
using System.Text;

namespace _2._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Smartphone s = new Smartphone();
            while (true)
            {
                if (s.locked)
                {
                    Console.WriteLine("Smartphone is locked!");
                    Console.Write("Please Enter a pin: ");
                    byte[] pin = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine());
                    s.unlock_phone(pin);
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
