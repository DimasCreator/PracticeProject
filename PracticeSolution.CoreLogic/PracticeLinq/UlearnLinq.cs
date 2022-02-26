using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PracticeSolution.CoreLogic.PracticeLinq
{
    public class UlearnLinq
    {
        /// <summary>
        /// Каждая строка либо пустая, либо содержит одно целое число. вернуть массив чисел
        /// </summary>
        public static int[] Task1(IEnumerable<string> lines)
        {
            return lines.Where(l => !string.IsNullOrEmpty(l)).Select(int.Parse).ToArray();
        }
        
        /// <summary>
        /// В каждой строке написаны две координаты точки (X, Y), разделенные пробелом.
        /// Реализуйте метод создающий из строк объекты Point в одно LINQ-выражение.
        /// Постарайтесь не использовать функцию преобразования строки в число более одного раза.
        /// </summary>
        public class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X, Y;
        }
        public static Point[] Task2(IEnumerable<string> lines)
        {
            return lines.Select(l => l.Split(' '))
                .Select(p => new Point(int.Parse(p[0]), int.Parse(p[1])))
                .ToArray();
        }
        public static IEnumerable<Point> Task3(Point p)
        {
            int[] d = {-1, 0, 1};
            return d.SelectMany(dx => d.Select(dy => new Point(p.X + dx, p.Y + dy)))//для каждого dx проходимся по dy
                .Where(point => !point.Equals(p));
        }
        
        /// <summary>
        /// Вам дан список всех классов в школе. Нужно получить список всех учащихся всех классов.
        /// Напишите решение этой задачи с помощью LINQ в одно выражение.
        /// </summary>
        public class Classroom
        {
            public List<string> Students = new List<string>();
        }
        public static string[] Task4(Classroom[] classes)
        {
            return classes.SelectMany(c => c.Students).ToArray();
        }
        
        /// <summary>
        /// Текст задан массивом строк. Вам нужно составить лексикографически упорядоченный список всех встречающихся в этом тексте слов.
        /// Слова нужно сравнивать регистронезависимо, а выводить в нижнем регистре.
        /// </summary>
        public static string[] Task5(params string[] textLines)
        {
            return textLines.SelectMany(l => Regex.Split(l, @"\W+"))
                .Where(w => !string.IsNullOrEmpty(w))
                .Select(w=>w.ToLower())
                .Distinct()
                .OrderBy(w=>w)
                .ToArray();
        }
        
        /// <summary>
        /// Дан текст, нужно составить список всех встречающихся в тексте слов, упорядоченный сначала по возрастанию длины слова, а потом лексикографически.
        /// Запрещено использовать ThenBy и ThenByDescending.
        /// </summary>
        public static List<string> Task6(string text)
        {
            return Regex.Split(text, @"\W+")
                .Where(s=>!string.IsNullOrEmpty(s))
                .Select(s=>s.ToLower())
                .Distinct()
                .OrderBy(s => Tuple.Create(s.Length, s)).ToList();
        }
        
        /// <summary>
        /// Дан список слов, нужно найти самое длинное слово из этого списка, а из всех самых длинных — лексикографически первое слово.
        /// Решите эту задачу в одно выражение.
        /// Не используйте методы сортировки — сложность сортировки O(N * log(N)), однако эту задачу можно решить за O(N).
        /// </summary>
        public static string Task7Aggregate(IEnumerable<string> words)
        {
            return words.Aggregate(words.First(),
                (max, s) => s.Length > max.Length ? s :
                    s.Length == max.Length ? string.CompareOrdinal(s, max) < 0 ? s : max : max);
        }
        public static string Task7Max(IEnumerable<string> words)
        {
            return words.Min(s => Tuple.Create(-s.Length, s))?.Item2;
        }
        
        /// <summary>
        /// Дан текст, нужно вывести count наиболее часто встречающихся в тексте слов вместе с их частотой.
        /// Среди слов, встречающихся одинаково часто, отдавать предпочтение лексикографически меньшим словам.
        /// Слова сравнивать регистронезависимо и выводить в нижнем регистре.
        /// </summary>
        public static Tuple<string, int>[] Task8(string text, int count)
        {
            return Regex.Split(text, @"\W+")
                .Where(word => !string.IsNullOrEmpty(word))
                .GroupBy(word => word.ToLower())
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .Take(count)
                .Select(g => Tuple.Create(g.Key, g.Count()))
                .ToArray();
        }
        
        /// <summary>
        /// Обратный индекс — это структура данных, часто использующаяся в задачах полнотекстового поиска нужного документа в большой базе документов.
        /// По своей сути обратный индекс напоминает индекс в конце бумажных энциклопедий, где для каждого ключевого слова указан список страниц, где оно встречается.
        /// Вам требуется по списку документов построить обратный индекс.
        /// Документ определен так:
        /// </summary>
        public static ILookup<string, int> Task9(Document[] documents)
        {
            return documents
                .SelectMany(d => Regex.Split(d.Text.ToLower(), @"\W+"), (d, word) => Tuple.Create(word, d.Id))
                .Where(t => !string.IsNullOrEmpty(t.Item1))
                .Distinct()
                .ToLookup(t => t.Item1, t => t.Item2);
        }
        /// Обратный индекс в нашем случае — это словарь ILookup<string, int>, ключом в котором является слово,
        /// а значениями — идентификаторы всех документов, содержащих это слово.
        public class Document
        {
            public int Id;
            public string Text;
        }
        

    }
}