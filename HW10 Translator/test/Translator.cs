using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Translator
    {
        private Dictionary<string, string> vocabulary;
        private string text;
        private string pathToText;
        private string pathToDictionary;
        private int countVariables = 3;

        public Translator() : this(@"../../../Text.txt", @"../../../Dictionary.txt")
        {

        }

        public Translator(string pathToText, string pathToDictionary)
        {
            vocabulary = new Dictionary<string, string>();
            text = "";
            this.pathToText = pathToText;
            this.pathToDictionary = pathToDictionary;
        }

        public Translator(Dictionary<string, string> vocabluary, string text, string pathToText, string pathToDictionary)
        {
            this.pathToText = pathToText;
            this.pathToDictionary = pathToDictionary;
            this.vocabulary = vocabluary;
            this.text = text;
        }

        public void AddText(string text)
        {
            this.text += text;
        }

        public void AddDictionary(Dictionary<string, string> dictionary)
        {
            this.vocabulary = dictionary;
        }

        public string ChangeWords()
        {
            string result = "";
            var words = text.Split(' ');
            foreach (string word in words)
            {
                char temp = '\0';
                string tempWord = string.Empty;
                int i = 0;
                if (word.Length > 0)
                {
                    if (char.IsPunctuation(word[word.Length - 1]))
                    {
                        temp = word[word.Length - 1];
                        while (!vocabulary.ContainsKey(word[0..^1].ToLower()) && i < countVariables)
                        {
                            AddToDictionary(word[0..^1].ToLower());
                            ++i;
                        }
                        tempWord = vocabulary[word[0..^1].ToLower()] + temp;
                    }
                    else
                    {
                        while (!vocabulary.ContainsKey(word.ToLower()) && i < countVariables)
                        {
                            AddToDictionary(word.ToLower());
                            ++i;
                        }
                        tempWord = vocabulary[word.ToLower()];
                    }
                }

                if (char.IsUpper(word[0]))
                {
                    tempWord = tempWord.Replace(tempWord[0], char.ToUpper(tempWord[0]));
                }

                result += tempWord + " ";
            }

            return result;
        }

        private void AddToDictionary(string word)
        {
            Console.WriteLine($"Введiть замiну для слова {word}");
            string value = Console.ReadLine();
            if (value == null || value.Length == 0)
            {
                return;
            }
            for (int i = 0; i < value.Length; ++i)
            {
                if (!char.IsLetter(value[i]))
                {
                    return;
                }
            }

            vocabulary.Add(word.ToLower(), value.ToLower());
            Reader.WriteToDictionary(word, value, @"../../../Dictionary.txt");   
        }
    }
}
