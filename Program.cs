using System;

namespace MOVD_Lab3_Part3
{

    internal class Program
    {
        static void Main()
        {
            double[,] matrix = {
            {-2, -2, 6, -9, 6},
            {0, 6, 20, -18, 20},
            {-8, 7, -10, -7, -9},
            {-1, -17, 0, -6, 24},
            {5, 5, 15, -11, 2}
            };

            double determinant = CalculateDeterminant(matrix, 5); // ми передаємо нашу матрицю, яка в нас 5х5 
            Console.WriteLine("Determinant: " + determinant + " . So now we can start out calculatoions :"); //і відразу знаючи її розмірність(n=5) -> ми можемо передати її в метод
            Console.WriteLine();

            if (determinant != 0) //дефолтна перевірка на невиродженість матриці
            {
                double[] solutions = SolveLinearEquations(matrix, 5);
                Console.WriteLine();
                Console.WriteLine("Solutions for System of linear equations :");
                for (int i = 0; i < solutions.Length; i++)
                {
                    Console.WriteLine("x" + (i + 1) + " = " + solutions[i]);
                }
            }
            else
            {
                Console.WriteLine("Determinant = 0 !");
            }
        }

        //Наш головний метод для знаходження визначника для Методу Гауса використовуючи рекурсивну функцію :
        static double CalculateDeterminant(double[,] matrix, int n)
        {
            if (n == 1)
            {
                return matrix[0, 0]; //просто виняток який поверне матрицю 1х1, в нас такого не буде ;)
            }


            double det = 0; // на визначник
            int sign = 1;   // змінна знаку для подальших обчислень

            //тут бігаємо циклом по першому рядку матриці 
            for (int i = 0; i < n; i++)
            {
                //і вже по ходу циклу ми створюємо підматрицю виключивши поточний стовпчик і рядок
                double[,] submatrix = new double[n - 1, n - 1];

                //Тепер ми заповнюємо нашу підматрицю пропустивши поточний рядок і стовпчик
                for (int j = 1; j < n; j++)
                {
                    for (int k = 0; k < n; k++) 
                    {
                        if (k < i)// якщо правдива уя умова - виключаємо поточний j-рядок та i-стовпчик та додаємо елемент з початкової в підматрицю 
                        {
                            submatrix[j - 1, k] = matrix[j, k]; // ну і -1 щоб не виходити за межі масиву і правильно заповнити підматрицю
                        }
                        else if (k > i)// якщо правдива ця умова - виключаємо поточний j-рядок та i-стовпчик та додаємо елемент з початкової в підматрицю 
                        {
                            submatrix[j - 1, k - 1] = matrix[j, k]; //// ну і -1(для рядка і стовпця) щоб не виходити за межі масиву і правильно заповнити підматрицю
                        }
                    }
                }

                //Тут ми використовуємо властивість рекурсії функції для знаходження визначника
                //Відповідно множимо результат на поточник елемент матриці ну і ще знак
                det += sign * matrix[0, i] * CalculateDeterminant(submatrix, n - 1);

                //Змінюємо знак для наступного члена
                sign = -sign;
            }
            //визначник aka Δ aka detΔ
            return det;
        }

        static double[] SolveLinearEquations(double[,] matrix, int n)
        {
            double[] solutions = new double[n];

            for (int i = 0; i < n; i++)
            {
                double[,] tempMatrix = new double[n, n];

                //Тепер тут вже для СЛАР створюємо тимчасову матрицю без стовпця i
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (k < i)
                            tempMatrix[j, k] = matrix[j, k];
                        else if (k > i)
                            tempMatrix[j, k - 1] = matrix[j, k];
                    }
                }

                //Шукаємо визначник для тимчасової матриці
                double tempDeterminant = CalculateDeterminant(tempMatrix, n - 1);
                Console.WriteLine($"Temp determinant result = {tempDeterminant}");

                // Знаходимо розв'язок рівняння: xi = Det(tempMatrix) / Det(matrix)
                solutions[i] = tempDeterminant / CalculateDeterminant(matrix, n);
            }

            return solutions;
        }
    }

}