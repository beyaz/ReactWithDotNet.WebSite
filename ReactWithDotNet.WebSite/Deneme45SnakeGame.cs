namespace ReactWithDotNet.WebSite;
using Math = ReactWithDotNet.Math;

using System;

[External]
static class document
{
    public static extern DomElement getElementById(string id);

    public static extern DomElement createElement(string tag);

    public static extern void addEventListener(string keydown, Action<DomEventArg> action);
}

[External]
class DomElement
{
    public string innerHTML;
    public string textContent;
    public DomElementStyle style;

    public extern void appendChild(DomElement child);
}

[External]
class DomEventArg
{
    public string key;
}

[External]
class DomElementStyle
{
    public string display;
    public string width;
    public string height;
    public string border;
    public string backgroundColor;
}


[External]
static class window
{
    public static extern void setInterval(Action action, int timeout);
}



class Random
{
    public int Next(int min, int max)
    {
        return (int)Math.floor(Math.random()*max + min); 
    }
}


    public class SnakeGame
    {
        // Game settings
        private const int Rows = 20;
        private const int Columns = 20;
        private const int CellSize = 20;
        private static int[,] grid = new int[Rows, Columns];
        private static int snakeLength = 3;
        private static (int X, int Y)[] snake = new (int, int)[Rows * Columns];
        private static (int X, int Y) food;
        private static string direction = "right";
        private static int score = 0;

        private static bool gameOver = false;

        public static void Start()
        {
            InitializeGame();

            //// Render initial state
            Render();

            //// Start the game loop
            //window.setInterval(GameLoop, 200);

            //// Attach controls
            //AttachControls();
        }

        private static void InitializeGame()
        {
            // Initialize the snake in the middle of the grid
            for (int i = 0; i < snakeLength; i++)
            {
                snake[i] = (Rows / 2, Columns / 2 - i);
                grid[Rows / 2, Columns / 2 - i] = 1;
            }

            SpawnFood();
        }

        private static void SpawnFood()
        {
            var random = new Random();

            while (true)
            {
                int foodX = random.Next(0, Rows);
                int foodY = random.Next(0, Columns);

                if (grid[foodX, foodY] == 0) // Only place food in empty cells
                {
                    food = (foodX, foodY);
                    grid[foodX, foodY] = 2; // Mark as food
                    break;
                }
            }
        }

        private static void GameLoop()
        {
            if (gameOver)
            {
                RenderGameOver();
                return;
            }

            MoveSnake();
            Render();
        }

        private static void MoveSnake()
        {
            // Calculate new head position based on direction
            var head = snake[0];
            var newHead = head;

            if (direction == "up") newHead.X--;
            else if (direction == "down") newHead.X++;
            else if (direction == "left") newHead.Y--;
            else if (direction == "right") newHead.Y++;

            // Check collision with walls
            if (newHead.X < 0 || newHead.X >= Rows || newHead.Y < 0 || newHead.Y >= Columns)
            {
                gameOver = true;
                return;
            }

            // Check collision with itself
            if (grid[newHead.X, newHead.Y] == 1)
            {
                gameOver = true;
                return;
            }

            // Check collision with food
            if (grid[newHead.X, newHead.Y] == 2)
            {
                snakeLength++;
                score += 10;
                SpawnFood();
            }

            // Move snake
            for (int i = snakeLength - 1; i > 0; i--)
            {
                snake[i] = snake[i - 1];
            }

            snake[0] = newHead;

            // Update grid
            Array.Clear(grid, 0, grid.Length); // Clear grid
            for (int i = 0; i < snakeLength; i++)
            {
                var part = snake[i];
                grid[part.X, part.Y] = 1;
            }

            grid[food.X, food.Y] = 2; // Keep food position
        }

        private static void Render()
        {
            var container = document.getElementById("game");
            container.innerHTML = "";

        // Display score
        var scoreDiv = document.getElementById("score");
        scoreDiv.textContent = "Score: " + score.ToString();

        // Render the grid
        for (int r = 0; r < Rows; r++)
        {
            var rowDiv = document.createElement("div");
            rowDiv.style.display = "flex";

            for (int c = 0; c < Columns; c++)
            {
                var cell = document.createElement("div");
                cell.style.width = $"{CellSize}px";
                cell.style.height = $"{CellSize}px";
                cell.style.border = "1px solid black";
                cell.style.backgroundColor = grid[r, c] == 1 ? "green" : grid[r, c] == 2 ? "red" : "white";
                rowDiv.appendChild(cell);
            }

            container.appendChild(rowDiv);
        }
    }

        private static void RenderGameOver()
        {
            var container = document.getElementById("game");
            container.innerHTML = "<h2>Game Over</h2>";
        }

        private static void AttachControls()
        {
            document.addEventListener("keydown", ev =>
            {
                if (ev.key == "ArrowUp" && direction != "down") direction = "up";
                else if (ev.key == "ArrowDown" && direction != "up") direction = "down";
                else if (ev.key == "ArrowLeft" && direction != "right") direction = "left";
                else if (ev.key == "ArrowRight" && direction != "left") direction = "right";
            });
        }
    }

