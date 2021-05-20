using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace _2._4
{
    class Smartphone
    {
        public bool locked {get; private set;} = true;
        string path = @"pin";
        byte[] pin_hash = null;
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
            pin_hash = new SHA256Managed().ComputeHash(salt + pin);
            File.WriteAllBytes(path, pin_hash);
            Console.WriteLine("\nnew pin created!");
            Console.WriteLine("pin: {0}, hash: {1}", ASCIIEncoding.ASCII.GetString(pin), ASCIIEncoding.UTF8.GetString(pin_hash));
        }

        public bool unlock_phone(byte[] pin)
        {
            byte[] new_hash = new SHA256Managed().ComputeHash(pin);
            Console.WriteLine("{0}", ASCIIEncoding.UTF8.GetString(new_hash));
            for(int i = 0; i < pin_hash.Length;i++){
                if(pin_hash[i] != new_hash[i]) return false;
            }
            locked = false;
            return true;
        }

        public void lock_phone() {
            locked = true;
        }
    }
}