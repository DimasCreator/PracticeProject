using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PracticeSolution.CoreLogic.SimpleNumericalAlgorithms;
using PracticeSolution.CoreLogic.SortingAlgorithms;

namespace PracticeSolution.CoreLogic.PracticeTasks
{
    public class PracticeTasks_p1
    {
        /// <summary>//TODO: Решить
        /// У Василия есть число a, которое он хочет превратить в число b.
        /// Для этого он может производить два типа операций:
        /// -  умножить имеющееся у него число на 2 (то есть заменить число x числом 2·x);
        /// -  приписать к имеющемуся у него числу цифру 1 справа (то есть заменить число x числом 10·x+1).
        /// -  Вам надо помочь Василию получить из числа a число b с помощью описанных операций, либо сообщить, что это невозможно.
        /// Обратите внимание, что в этой задаче не требуется минимизировать количество операций.
        /// Достаточно найти любой из способов получить из числа a число b.
        /// https://tproger.ru/problems/technocup-problems-2017/ - разбор решения
        /// </summary>
        public void Task1()
        {
            
        }

        /// <summary>TODO: Решить
        /// Василий вышел из магазина, и ему стало интересно пересчитать сумму в чеке.
        /// Чек представляет собой строку, в которой названия покупок и их цены записаны подряд без пробелов.
        /// Чек имеет вид «name1price1name2price2…namenpricen»,
        /// где namei (название i-го продукта) — это непустая строка длины не более 10, состоящая из строчных букв латинского алфавита,
        /// а pricei (цена i-го продукта) — это непустая строка, состоящая из точек и цифр.
        /// Продукты с одинаковым названием могут иметь разные цены.
        /// Цена каждого продукта записана в следующем формате. Если продукт стоит целое количество рублей, то копейки не пишутся.
        /// Иначе, после записи количества рублей к цене приписывается точка,
        /// за которой следом ровно двумя цифрами записаны копейки (если копеек менее 10, то используется лидирующий ноль).
        /// Также, каждые три разряда (от менее значимых к более значимым) в записи рублей разделяются точками.
        /// Лишние лидирующие нули недопустимы, запись цены всегда начинается с цифры и заканчивается цифрой.
        /// Например, записи цен:
        /// «234», «1.544», «149.431.10», «0.99» и «123.05» являются корректными,
        /// «.333», «3.33.11», «12.00», «.33», «0.1234» и «1.2» не являются корректными.
        /// Напишите программу, которая по содержимому чека найдет суммарную цену всех покупок.
        /// https://tproger.ru/problems/technocup-problems-2017/ - разбор
        /// </summary>
        public void Task2()
        {
            
        }

        #region Task3 
        // На стандартный вход подается логическое выражение и его булевы значения, необходимо вернуть результат самого логического выражения.
        // Список возможных логических операторов:
        // & - логическое И
        // | - логическое ИЛИ
        // ! - отрицание
        // = - эквивалентность
        // Приоритет операторов соответствует стандартным приоритетам алгебры логики.
        //
        // Входные данные: a&b|c=d&!a a True b False c True d True
        // Выходные данные: False
        
        // Тестовые данные:
        // a&b|c=d&!a a True b False c True d True -> False
        // a&!b&a&a|!c=d&!a|!a|!b a True b False c True d True -> True
        // a&!b&c|a|a|c=c&b a True b False c True -> False
        // a&b&c&b&c&!a|!a&b=b|c|a a True b False c True -> False
        // a&b&c=!a|!k&d a True b False c True d True k True -> True
        // a&!b&a&a|!c=d a True b False c True d True -> True
        
        public bool Task3(string input)
        {
            string expression = GetBooleanExpressionFromString(input);
            string[] expressions = expression.Split('=');
            bool result = SolveBooleanExpression(expressions[0]) == SolveBooleanExpression(expressions[1]);
            return result;
        }
        private string GetBooleanExpressionFromString(string input)
        {
            string[] elements = input.Split(' ');
            Dictionary<string, int> members = new Dictionary<string, int>();
            
            for (int i = 1; i < elements.Length; i += 2)
            {
                members[elements[i]] = bool.Parse(elements[i + 1]) ? 1 : 0;
            }

            foreach (var pair in members)
            {
                elements[0] = elements[0].Replace(pair.Key.ToString(), pair.Value.ToString());
            }
            
            return elements[0];
        }
        private bool SolveBooleanExpression(string expression)
        {
            for (var i = 0; i < expression.Length; i++)
            {
                if (expression[i] != '!') continue;
                expression = expression.Remove(i, 1);
                expression = expression.Insert(i, expression[i] == '0' ? "1" : "0");
                expression = expression.Remove(i + 1, 1);
            }
            
            var multiplicationOperations = expression.Split('|');

            for (var j = 0; j < multiplicationOperations.Length; j++)
            {
                for (var i = 0; i < multiplicationOperations[j].Length; i++)
                {
                    if (multiplicationOperations[j][i] != '&') continue;

                    if (multiplicationOperations[j][i - 1] == '0' || multiplicationOperations[j][i + 1] == '0')
                    {
                        multiplicationOperations[j] = multiplicationOperations[j].Insert(i + 2, "0");
                        multiplicationOperations[j] = multiplicationOperations[j].Remove(i - 1, 3);
                        i--;
                    }
                    else
                    {
                        multiplicationOperations[j] = multiplicationOperations[j].Insert(i + 2, "1");
                        multiplicationOperations[j] = multiplicationOperations[j].Remove(i - 1, 3);
                        i--;
                    }
                }
            }

            string additionOperations = multiplicationOperations[0];
            for (int i = 1; i < multiplicationOperations.Length; i++)
            {
                additionOperations += "|" + multiplicationOperations[i];
            }

            for (int i = 0; i < additionOperations.Length; i++)
            {
                if (additionOperations[i] != '|') continue;

                if (additionOperations[i - 1] == '0' && additionOperations[i + 1] == '0')
                {
                    additionOperations = additionOperations.Insert(i + 2, "0");
                    additionOperations = additionOperations.Remove(i - 1, 3);
                    i--;
                }
                else
                {
                    additionOperations = additionOperations.Insert(i + 2, "1");
                    additionOperations = additionOperations.Remove(i - 1, 3);
                    i--;
                }
            }
            return additionOperations == "1";
        }

        #endregion

        #region Task4

        public class T4
        {
            //Написать код для разбиения на чанки
            //Задача с собеседования на стажировку Касперского
            public static IEnumerable<ICollection<T>> GetChunks<T>(IEnumerable<T> source, int chunkSize)
            {
                List<List<T>> chunksList = new List<List<T>>();
                chunksList.Add(new List<T>());
                int indexRow = 0;
                int currentChunkElementIndex = 0;
                foreach (var i in source)
                {
                    if (currentChunkElementIndex == chunkSize)
                    {
                        yield return chunksList[indexRow];
                        indexRow++;
                        currentChunkElementIndex = 0;
                        chunksList.Add(new List<T>());
                    }

                    chunksList[indexRow].Add(i);
                    currentChunkElementIndex++;
                }

                yield return chunksList[indexRow];
            }

            //Может более верное для yield
            public static IEnumerable<ICollection<T>> GetChunks2<T>(IEnumerable<T> source, int chunkSize)
            {
                int currentChunkElementIndex = 0;
                LinkedList<T> list = new LinkedList<T>();
                foreach (var i in source)
                {
                    if (currentChunkElementIndex == chunkSize)
                    {
                        yield return list;
                        list.Clear();
                        currentChunkElementIndex = 0;
                    }

                    list.AddLast(i);
                    currentChunkElementIndex++;
                }

                if (list.Count != 0) yield return list;
            }

            //Решение без yield с связанным списком
            public static IEnumerable<ICollection<T>> GetChunks3<T>(IEnumerable<T> source, int chunkSize)
            {
                LinkedList<LinkedList<T>> chunkList = new LinkedList<LinkedList<T>>();
                int currentChunkElementIndex = 0;
                LinkedList<T> currentChunkList = chunkList.AddLast(new LinkedList<T>()).Value;
                foreach (var i in source)
                {
                    if (currentChunkElementIndex >= chunkSize)
                    {
                        currentChunkElementIndex = 0;
                        currentChunkList = chunkList.AddLast(new LinkedList<T>()).Value;
                    }

                    currentChunkList.AddLast(i);
                    currentChunkElementIndex++;
                }

                return chunkList;
            }

            public static void Task()
            {
                var list = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
                var chunks = GetChunks3(list, 4);
                foreach (var chunk in chunks)
                {
                    foreach (var item in chunk)
                    {
                        Console.Write("{0} ", item);
                    }

                    Console.WriteLine();
                }
// Expected output:
// 1 2 3 4
// 5 6 7 8
// 9 10 11
            }
        }

        #endregion

        #region Yandex intern

        /// <summary>
        /// Вывести все простые числа из диапазона заданного пользователем
        /// Условность, что число больше 2
        /// </summary>
        public IEnumerable<int> Task5(int n)
        {
            if (n < 2) throw new Exception();
            return Enumerable.Range(2, n-1).Where(NumericAlgorithm.IsSimple);
        }

        /// <summary>
        /// на вход подается массив чисел, нужно вывести диапазоны одной строкой ([1, 4, 6, 5, 7, 2, 19] -> '1-2, 4-7, 9')
        /// </summary>
        public string Task6(int[] array)
        {
            StringBuilder diapasons = new StringBuilder();

            if(array == null || array.Length == 0)
            {
                return "";
            }
            if(array.Length == 1)
            {
                return array[0].ToString();
            }

            Sorter.BubbleSort(array);
            int currentStart = array[0];
            int currentFinish = array[0];
            for(int i = 1; i < array.Length; i++)
            {
                if(array[i] - array[i-1] <= 1)
                {
                    currentFinish = array[i];
                }
                else
                {
                    if (currentStart != currentFinish)
                        diapasons.Append($"[{currentStart}-{currentFinish}]");
                    else
                        diapasons.Append($"[{currentFinish}]");
                    currentStart = array[i];
                    currentFinish = array[i];
                }
            }
            if (currentStart != currentFinish)
                diapasons.Append($"[{currentStart}-{currentFinish}]");
            else
                diapasons.Append($"[{currentFinish}]");

            return diapasons.ToString();
        }
        /*Тесты
         * Console.WriteLine(YandexOnline.Task2(new[] { 1, 4, 6, 5, 7, 2, 9 })); // 1-2, 4-7, 9
         * Console.WriteLine(YandexOnline.Task2(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })); // 1-10
         * Console.WriteLine(YandexOnline.Task2(new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 })); // 1-10
         * Console.WriteLine(YandexOnline.Task2(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })); // 1
         * Console.WriteLine(YandexOnline.Task2(new[] { -4, -3, -2, -1, 0, 1, 2, 3, 4 })); // -4-4
         * Console.WriteLine(YandexOnline.Task2(new[] { -65, -64, -63, -62, -61 })); // -65--61
         * Console.WriteLine(YandexOnline.Task2(new[] { 100, 45, 78, -100, -34, -67, 412 })); // -100 -67 -34 45 78 100 412 
         * Console.WriteLine(YandexOnline.Task2(new[] { 1, 2, 3, 4, 9, 8, 7, 6 })); // 1-4 6-9
         * Console.WriteLine(YandexOnline.Task2(new[] { 1, 2, 3, 47, 8, 9, 15, 16, 17 })); // 1-3 8-9 15-17 47
         */

        /// <summary>
        /// Вывести максимальную длину подстроки с одинаковыми буквами из входной строки
        /// </summary>
        public static int Task7(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            int maxLength = 1;
            int currentLength = 1;
            for(int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    currentLength++;
                }
                else
                {
                    if(maxLength < currentLength)
                    {
                        maxLength = currentLength;
                    }
                    currentLength = 1;
                }
            }
            if (maxLength < currentLength)
            {
                maxLength = currentLength;
            }
            return maxLength;
        }
        /*Тесты
         * Console.WriteLine(YandexOnline.Task3("aaaavvvvccccc")); // 5
         * Console.WriteLine(YandexOnline.Task3("abjsanbjnasjvbdijansdvjisdn;iccccccmnbflksdnklfdnksblnfkdsl")); // 6
         * Console.WriteLine(YandexOnline.Task3("ccbbccbbccbbccbb")); // 2
         * Console.WriteLine(YandexOnline.Task3("")); // 0
         * Console.WriteLine(YandexOnline.Task3(null)); // 0
         * Console.WriteLine(YandexOnline.Task3("a")); // 1
         * Console.WriteLine(YandexOnline.Task3("anvmdlhgjrl;'dcksa")); // 1
         * Console.WriteLine(YandexOnline.Task3("nbmfjdjjjjdkdlcllldjkrkkkkkkk")); // 7
         * Console.WriteLine(YandexOnline.Task3("asdczxmnb")); // 1
         */

        #endregion
        
        /// <summary>
        /// Дано: Массив целых чисел, в котором каждое число встречается два раза, и лишь одно число 1 раз.
        /// Надо: Найти это число.
        /// </summary>
        public int Task8()
        {
            int[] sequence = {1, 2, 3, 4, 65, 4, 3, 2, 1};
            var single = sequence.Aggregate(0, (acc, num) => acc ^ num);
            return single;
        }
    }
}