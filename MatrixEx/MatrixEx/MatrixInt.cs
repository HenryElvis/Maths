using System;
using System.Collections.Generic;

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
            matrix = new MatrixInt(matrix._array);
            
            int line = 0;
            int column = 0;

            line = matrix.NbLines < NbLines ? NbLines : matrix.NbLines;
            column = matrix.NbColumns < NbColumns ? NbColumns : matrix.NbColumns;
            
            MatrixInt newMatrix = new MatrixInt(line, column);
            
            if (matrix.NbLines != NbColumns)
                throw new MatrixMultiplyException();
            
            for (int x = 0; x < line; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    newMatrix[x, y] = _array[x, 0] * matrix._array[0, y] + _array[x, 1] * 
                        matrix._array[1, y];
                }
            }
            
            return newMatrix;
        }        
        
        public static MatrixInt Multiply(MatrixInt a, MatrixInt b)
        {
            int line = 0;
            int column = 0;

            line = a.NbLines < b.NbLines ? b.NbLines : a.NbLines;
            column = a.NbColumns < b.NbColumns ? b.NbColumns : a.NbColumns;
            
            MatrixInt newMatrix = new MatrixInt(line, column);

            if (b.NbLines != a.NbColumns)
                throw new MatrixMultiplyException();
            
            for (int x = 0; x < line; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    newMatrix[x, y] = a._array[x, 0] * b._array[0, y] + a._array[x, 1] * b._array[1, y];
                }
            }
            
            return newMatrix;
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
            return Multiply(a, b);
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

        public MatrixInt Transpose()
        {
            MatrixInt matrix = new MatrixInt(NbColumns, NbLines);

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    matrix[x, y] = _array[y, x];
                }
            }
            
            return matrix;
        }

        public static MatrixInt Transpose(MatrixInt matrix)
        {
            MatrixInt newMatrix = new MatrixInt(matrix.NbColumns, matrix.NbLines);

            for (int x = 0; x < newMatrix.NbLines; x++)
            {
                for (int y = 0; y < newMatrix.NbColumns; y++)
                {
                    newMatrix[x, y] = matrix[y, x];
                }
            }
            
            return newMatrix;
        }

        public static MatrixInt GenerateAugmentedMatrix(MatrixInt matrix, MatrixInt matrix1)
        {
            MatrixInt firstMatrix = new MatrixInt(matrix.NbLines, matrix.NbColumns + 1);
            MatrixInt secondMatrix = new MatrixInt(matrix.NbLines, matrix.NbColumns + 1);

            for (int x = 0; x < firstMatrix.NbLines; x++)
            {
                for (int y = 0; y < firstMatrix.NbColumns; y++)
                {
                    if (y != matrix.NbColumns)
                        firstMatrix[x, y] = matrix[x, y];
                    else
                        firstMatrix[x, matrix.NbColumns] = 0;

                    // Console.WriteLine($"({x}, {y}) : {firstMatrix[x, y]}");
                }
            }
            
            for (int x = 0; x < secondMatrix.NbLines; x++)
            {
                Console.WriteLine(matrix1[0, 0]);
                
                for (int y = 0; y < secondMatrix.NbColumns; y++)
                {
                    if (y == matrix.NbColumns)
                        secondMatrix[x, y] = matrix1[x, 0];
                    else
                        secondMatrix[x, y] = 0;
                    
                    // Console.WriteLine($"({x}, {y}) : {secondMatrix[x, y]}");
                }
            }

            return firstMatrix + secondMatrix;
        }

        public (MatrixInt, MatrixInt) Split(int num)
        {
            MatrixInt matrixDefault = new MatrixInt(NbLines,NbColumns);
            
            MatrixInt matrix = new MatrixInt(matrixDefault.NbLines,matrixDefault.NbColumns - 1);
            MatrixInt matrix1 = new MatrixInt(matrixDefault.NbLines,matrixDefault.NbColumns - num - 1);

            for (int x = 0; x < matrixDefault.NbLines; x++)
            {
                for (int y = 0; y < matrixDefault.NbColumns; y++)
                {
                    matrixDefault[x, y] = _array[x, y];
                }
            }
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    matrix[x, y] = matrixDefault[x, y];
                }
            }

            for (int x = 0; x < matrixDefault.NbLines; x++)
            {
                for (int y = 0; y < matrixDefault.NbColumns; y++)
                {
                    if (y == matrixDefault.NbColumns)
                        matrix1[x, 0] = matrixDefault[x, y];
                }
            }
            
            return (matrix, matrix1);
        }
    }

    public static class MatrixElementaryOperations
    {
        public static void SwapLines(MatrixInt matrix, int firstLine, int otherLine)
        {
            List<int> line = new List<int>();
            List<int> line1 = new List<int>();
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == firstLine)
                        line.Add(matrix[x, y]);
                    if (x == otherLine)
                        line1.Add(matrix[x, y]);
                }
            }

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == firstLine)
                        matrix[x, y] = line1[y];
                    if (x == otherLine)
                        matrix[x, y] = line[y];
                }
            }
        }
        
        public static void SwapLines(MatrixFloat matrix, int firstLine, int otherLine)
        {
            List<float> line = new List<float>();
            List<float> line1 = new List<float>();
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == firstLine)
                        line.Add(matrix[x, y]);
                    if (x == otherLine)
                        line1.Add(matrix[x, y]);
                }
            }

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == firstLine)
                        matrix[x, y] = line1[y];
                    if (x == otherLine)
                        matrix[x, y] = line[y];
                }
            }
        }        
        
        public static void SwapColumns(MatrixInt matrix, int firstColumn, int otherColumn)
        {
            List<int> column = new List<int>();
            List<int> column1 = new List<int>();
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (y == firstColumn)
                        column.Add(matrix[x, y]);
                    if (y == otherColumn)
                        column1.Add(matrix[x, y]);
                }
            }

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (y == firstColumn)
                        matrix[x, y] = column1[x];
                    if (y == otherColumn)
                        matrix[x, y] = column[x];
                }
            }
        }

        public static void MultiplyLine(MatrixInt matrix, int line, int mult)
        {
            if (mult == 0)
                throw new MatrixScalarZeroException();
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == line)
                        matrix[x, y] *= mult;
                }
            }
        }
        
        public static void DivideLine(MatrixFloat matrix, int line, float div)
        {
            if (div == 0)
                throw new MatrixScalarZeroException();
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == line)
                        matrix[x, y] /= div;
                }
            }
        }

        public static void MultiplyColumn(MatrixInt matrix, int column, int mult)
        {
            if (mult == 0)
                throw new MatrixScalarZeroException();
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (y == column)
                        matrix[x, y] *= mult;
                }
            }
        }

        public static void AddLineToAnother(MatrixInt matrix, int line0, int line1, int mult)
        {
            List<int> firstLine = new List<int>();

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == line0)
                        firstLine.Add(matrix[x, y] * mult);
                }
            }

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (x == line1)
                        matrix[x, y] += firstLine[y];
                }
            }
        }

        public static void AddColumnToAnother(MatrixInt matrix, int column0, int column1, int mult)
        {
            List<int> firstColumn = new List<int>();

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (y == column0)
                        firstColumn.Add(matrix[x, y] * mult);
                }
            }

            for (int x = 0; x < matrix.NbLines; x++)
            {
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    if (y == column1)
                        matrix[x, y] += firstColumn[x];
                }
            }
        }
    }

    public class MatrixFloat
    {
        public int NbLines = 0;
        public int NbColumns = 0;

        private float[,] _array;

        public float this[int i, int j]
        {
            get => _array[i, j];
            set => _array[i, j] = value;
        }
        
        public MatrixFloat(float[,] array)
        {
            NbLines = array.GetLength(0);
            NbColumns = array.GetLength(1);

            _array = new float[NbLines, NbColumns];
            Array.Copy(array, _array, _array.Length);
        }

        public float[,] ToArray2D()
        {
            return _array;
        }
    }

    public class MatrixRowReductionAlgorithm
    {
        public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat m0, MatrixFloat m1)
        {
            MatrixFloat matrix = new MatrixFloat(new float[m0.NbLines, m0.NbColumns]);
            MatrixFloat matrix1 = new MatrixFloat(new float[m1.NbLines, m1.NbColumns]);

            // MatrixElementaryOperations.SwapLines(m0, 0, 1);
            // MatrixElementaryOperations.DivideLine(m0, 0, 3);
            
            for (int x = 0; x < matrix.NbLines; x++)
            {
                // if (m0[x, 0] < m0[++x, 0])
                //     MatrixElementaryOperations.SwapLines(m0, x, ++x);
                
                for (int y = 0; y < matrix.NbColumns; y++)
                {
                    Console.WriteLine(m0[x, y]);
                }
            }
            
            return (m0, m1);
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
        public MatrixMultiplyException()
        {
            
        }
    }

    public class MatrixScalarZeroException : Exception
    {
        public MatrixScalarZeroException()
        {
            
        }
    }
}