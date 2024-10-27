using System;
using System.Diagnostics;

namespace Lab6
{
    class Program
    {
        static void Main()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true;

            int n = GetValidInput("Введите до какого числа мне загадывать:", "Установочное число (максимум)");
            int k = GetValidInput("Введите количество попыток на угадывание числа:", "Установленное количество попыток");

            Random rnd = new();
            int number = rnd.Next(1, n);
            Trace.WriteLine($"{DateTime.Now}: [INFO] Сгенерированное число для угадывания: {number}");

            int win = Game(k, number, n);
            if (win == 0)
            {
                Console.WriteLine("Ты проиграл. Я загадал число {0}", number);
                Trace.WriteLine($"{DateTime.Now}: [RESULT] Проигрыш. Число было: {number}");
            }

            Trace.Close();
        }

        private static int GetValidInput(string prompt, string logLabel)
        {
            int result;
            while (true)
            {
                Console.WriteLine(prompt);
                string input = "" + Console.ReadLine();
                Trace.WriteLine($"{DateTime.Now}: [INFO] Введено значение для {logLabel}: {input}");

                if (int.TryParse(input, out result) && result > 0)
                {
                    Trace.WriteLine($"{DateTime.Now}: [INFO] Принято корректное значение для {logLabel}: {result}");
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода: пожалуйста, введите положительное целое число.");
                    Trace.WriteLine($"{DateTime.Now}: [ERROR] Некорректный ввод для {logLabel}");
                }
            }
            return result;
        }

        public static int Game(int k, int number, int n)
        {
            int win1 = 0;
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine("Угадайте число от 1 до {0}", n);
                string input = "" + Console.ReadLine();
                Trace.WriteLine($"{DateTime.Now}: [INFO] Введённое пользователем число: {input}");

                if (!int.TryParse(input, out int a))
                {
                    Console.WriteLine("Вы ввели не целое число.");
                    Trace.WriteLine($"{DateTime.Now}: [ERROR] Некорректный ввод: не удалось преобразовать в целое число.");
                    i--;
                }
                else
                {
                    switch (a)
                    {
                        case int b when b == number:
                            Console.WriteLine("Поздравляю! Вы угадали число {0} за {1} попыток", number, i + 1);
                            Trace.WriteLine($"{DateTime.Now}: [RESULT] Успех. Число угадано: {b}. Попыток: {i + 1}");
                            win1 = 1;
                            i = k;
                            break;

                        case int b when b < 0 || b > n:
                            Console.WriteLine("Ваше число выходит из диапазона от 1 до {0}", n);
                            Trace.WriteLine($"{DateTime.Now}: [WARNING] Число вне диапазона: {b}");
                            i--;
                            break;

                        case int b when b > number:
                            Console.WriteLine("Загаданное число меньше. Осталось {0} попыток.", k - i - 1);
                            Trace.WriteLine($"{DateTime.Now}: [INFO] Попытка {i + 1}: число {b} больше загаданного");
                            break;

                        case int b when b < number:
                            Console.WriteLine("Загаданное число больше. Осталось {0} попыток.", k - i - 1);
                            Trace.WriteLine($"{DateTime.Now}: [INFO] Попытка {i + 1}: число {b} меньше загаданного");
                            break;
                    }
                }
            }
            return win1;
        }
    }
}
