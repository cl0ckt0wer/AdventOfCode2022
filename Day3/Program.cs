// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
var input = File.ReadAllLines("input.txt");
Console.WriteLine($"count of input = {input.Length}");


 var count = input.AsParallel().Select(GetPriority).Sum();
Console.WriteLine($"pass1 : {count}");
int priority = 0;
var priorityLookup = BuildPriorityLookup();
//group the input into triads
for (int i = 0; i < input.Length; i += 3)
{
    //get insersection of three entries
    var intersection = input[i].Intersect(input[i + 1]).Intersect(input[i + 2]).FirstOrDefault();
    priority  += priorityLookup[intersection];
}
Console.WriteLine($"Id badge priority lookup is {priority}");
static int GetPriority(string arg1, int arg2)
{
    //split arg1 in half
    
    var halve = arg1.Length / 2;
    var left = arg1.Substring(0, halve);
    var right = arg1.Substring(halve);
    var dupletter = left.Intersect(right).FirstOrDefault();
    var priority = BuildPriorityLookup()[dupletter];
    return priority;
}

static Dictionary<char,int> BuildPriorityLookup()
{
    var ret = new Dictionary<char, int>();
    var start = 'a';
    var priority = 1;
    while (start <= 'z')
    {
        ret.TryAdd(start, priority);
        start++;
        priority++;
    }
    start = 'A';
    priority = 27;
    while (start <= 'Z')
    {
        ret.TryAdd(start, priority);
        start++;
        priority++;
    }
    return ret;
}
