using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task2
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

                HashSet<string> wordHashSet = new HashSet<string>(wordText);

                Dictionary<string, int> wordDictionary = new Dictionary<string, int>();

                Console.WriteLine("Введите минимальную длину слова для поиска:\n");

                string lengthString;
                int lengthInt = 0;

                do
                {
                    lengthString = Console.ReadLine();
                }
                while (!int.TryParse(lengthString, out lengthInt) || (lengthInt < 0));

                foreach (string word in wordHashSet)
                    if (word.Length < lengthInt)
                        wordHashSet.Remove(word);

                Console.WriteLine("\nИдёт подсчёт ...");

                foreach (var w in wordHashSet)
                {
                    int count = 0;

                    foreach (var ww in wordText)
                    {
                        if (w == ww)
                            count++;
                    }

                    wordDictionary.Add(w, count);
                }

                var wordArray = wordDictionary.ToArray();

                for (int i = 0; i < wordArray.Length; i++)
                {
                    for (int k = i; k < wordArray.Length; k++)
                    {
                        if (wordArray[i].Value < wordArray[k].Value)
                            (wordArray[i], wordArray[k]) = (wordArray[k], wordArray[i]);
                    }
                }

                Console.WriteLine("\nТоп-10 слов текста:\n");

                for (int i = 0; i < 10; i++)
                    Console.WriteLine($"{i + 1}. {wordArray[i].Key} ({wordArray[i].Value})");
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
