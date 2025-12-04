using System.Runtime.CompilerServices;
using System.Transactions;

var lines = File.ReadAllLines("input.txt");
var grid = new char[lines.Length, lines[0].Length];

for (var i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    for (var j = 0; j < line.Length; j++)
    {
        grid[i, j] = line[j];
    }
}

Print2DArray(grid);

var runningTotal = 0;
var papersRemoved = 0;
do
{
    papersRemoved = 0;
    grid = RemovePapers(grid);

    
    for (var i = 0; i < grid.GetLength(0); i++)
    {
        for (var j = 0; j < grid.GetLength(1); j++)
        {
            if (grid[i,j] == '#')
            {
                grid[i,j] = '.';
                papersRemoved++;
            }
        }
    }
    runningTotal += papersRemoved;
} while (papersRemoved > 0);



Print2DArray(grid);

Console.WriteLine(runningTotal);

char[,] RemovePapers(char[,] grid)
{
    var runningCount = 0;

    for (var i = 0; i < grid.GetLength(0); i++)
    {
        for (var j = 0; j < grid.GetLength(1); j++)
        {
            if (grid[i, j] != '@')
            {
                continue;
            }

            var canAccess = CanAccessSpace(grid, i, j);
            if (canAccess)
            {
                grid[i, j] = '#';
                runningCount++;
            }
        }
    }

    return grid;
}

bool CanAccessSpace(char[,] grid, int i, int j)
{
    var ajacentFreeSpaces = 0;

    //Check top left
    var topLeft = IsFreeSpace(grid, i - 1, j - 1);
    if (topLeft)
    {
        ajacentFreeSpaces++;
    }

    var topMiddle = IsFreeSpace(grid, i - 1, j);
    if (topMiddle)
    {
        ajacentFreeSpaces++;
    }

    var topRight = IsFreeSpace(grid, i - 1, j + 1);
    if (topRight)
    {
        ajacentFreeSpaces++;
    }

    var left = IsFreeSpace(grid, i, j - 1);
    if (left)
    {
        ajacentFreeSpaces++;
    }

    var right = IsFreeSpace(grid, i, j + 1);
    if (right)
    {
        ajacentFreeSpaces++;
    }

    var bottomLeft = IsFreeSpace(grid, i + 1, j - 1);
    if (bottomLeft)
    {
        ajacentFreeSpaces++;
    }

    var bottomMiddle = IsFreeSpace(grid, i + 1, j);
    if (bottomMiddle)
    {
        ajacentFreeSpaces++;
    }

    var bottomRight = IsFreeSpace(grid, i + 1, j + 1);
    if (bottomRight)
    {
        ajacentFreeSpaces++;
    }

    return ajacentFreeSpaces >= 5;
}

bool IsFreeSpace(char[,] grid, int i, int j)
{
    if (i < 0)
    {
        return true;
    }
    if (j < 0)
    {
        return true;
    }

    if (i >= grid.GetLength(0))
    {
        return true;
    }
    if (j >= grid.GetLength(1))
    {
        return true;
    }

    if (grid[i, j] != '@' && grid[i, j] != '#')
    {
        return true;
    }

    return false;
}


void Print2DArray<T>(T[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            Console.Write(matrix[i, j] + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}