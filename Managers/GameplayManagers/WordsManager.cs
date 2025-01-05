using System;
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

        private string _correctWord, _lastWord;

        public WordsManager()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "Content", "Resources/words.json");
            _jsonContent = File.ReadAllText(_filePath);
            _wordsList = JsonSerializer.Deserialize<WordsList>(_jsonContent);
            _correctWord = string.Empty;
            _lastWord = string.Empty;
        }

        public List<string> GetWords(Difficulty difficulty, int level)
        {
            List<string> words = new();
            Dictionary<string, List<string>> wordWithForms = new();
            Random random = new();

            foreach (var difficultyLevel in _wordsList.DifficultyLevels)
            {
                if(difficultyLevel.Level <= level) // TO CHANGE
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
    
                                switch(difficulty)
                                {
                                    case Difficulty.EASY:
                                        wordWithForms.Add(
                                            word.Word, 
                                            new List<string> { 
                                                shuffledWordForms[0],
                                            }
                                        );
                                        break;

                                    case Difficulty.NORMAL:
                                        wordWithForms.Add(
                                            word.Word, 
                                            new List<string> { 
                                                shuffledWordForms[0],
                                                shuffledWordForms[1]
                                            }
                                        );
                                        break;

                                    case Difficulty.HARD:
                                        wordWithForms.Add(
                                            word.Word, 
                                            new List<string> { 
                                                shuffledWordForms[0],
                                                shuffledWordForms[1],
                                                shuffledWordForms[2]
                                            }
                                        );
                                        break;

                                    default:
                                        throw new Exception("Cannot specify difficulty to create word list.");
                                }
                            }
                        }
                    }
                }
            }
            
            
            Dictionary<string, List<string>> shuffledWordWithForms = wordWithForms.OrderBy(x => random.Next()).ToDictionary(x => x.Key, x => x.Value);

            _correctWord = shuffledWordWithForms.ElementAt(0).Key.ToString();

            while(_lastWord == _correctWord)
            {
                shuffledWordWithForms = wordWithForms.OrderBy(x => random.Next()).ToDictionary(x => x.Key, x => x.Value);
                _correctWord = shuffledWordWithForms.ElementAt(0).Key.ToString();
            }

            words.Add(_correctWord);
            words.Add(shuffledWordWithForms.ElementAt(0).Value[0].ToString());
            if(difficulty == Difficulty.NORMAL || difficulty == Difficulty.HARD)  words.Add(shuffledWordWithForms.ElementAt(0).Value[1].ToString());
            if(difficulty == Difficulty.HARD)  words.Add(shuffledWordWithForms.ElementAt(0).Value[2].ToString());

            words = words.OrderBy(x => random.Next()).ToList();

            _lastWord = _correctWord;

            return words;
        }

        public string GetCorrectWord()
        {
            return _correctWord;
        }


        // private Dictionary<string, List<string>> WordsHelper(WordsList _wordsList, Difficulty difficulty, int level)
        // {
        //     Dictionary<string, List<string>> wordWithForms = new();

        //     foreach (var difficultyLevel in _wordsList.DifficultyLevels)
        //     {
        //         if(difficultyLevel.Level == level) // TO CHANGE
        //         {
        //             foreach (var form in difficultyLevel.Forms)
        //             {
        //                 if(form.FormNumber >= (int)difficulty + 2)
        //                 {
        //                     foreach (var word in form.Words)
        //                     {
        //                         List<string> wordForms = new();
                            
        //                         foreach (var incorrectForm in word.IncorrectForms)
        //                         {
        //                             wordForms.Add(incorrectForm);
        //                         }
                                
        //                         List<string> shuffledWordForms = wordForms.OrderBy(x => random.Next()).ToList();

        //                         switch(difficulty)
        //                         {
        //                             case Difficulty.EASY:
        //                                 wordWithForms.Add(
        //                                     word.Word, 
        //                                     new List<string> { 
        //                                         shuffledWordForms[0],
        //                                         shuffledWordForms[1],
        //                                         shuffledWordForms[2]
        //                                     }
        //                                 );
        //                                 break;

        //                             case Difficulty.NORMAL:
        //                                 wordWithForms.Add(
        //                                     word.Word, 
        //                                     new List<string> { 
        //                                         shuffledWordForms[0],
        //                                         shuffledWordForms[1],
        //                                         shuffledWordForms[2]
        //                                     }
        //                                 );
        //                                 break;

        //                             case Difficulty.HARD:
        //                                 wordWithForms.Add(
        //                                     word.Word, 
        //                                     new List<string> { 
        //                                         shuffledWordForms[0],
        //                                         shuffledWordForms[1],
        //                                         shuffledWordForms[2]
        //                                     }
        //                                 );
        //                                 break;

        //                             default:
        //                                 throw new Exception("Can not specify diffuculty to create words list.");
        //                         }
                                
        //                     }
        //                 }
        //             }
        //         }
        //     }

        //     return wordWithForms;
        // }
    }
}