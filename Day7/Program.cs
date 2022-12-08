// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

IDir rootFs = new Folder();
var currentLocation = new Stack<string>();
var input = File.ReadAllLines("input.txt");
foreach(var command in input)
{
    var splitCommand = command.Split(" ");
    switch (splitCommand[0])
    {
        case "$":
            ProcessCommand(rootFs, splitCommand, currentLocation);
            break;
        default:
            ProcessOutput(rootFs, splitCommand, currentLocation);
            break;
    }
}
void ProcessOutput(IDir rootFs, string[] splitCommand, Stack<string> currentLocation)
{
    var findLocation = rootFs;
    foreach (var folder in currentLocation.ToArray().Reverse())
    {
        findLocation = findLocation.Contents.FirstOrDefault(x => x.DirType == DirType.Folder && x.Name == folder);
    }
    if (int.TryParse(splitCommand[0], out int fileSize))
    {
        findLocation.Contents.Add(new MyFile()
        {
            Name = splitCommand[1],
            Size = fileSize
        }) ;
    }
    else if (splitCommand[0] == "dir")
    {
        findLocation.Contents.Add(new Folder()
        {
            Name = splitCommand[1]
        });
    }
}

void ProcessCommand(IDir rootFs, string[] splitCommand, Stack<string> currentLocation)
{
    switch (splitCommand[1])
    {
        case "cd":
            UpdatePath(splitCommand, currentLocation);
            break;
            //ls output is processed elsewhere
    }
}

void UpdatePath(string[] splitCommand, Stack<string> currentLocation )
{
    switch (splitCommand[2])
    {
        case "..":
            currentLocation.Pop();
            break;
        case "/":
            currentLocation.Clear();
            break;
        default:
            currentLocation.Push(splitCommand[2]);
            break;
    }   
}

//foreach (var comand in input)
//{
//    var sp = comand.Split(" ");

//    if (sp[0] == "$"){
//        switch (sp[1])
//        {
//            case "ls":
//                continue;
//                break;
//            case "cd":
//                if (sp[2] == "/")
//                {
//                    currentLocation = "/";
//                }
//                else if (sp[2] == "..")
//                {
//                    var sp2 = currentLocation.Split("/", StringSplitOptions.RemoveEmptyEntries);
//                    sp2 = sp2[..^1];
//                    currentLocation = "/" +string.Join("/", sp2) + "/";
//                }
//                else
//                {
//                    currentLocation += sp[2] + "/";
//                }
//                break;
//            default:
//                throw new NotImplementedException();
//                break;
//        }
//        continue;
//    }
//    else if (sp[0] == "dir")
//    {
//        //check if folder exists
//        var splitCurrentLocation = currentLocation.Split("/", StringSplitOptions.RemoveEmptyEntries);
//        var currentDir = rootFs;
//        foreach (var dir in splitCurrentLocation)
//        {
//            currentDir = currentDir.Contents?.FirstOrDefault(x => x.Name == dir);
//        }
//        var dirNew = currentDir.Contents?.FirstOrDefault(x => x.Name == sp[1]);
//        if (dirNew != null)
//        {
//            Console.WriteLine("Directory already exists");
//            continue;
//        }
//        currentDir.Contents?.Add(new Folder()
//        {
//            Name = sp[1]
//        });
//    }
//    else if (int.TryParse(sp[0], out int size))
//    {
//        var splitCurrentLocation = currentLocation.Split("/", StringSplitOptions.RemoveEmptyEntries);
//        var currentDir = rootFs;
//        foreach (var dir in splitCurrentLocation)
//        {
//            currentDir = currentDir?.Contents?.FirstOrDefault(x => x.Name == dir);
//        }
//        var fileNew = currentDir.Contents?.FirstOrDefault(x => x.Name == sp[1]);
//        if (fileNew != null)
//        {
//            Console.WriteLine("File already exists");
//            continue;
//        }
//        currentDir.Contents?.Add(new MyFile()
//        {
//            Name = sp[1],
//            Size = size
//        });
//    }
//}
//caluclate total size of every folder
var listOfFoldersAndSizes = new List<(string?, int)>();
listOfFoldersAndSizes.Add( ("/", rootFs.Size));
GetListOfFoldersAndSizes(rootFs, listOfFoldersAndSizes, DirType.Folder);
var itemcount = listOfFoldersAndSizes.Count;
Console.WriteLine($"Inventory at {itemcount} items");
var step1 = listOfFoldersAndSizes.Where(x => x.Item2 < 100000).ToList();
var step2 = step1.Sum(x => x.Item2);

Console.WriteLine($"part 1 answer is: {step2}");
//debugging, did i make the right tree?
var json = JsonConvert.SerializeObject(rootFs, Formatting.Indented);
File.WriteAllText("output.json", json);

var totalDiskSpace = 70000000;
var diskSpaceNeeded = 30000000;
var diskSpaceUsed =  rootFs.Size;
var diskSpaceFree = totalDiskSpace - diskSpaceUsed;
var diskSpaceNeededFree = diskSpaceNeeded - diskSpaceFree;



Console.WriteLine($"I have {totalDiskSpace} disk space");
Console.WriteLine($"I need {diskSpaceNeeded} disk space free");
Console.WriteLine($"I have {diskSpaceUsed} disk space used");
Console.WriteLine($"I have {diskSpaceFree} disk space free");
Console.WriteLine("I need to free up {0} disk space", diskSpaceNeeded - diskSpaceFree);

var foldersThatAreBigEnoughToBeDeleted = listOfFoldersAndSizes.Where(x => x.Item2 >= diskSpaceNeededFree).ToList();

var foldersThatAreBigEnoughToBeDeletedSorted = foldersThatAreBigEnoughToBeDeleted.OrderBy(x => x.Item2).ToList();

var smallestFolderThatIsBigEnough = foldersThatAreBigEnoughToBeDeletedSorted.First();

Console.WriteLine("The smallest folder that is big enough is {0} with size {1}", smallestFolderThatIsBigEnough.Item1, smallestFolderThatIsBigEnough.Item2);
//it is not wnmrcvqd - 545729







void GetListOfFoldersAndSizes(IDir rootFs, List<(string?, int)> listOfFoldersAndSizes, DirType item)
{
    //just folders for now
    foreach (var content in rootFs.Contents)
    {
        if (content.Contents.Count >= 0)
        {
            listOfFoldersAndSizes.Add((content.Name, content.Size));
            GetListOfFoldersAndSizes(content, listOfFoldersAndSizes, item);
        }
    }
}

public interface IDir
{
    public DirType DirType { get; set; }
    public string? Name { get; set; }
    public int Size { get; set; }
    public List<IDir>? Contents { get; set; }

}
public class MyFile : IDir
{
    public DirType DirType { get; set; } = DirType.File;
    public string? Name { get; set; }
    public int Size { get; set; }
    public List<IDir>? Contents
    {
        get
        {
            return new List<IDir>();
        }
        set
        {
            return;
        }
    }
}
public class Folder : IDir
{
    public DirType DirType { get; set; } = DirType.Folder;
    public string? Name { get; set; }
    public Folder()
    {
        {
            Contents = new List<IDir>();
        }
    }
    public int Size
    {

        get
        {
            return Contents.Sum(x => x.Size);
        }
        set
        {
            return;
        }

    }
    public List<IDir>? Contents { get; set; }
}
public enum DirType
{
    File,
    Folder
}