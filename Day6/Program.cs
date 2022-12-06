var part1Answer = GetFirstUniqueSubstring(File.ReadAllLines("input.txt")[0], 4);
var part2Answer = GetFirstUniqueSubstring(File.ReadAllLines("input.txt")[0], 14);
Console.WriteLine($"The first part answer is: {part1Answer}. The second part answer is: {part2Answer}");

int GetFirstUniqueSubstring(string messageString, int uniqueLength, int result = 0)
{
    while (true)
    {
        if (messageString.Substring(result, uniqueLength).Distinct().Count() == uniqueLength) break;
        result++;
    }
    return result + uniqueLength;
}
