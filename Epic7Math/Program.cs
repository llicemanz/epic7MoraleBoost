using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Epic7Math
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CharModel> charModels = LoadCharactersData();
            //Input the character list here:
           var list = new List<string> { "Bellona", "Kise", "Angelica", "Luna", "tamarinne", "kluri", "lorina", "taranor-guard"};
           

            //All Characters
            //var list = new List<string>();
            //foreach (CharModel charac in charModels)
            //{
            //    list.Add(charac.Name);
            //    //Console.WriteLine(charac.Name + ",");
            //}


            var result = GetPermutations(list, 4);
            List<TeamMoraleBoost> bestTeamList = new List<TeamMoraleBoost>();
            foreach(var tempPart in result)
            {
                List<CharModel> tempCharList = new List<CharModel>();
                foreach (string character in tempPart)
                {
                    CharModel tempChar = new CharModel();
                    tempChar = GetCharacter(character.Trim().ToLower(), charModels);
                    tempCharList.Add(tempChar);
                }
                bestTeamList.Add(GetTeamMoraleBoost(tempCharList));
                
            }

            bestTeamList.Sort((x, y) => y.TotalMoraleBoost.CompareTo(x.TotalMoraleBoost));

            int totalboost = 0;
            while(bestTeamList[totalboost].TotalMoraleBoost >= 20)
            {
                //If you want a specific character to be in the team, In v2 you should be able to do it from the input
                //if (bestTeamList[totalboost].TeamList[0].Name == "luna" || bestTeamList[totalboost].TeamList[1].Name == "luna" || bestTeamList[totalboost].TeamList[2].Name == "luna" || bestTeamList[totalboost].TeamList[3].Name == "luna")
                //{
                    Debug.WriteLine("The total morale is " + bestTeamList[totalboost].TotalMoraleBoost + " With the team of " + bestTeamList[totalboost].TeamList[0].Name + "," + bestTeamList[totalboost].TeamList[1].Name + "," + bestTeamList[totalboost].TeamList[2].Name + "," + bestTeamList[totalboost].TeamList[3].Name);
                    Debug.WriteLine("Using the abilities of: " + bestTeamList[totalboost].MoraleBoostList[0].CharacterName + " using " + bestTeamList[totalboost].MoraleBoostList[0].Characterchat + " and the character " + bestTeamList[totalboost].MoraleBoostList[1].CharacterName + " using " + bestTeamList[totalboost].MoraleBoostList[1].Characterchat);

                    Console.WriteLine("The total morale is " + bestTeamList[totalboost].TotalMoraleBoost + " With the team of " + bestTeamList[totalboost].TeamList[0].Name + "," + bestTeamList[totalboost].TeamList[1].Name + "," + bestTeamList[totalboost].TeamList[2].Name + "," + bestTeamList[totalboost].TeamList[3].Name);
                    Console.WriteLine("Using the abilities of: " + bestTeamList[totalboost].MoraleBoostList[0].CharacterName + " using " + bestTeamList[totalboost].MoraleBoostList[0].Characterchat + " and the character " + bestTeamList[totalboost].MoraleBoostList[1].CharacterName + " using " + bestTeamList[totalboost].MoraleBoostList[1].Characterchat);
                //}
                totalboost++;
            }
            Console.WriteLine(totalboost);
            Debug.WriteLine(totalboost);

        }


        static TeamMoraleBoost GetTeamMoraleBoost(List<CharModel> characterList)
        {
            TeamMoraleBoost tempMoraleBoostList = new TeamMoraleBoost();
            List<CharacterMoraleBoost> tempCharacterMoraleBoost = new List<CharacterMoraleBoost>();

            foreach (CharModel character in characterList)
            {
                tempCharacterMoraleBoost.Add(GetCharacterMoraleBoost(character, 1, characterList));
                tempCharacterMoraleBoost.Add(GetCharacterMoraleBoost(character, 2, characterList));
            }

            tempCharacterMoraleBoost.Sort((x,y) => y.MoraleBoost.CompareTo(x.MoraleBoost));
            tempMoraleBoostList.TeamList = characterList;
            tempMoraleBoostList.TotalMoraleBoost = tempCharacterMoraleBoost[0].MoraleBoost + tempCharacterMoraleBoost[1].MoraleBoost;
            if(tempCharacterMoraleBoost[0].Characterchat != tempCharacterMoraleBoost[1].Characterchat)
            { 
                tempMoraleBoostList.MoraleBoostList = new List<CharacterMoraleBoost>
                {
                    tempCharacterMoraleBoost[0],
                    tempCharacterMoraleBoost[1]
                };
                return tempMoraleBoostList;
            }
            else
            {
                for (int x = 1; x < tempCharacterMoraleBoost.Count; x++)
                {
                    if(tempCharacterMoraleBoost[0].Characterchat != tempCharacterMoraleBoost[x].Characterchat)
                    {
                        tempMoraleBoostList.MoraleBoostList = new List<CharacterMoraleBoost>
                        {
                            tempCharacterMoraleBoost[0],
                            tempCharacterMoraleBoost[x]
                        };
                        return tempMoraleBoostList;
                    }
                }
            }
            return tempMoraleBoostList;
        }

        static CharacterMoraleBoost GetCharacterMoraleBoost(CharModel character, int chat, List<CharModel> characterList)
        {
            List<CharacterMoraleBoost> charMoraleList = new List<CharacterMoraleBoost>();
            CharacterMoraleBoost tempCharacterMoraleBoostChat = new CharacterMoraleBoost();
            if (chat == 1)
            {
                tempCharacterMoraleBoostChat.CharacterName = character.Name;
                tempCharacterMoraleBoostChat.Characterchat = character.Charts.Chat1;
                int moraleBoost = 0;
                foreach(CharModel tempChar in characterList)
                {
                    if(character.Name != tempChar.Name)
                    { 
                    moraleBoost += tempChar.values[character.Charts.Chat1];
                    }
                }
                tempCharacterMoraleBoostChat.MoraleBoost = moraleBoost;
                
            }
            else if (chat == 2)
            {
                tempCharacterMoraleBoostChat.CharacterName = character.Name;
                tempCharacterMoraleBoostChat.Characterchat = character.Charts.Chat2;
                int moraleBoost = 0;
                foreach (CharModel tempChar in characterList)
                {
                    if (character != tempChar)
                    {
                        moraleBoost += tempChar.values[character.Charts.Chat2];
                    }
                }
                tempCharacterMoraleBoostChat.MoraleBoost = moraleBoost;
                
            }
            else
            {
                throw new Exception("Wrong typoe of chat");
            }

            return tempCharacterMoraleBoostChat;
        }

        static List<CharModel> LoadCharactersData()
        {
            List<CharModel> charModels = new List<CharModel>();
            List<string> tempList = new List<string>();
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"\library.json"))
            {
                string json = r.ReadToEnd();
                dynamic charList = JObject.Parse(json);
                foreach (var characters in charList)
                {
                    CharModel tempCharacter = new CharModel
                    {
                        Name = characters.Name
                    };

                    dynamic chatList = JObject.Parse(characters.Value.ToString());

                    Chats tempChats = new Chats
                    {
                        Chat1 = chatList.chats[0],
                        Chat2 = chatList.chats[1]
                    };
                    tempCharacter.Charts = tempChats;

                    tempCharacter.values = new Dictionary<string, int>
                    {
                        { "advice", (int)chatList.values["advice"].Value },
                        { "belief", (int)chatList.values["belief"].Value },
                        { "bizarre-story", (int)chatList.values["bizarre-story"].Value },
                        { "comforting-cheer", (int)chatList.values["comforting-cheer"].Value },
                        { "complain", (int)chatList.values["complain"].Value },
                        { "criticism", (int)chatList.values["criticism"].Value },
                        { "cute-cheer", (int)chatList.values["cute-cheer"].Value },
                        { "dream", (int)chatList.values["dream"].Value },
                        { "food-story", (int)chatList.values["food-story"].Value },
                        { "gossip", (int)chatList.values["gossip"].Value },
                        { "happy-memory", (int)chatList.values["happy-memory"].Value },
                        { "heroic-cheer", (int)chatList.values["heroic-cheer"].Value },
                        { "heroic-tale", (int)chatList.values["heroic-tale"].Value },
                        { "horror-story", (int)chatList.values["horror-story"].Value },
                        { "interesting-story", (int)chatList.values["interesting-story"].Value },
                        { "joyful-memory", (int)chatList.values["joyful-memory"].Value },
                        { "myth", (int)chatList.values["myth"].Value },
                        { "occult", (int)chatList.values["occult"].Value },
                        { "reality-check", (int)chatList.values["reality-check"].Value },
                        { "sad-memory", (int)chatList.values["sad-memory"].Value },
                        { "self-indulgent", (int)chatList.values["self-indulgent"].Value },
                        { "unique-comment", (int)chatList.values["unique-comment"].Value }
                    };
                    charModels.Add(tempCharacter);
                }
            }

            return charModels;
        }

        static CharModel GetCharacter(string name, List<CharModel> charModels)
        {
            CharModel tempCharacter = new CharModel();
            tempCharacter = charModels.Find(x => x.Name == name);
            if(tempCharacter == null)
            {
                throw new Exception("Character not found");
            }
            return tempCharacter;
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }
                ++i;
            }
        }
    }
}
