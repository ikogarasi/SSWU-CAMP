using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10_Matrix
{
    public class Matrix
    {
        private int[,] _data;
        public int NumRows { get; private set; }
        public int NumCols { get; private set; }

        public Matrix(int rows, int cols)
        {
            Reset(rows, cols);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < NumRows; ++i)
                for (int j = 0; j < NumCols; j++)
                {
                    yield return _data[i, j];
                }
        }

        public void Reset(int newRows, int newCols)
        {
            if (newRows <= 0 || newCols <= 0)
            {
                throw new ArgumentOutOfRangeException("Error: " + ((newRows < 0) ? nameof(newRows) : nameof(newCols)) + " must be > 0");
            }
            NumRows = newRows;
            NumCols = newCols;
            _data = new int[newRows, newCols];
        }

        public IEnumerable<int> GetDiagonalMatrix()
        {
            for (int i = 0; i < NumRows; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j <= i; ++j)
                    {
                        yield return _data[j, i - j];
                    }
                }
                else
                {
                    for (int j = i; j >= 0; --j)
                    {
                        yield return _data[j, i - j];
                    }
                }
            }

            for (int i = NumRows; i <= (NumRows - 1) * 2; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = i - NumRows + 1; j < NumRows; ++j)
                    {
                        yield return _data[j, i - j];
                    }
                }
                else
                {
                    for (int j = NumRows - 1; j >= i - NumRows + 1; --j)
                    {
                        yield return _data[j, i - j];
                    }
                }
            }
        }

        public IEnumerable<int> GetHorizontalMatrix()
        {
            for (int i = 0; i < NumRows; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < NumCols; ++j)
                    {
                        yield return _data[i, j];
                    }
                }

                else
                {
                    for (int j = NumCols - 1; j >= 0; --j)
                    {
                        yield return _data[i, j];
                    }
                }
            }
        }

        public void GenerateHorizontalSnake()
        {
            int value = 0;

            for (int i = 0; i < NumRows; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < NumCols; ++j)
                    {
                        _data[i, j] = ++value;
                    }
                }

                else
                {
                    for (int j = NumCols - 1; j >= 0; --j)
                    {
                        _data[i, j] = ++value;
                    }
                }
            }
        }

        public void GenerateDiagonalSnake()
        {
            if (NumCols != NumRows)
            {
                throw new ArgumentException("Number of columns must be equal to number of rows");
            }

            int value = 0;

            for (int i = 0; i < NumRows; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j <= i; ++j)
                    {
                        _data[j, i - j] = ++value;
                    }
                }
                else
                {
                    for (int j = i; j >= 0; --j)
                    {
                        _data[j, i - j] = ++value;
                    }
                }
            }

            for (int i = NumRows; i <= (NumRows - 1) * 2; ++i)
            {
                if (i % 2 == 0)
                {
                    for (int j = i - NumRows + 1; j < NumRows; ++j)
                    {
                        _data[j, i - j] = ++value;
                    }
                }
                else
                {
                    for (int j = NumRows - 1; j >= i - NumRows + 1; --j)
                    {
                        _data[j, i - j] = ++value;
                    }
                }
            }
        }

        public override string ToString()
        {
            string temp = NumRows + " " + NumCols + "\n";
            for (int i = 0; i < NumRows; ++i)
            {
                for (int j = 0; j < NumCols; j++)
                {
                    temp += _data[i, j] + " ";
                }
                temp += "\n";
            }

            return temp;
        }
    }
}
