// See https://aka.ms/new-console-template for more information

//read in input 
var output = File.ReadAllLines("input.txt");
//find index of first string that begins with " 1"
var index = Array.FindIndex(output, x => x.StartsWith(" 1"));
//split array into two indexes based on index found
var first = output.Take(index).ToArray();
var second = output.Skip(index + 2).ToArray();
//get stack list
var stackEnum = output[index].Split(" ", StringSplitOptions.RemoveEmptyEntries);
var stackList = new List<Stack<char>>();

//create stacks for each column 
foreach (var s in stackEnum)
{
    stackList.Add(new Stack<char>());
}
//load starting state
foreach (var row in first.Reverse())
{
    for (int i = 0; i < stackList.Count; i++)
    {
        var pick = row[1 + (i * 4)];
        if (pick == ' ')
            continue;
        stackList[i].Push(pick);
    }
}
//process moves
foreach (var row in second)
{

    var move = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var i = int.Parse(move[1]);
    while (i > 0)
    {
        var pick = stackList[int.Parse(move[3]) - 1].Pop();
        stackList[int.Parse(move[5]) - 1].Push(pick);
        i--;
    }

}
Console.Write("Part 1: ");
//print out final state
foreach (var stack in stackList)
{
    Console.Write(stack.Pop());
}
Console.WriteLine();
//part 2
//reset stacks
for(int i = 0; i<stackList.Count(); i++)
{
    stackList[i].Clear();
}
//load starting state
foreach (var row in first.Reverse())
{
    for (int i = 0; i < stackList.Count; i++)
    {
        var pick = row[1 + (i * 4)];
        if (pick == ' ')
            continue;
        stackList[i].Push(pick);
    }
}
//process moves
foreach (var row in second)
{
    var picker = new Stack<char>();
    var move = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var i = int.Parse(move[1]);
    while (i > 0)
    {
        var pick = stackList[int.Parse(move[3]) - 1].Pop();
        //stackList[int.Parse(move[5]) - 1].Push(pick);
        picker.Push(pick);
        i--;
    }
    while(picker.Count() > 0)
    {
        stackList[int.Parse(move[5]) - 1].Push(picker.Pop());
    }

}
Console.Write("Part 2: ");
//print out final state
foreach (var stack in stackList)
{
    Console.Write(stack.Pop());
}
Console.WriteLine();
Console.WriteLine("Finito");
