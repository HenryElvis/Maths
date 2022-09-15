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
        }

        public int this[int i, int j] => _array[i, j];

        public MatrixInt(int[,] newArray)
        {
            NbLines = newArray.GetLength(0);
            NbColumns = newArray.GetLength(1);
            
            _array = newArray;
        }

        public int[,] ToArray2D()
        {
            return new int[NbLines, NbColumns];
        }
    }
}