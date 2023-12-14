using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PassHashUtility
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            if (args.Length < 4 || args.Length > 5)
                throw new ArgumentException($"Wrong arguement amount: {args.Length}");

            string password = null;
            string salt = null;
            bool use3Des = false;

            for (int i = 0; i < args.Length; i += 2)
            {
                string arg = args[i];

                switch (arg)
                {
                    case "-p":
                        password = args[i + 1];
                        break;
                    case "-s":
                        salt = args[i + 1];
                        break;
                    case "-d":
                        use3Des = true;
                        break;
                    default:
                        throw new ArgumentException($"Incorrect arguement: {arg}");
                }
            }

            // Validate required parameters
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Password connot be Null or Empty");

            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException("Salt connot be Null or Empty");


            // Perform hashing
            string hashedPassword = ComputeSha256(ComputeSha256(password) + salt);

            // Output result
            Console.WriteLine($"Hashed Password: {hashedPassword}");
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: my-awesome-utility -p <password> -s <salt> -d");
        }

        static string ComputeSha256(string inputString)
        {
            byte[] hash;

            using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                byte[] byteString = Encoding.UTF8.GetBytes(inputString);
                hash = hashAlgorithm.ComputeHash(byteString);
            }
            
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
