using System;
using System.Collections.Generic;
using System.Text;

namespace Epic7Math
{
    class CharModel
    {
        public string Name { get; set; }
        public Chats Charts { get; set; }

        public Dictionary<string, int> values;

    }

    class Chats
    {
        public string Chat1 { get; set; }
        public string Chat2 { get; set; }
    }

    class CharacterMoraleBoost
    {
        public string CharacterName { get; set; }
        public string Characterchat { get; set; }
        public int MoraleBoost { get; set; }

    }
    class TeamMoraleBoost
    { 
        public int TotalMoraleBoost { get; set; }
        public List<CharModel> TeamList { get; set; }
        public List<CharacterMoraleBoost> MoraleBoostList { get; set; }

    }

    class Values
    {
        public Dictionary<string, int> Advice { get; set; }
        public Dictionary<string, int> Belief { get; set; }
        public Dictionary<string, int> BizarreStory { get; set; }
        public Dictionary<string, int> ComfortingCheer { get; set; }
        public Dictionary<string, int> Complain { get; set; }
        public Dictionary<string, int> Criticism { get; set; }
        public Dictionary<string, int> CuteCheer { get; set; }
        public Dictionary<string, int> Dream { get; set; }
        public Dictionary<string, int> FoodStory { get; set; }
        public Dictionary<string, int> Gossip { get; set; }
        public Dictionary<string, int> HappyMemory { get; set; }
        public Dictionary<string, int> HeroicCheer { get; set; }
        public Dictionary<string, int> HeroicTale { get; set; }
        public Dictionary<string, int> HorrorStory { get; set; }
        public Dictionary<string, int> InterestingStory { get; set; }
        public Dictionary<string, int> JoyfulMemory { get; set; }
        public Dictionary<string, int> Myth { get; set; }
        public Dictionary<string, int> Occult { get; set; }
        public Dictionary<string, int> RealityCheck { get; set; }
        public Dictionary<string, int> SadMemory { get; set; }
        public Dictionary<string, int> SelfIndulgent { get; set; }
        public Dictionary<string, int> UniqueComment { get; set; }

    }
    
}
