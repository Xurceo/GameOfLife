using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GameOfLife
{
    public class GameGrid
    {
        private bool[,] _nextState;
        public DispatcherTimer Timer;
        public Canvas Canvas { get; set; }
        public Cell[,] Cells { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public GameGrid(int width, int height, int cellSize)
        {
            Width = width;
            Height = height;
            Canvas = new Canvas()
            {
                Margin = new Thickness(0, 50, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            Cells = new Cell[height, width];
            _nextState = new bool[height, width];

            InitGrid(cellSize);

            Timer = new DispatcherTimer();
            Timer.Tick += (s, e) => UpdateGrid();
        }

        public void InitGrid(int cellSize)
        {
            double spacing = 0;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Cell cell = new(i * (cellSize + spacing), j * (cellSize + spacing), 5, 5, Canvas);
                    Cells[j, i] = cell;
                }
            }
        }

        public void UpdateGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int aliveNeighbors = CountAliveNeighbors(x, y);
                    bool currentState = Cells[y, x].Alive;

                    if (currentState && (aliveNeighbors < 2 || aliveNeighbors > 3))
                        _nextState[y, x] = false;
                    else if (!currentState && aliveNeighbors == 3)
                        _nextState[y, x] = true;
                    else
                        _nextState[y, x] = currentState;
                }
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[y, x].Alive = _nextState[y, x];
                }
            }
        }

        private int CountAliveNeighbors(int x, int y)
        {
            int count = 0;

            int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

            for (int i = 0; i < 8; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (nx >= 0 && nx < Width && ny >= 0 && ny < Height)
                {
                    if (Cells[ny, nx].Alive)
                        count++;
                }
            }
            return count;
        }

        public void Run(int milliseconds)
        {
            if (milliseconds <= 0) return;

            Timer.Interval = TimeSpan.FromMilliseconds(milliseconds);

            if (!Timer.IsEnabled)
            {
                Timer.Start();
            }
            else
            {
                Timer.Stop();
            }
        }

        public void ClearGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cells[y, x].Alive = false;
                }
            }
        }
    }
}
