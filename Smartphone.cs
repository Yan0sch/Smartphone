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
        byte[] pin_hash = null;

        int lock_time = 5000;

        int counter = 0;
        public Smartphone()
        {
            if (File.Exists(path))
            {
                pin_hash = File.ReadAllBytes(path);
            }

            if (pin_hash == null)
            {
                get_new_pin();
            }
        }
        private void get_new_pin()
        {
            Console.Write("Enter new pin: ");
            byte[] pin = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine());
            pin_hash = new SHA256Managed().ComputeHash(pin);
            File.WriteAllBytes(path, pin_hash);
            Console.WriteLine("\nnew pin created!");
        }

        public bool unlock_phone(byte[] pin)
        {
            byte[] new_hash = new SHA256Managed().ComputeHash(pin);
            for (int i = 0; i < pin_hash.Length; i++)
            {
                if (pin_hash[i] != new_hash[i])
                {
                    counter++;
                    if (counter >= 3)
                    {
                        counter = 0;
                        Console.WriteLine("Smartphone is locked. Try again in {0} s.", lock_time / 1000);
                        Thread.Sleep(lock_time);
                        lock_time *= 2;
                    }
                    return false;
                }
            }
            locked = false;
            return true;
        }

        public void lock_phone()
        {
            locked = true;
        }
    }
}