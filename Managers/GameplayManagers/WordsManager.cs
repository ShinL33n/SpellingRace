using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SpellingRace.Managers.GameplayManagers
{
    public class WordsManager
    {  
        private readonly string _filePath;
        private readonly string _jsonContent;
        private readonly WordsList _wordsList;

        public WordsManager()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "Content", "Resources/words.json");
            _jsonContent = File.ReadAllText(_filePath);
            _wordsList = JsonSerializer.Deserialize<WordsList>(_jsonContent);
        }

        public List<string> GetWords(Difficulty difficulty, int level)
        {
            List<string> words = new();
            Dictionary<string, List<string>> wordWithForms = new();
            Random random = new();

            switch(difficulty)
            {
                case Difficulty.EASY: // Need a word with 2 or more forms
                    break;

                case Difficulty.NORMAL: // Need a word with 3 or more forms
                    foreach (var difficultyLevel in _wordsList.DifficultyLevels)
                    {
                        if(difficultyLevel.Level == level)
                        {
                            foreach (var form in difficultyLevel.Forms)
                            {
                                if(form.FormNumber >= (int)difficulty + 2)
                                {
                                    foreach (var word in form.Words)
                                    {
                                        List<string> wordForms = new();
                                    
                                        foreach (var incorrectForm in word.IncorrectForms)
                                        {
                                            wordForms.Add(incorrectForm);
                                        }
                                        
                                        List<string> shuffledWordForms = wordForms.OrderBy(x => random.Next()).ToList();
            
                                        wordWithForms.Add(
                                            word.Word, 
                                            new List<string> { 
                                                shuffledWordForms[0],
                                                shuffledWordForms[1] 
                                            }
                                        );
                                    }
                                }
                            }
                        }
                    }
                    break;

                case Difficulty.HARD: // Need a word with 4 or more forms
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Difficulty level not set.");

            }

            Dictionary<string, List<string>> shuffledWordWithForms = wordWithForms.OrderBy(x => random.Next()).ToDictionary(x => x.Key, x => x.Value);

            words.Add(shuffledWordWithForms.ElementAt(0).Key.ToString());
            words.Add(shuffledWordWithForms.ElementAt(0).Value[0].ToString());
            words.Add(shuffledWordWithForms.ElementAt(0).Value[1].ToString());

            return words;
        }
    }
}