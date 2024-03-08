using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pokus3.Model;

namespace Pokus3
{
    public static class Game
    {
        private static Cell[,] gameBoard;
        private static List<Cell> snake;

        //Game initialisation
        public static void Initialize()
        {
            int dim = DataStore.cfgData.dim;
            gameBoard = new Cell[dim, dim];

            
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    Cell c = new Cell(i, j, cellState.Empty);
                    gameBoard[i, j] = c;
                }
            }

            gameBoard[dim/2, dim/2].state = cellState.Head;
            snake = new List<Cell>();
            snake.Add(gameBoard[dim / 2, dim / 2]);

            GenerateFood();
        }

        //Random generation of the food on game board
        //false - no space found (practically impossible to happen)
        //true - OKay
        public static bool GenerateFood()
        {
            int dim = DataStore.cfgData.dim;
            bool foundSpace = false;
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (gameBoard[i, j].state == cellState.Empty) foundSpace = true;
                }
            }
            if (!foundSpace) return foundSpace;

            int x, y;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            while (true)
            {
                x = rnd.Next(0, dim);
                y = rnd.Next(0, dim);

                if (gameBoard[x, y].state == cellState.Empty)
                {
                    gameBoard[x, y].state = cellState.Food;
                    break;
                }
            }

            return true;
        }

        //Moving
        //false - crash
        //true - OKay
        public static bool Shift(Direction dir)
        {
            int dim = DataStore.cfgData.dim;
            int headX = snake[0].x; int headY = snake[0].y;

            int requestedX = -1, requestedY = -1;
            if (dir == Direction.Right) { requestedX = headX + 1; requestedY = headY; }
            else if (dir == Direction.Left) { requestedX = headX - 1; requestedY = headY; }
            else if (dir == Direction.Down) { requestedX = headX; requestedY = headY + 1; }
            else if (dir == Direction.Up) { requestedX = headX; requestedY = headY - 1; }

            if (requestedX < 0 || requestedY < 0 || requestedX >= dim || requestedY >= dim)
            {
                //Wall crash
                return false;
            }

            if (gameBoard[requestedX, requestedY].state == cellState.Body)
            {
                //Body crash
                return false;
            }
            else if (gameBoard[requestedX, requestedY].state == cellState.Empty) //Empty cell
            {
                Cell newHead = gameBoard[requestedX, requestedY];
                newHead.state = cellState.Head;
                snake.Insert(0, newHead);
                snake[1].state = cellState.Body;
                snake[snake.Count - 1].state = cellState.Empty;
                snake.Remove(snake[snake.Count-1]);
            }
            else if (gameBoard[requestedX, requestedY].state == cellState.Food) //Food cell - extending the snake
            {
                Cell newHead = gameBoard[requestedX, requestedY];
                newHead.state = cellState.Head;
                snake.Insert(0, newHead);
                snake[1].state = cellState.Body;
                GenerateFood();
            }
            return true;
        }

        //aux method for getting a single cell from the outside
        public static Cell GetCell(int x, int y)
        {
            return gameBoard[x, y];
        }

        //aux method for getting score
        public static int GetLength()
        {
            return snake.Count;
        }
    }

    //Class representing a single cell
    public class Cell
    {
        public int x = -1;

        public int y = -1;

        public cellState state = cellState.Empty;

        public Cell(int _x, int _y, cellState _state)
        {
            x = _x;
            y = _y;
            state = _state;
        }
    }
}
