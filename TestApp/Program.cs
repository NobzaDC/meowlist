namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");
            var generalDefaultPassword = BCrypt.Net.BCrypt.HashPassword("abc123!");

            Console.WriteLine("adminPasswordHash: " + adminPasswordHash);
            Console.WriteLine("generalDefaultPassword: " + generalDefaultPassword);
            Console.ReadLine();
        }
    }
}
