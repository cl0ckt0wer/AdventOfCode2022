
var input = File.ReadAllLines("input.txt");
Console.WriteLine($"count of input = {input.Length}");
var count = 0;
count = input.AsParallel().Select(IsOverLappedCompletely).Sum();
Console.WriteLine($"Part 1: {count}");
//count = input.AsParallel().Select(IsOverlapped).Sum();
//Console.WriteLine($"Part 2: {count}");
count = input.AsParallel().Select(IsOverlapped2).Sum();
Console.WriteLine($"Part 2: {count}");

// https://stackoverflow.com/questions/325933/determine-whether-two-date-ranges-overlap
static int IsOverlapped2(string arg1, int arg2)
{
    var split = arg1.Split(',');
    var leftsplit = split[0].Split('-');
    var rightsplit = split[1].Split('-');
    var firstleft = int.Parse(leftsplit[0]);
    var firstright = int.Parse(leftsplit[1]);
    var secondleft = int.Parse(rightsplit[0]);
    var secondright = int.Parse(rightsplit[1]);
    if (firstleft <= secondright && secondleft <= firstright)
    {
        return 1;
    }
    return 0;
}
static int IsOverlapped(string arg1, int arg2)
{
    var split = arg1.Split(',');
    var leftsplit = split[0].Split('-');
    var rightsplit = split[1].Split('-');
    var firstleft = int.Parse(leftsplit[0]);
    var firstright = int.Parse(leftsplit[1]);
    var secondleft = int.Parse(rightsplit[0]);
    var secondright = int.Parse(rightsplit[1]);
    //is first left in range of second
    if (firstleft >= secondleft && firstleft <= secondright)
    {
        return 1;
    }
    //is first right in range of second
    if (firstright >= secondleft && firstright <= secondright)
    {
        return 1;
    }
    //is second left in range of first
    if (secondleft >= firstleft && secondleft <= firstright)
    {
        return 1;
    }
    //is second right in range of first
    if (secondright >= firstleft && secondright <= firstright)
    {
        return 1;
    }
    return 0;
}

static int IsOverLappedCompletely(string arg1, int arg2)
{
    //split arg1 on -
    var split = arg1.Split(',');
    var leftsplit = split[0].Split('-');
    var rightsplit = split[1].Split('-');
    var firstleft = int.Parse(leftsplit[0]);
    var firstright = int.Parse(leftsplit[1]);
    var secondleft = int.Parse(rightsplit[0]);
    var secondright = int.Parse(rightsplit[1]);
    if (firstleft <= secondleft && firstright >= secondright)
    {
        return 1;
    }
    if (secondleft <= firstleft && secondright >= firstright)
    {
        return 1;
    }
    return 0;
    
}