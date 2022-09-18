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

            // _array = newArray;
            
            _array = new int[this.NbLines,this.NbColumns];
            Array.Copy(newArray, _array, _array.Length);
        }

        public MatrixInt(MatrixInt matrix)
        {
            // _array = new int[NbLines, NbColumns];
            // _array =  matrix._array;

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
            return matrix.Multiply(mult);
        }
        
        public int[,] ToArray2D()
        {
            return _array;
        }
    }
}