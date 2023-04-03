using System.Globalization;
using System.IO;
using System.Reflection;
var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!;

var summary = File.ReadLines(Directory.GetFiles(directory.ToString(), "*.csv", SearchOption.TopDirectoryOnly).FirstOrDefault()!);
var target = Directory.CreateDirectory(directory + "\\out");
var fileTarget = target + "\\new-summary.csv";
File.Delete(fileTarget);

Console.WriteLine("Texto contido no arquivo original: ");
using (StreamWriter sw = File.AppendText(fileTarget))
{
    for (int i = 0; i < summary.Count(); i++)
    {
        Console.WriteLine(summary.ToArray()[i]);
        if (i == 0)
            sw.WriteLine("Produto;Valor;Quantidade;Total");
        else
        {
            string[] columns = summary.ToArray()[i].Split(",");
            var total = decimal.Parse(columns[columns.Length - 2], CultureInfo.InvariantCulture) * Convert.ToInt16(columns[columns.Length - 1]);
            sw.WriteLine($"{summary.ToArray()[i].Replace(",", ";")};{total.ToString("n2", CultureInfo.InvariantCulture)}");
        }
            
    }
}
Console.WriteLine("\nNovo texto com tratamento de dados:");
foreach(string line in File.ReadAllLines(fileTarget))
    Console.WriteLine(line);
Console.WriteLine("\nO novo arquivo foi salvo no caminho: " + target.ToString());