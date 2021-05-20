using System;
using System.Text;

namespace _2._4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new Smartphone object
            Smartphone s = new Smartphone();

            while (true)
            {   
                // When the Smartphone is locked, the program asks for a pin
                if (s.locked)
                {
                    // Change text color to red and say that the Smartphone is locked
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Smartphone is locked!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    // Ask for a pin
                    Console.Write("Please Enter a pin: ");
                    byte[] pin = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine());
                    s.unlock_phone(pin);
                }
                else
                {
                    // Change text color to green and say that the Smartphone is unlocked
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Smartphone is unlocked!");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Ask what to do next (lock, turn off or do noting)
                    Console.WriteLine("Do you want to lock? ");
                    switch (Console.ReadLine())
                    {
                        case "yes":
                        case "y": s.lock_phone(); break;
                        case "turn off":
                        case "quit":
                        case "q": return;
                        default: Console.WriteLine("You can only lock, unlock or turn off the Smartphone!"); break;
                    }
                }
            }
        }
    }
}
