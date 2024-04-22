using System;
using System.Collections.Generic;

class memorygm
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Memory Game!");
        Console.WriteLine("Enter the size of the board (even number): ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size % 2 != 0)
        {
            Console.WriteLine("Please enter an even number for the size of the board.");
            return;
        }

        char[,] board = GenerateBoard(size);
        char[,] displayBoard = GenerateDisplayBoard(size);

        ShuffleBoard(board);

        bool[,] flippedTiles = new bool[size, size];
        int remainingPairs = size * size / 2;

        while (remainingPairs > 0)
        {
            DisplayBoard(displayBoard);

            Console.WriteLine("Enter the coordinates of the tile you want to flip (row column): ");
            int row = Convert.ToInt32(Console.ReadLine()) - 1;
            int col = Convert.ToInt32(Console.ReadLine()) - 1;

            if (row < 0 || row >= size || col < 0 || col >= size)
            {
                Console.WriteLine("Invalid coordinates. Try again.");
                continue;
            }

            if (flippedTiles[row, col])
            {
                Console.WriteLine("This tile has already been flipped. Choose another one.");
                continue;
            }

            flippedTiles[row, col] = true;
            displayBoard[row, col] = board[row, col];
            DisplayBoard(displayBoard);

            Console.WriteLine("Enter the coordinates of another tile you want to flip (row column): ");
            int row2 = Convert.ToInt32(Console.ReadLine()) - 1;
            int col2 = Convert.ToInt32(Console.ReadLine()) - 1;

            if (row2 < 0 || row2 >= size || col2 < 0 || col2 >= size)
            {
                Console.WriteLine("Invalid coordinates. Try again.");
                continue;
            }

            if (flippedTiles[row2, col2])
            {
                Console.WriteLine("This tile has already been flipped. Choose another one.");
                continue;
            }

            flippedTiles[row2, col2] = true;
            displayBoard[row2, col2] = board[row2, col2];
            DisplayBoard(displayBoard);

            if (board[row, col] != board[row2, col2])
            {
                displayBoard[row, col] = '-';
                displayBoard[row2, col2] = '-';
                flippedTiles[row, col] = false;
                flippedTiles[row2, col2] = false;
                Console.WriteLine("No match! Try again.");
            }
            else
            {
                Console.WriteLine("Match found!");
                remainingPairs--;
            }
        }

        Console.WriteLine("Congratulations! You've matched all the pairs!");
    }

    static char[,] GenerateBoard(int size)
    {
        char[] symbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P' };
        char[,] board = new char[size, size];
        List<char> symbolList = new List<char>();

        for (int i = 0; i < size * size / 2; i++)
        {
            symbolList.Add(symbols[i]);
            symbolList.Add(symbols[i]);
        }

        Random rand = new Random();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int index = rand.Next(0, symbolList.Count);
                board[i, j] = symbolList[index];
                symbolList.RemoveAt(index);
            }
        }

        return board;
    }

    static char[,] GenerateDisplayBoard(int size)
    {
        char[,] displayBoard = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                displayBoard[i, j] = '-';
            }
        }

        return displayBoard;
    }

    static void ShuffleBoard(char[,] board)
    {
        Random rand = new Random();

        int size = board.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int row = rand.Next(0, size);
                int col = rand.Next(0, size);

                char temp = board[i, j];
                board[i, j] = board[row, col];
                board[row, col] = temp;
            }
        }
    }

    static void DisplayBoard(char[,] board)
    {
        int size = board.GetLength(0);

        Console.WriteLine("Current Board:");
        Console.WriteLine();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
