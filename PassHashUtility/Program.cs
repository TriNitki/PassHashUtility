using System;
using System.Security.Cryptography;
using System.Text;

namespace PassHashUtility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Validate number of possible arguments
            if (args.Length < 4 || args.Length > 5)
            {
                Console.WriteLine("Usage (-d is optional): my-awesome-utility -p <password> -s <salt> -d");
                throw new ArgumentException($"Wrong arguement amount: {args.Length}");
            }    

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

            Console.WriteLine($"Hashed Password: {hashedPassword}");
        }

        /// <summary>
        ///  Hash the input string using the SHA256 algorithm
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        static string ComputeSha256(string inputString)
        {
            byte[] hash;

            // Hash input string
            using (HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                byte[] byteString = Encoding.UTF8.GetBytes(inputString);
                hash = hashAlgorithm.ComputeHash(byteString);
            }
            
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
