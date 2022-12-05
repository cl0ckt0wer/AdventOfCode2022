// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt");
var inventory = new List<Elf>();
Elf e = new Elf() { Id = 1 };
foreach (var s in input)
{
    if (string.IsNullOrEmpty(s))
    {

        var newe = new Elf();
        newe.Id = e.Id + 1;
        inventory.Add(e);
        e = newe;
    }
    else
    {
        e.Calories += int.Parse(s);
    }

}

Elf? fattestElf = inventory.OrderByDescending(x => x.Calories).First();
Console.WriteLine($"The fattest elf is carrying {fattestElf.Calories}");
var top3 = inventory.OrderByDescending(x => x.Calories).Take(3);
var lionsShare = top3.Sum(x => x.Calories);
Console.WriteLine($"The Top 3 elves are carrying {lionsShare}");


public class Elf
{
    public int Id { get; set; }
    public int Calories { get; set; }
}