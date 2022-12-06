// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt");
var answer = input.Select(x => GetStartIndex(x, 0)).Sum();
var part2answer = GetMessageStart(input[0]);

int GetMessageStart(string v)
{
    var i = 0;
    while (true)
    {
        if (v.Substring(i, 14).Distinct().Count() == 14) break;
        i++;
    }
    //Console.WriteLine(v.Substring(i, 14));
    return i + 14;

}

Console.WriteLine($"Part 1:{answer}");
Console.WriteLine($"Part 2: {part2answer}");

static int GetStartIndex(string input, int v)
{
    if (input[v] != input[v + 1] && input[v] != input[v + 2] && input[v] != input[v + 3])
    {
        if (input[v + 1] != input[v + 2] && input[v+1] != input[v + 3])
        {
            if (input[v + 2] != input[v + 3])
            {
                return v + 4;
            }
        }
    }
    return GetStartIndex(input, v + 1);
    
}