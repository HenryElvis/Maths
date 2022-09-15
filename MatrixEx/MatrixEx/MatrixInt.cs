﻿namespace MatrixEx
{
    public class MatrixInt
    {
        public int NbLines = 0;
        public int NbColumns = 0;

        private int[,] _array;
        private MatrixInt _matrix;

        public MatrixInt(int lines, int column)
        {
            NbLines = lines;
            NbColumns = column;

            // _matrix = new MatrixInt(NbLines, NbColumns);
        }

        public int this[int i, int j]
        {
            get => _array[i, j];
            set
            {
                // _matrix = new MatrixInt(i, j);
                //_array[i, j] = value;
                // _matrix = new MatrixInt(_array);
                _matrix[i, j] = value;
                // this[i, j] = value;
            }
            
            // set => _matrix[i, j] = value;
        }

        public MatrixInt(int[,] newArray)
        {
            NbLines = newArray.GetLength(0);
            NbColumns = newArray.GetLength(1);
            
            _array = newArray;
        }

        public MatrixInt(MatrixInt matrix)
        {
            // _array = matrix;
        }

        public int[,] ToArray2D()
        {
            return new int[NbLines, NbColumns];
        }
    }
}