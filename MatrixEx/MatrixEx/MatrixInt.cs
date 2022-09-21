using System;

namespace MatrixEx
{
    public class MatrixInt
    {
        public int NbLines = 0;
        public int NbColumns = 0;

        private int[,] _array;

        public MatrixInt(int lines, int column)
        {
            NbLines = lines;
            NbColumns = column;

            _array = new int[NbLines, NbColumns];
        }

        public int this[int i, int j]
        {
            get => _array[i, j];
            set => _array[i, j] = value;
        }

        public MatrixInt(int[,] newArray)
        {
            NbLines = newArray.GetLength(0);
            NbColumns = newArray.GetLength(1);

            _array = new int[this.NbLines, this.NbColumns];
            Array.Copy(newArray, _array, _array.Length);
        }

        public MatrixInt(MatrixInt matrix)
        {
            NbLines = matrix.NbLines;
            NbColumns = matrix.NbColumns;

            _array = new int[matrix.NbLines, matrix.NbColumns];
            Array.Copy(matrix._array, _array, _array.Length);
        }

        private MatrixInt(int val)
        {
            NbLines = val;
            NbColumns = val;

            _array = new int[NbLines, NbColumns];

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i == j)
                    {
                        _array[i, j] = 1;
                    }
                }
            }
        }

        public static MatrixInt Identity(int value)
        {
            return new MatrixInt(value);
        }

        public bool IsIdentity()
        {
            if (_array.GetLength(0) != _array.GetLength(1))
                return false;

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    if (i == j)
                    {
                        if (_array[i, j] != 1)
                            return false;
                    }
                    else
                    {
                        if (_array[i, j] != 0)
                            return false;
                    }
                }
            }

            return true;
        }

        public MatrixInt Multiply(int mult)
        {
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                {
                    _array[i, j] *= mult;
                }
            }

            return new MatrixInt(_array);
        }

        public static MatrixInt Multiply(MatrixInt matrix, int mult)
        {
            matrix = new MatrixInt(matrix._array);

            return matrix.Multiply(mult);
        }

        public MatrixInt Multiply(MatrixInt matrix)
        {
            return matrix;
        }        
        
        public static MatrixInt Multiply(MatrixInt a, MatrixInt b)
        {
            return a;
        }

        public static MatrixInt operator *(MatrixInt a, int mult)
        {
            return Multiply(a, mult);
        }

        public static MatrixInt operator *(int mult, MatrixInt a)
        {
            return Multiply(a, mult);
        }

        public static MatrixInt operator *(MatrixInt a, MatrixInt b)
        {
            return a;
        }

        public static MatrixInt operator -(MatrixInt a)
        {
            a *= -1;
            
            return new MatrixInt(a);
        }

        public void Add(MatrixInt matrix)
        {
            matrix = new MatrixInt(matrix._array);

            if (matrix.NbLines != NbLines ||
                matrix.NbColumns != NbColumns)
                throw new MatrixSumException();
            
            for (int i = 0; i <= NbLines - 1; i++)
            {
                for (int j = 0; j <= NbColumns - 1; j++)
                {
                    _array[i, j] += matrix._array[i, j];
                }
            }
        }

        public static MatrixInt Add(MatrixInt a, MatrixInt b)
        {
            MatrixInt newMatrix = null;

            a = new MatrixInt(a._array);
            b = new MatrixInt(b._array);

            if (a.NbLines != b.NbLines ||
                a.NbColumns != b.NbColumns)
                throw new MatrixSumException();
            
            for (int i = 0; i <= a.NbLines - 1; i++)
            {
                for (int j = 0; j <= a.NbColumns - 1; j++)
                {
                    a[i, j] = a._array[i, j] + b._array[i, j];

                    newMatrix = new MatrixInt(a);
                }
            }

            return newMatrix;
        }

        public static MatrixInt operator-(MatrixInt a, MatrixInt b)
        {
            MatrixInt newMatrix = null;

            a = new MatrixInt(a._array);
            b = new MatrixInt(b._array);

            for (int i = 0; i <= a.NbLines - 1; i++)
            {
                for (int j = 0; j <= a.NbColumns - 1; j++)
                {
                    a[i, j] = a._array[i, j] - b._array[i, j];

                    newMatrix = new MatrixInt(a);
                }
            }

            return newMatrix;
        }

        public static MatrixInt operator +(MatrixInt a, MatrixInt b)
        {
            return Add(a, b);
        }

        public int[,] ToArray2D()
        {
            return _array;
        }
    }

    public class MatrixSumException : Exception
    {
        public MatrixSumException()
        {
            
        }
    }

    public class MatrixMultiplyException : Exception
    {
        
    }
}