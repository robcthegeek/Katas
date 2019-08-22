using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Animalia
{
    internal static class Extensions
    {
        internal static Random rng = new Random();

        internal static string Random(this string[] things)
        {
            return things[rng.Next(0, things.Length)];
        }

        internal static void Output(this string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        internal static string[] ResourceText(this string file)
        {
            var assembly = Assembly.GetEntryAssembly();
            var names = assembly.GetManifestResourceNames();
            var resourceStream = assembly.GetManifestResourceStream($"Animalia.{file}");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var text = reader.ReadToEnd();
                return text.Split(Environment.NewLine);
            }
        }
    }
}