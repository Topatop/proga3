using System;

class Program
{


    static void Main()
    {
        // Часть 1
        const int N = 10; 
        double[] array = { 1.5, -2.0, 3.7, -4.2, 0.0, 5.6, -7.8, 9.3, -0.5, 6.1 };
        
        Console.WriteLine("Исходный массив: " + string.Join(", ", array));
        
        Console.WriteLine($"Сумма элементов массива с нечетными номерами: {SumOddIndexedElements(array)}");
        Console.WriteLine($"Сумма элементов между первым и последним отрицательными элементами: {SumBetweenNegatives(array)}");

        CompressArray(ref array);

        Console.WriteLine("Сжатый массив: " + string.Join(", ", array));
        
        // Часть 2
        int[,] matrix =
        {
            {1, 2, 3, 4, 5},
            {6, 7, 8, 9, 10},
            {11, 12, 13, 14, 15},
            {16, 17, 18, 19, 20},
            {21, 22, 23, 24, 25}
        };

        PrintMatrix(matrix);

        int productOfRowsWithoutNegatives = ProductOfRowsWithoutNegatives(matrix);
        Console.WriteLine($"Произведение элементов в строках без отрицательных элементов: {productOfRowsWithoutNegatives}");

        int maxDiagonalSum = MaxDiagonalSum(matrix);
        Console.WriteLine($"Максимум среди сумм элементов диагоналей, параллельных главной диагонали: {maxDiagonalSum}");
    }

    private static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static double SumOddIndexedElements(double[] array)
    {
        double sum = 0;
        for (int i = 1; i < array.Length; i += 2)
        {
            sum += array[i];
        }
        return sum;
    }

    static double SumBetweenNegatives(double[] array)
    {
        int firstNegativeIndex = -1;
        int lastNegativeIndex = -1;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < 0)
            {
                if (firstNegativeIndex == -1)
                {
                    firstNegativeIndex = i;
                }
                lastNegativeIndex = i;
            }
        }

        double sum = 0;

        if (firstNegativeIndex != -1 && lastNegativeIndex != -1 && firstNegativeIndex != lastNegativeIndex)
        {
            for (int i = firstNegativeIndex + 1; i < lastNegativeIndex; i++)
            {
                sum += array[i];
            }
        }

        return sum;
    }

    static void CompressArray(ref double[] array)
    {
        int newSize = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (Math.Abs(array[i]) > 1.0)
            {
                array[newSize++] = array[i];
            }
        }

        // Заполняем оставшиеся элементы массива нулями
        while (newSize < array.Length)
        {
            array[newSize++] = 0.0;
        }
    }
    
    static int ProductOfRowsWithoutNegatives(int[,] matrix)
    {
        int product = 1;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            bool hasNegative = false;

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] < 0)
                {
                    hasNegative = true;
                    break;
                }
            }

            if (!hasNegative)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    product *= matrix[i, j];
                }
            }
        }

        return product;
    }

    static int MaxDiagonalSum(int[,] matrix)
    {
        int maxSum = int.MinValue;

        // Суммируем элементы диагоналей, параллельных главной
        for (int k = 0; k < matrix.GetLength(1); k++)
        {
            int diagonalSum = 0;
            for (int i = 0, j = k; i < matrix.GetLength(0) && j < matrix.GetLength(1); i++, j++)
            {
                diagonalSum += matrix[i, j];
            }

            maxSum = Math.Max(maxSum, diagonalSum);
        }

        for (int k = 1; k < matrix.GetLength(0); k++)
        {
            int diagonalSum = 0;
            for (int i = k, j = 0; i < matrix.GetLength(0) && j < matrix.GetLength(1); i++, j++)
            {
                diagonalSum += matrix[i, j];
            }

            maxSum = Math.Max(maxSum, diagonalSum);
        }

        return maxSum;
    }
    
}
