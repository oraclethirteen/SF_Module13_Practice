using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ВАЖНО! Перед компиляцией необходимо поместить файл "Text1.txt" на рабочий стол
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Text1.txt");
                var text = File.ReadAllText(path);

                //Преобразование текста в массив слов без пробелов и знаков пунктуации
                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
                var delimiters = new char[] { ' ', '\n', '\r' };
                var wordText = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                var wordList = new List<string>(wordText);

                var stopwatch = Stopwatch.StartNew();

                wordList.Insert(wordList.Count / 2, "Новая строка 1");

                Console.WriteLine("Строка добавлена в список");
                Console.WriteLine($"Вставка в List заняла {stopwatch.Elapsed.TotalMilliseconds} мс\n");
                Console.ReadKey();

                var wordLinkedList = new LinkedList<string>(wordList);

                var midStr = wordLinkedList.Find("Новая строка 1");

                stopwatch = Stopwatch.StartNew();

                wordLinkedList.AddAfter(midStr, "Новая строка 2");

                Console.WriteLine("Строка добавлена в список");
                Console.WriteLine($"Вставка в LinkedList заняла {stopwatch.Elapsed.TotalMilliseconds} мс\n");
                Console.ReadKey();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка!");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
