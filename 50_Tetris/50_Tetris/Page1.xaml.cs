using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _50_Tetris
{
    /// <summary>
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public struct Coords { public int row; public int col; public Coords(int r, int c) { row = r; col = c; } }
    public enum FieldStatus { Free, Taken }
    public enum Shapes { Line, Square, T, LShape, LShapeReversed, ZShape, ZShapeReversed }

    public partial class Page1 : Page
    {
        public Thread childLoopThread;
        FieldStatus[,] grid;
        Random random = new Random();
        bool fallBlockade = true;
        Block block;
        Block nextBlock;
        ImageSource clearBlock = new BitmapImage(new Uri("Images/clear.png", UriKind.Relative));
        ImageSource[] blocks = {
            new BitmapImage(new Uri("Images/blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Images/green.png", UriKind.Relative)),
            new BitmapImage(new Uri("Images/red.png", UriKind.Relative)),
            new BitmapImage(new Uri("Images/yellow.png", UriKind.Relative))
        };
        int delay = 1000;

        public Page1()
        {
            InitializeComponent();
            PrepareGame();
            StartGame();
        }

        private void PrepareGame()
        {
            foreach (Image i in MainGrid.Children)
            {
                i.Source = clearBlock;
            }
            grid = new FieldStatus[MainGrid.RowDefinitions.Count, MainGrid.ColumnDefinitions.Count];
            ThreadStart fallThread = new ThreadStart(MakeBlockFall);
            childLoopThread = new Thread(fallThread);
            childLoopThread.Start();
        }
        private void StartGame()
        {
            ClearFilledLines();
            if (!MakeNewBlock())
            {
                fallBlockade = false;
                return;
            }
            fallBlockade = false;
            EndGame();
        }
        private void ClearFilledLines()
        {
            for (int i = grid.GetLength(0) - 1; i >= 0; i--) // 19 - 0
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == FieldStatus.Free)
                        break;
                    else if (j == grid.GetLength(1) - 1)
                    {
                        RemoveRow(i);
                        LowerBoard(i);
                        i++;
                    }
                }
            }
        }
        private void RemoveRow(int rowToRemove)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                grid[rowToRemove, i] = FieldStatus.Free;
            }
        }
        private void LowerBoard(int freeRow)
        {
            for (int i = freeRow; i > 0; i--)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = grid[i - 1, j];
                    ((Image)MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j)).Source = ((Image)MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == i - 1 && Grid.GetColumn(e) == j)).Source;
                }
            }
        }
        private bool MakeNewBlock()
        {
            Coords[] coords = new Coords[4];
            Shapes nextShape = (Shapes)random.Next(Enum.GetNames(typeof(Shapes)).Length);
            if (nextBlock == null)
                block = new Block(blocks[random.Next(4)], (Shapes)random.Next(Enum.GetNames(typeof(Shapes)).Length));
            else
                block = nextBlock;
            DefineShape(block.shape, coords);
            if (!MakingNewBlockPossible(coords))
                return true;
            nextBlock = new Block(blocks[random.Next(4)], nextShape);
            SetNextBlockImage(nextShape);
            block.blockCoords = coords;
            return false;
        }
        private void DefineShape(Shapes shape, Coords[] coords)
        {
            if (shape == Shapes.Line)
            //if (true)
            {
                coords[0] = new Coords(0, 4);
                coords[1] = new Coords(1, 4);
                coords[2] = new Coords(2, 4);
                coords[3] = new Coords(3, 4);
            }
            else if (shape == Shapes.Square)
            {
                coords[0] = new Coords(0, 4);
                coords[1] = new Coords(0, 5);
                coords[2] = new Coords(1, 4);
                coords[3] = new Coords(1, 5);
            }
            else if (shape == Shapes.T)
            {
                coords[0] = new Coords(0, 3);
                coords[1] = new Coords(0, 4);
                coords[2] = new Coords(0, 5);
                coords[3] = new Coords(1, 4);
            }
            else if (shape == Shapes.LShape)
            {
                coords[0] = new Coords(0, 4);
                coords[1] = new Coords(1, 4);
                coords[2] = new Coords(2, 4);
                coords[3] = new Coords(2, 5);
            }
            else if (shape == Shapes.LShapeReversed)
            {
                coords[0] = new Coords(0, 5);
                coords[1] = new Coords(1, 5);
                coords[2] = new Coords(2, 5);
                coords[3] = new Coords(2, 4);
            }
            else if (shape == Shapes.ZShape)
            {
                coords[0] = new Coords(0, 3);
                coords[1] = new Coords(0, 4);
                coords[2] = new Coords(1, 4);
                coords[3] = new Coords(1, 5);
            }
            else if (shape == Shapes.ZShapeReversed)
            {
                coords[0] = new Coords(1, 3);
                coords[1] = new Coords(1, 4);
                coords[2] = new Coords(0, 4);
                coords[3] = new Coords(0, 5);
            }
        }
        private void SetNextBlockImage(Shapes shape)
        {
            Coords[] coords = new Coords[4];
            DefineShape(shape, coords);
            foreach (Image img in NextBlockGrid.Children)
            {
                img.Source = clearBlock;
            }
            foreach (Coords c in coords)
            {
                ((Image)NextBlockGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == (c.row + 1) % 4 && Grid.GetColumn(e) == c.col - 3)).Source = block.img;
            }
        }

        public new void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && MoveLeftPossible())
                MakeBlockMoveLeft();
            else if (e.Key == Key.Right && MoveRightPossible())
                MakeBlockMoveRight();
            else if (e.Key == Key.Down && FallPossible())
                MakeBlockMoveDown();
            else if (e.Key == Key.Up && RotationPossible())
                MakeBlockRotate();
        }

        // CHECKING, IF OPTION IS POSSIBLE
        private bool MakingNewBlockPossible(Coords[] coords)
        {
            foreach (Coords c in coords)
                if (grid[c.row, c.col] == FieldStatus.Taken)
                    return false;
            return true;
        }
        private bool FallPossible()
        {
            foreach (Coords c in block.blockCoords)
                if (c.row == grid.GetLength(0) - 1 || grid[c.row + 1, c.col] == FieldStatus.Taken)
                    return false;
            return true;
        }
        private bool MoveLeftPossible()
        {
            foreach (Coords c in block.blockCoords)
                if (c.col == 0 || grid[c.row, c.col - 1] == FieldStatus.Taken)
                    return false;
            return true;
        }
        private bool MoveRightPossible()
        {
            foreach (Coords c in block.blockCoords)
                if (c.col == grid.GetLength(1) - 1 || grid[c.row, c.col + 1] == FieldStatus.Taken)
                    return false;
            return true;
        }
        private bool RotationPossible()
        {
            Coords[] oldCoords = (Coords[])block.blockCoords.Clone();
            block.Rotate();
            try
            {
                foreach (Coords c in block.blockCoords)
                {
                    try
                    {
                        if (grid[c.row, c.col] == FieldStatus.Taken)
                        {
                            return false;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return false;
                    }
                }
                return true;
            }
            finally
            {
                block.blockCoords = oldCoords;
            }
        }

        // MAKING MOVE
        private void MakeBlockFall()
        {
            while (true)
            {
                if (!fallBlockade)
                {
                    System.Threading.Thread.Sleep(delay);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        if (FallPossible())
                        {
                            MakeBlockMoveDown();
                        }
                        else
                        {
                            fallBlockade = true;
                            TurnBlockIntoTakenFields();
                            StartGame();
                        }
                    });
                }
            }
        }
        private void MakeBlockMoveLeft()
        {
            EraseBlockFromGrid();
            block.MoveLeft();
            PutBlockOnGrid();
        }
        private void MakeBlockMoveRight()
        {
            EraseBlockFromGrid();
            block.MoveRight();
            PutBlockOnGrid();
        }
        private void MakeBlockMoveDown()
        {
            EraseBlockFromGrid();
            block.Fall();
            PutBlockOnGrid();
        }
        private void MakeBlockRotate()
        {
            EraseBlockFromGrid();
            block.Rotate();
            PutBlockOnGrid();
        }

        public void TurnBlockIntoTakenFields()
        {
            foreach (Coords c in block.blockCoords)
                grid[c.row, c.col] = FieldStatus.Taken;
        }

        public void EraseBlockFromGrid()
        {
            foreach (Coords c in block.blockCoords)
            {
                ((Image)MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == c.row && Grid.GetColumn(e) == c.col)).Source = clearBlock;
            }
        }
        public void PutBlockOnGrid()
        {
            foreach (Coords c in block.blockCoords)
            {
                ((Image)MainGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == c.row && Grid.GetColumn(e) == c.col)).Source = block.img;
            }
        }

        private void EndGame()
        {
            MainGrid.IsEnabled = false;
            childLoopThread.Abort();
            EndGameGrid.Visibility = Visibility.Visible;
        }

        private void FinishGameButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            PrepareGame();
            StartGame();
            EndGameGrid.Visibility = Visibility.Hidden;
        }
    }
    public class Block
    {
        public Coords[] blockCoords = new Coords[4];
        public ImageSource img;
        public Shapes shape;
        public int horizontalOffset = 3;
        public int verticalOffset = 0;

        public Block(ImageSource imgSrc, Shapes shape)
        {
            img = imgSrc;
            this.shape = shape;
        }
        public void Fall()
        {
            for (int i = 0; i < blockCoords.Length; i++)
            {
                blockCoords[i].row++;
            }
            verticalOffset++;
        }
        public void MoveLeft()
        {
            for (int i = 0; i < blockCoords.Length; i++)
                blockCoords[i].col--;
            horizontalOffset--;
        }
        public void MoveRight()
        {
            for (int i = 0; i < blockCoords.Length; i++)
                blockCoords[i].col++;
            horizontalOffset++;
        }
        public void Rotate()
        {
            int offsetR = 20;
            int offsetC = 20;
            if (shape == Shapes.Line)
            {
                for (int i = 0; i < blockCoords.Length; i++)
                {
                    offsetC = (blockCoords[i].col < offsetC) ? blockCoords[i].col : offsetC;
                    offsetR = (blockCoords[i].row < offsetR) ? blockCoords[i].row : offsetR;
                }
                if (blockCoords[0].col == blockCoords[1].col)
                    offsetC--;
                else
                    offsetR--;
                for (int i = 0; i < blockCoords.Length; i++)
                {
                    blockCoords[i].col -= offsetC;
                    blockCoords[i].row -= offsetR;

                    int tmp = blockCoords[i].col;
                    blockCoords[i].col = blockCoords[i].row;
                    blockCoords[i].row = tmp;

                    blockCoords[i].col += offsetC;
                    blockCoords[i].row += offsetR;
                }
            }
            else if (shape != Shapes.Square)
            {
                if (shape == Shapes.LShapeReversed)
                    horizontalOffset++;
                else if (shape == Shapes.T)
                    verticalOffset--;
                for (int i = 0; i < blockCoords.Length; i++)
                {
                    blockCoords[i].col -= horizontalOffset;
                    blockCoords[i].row -= verticalOffset;

                    if (blockCoords[i].Equals(new Coords(1, 0)) || blockCoords[i].Equals(new Coords(1, 2)))
                    {
                        int tmp = blockCoords[i].row;
                        blockCoords[i].row = blockCoords[i].col;
                        blockCoords[i].col = tmp;
                    }
                    else if (blockCoords[i].Equals(new Coords(0, 1)))
                    {
                        blockCoords[i].col++;
                        blockCoords[i].row++;
                    }
                    else if (blockCoords[i].Equals(new Coords(2, 1)))
                    {
                        blockCoords[i].col--;
                        blockCoords[i].row--;
                    }
                    else if (blockCoords[i].Equals(new Coords(0, 0)))
                    {
                        blockCoords[i].col += 2;
                    }
                    else if (blockCoords[i].Equals(new Coords(0, 2)))
                    {
                        blockCoords[i].row += 2;
                    }
                    else if (blockCoords[i].Equals(new Coords(2, 2)))
                    {
                        blockCoords[i].col -= 2;
                    }
                    else if (blockCoords[i].Equals(new Coords(2, 0)))
                    {
                        blockCoords[i].row -= 2;
                    }

                    blockCoords[i].col += horizontalOffset;
                    blockCoords[i].row += verticalOffset;
                }

                if (shape == Shapes.LShapeReversed)
                    horizontalOffset--;
                else if (shape == Shapes.T)
                    verticalOffset++;
            }
        }
    }
}
