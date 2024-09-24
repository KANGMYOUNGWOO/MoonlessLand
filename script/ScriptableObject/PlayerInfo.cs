using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo :ScriptableObject
{

    public struct DiseaseInfo
    {
        public Disease disease;
        public int    DiseaseCode;
        public int    DiseaseTurn;
        public int    t_DiseaseValue;     
        public int    e_DiseaseValue;
     
    }


    public struct ItemInfo
    {
            
        public int ItemDecay;
        public int ItemSmell;
        
        public int ItemCount;
       

        public Item item;

        public Item.ItemType type;
    }          
   


    public class CardModule
    { 
        public int Strength;
        public int Agility ;
        public int Stealth ;
        public int Examine ;

        public int module;

        public int[] AttackTrait = new int[5];
        public int[] GuardTrait = new int[5];
        public int[] PassiveTrait = new int[10];
            
    }


    public class ActData
    {
        public int Current_EventCode;
        public int Event_index;
        public int Essential_index;
        public string current_Area;
        public int    current_Area_Level;
        public LogicManager.GameMode gameMode;
        public bool bisPrologue = false;

    }

    public int hp;
    public int mp;
    public int armor;

    public struct Stat
    {
        public int strength;
        public int examine;
        public int agility;
        public int stealth;
    }

    public int currentStrength;
    public int currentExamine;
    public int currentAgility;
    public int currentStealth;
  

    public DiseaseInfo[]  DiseaseArray = new DiseaseInfo[5];
    public ItemInfo[]     ItemArray = new ItemInfo[28];
    public Stat[]         StatArray = new Stat[6];
    public int[]          ModuleArray = new int[45];
    
    public int[]          CombineArray = new int[3];
    public int[]          tokenArray = new int[7];


    public List<int>      AttackCardList = new List<int>();
    public List<int>      GuardCardList = new List<int>();
    public List<int>      PassiveCardList = new List<int>();

    public List<int>      TestCardList = new List<int>();
   
    public ActData ProgressData = new ActData();

    public CardModule CardModule1 = new CardModule();
    public CardModule CardModule2 = new CardModule();
    public CardModule CardModule3 = new CardModule();
    public CardModule CardModule4 = new CardModule();
    public CardModule CardModule5 = new CardModule();
    public CardModule CardModule6 = new CardModule();

    public CardModule CurrentModule = new CardModule();
   
    

    public void Reset()
    {
        for(int i=0; i < DiseaseArray.Length; i++)
        {
          
        }
    }

   

    public void OnEnable()
    {
        CurrentModule = CardModule1;
    }

}
