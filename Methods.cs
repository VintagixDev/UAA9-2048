using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5T2024_ClaudeMael_2048
{
    public struct Methods
    {

        /// <summary>
        /// Génère une grille de taille customisable (entre 4 et 6)
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="grid"></param>
        public void CreateGrid(byte gridSize, out uint[,] grid)
        {
            Random random = new Random();
            grid = new uint[gridSize, gridSize];
            for(int row = 0; row < gridSize; row++)
            {
                for(int column = 0; column < gridSize; column++)
                {
                    grid[row, column] = 0;
                }
            }
            
        }

        public bool CreateBlock(ref uint[,] grid)
        {

            Random random = new Random();
            bool hasPlace = false;

            // Regarde si la grid contient une place libre
            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == 0) hasPlace = true;
                }
            }

            // Génère un bloc dans une place libre aléatoire
            if(hasPlace)
            {
                int randomRow;
                int randomCol;
                do
                {
                    randomRow = random.Next(0, grid.GetLength(0));
                    randomCol = random.Next(0, grid.GetLength(0));
                } while (grid[randomRow, randomCol] != 0);
                if(random.Next(0, 10+1) == 1)
                {
                    grid[randomRow, randomCol] = 4;

                }
                else
                {
                    grid[randomRow, randomCol] = 2;
                }
            }
            return hasPlace;
        }

        public bool CanMove(uint[,] grid)
        {
            // check si il y a des places libre
            for (int row = 0; row <= grid.GetLength(0) - 1; row++)
            {

                for (int column = 0; column <= grid.GetLength(1) - 1; column++)
                {
                    if (grid[row, column] == 0) return true;
                }
            }

            // check si 2 blocs peuvent fusionner horizontalement
            for (int row = 0; row <= grid.GetLength(0) -1; row++)
            {

                for (int column = 0; column <= grid.GetLength(1) - 2; column++)
                {
                    if (grid[row, column] == grid[row, column+1]) return true;
                }

            }

            // check si 2 blocs peuvent fusionner verticalement
            for (int column = 0; column <= grid.GetLength(1) - 1; column++)
            {

                for (int row = 0; row <= grid.GetLength(0) - 2; row++)
                {
                    if (grid[row, column] == grid[row+1, column]) return true;
                }

            }
            return false;


        }


        public void MoveAndMergeBlocks(ref uint[,] grid)
        {
            switch (Console.ReadKey().Key)
            {


                case ConsoleKey.LeftArrow:
                    for(int row = 0; row < grid.GetLength(0); row++)
                    {
                        MoveBlocks(0, row, ref grid);
                        for(int col = 0; col < grid.GetLength(1)-1; col++)
                        {
                            if (grid[row, col] != 0)
                            {
                                if (grid[row, col] == grid[row, col + 1])
                                {
                                    grid[row, col] = grid[row, col] * 2;
                                    grid[row, col+1] = 0;
                                }
                            }
                        }
                        MoveBlocks(0, row, ref grid);
                    }
                    break;


                case ConsoleKey.RightArrow:
                    for (int row = 0; row < grid.GetLength(0); row++)
                    {
                        MoveBlocks(1, row, ref grid);
                        for (int col = grid.GetLength(1) - 1; col > 0; col--)
                        {
                            if (grid[row, col] != 0)
                            {
                                if (grid[row, col] == grid[row, col - 1])
                                {
                                    grid[row, col] = grid[row, col - 1] * 2;
                                    grid[row, col - 1] = 0;
                                }
                            }
                        }
                        MoveBlocks(1, row, ref grid);
                    }
                    break;

                case ConsoleKey.UpArrow:
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        MoveBlocks(2, col, ref grid);
                        for (int row = 0; row < grid.GetLength(0)-2; row++)
                        {
                            if (grid[row, col] != 0)
                            {
                                if (grid[row, col] == grid[row+1, col])
                                {
                                    grid[row, col] = grid[row + 1, col] * 2;
                                    grid[row + 1, col] = 0;
                                }
                            }
                        }
                        MoveBlocks(2, col, ref grid);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    for (int col = 0; col < grid.GetLength(1); col++)
                    {
                        MoveBlocks(3, col, ref grid);
                        for (int row = grid.GetLength(0) - 1; row > 0; row--)
                        {
                            if (grid[row, col] != 0)
                            {
                                if (grid[row, col] == grid[row-1, col])
                                {
                                    grid[row, col] = grid[row - 1, col] * 2;
                                    grid[row - 1, col] = 0;
                                }
                            }
                        }
                        MoveBlocks(3, col, ref grid);
                    }
                    break;
            }
        }


        public void MoveBlocks(byte dir, int rowOrCol, ref uint[,] grid)
        {
            int place;
            uint temp;
            switch (dir)
            {
                // GAUCHE
                case 0:
                    place = 0;
                    for(int col = 0; col < grid.GetLength(1); col++)
                    {
                        if (grid[rowOrCol, col] != 0)
                        {
                            temp = grid[rowOrCol, col];
                            grid[rowOrCol, col] = 0;
                            grid[rowOrCol, place] = temp;
                            place++;
                        }
                    }
                    break;
                    // DROITE
                case 1:
                    place = grid.GetLength(1) - 1;

                    for (int col = grid.GetLength(1)-1; col >= 0; col--)
                    {
                        if (grid[rowOrCol, col] != 0)
                        {
                            temp = grid[rowOrCol, col];
                            grid[rowOrCol, col] = 0;
                            grid[rowOrCol, place] = temp;
                            place--;

                        }
                    }
                    
                    break;
                // HAUT
                case 2:
                    
                    place = 0;
                    for(int row = 0; row < grid.GetLength(0); row++)
                    {
                        if (grid[row, rowOrCol] != 0)
                        {
                            temp = grid[row, rowOrCol];
                            grid[row, rowOrCol] = 0;
                            grid[place, rowOrCol] = temp;
                            place++;
                        }
                    }
                    
                    break;
                // BAS
                case 3:
                    
                    place = grid.GetLength(0) - 1;
                    for (int row = grid.GetLength(0)-1; row >= 0; row--)
                    {
                        if (grid[row, rowOrCol] != 0)
                        {
                            temp = grid[row, rowOrCol];
                            grid[row, rowOrCol] = 0;
                            grid[place, rowOrCol] = temp;
                            place--;
                        }
                       
                    }
                    break;
            }
        }

        public bool hasWon(uint[,] grid)
        {
            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public string ConcateneGrid(uint[,] grid)
        {
            string concatene = "";
            for (int row = 0; row <= grid.GetLength(0) - 1; row++)
            {

                string ligne = "[";
                for (int column = 0; column <= grid.GetLength(1) - 1; column++)
                {
                    ligne = ligne + grid[row, column];
                    if (column != grid.GetLength(1) - 1)
                    {
                        ligne = ligne + "|";
                    }
                    else
                    {
                        ligne = ligne + "]";
                    }
                }
                concatene = concatene + "\n" + ligne;
            }
            return concatene;
        }


        public void LireByte(string question, out byte output)
        {
            Console.WriteLine(question);
            string nUser = Console.ReadLine();
            while(!byte.TryParse(nUser, out output)){
                Console.WriteLine(question);
                nUser = Console.ReadLine();
            }
        }

        public void LireChar(string question, out char output)
        {
            Console.WriteLine(question);
            string nUser = Console.ReadLine();
            while (!char.TryParse(nUser, out output))
            {
                Console.WriteLine(question);
                nUser = Console.ReadLine();
            }
        }

    }
}
