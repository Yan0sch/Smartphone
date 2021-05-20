using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace _2._4
{
    class Smartphone
    {
        public bool locked { get; private set; } = true;
        string path = @"pin";
        byte[] pin_hash;

        int lock_time = 5000;

        int counter = 0;
        public Smartphone()
        {
            // check if a file with a hash exists and load it
            if (File.Exists(path)) pin_hash = File.ReadAllBytes(path);

            // if no file exists generate a new pin
            else get_new_pin();
        }
        private void get_new_pin()
        {
            string pin;
            while (true)
            {
                // get a pin and check if it is valid (a number)
                Console.Write("Enter new pin: ");
                pin = Console.ReadLine();
                if (Int32.TryParse(pin, out _))
                {
                    // string to byte array
                    byte[] byte_pin = ASCIIEncoding.ASCII.GetBytes(pin);
                    // hash the pin
                    pin_hash = new SHA256Managed().ComputeHash(byte_pin);
                    File.WriteAllBytes(path, pin_hash);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nnew pin created!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a vaild pin!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public bool unlock_phone(byte[] pin)
        {
            // hash the pin
            byte[] new_hash = new SHA256Managed().ComputeHash(pin);

            // check if the hashs are the same
            // iterate over each byte and check if they are the same
            for (int i = 0; i < pin_hash.Length; i++)
            {
                if (pin_hash[i] != new_hash[i])
                {
                    // if three times the wrong pin lock the Smartpone for a increasing time
                    counter++;
                    if (counter >= 3)
                    {
                        counter = 0;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Smartphone is locked. Try again in {0} s.", lock_time / 1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(lock_time);
                        lock_time *= 2;
                    }
                    return false;
                }
            }

            // if the pin is correct unlock the Smartphone
            locked = false;
            return true;
        }

        public void lock_phone()
        {
            locked = true;
        }
    }
}