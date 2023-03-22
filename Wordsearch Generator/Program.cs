namespace Wordsearch
{
    using System;
    using System.Linq;
    class WordSearch
    {
        static void InsertWord(char[,] grid, string word)
        {
            Random rnd = new Random();
            int direction = rnd.Next(4);
            bool insert = false;
            switch (direction)
            {
                case 1: // Insert horizontally
                        // Generate random row and column
                        // Checks if word can fit, Inserts the word
                    insert = false;
                    int insertAttempt = 0;
                    while (!insert)
                    {
                        while (insertAttempt < (grid.GetLength(0) * grid.GetLength(1)))
                        {
                            int row = rnd.Next(grid.GetLength(0));
                            int col = rnd.Next(grid.GetLength(1) - word.Length + 1);
                            if (canInsert(grid, word, row, col, 0, 1))
                            {
                                for (int i = 0; i < word.Length; i++)
                                {
                                    grid[row, col + i] = word[i];
                                }
                                insertAttempt = (grid.GetLength(0) * grid.GetLength(1)) + 100;
                                Console.WriteLine(word);
                            }
                            else
                            {
                                insertAttempt ++;
                            }
                        }
                        insert = true;

                    }
                    break;

                case 2: // Insert vertically
                        // Generate random row and column
                        // Checks if word can fit, Inserts the word
                    insert = false;
                    insertAttempt = 0;
                    while (!insert)
                    {
                        while (insertAttempt < (grid.GetLength(0) * grid.GetLength(1)))
                        {
                            int row = rnd.Next(grid.GetLength(0) - word.Length + 1);
                            int col = rnd.Next(grid.GetLength(1));
                            if (canInsert(grid, word, row, col, 1, 0))
                            {
                                for (int i = 0; i < word.Length; i++)
                                {
                                    grid[row + i, col] = word[i];
                                }
                                insertAttempt = (grid.GetLength(0) * grid.GetLength(1)) + 100;
                                Console.WriteLine(word);
                            }
                            else
                            {
                                insertAttempt ++;
                            }
                        }
                        insert = true;
                    }
                    break;

                case 3: // Insert diagonally (bottom-left to top-right)
                        // Generate random row and column
                        // Checks if word can fit, Inserts the word
                    insert = false;
                    insertAttempt = 0;
                    while (!insert)
                    {
                        while (insertAttempt < (grid.GetLength(0) * grid.GetLength(1)))
                        {
                            int row = rnd.Next(grid.GetLength(0) - word.Length + 1);
                            int col = rnd.Next(grid.GetLength(1) - word.Length + 1);
                            if (canInsert(grid, word, row, col, 1, 1))
                            {
                                for (int i = 0; i < word.Length; i++)
                                {
                                    grid[row + i, col + i] = word[i];
                                }
                                insertAttempt = (grid.GetLength(0) * grid.GetLength(1)) + 100;
                                Console.WriteLine(word);
                            }
                            else
                            {
                                insertAttempt ++;
                            }
                        }
                        insert = true;
                    }
                    break;

                case 4: // Insert diagonally (top-right to bottom-left)
                        // Generate random row and column
                        // Checks if word can fit, Inserts the word
                    insertAttempt = 0;
                    insert = false;
                    while (!insert)
                    {
                        while (insertAttempt < (grid.GetLength(0) * grid.GetLength(1)))
                        {
                            int row = rnd.Next(grid.GetLength(0) - word.Length + 1);
                            int col = rnd.Next(word.Length - 1, grid.GetLength(1) - 1);
                            if (canInsert(grid, word, row, col, 1, -1))
                            {
                                for (int i = 0; i < word.Length; i++)
                                {
                                    grid[row + i, col - i] = word[i];
                                }
                                insertAttempt = (grid.GetLength(0) * grid.GetLength(1)) + 100;
                                Console.WriteLine(word);
                            }
                            else
                            {
                                insertAttempt ++;
                            }
                        }
                        insert = true;
                    }
                    break;
            }
        }

        // Checks to see if a whole word can be inserted and if so, returns true, else false
        static bool canInsert(char[,] grid, string word, int row, int col, int rowIter, int colIter)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (grid[row + rowIter * i, col + colIter * i] != '+' &&
                    grid[row + rowIter * i, col + colIter * i] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        static void Main()
        {
            // Getting the dimensions for the square wordsearch
            Console.WriteLine("Enter Width/Height: ");
            int Len = Convert.ToInt32(Console.ReadLine());

            // Create the grid
            char[,] grid = new char[Len, Len]; // Creates an array to store all characters in the wordsearch

            // Fills the grid with placeholders (+'s)
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = (char)('+');
                }
            }
            
            // Reads in a list of words from a CSV file and creates a list of words to insert into the wordsearch
            string wordfileLoc = @"File Directory Here!";
            string[] words = { "" };
            var reader = new StreamReader(File.OpenRead(wordfileLoc));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                words = line.Split(',');
            }

            // Sorts words into length order and removes words too long for the Wordsearch
            var sortedWords = words.OrderByDescending(n => n);
            string[] filteredWords = sortedWords.Where(word => word.Length <= Len).ToArray();

            // Inserts words into the grid
            foreach (string word in filteredWords)
            {
                InsertWord(grid, word.ToUpper());
            }
            Console.WriteLine("Warning! Some words may not be able to fit into the generated area!");

            // Fills the grid with random letters for the remaining placeholders
            Random rnd = new Random();
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == '+')
                    {
                        grid[row, col] = (char)('A' + rnd.Next(26));
                    }
                }
            }

            // Prints the grid to the console
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.Write(grid[row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey(); // Keeps Console window open when run through *.exe file
        }
    }
}
