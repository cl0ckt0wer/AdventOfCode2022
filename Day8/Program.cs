// See https://aka.ms/new-console-template for more information
var input = File.ReadAllLines("input.txt");
//array of string to array of int array
var intArray = input.Select(x =>
{
    var ret = new int[x.Length];
    for (int i = 0; i < x.Length; i++)
    {
        ret[i] = int.Parse(x.Substring(i, 1));
    }
    return ret;
}).ToArray();

int isTreeVisibleCount = 0;
for (int i = 0; i < intArray.Length; i++)
{
    for (int ii = 0; ii < intArray.Length; ii++)
    {
        if (IsVisibleFromOutsideFromAllDirections(i, ii, intArray))
        {
            isTreeVisibleCount++;
        }
    }
}

Console.WriteLine($"There are {isTreeVisibleCount} tree(s) visible from outside the matrix in all directions");
isTreeVisibleCount = 0;
for (int i = 0; i < intArray.Length; i++)
{
    for (int ii = 0; ii < intArray.Length; ii++)
    {
        if (IsVisibleFromOutsideFromAnyDirection(i, ii, intArray))
        {
            isTreeVisibleCount++;
        }
    }
}
Console.WriteLine($"There are {isTreeVisibleCount} tree(s) visible from at least one direction.");

var senicScore = 0;
for (int i = 0; i < intArray.Length; i++)
{
    for (int ii = 0; ii < intArray.Length; ii++)
    {
        var iiiSenicScore = GetScore(i, ii, intArray);
        Console.WriteLine($"Tree [{i}][{ii}] score: {iiiSenicScore}");
        if (iiiSenicScore > senicScore) senicScore = iiiSenicScore;
    }

}
Console.WriteLine($"Max senic score is {senicScore}");
 Console.WriteLine($"3,2 upscore is {GetUpScore(3, 2, intArray, intArray[3][2])}");
Console.WriteLine($"3,2 dowmscore is {GetDownScore(3, 2, intArray, intArray[3][2])}");

//167 is too low

int GetScore(int i, int ii, int[][] intArray)
{
    
    var ret = 0;
    ret = GetUpScore(i, ii, intArray, intArray[i][ii]);
    ret = ret * GetDownScore(i, ii, intArray, intArray[i][ii]);
    ret = ret * GetLeftScore(i, ii, intArray, intArray[i][ii]);
    ret = ret * GetRightScore(i, ii, intArray, intArray[i][ii]);
    return ret;
}

int GetRightScore(int i, int ii, int[][] intArray, int treeHeight)
{
    ii++;
    if (ii >= intArray[i].Length) return 0;
    if (treeHeight > intArray[i][ii]) return 1 + GetRightScore(i, ii, intArray, treeHeight);
    return 1;
}

int GetLeftScore(int i, int ii, int[][] intArray, int treeHeight)
{
    ii--;
    if (ii < 0) return 0;
    if (treeHeight > intArray[i][ii]) return 1 + GetLeftScore(i, ii, intArray, treeHeight);
    return 1;
}

int GetDownScore(int i, int ii, int[][] intArray, int treeHeight)
{
    i++;
    if (i + 1 > intArray.Length)
        return 0;
    if (treeHeight > intArray[i][ii])
        return  1 + GetDownScore(i, ii, intArray, treeHeight);
    return 1;
}

int GetUpScore(int i, int ii, int[][] intArray, int treeHeight)
{
    i--;
    if (i < 0)
        return 0;
    if (treeHeight > intArray[i][ii])
        return  1 + GetUpScore(i, ii, intArray, treeHeight);
    return 1;
}

bool IsVisibleFromOutsideFromAnyDirection(int i, int ii, int[][] intArray)
{
    //is Tree blocked from below
    if (!IsTreeBlockedFromBelow(i, ii, intArray, intArray[i][ii])) return true;
    if (!IsTreeBlockedFromAbove(i, ii, intArray, intArray[i][ii])) return true;
    if (!IsTreeBlockedFromLeft(i, ii, intArray, intArray[i][ii])) return true;
    if (!IsTreeBlockedFromRight(i, ii, intArray, intArray[i][ii])) return true;
    return false;

}

bool IsVisibleFromOutsideFromAllDirections(int i, int ii, int[][] intArray)
{
    var treeHeight = intArray[i][ii];
    //is Tree blocked from below
    if (!IsTreeBlockedFromBelow(i, ii, intArray, treeHeight))
        if (!IsTreeBlockedFromAbove(i, ii, intArray, treeHeight))
            if (!IsTreeBlockedFromLeft(i, ii, intArray, treeHeight))
                if (!IsTreeBlockedFromRight(i, ii, intArray, treeHeight))
                    return true;

    return false;


}


bool IsTreeBlockedFromRight(int i, int ii, int[][] intArray, int treeHeight)
{
    ii++;
    if (ii >= intArray[i].Length) return false;
    if (treeHeight > intArray[i][ii]) return IsTreeBlockedFromRight(i, ii, intArray, treeHeight);
    return true;
}

bool IsTreeBlockedFromLeft(int i, int ii, int[][] intArray, int treeHeight)
{
    ii--;
    if (ii < 0) return false;
    if (treeHeight > intArray[i][ii]) return IsTreeBlockedFromLeft(i, ii, intArray, treeHeight);
    return true;
}

bool IsTreeBlockedFromAbove(int i, int ii, int[][] intArray, int treeHeight)
{
    i--;
    if (i < 0)
        return false;
    if (treeHeight > intArray[i][ii])
        return IsTreeBlockedFromAbove(i, ii, intArray, treeHeight);
    return true;
}

bool IsTreeBlockedFromBelow(int i, int ii, int[][] intArray, int treeHeight)
{
    i++;
    if (i + 1 > intArray.Length)
        return false;
    if (treeHeight > intArray[i][ii])
        return IsTreeBlockedFromBelow(i, ii, intArray, treeHeight);
    return true;

}

Console.WriteLine();