using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.IO;

using LitJson;

#region 식물 클래스
public struct Vegetal
{
    
    public string name;
    public string subname;
    public string explain;
    public string knowledge1;
    public string knowledge2;
    public string knowledge3;
    public string bonus1;
    public string bonus2;
    public string bonus3;

    public Vegetal(string name, string subname, string explain , string knowledge1 , string knowledge2, string knowledge3, string bonus1 , string bonus2 , string bonus3)
    {
        
        this.name = name;
        this.subname = subname;
        this.explain = explain;
        this.knowledge1 = knowledge1;
        this.knowledge2 = knowledge2;
        this.knowledge3 = knowledge3;
        this.bonus1 = bonus1;
        this.bonus2 = bonus2;
        this.bonus3 = bonus3;

    }


}
#endregion
#region 동물 클래스 
public struct beast
{
    public string name;
    public string subname;   

    public int    hp;
    public int    MaxBenefit;
    public int    MaxPenalty;
    public int    strength;
    public int    agility;
    public int    examine;
    public int    stealth;

    public string weak;
    public string immune;
    public string benefit;
    public string penalty;

    public int    EssentialReward;
    public int    BonusReward;
    public int    ExtraReward1;
    public int    ExtraReward2;

    public beast(string name,string subname, int hp,int maxben ,int maxpen , int str, int agi, int exa, int ste , string weak, string imm, string ben, string pen , int er, int br, int ex1, int ex2)
    {
        this.name       = name;
        this.subname    = subname;
        this.hp         = hp;
        this.MaxBenefit = maxben;
        this.MaxPenalty = maxpen;
        this.strength   = str;
        this.agility    = agi;
        this.examine    = exa;
        this.stealth    = ste;
        this.weak       = weak;
        this.immune     = imm;
        this.benefit    = ben;
        this.penalty    = pen;
        this.EssentialReward = er;
        this.BonusReward = br;
        this.ExtraReward1 = ex1;
        this.ExtraReward2 = ex2;
        
    }
}
#endregion



#region 질병 참조
public struct refDisease
{
   
    public int    d_code;

    public int    turn;
    public int    t_value;

    
    public int    e_value;

    
    //public int    s_value;
   
   
   

    public refDisease(int code, int turn, int value1, int value2)
    {
        this.d_code = code;
        this.turn = turn;
        this.t_value = value1;
        
        this.e_value = value2;
       
        
                            
    }

}
#endregion
#region  현재 진행 상황
public class ProgressData
{
    public int EventCode;
    public int EvnetIndex;
    public int EssentialIndex;
    public string Area;
    public string GameMode;

}


#endregion
#region 인벤토리 참조

public struct refItem
{ 
    public int itemcode;
    public int ItemDecay;
    public int ItemSmell;
    public int ItemCount;
  
    public refItem(int code ,int decay, int smell, int count)
    {
        itemcode = code;        
        ItemDecay = decay;
        ItemSmell = smell;
        ItemCount = count;
    }

}

#endregion
#region 모듈 클래스
public struct refModule
{
    public int code;
    public int Strength;
    public int Agility;
    public int Stealth;
    public int Examine;
    public int attackTrait1;
    public int attackTrait2;
    public int attackTrait3;
    public int attackTrait4;
    public int attackTrait5;
    public int guardTrait1;
    public int guardTrait2;
    public int guardTrait3;
    public int guardTrait4;
    public int guardTrait5;
    public int trait1;
    public int trait2;
    public int trait3;
    public int trait4;
    public int trait5;
    public int trait6;
    public int trait7;
    public int trait8;
    public int trait9;
    public int trait10;    
    
    public refModule(int code, int strength, int agility, int stealth, int examine, int at1, int at2, int at3,
        int at4, int at5, int gt1,int gt2, int gt3 , int gt4, int gt5 , int t1, int t2, int t3, int t4, int t5 , int t6, int t7, int t8, int t9, int t10)
    {
        this.code = code;
        this.Strength = strength;
        this.Agility = agility;
        this.Stealth = stealth;
        this.Examine = examine;
        this.attackTrait1 = at1;
        this.attackTrait2 = at2;
        this.attackTrait3 = at3;
        this.attackTrait4 = at4;
        this.attackTrait5 = at5;
        this.guardTrait1 = gt1;
        this.guardTrait2 = gt2;
        this.guardTrait3 = gt3;
        this.guardTrait4 = gt4;
        this.guardTrait5 = gt5;
        this.trait1 = t1;
        this.trait2 = t2;
        this.trait3 = t3;
        this.trait4 = t4;
        this.trait5 = t5;
        this.trait6 = t6;
        this.trait7 = t7;
        this.trait8 = t8;
        this.trait9 = t9;
        this.trait10 = t10;



    }
}

#endregion
#region 테스트카드 클래스
public struct refTestCard
{
    public int code;
    public int over;
    public int success;
    public int fail;
    public int hell1;
    public int hell2;
    public int hell3;

    public refTestCard(int code, int over, int success , int fail, int hell1, int hell2, int hell3)
    {
        this.code        = code;
        this.over        = over;
        this.success     = success;
        this.fail        = fail;
        this.hell1       = hell1;
        this.hell2       = hell2;
        this.hell3       = hell3;
    }
}
#endregion

#region 특성카드 클래스

public struct passiveCard
{
    public int code;
    public string Name;
    public int condition;
    public int value;
    public string keyword;
    public string calculType;
    public string type;
    public string Explain;
    public string Card1;
    public string Card2;
    public string Card3;   

    public passiveCard(int code, string Name, int condition, int value, string keyword, string calcultype, string type, string Explain, string Card1, string Card2, string Card3)
    {
        this.code = code;
        this.Name = Name;
        this.condition = condition;
        this.value = value;
        this.keyword = keyword;
        this.calculType = calcultype;
        this.type = type;
        this.Explain = Explain;
        this.Card1 = Card1;
        this.Card2 = Card2;
        this.Card3 = Card3;
    }
}

public struct cubeCard
{
    public int code;   
    public int redValue;
    public int blueValue;
    public int greenValue;
    public string Name;
    public string keyword;
    public string explain;

    public cubeCard(int code, int red, int blue, int green, string Name, string key, string exp )
    {
        this.code = code;
        this.redValue = red;
        this.blueValue = blue;
        this.greenValue = green;
        this.Name = Name;
        this.keyword = key;
        this.explain = exp;
    }
}

public struct attackCard
{
    public int Code;
    public int MaxLevel;
    public int MinLevel;
   
    public int ValueA;
    public int ValueB;
    public int ValueC;
    public int ValueD;

    public string Name;
    public string Explain;
    public string DamageType;
    public string TestType;

    public int    Token1Type;
    public int    Token2Type;
    public int    Token1Count;
    public int    Token2Count;
   

    public attackCard(int code, int maxlv, int minlv, int valuea, int valueb, int valuec, int valued,  string name, string explain, string damagetype, string testtype,
        int token1type, int token2type ,int token1count , int token2count)
    {
        Code        = code;
        MaxLevel    = maxlv;
        MinLevel    = minlv;
       
        ValueA      = valuea;
        ValueB      = valueb;
        ValueC      = valuec;
        ValueD      = valued;

        Name        = name;
        Explain     = explain;
        DamageType  = damagetype;
        TestType    = testtype;
        
        Token1Type  = token1type;
        Token2Type  = token2type;
        Token1Count = token1count;
        Token2Count = token2count;
        
    }
}


public struct guardCard
{
    public int Code;
    public int Cost;
    public int Cooltime;
    public int Damage;
    public string Name;
    public string Explain;
    public string TestType;
    public string Keyword;
    public int Token1Type;
    public int Token2Type;
    public int Token1Count;
    public int Token2Count;
    public int Phase;

    public guardCard(int code, int cost, int cooltime, int damage, string name, string explain, string testtype,  string key ,int token1type , int token2type, int token1count, int token2count, int phase)
    {
        Code = code;
        Cost = cost;
        Cooltime = cooltime;
        Damage = damage;
        Name = name;
        Explain = explain;
        TestType = testtype;         
        Keyword = key;
        Token1Type = token1type;
        Token2Type = token2type;
        Token1Count = token1count;
        Token2Count = token2count;
        Phase       = phase;       
    }
}

#endregion




public class Knowledge
{
    public int bisFirst;
    public int bisBonus1;
    public int bisBonus2;
    public int bisBonus3;

}
public class ResourceManager : MonoBehaviour,IManager
{

    #region 파일 경로
    string filepath;

    #endregion
    #region
    [SerializeField] private Transform BattleUIParent;
    [SerializeField] private Transform CanvasParent;

    #endregion

    #region 매니저 
    public GameManager gameManager { get { return GameManager.gameManager; } }

    private CharacterManager characterManager;
    private LogicManager logicManager;
    #endregion
    #region 정보 

    public Dictionary<int, Disease> DiseaseDictionary = new Dictionary<int, Disease>();
    public Dictionary<int, nimrod> NimrodDictionary = new Dictionary<int, nimrod>();
    public Dictionary<int, refTestCard> TestCardDictionary = new Dictionary<int, refTestCard>();   
    public Dictionary<int, Item> ItemDictionary = new Dictionary<int, Item>();
    public Dictionary<int, passiveCard> PassiveDictionary = new Dictionary<int, passiveCard>();
    public Dictionary<int, attackCard> AttackDictionary = new Dictionary<int, attackCard>();
    public Dictionary<int, guardCard> GuardDictionary = new Dictionary<int, guardCard>();


    #endregion
    #region 글 모음
    public Dictionary<int, string> AD_Current = new Dictionary<int, string>();
    public Dictionary<int, string> AD_Intro = new Dictionary<int, string>();
    public Dictionary<int, string> AD_Tutorial = new Dictionary<int, string>();
    public Dictionary<int, string> AD_Garden = new Dictionary<int, string>();
    public Dictionary<int, string> AD_Observation = new Dictionary<int, string>();
   
   

    //public string[] AD_Monsters;
    public Dictionary<string, string[]> AD_Monsters = new Dictionary<string, string[]>();


    #endregion 이미지 레퍼런스 레이블 모음

    public Dictionary<string, AssetReferenceSprite> R_DiseaseIconDictionary = new Dictionary<string, AssetReferenceSprite>();    
    public Dictionary<string, AssetReferenceSprite> R_ActionCardDictionary = new Dictionary<string, AssetReferenceSprite>();

    public Dictionary<int, AssetReferenceSprite> R_ItemIconDictionary = new Dictionary<int, AssetReferenceSprite>();
    public Dictionary<int, AssetReferenceSprite> R_CardFrontDictionary = new Dictionary<int, AssetReferenceSprite>();
    public Dictionary<int, AssetReferenceSprite> R_CardBackDictionary = new Dictionary<int, AssetReferenceSprite>();
    public Dictionary<int, AssetReferenceSprite> R_NimrodDictionary = new Dictionary<int, AssetReferenceSprite>();
    public Dictionary<int, AssetReferenceSprite> R_TokenDictionary = new Dictionary<int, AssetReferenceSprite>();
    public Dictionary<int, AssetReferenceSprite> R_IconDictionary = new Dictionary<int, AssetReferenceSprite>();
    public Dictionary<int, AssetReferenceSprite> R_ResultDictionary = new Dictionary<int, AssetReferenceSprite>();
    #region

   

    #endregion

    #region 이미지 모음






    public Dictionary<string, Sprite> I_DiseaseDictionary = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> I_ItemDictionary = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> I_ActionCardDictionary = new Dictionary<string, Sprite>();
    


    public Dictionary<int, Sprite> I_CardFrontDictionary = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> I_CardBackDictionary = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> I_NimrodDictionary = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> I_ResultDictionary = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> I_TokenDictionary = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> I_IconDictionary = new Dictionary<int, Sprite>();
    
    

    #endregion

    #region addressable Reference
   // [SerializeField]
   // private AssetReference CardReference;


    [SerializeField] private AssetReferenceGameObject BookRef;
    [SerializeField] private AssetReferenceGameObject DiseaseTabRef;
    [SerializeField] private AssetReferenceGameObject UserInfoRef;
    [SerializeField] private AssetReferenceGameObject TokensRef;
    [SerializeField] private AssetReferenceGameObject TestRef;
    [SerializeField] private AssetReferenceGameObject NimrodChangerRef;
    [SerializeField] private AssetReferenceGameObject PopUpRef;
    [SerializeField] private AssetReferenceGameObject WorldMapRef;
    [SerializeField] private AssetReferenceGameObject NimrodRef;
    [SerializeField] private AssetReferenceGameObject CardRejectionRef;
    [SerializeField] private AssetReferenceGameObject DiseaseRemoverRef;
    [SerializeField] private AssetReferenceGameObject TutorialRef;
    [SerializeField] private AssetReferenceGameObject PrologueRef;
    [SerializeField] private AssetReferenceGameObject StartSceneRef;
    


    #endregion

    #region BookData

    public List<Vegetal> vegetalList = new List<Vegetal>();
    public Dictionary<string, beast> BeastDicitonary = new Dictionary<string, beast>(); 

    private JsonData vegetalData;
    private JsonData beastData;


    public BookData bookdata { get; set; }



    #endregion

    #region PlayerData
    private JsonData DiseaseData; 
    private JsonData NimrodData;
    private JsonData ItemData;
    private JsonData ResearchData;
    private JsonData numData;
   

    public PlayerInfo characterInfo { get; set; }

    #endregion

    #region 초기화

    private void Awake()
   {
               
        characterInfo = (PlayerInfo)Resources.Load("Info/CharacterInfo/PlayerData", typeof(ScriptableObject));
        bookdata = (BookData)Resources.Load("Info/CharacterInfo/BookData", typeof(ScriptableObject));


        filepath = Application.persistentDataPath;

        DiseaseData = LoadJson("/Disease");
        ItemData = LoadJson("/Item");
        ResearchData = LoadJson("/ResearchData");
        NimrodData = LoadJson("/module");

        LoadImage("DiseaseIcon", I_DiseaseDictionary);
        LoadImage("NimrodIcon", I_NimrodDictionary);
        LoadImage("ActionCard", I_ActionCardDictionary);
        LoadImage("ResultIcon", I_ResultDictionary);
        LoadImage("CardBack", I_CardBackDictionary);
        LoadImage("CardFront", I_CardFrontDictionary);
        LoadImage("Token", I_TokenDictionary);
        LoadImage("Icon", I_IconDictionary);

        //LoadImage("DiseaseIcon", I_DiseaseDictionary);
        //LoadImage("NimrodIcon", I_NimrodDictionary);
        //LoadImage("ActionCard", I_ActionCardDictionary);
        //LoadImage("ResultIcon",I_ResultDictionary);
        //LoadImage("CardBack",I_CardBackDictionary);
        //LoadImage("CardFront", I_CardFrontDictionary);
        //LoadImage("Token", I_TokenDictionary);
        //LoadImage("Icon",I_IconDictionary);
     
        LoadText("Monster");

        LoadTestCard("TestCard");
        loadPassiveCard("PassiveCard");
        loadAttackCard("AttackCard");
        loadGuardCard("GuardCard");
     

        LoadScriptableObject<Disease>("Disease",DiseaseDictionary);
        LoadScriptableObject<Item>("Item",ItemDictionary);
        LoadScriptableObject<nimrod>("Nimrod", NimrodDictionary);
        LoadDictionary("Dictionary",vegetalData);
        LoadDictionary("Dictionary_Beast");
        LoadDictNum("NumberData");

        // LoadText("Observation",AD_Observation);
                    
            
       

        LoadText("Observation", AD_Observation);

        LoadText("YenaGarden", AD_Garden);

        LoadText("Tutorial", AD_Tutorial);



        //SpawnUI("BattleUI",BattleUIParent,1);
        //SpawnUI("NimrodChanger",CanvasParent,18);
        //SpawnUI("CardRejection", CanvasParent, 20);

        SpawnUI(BookRef);
        SpawnUI(DiseaseTabRef);
        SpawnUI(UserInfoRef);
        SpawnUI(TokensRef);
        SpawnUI(TestRef);
        SpawnUI(PopUpRef);
        SpawnUI(WorldMapRef);
        SpawnUI(NimrodRef);
        SpawnUI(NimrodChangerRef);
        SpawnUI(CardRejectionRef);
        SpawnUI(DiseaseRemoverRef);
        SpawnUI(TutorialRef);
        SpawnUI(PrologueRef);
        SpawnUI(StartSceneRef);



    }

    // Start is called before the first frame update
    void Start()
    {
        
        characterManager =  GameManager.GetManagerClass<CharacterManager>();
        logicManager     =  GameManager.GetManagerClass<LogicManager>();
        
        
        characterManager.characterInfo = characterInfo;
        logicManager.playerInfo = characterInfo;
        
      
        //gameManager.bookData(bookdata);



    }

    #endregion



    #region 로드


    #region json 데타 범용 읽기
    private JsonData LoadJson(string path)
    {
        JsonData json;       
        if (!File.Exists(filepath + path+".json")) { return JsonCatcher("Info/CharacterInfo" + path); }
        else
        {
            json = JsonMapper.ToObject(File.ReadAllText(filepath + path+".json"));                     
            return json;
        }

    }

    #endregion
    #region 내가 지금 까지 진행한 정보를 받아온다.

    private void LoadProgress(string path)
    {

        ProgressData classData;
        TextAsset text;
        string stringData = "";

       
        if (!File.Exists(filepath + path))
        {
            text = (TextAsset)Resources.Load("Info/CharacterInfo" + path, typeof(TextAsset));
            stringData = text.text;            
        }
        else
        {
            stringData = JsonUtility.ToJson(File.ReadAllText(filepath + path));
        }

        classData = JsonUtility.FromJson<ProgressData>(stringData);

        characterInfo.ProgressData.current_Area = classData.Area;
        //ChangeArticle(classData.Area);


        characterInfo.ProgressData.Current_EventCode = classData.EventCode;
        characterInfo.ProgressData.Event_index = classData.EvnetIndex;
        characterInfo.ProgressData.Essential_index = classData.EssentialIndex;
        switch (classData.GameMode)
        {
            case "Read":
                characterInfo.ProgressData.gameMode = LogicManager.GameMode.Read;
                break;
            case "Battle":
                characterInfo.ProgressData.gameMode = LogicManager.GameMode.Battle;
                break;
            case "Map":
                characterInfo.ProgressData.gameMode = LogicManager.GameMode.Map;
                break;


        }

        

    }
    #endregion
    #region 내가 걸린 질병의 데이터를 받아온다.

    private void d_LoadPlayerData(JsonData data)
    {
        //yield return new WaitUntil(()=>data[0][0].ToString() != null);

        Disease tempdisease;
        for (int i = 0; i < data.Count; i++)
        {
          
            tempdisease = DiseaseDictionary[int.Parse(data[i][0].ToString())];
            characterInfo.DiseaseArray[i].disease = tempdisease;
            characterInfo.DiseaseArray[i].DiseaseCode = int.Parse(data[i][0].ToString());           
            characterInfo.DiseaseArray[i].DiseaseTurn = int.Parse(data[i][1].ToString());
            characterInfo.DiseaseArray[i].t_DiseaseValue = int.Parse(data[i][2].ToString());         
            characterInfo.DiseaseArray[i].e_DiseaseValue = int.Parse(data[i][3].ToString());
           
        }
    }
    #endregion

   
    
    
  

    #region 스탯 받아오기
    private void s_LoadPlayerData(JsonData data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            characterInfo.hp = int.Parse(data[i][0].ToString());
            characterInfo.mp = int.Parse(data[i][1].ToString());
            //characterInfo.stat.strength = int.Parse(data[i][2].ToString());
            //characterInfo.stat.examine = int.Parse(data[i][3].ToString());
            //characterInfo.stat.agility = int.Parse(data[i][3].ToString());
            //characterInfo.stat.stealth = int.Parse(data[i][3].ToString());


        }
    }
    #endregion
    #region 인벤토리 받아오기
    private void i_LoadPlayerData(JsonData data)
    {
        Item tempitem;
        for (int i=0; i < data.Count; i++)
        {
           
            tempitem = ItemDictionary[int.Parse(data[i][0].ToString())];
            characterInfo.ItemArray[i].item = tempitem;           
            characterInfo.ItemArray[i].ItemDecay = int.Parse(data[i][1].ToString());
            characterInfo.ItemArray[i].ItemSmell = int.Parse(data[i][2].ToString());
            characterInfo.ItemArray[i].ItemCount = int.Parse(data[i][3].ToString());
           
        }

    }
    #endregion
    #region 플레이어의 모듈정보 받아오기
    private void n_LoadPlayerData(JsonData data)
    {
       
            characterInfo.CardModule1.module = int.Parse(data[0][0].ToString());
            characterInfo.CardModule2.module = int.Parse(data[1][0].ToString());
            characterInfo.CardModule3.module = int.Parse(data[2][0].ToString());
            characterInfo.CardModule4.module = int.Parse(data[3][0].ToString());
            characterInfo.CardModule5.module = int.Parse(data[4][0].ToString());
            characterInfo.CardModule6.module = int.Parse(data[5][0].ToString());
            
            characterInfo.CardModule1.Strength = int.Parse(data[0][1].ToString());
            characterInfo.CardModule2.Strength = int.Parse(data[1][1].ToString());
            characterInfo.CardModule3.Strength = int.Parse(data[2][1].ToString());
            characterInfo.CardModule4.Strength = int.Parse(data[3][1].ToString());
            characterInfo.CardModule5.Strength = int.Parse(data[4][1].ToString());
            characterInfo.CardModule6.Strength = int.Parse(data[5][1].ToString());

            characterInfo.CardModule1.Agility = int.Parse(data[0][2].ToString());
            characterInfo.CardModule2.Agility = int.Parse(data[1][2].ToString());
            characterInfo.CardModule3.Agility = int.Parse(data[2][2].ToString());
            characterInfo.CardModule4.Agility = int.Parse(data[3][2].ToString());
            characterInfo.CardModule5.Agility = int.Parse(data[4][2].ToString());
            characterInfo.CardModule6.Agility = int.Parse(data[5][2].ToString());

            characterInfo.CardModule1.Examine = int.Parse(data[0][3].ToString());
            characterInfo.CardModule2.Examine = int.Parse(data[1][3].ToString());
            characterInfo.CardModule3.Examine = int.Parse(data[2][3].ToString());
            characterInfo.CardModule4.Examine = int.Parse(data[3][3].ToString());
            characterInfo.CardModule5.Examine = int.Parse(data[4][3].ToString());
            characterInfo.CardModule6.Examine = int.Parse(data[5][3].ToString());

            characterInfo.CardModule1.Stealth = int.Parse(data[0][4].ToString());
            characterInfo.CardModule2.Stealth = int.Parse(data[1][4].ToString());
            characterInfo.CardModule3.Stealth = int.Parse(data[2][4].ToString());
            characterInfo.CardModule4.Stealth = int.Parse(data[3][4].ToString());
            characterInfo.CardModule5.Stealth = int.Parse(data[4][4].ToString());
            characterInfo.CardModule6.Stealth = int.Parse(data[5][4].ToString());


        for(int j =5; j<10;j++)
        {
            characterInfo.CardModule1.AttackTrait[j-5] = int.Parse(data[0][j].ToString());
            characterInfo.CardModule2.AttackTrait[j-5] = int.Parse(data[1][j].ToString());
            characterInfo.CardModule3.AttackTrait[j-5] = int.Parse(data[2][j].ToString());
            characterInfo.CardModule4.AttackTrait[j-5] = int.Parse(data[3][j].ToString());
            characterInfo.CardModule5.AttackTrait[j-5] = int.Parse(data[4][j].ToString());
            characterInfo.CardModule6.AttackTrait[j-5] = int.Parse(data[5][j].ToString());
        }

        for (int j = 10; j < 15; j++)
        {
            characterInfo.CardModule1.GuardTrait[j - 10] = int.Parse(data[0][j].ToString());
            characterInfo.CardModule2.GuardTrait[j - 10] = int.Parse(data[1][j].ToString());
            characterInfo.CardModule3.GuardTrait[j - 10] = int.Parse(data[2][j].ToString());
            characterInfo.CardModule4.GuardTrait[j - 10] = int.Parse(data[3][j].ToString());
            characterInfo.CardModule5.GuardTrait[j - 10] = int.Parse(data[4][j].ToString());
            characterInfo.CardModule6.GuardTrait[j - 10] = int.Parse(data[5][j].ToString());
        }




        for (int j =15; j<25;j++)
            {
                characterInfo.CardModule1.PassiveTrait[j-15] = int.Parse(data[0][j].ToString());
                characterInfo.CardModule2.PassiveTrait[j-15] = int.Parse(data[1][j].ToString());
                characterInfo.CardModule3.PassiveTrait[j-15] = int.Parse(data[2][j].ToString());
                characterInfo.CardModule4.PassiveTrait[j-15] = int.Parse(data[3][j].ToString());
                characterInfo.CardModule5.PassiveTrait[j-15] = int.Parse(data[4][j].ToString());
                characterInfo.CardModule6.PassiveTrait[j-15] = int.Parse(data[5][j].ToString());
            }
           
        


    }



    #endregion
    #region 도감 정보 받아오기

    private void r_loadPlayerData(List<Vegetal> vegetals, JsonData data)
    {
        Vegetal vegeta;
        bookdata.ClearDicitonary();
        for(int i=0;i<vegetals.Count;i++)
        {
            vegeta = vegetals[i];
            bookdata.StartDictionary(vegeta.name, vegeta.subname,vegeta.explain,vegeta.knowledge1,vegeta.knowledge2,vegeta.knowledge3,vegeta.bonus1,vegeta.bonus2,vegeta.bonus3
                ,int.Parse(data[i][1].ToString()), int.Parse(data[i][2].ToString()), int.Parse(data[i][3].ToString()), int.Parse(data[i][4].ToString()));
        }

    }

    #endregion


    #region 카드 정보 받아오기
    private void LoadTestCard(string code)
    {
        TextAsset textData;
        JsonData data;
        int cardcode;
        int over;
        int success;
        int fail;
        int hell1;
        int hell2;
        int hell3;

        Addressables.LoadAssetAsync<TextAsset>(code).Completed +=
            (handle) =>
            {
                textData = handle.Result;
                data = JsonMapper.ToObject(textData.text);
                for(int i=0; i<data.Count;i++)
                {
                    cardcode     = int.Parse(data[i][0].ToString());
                    over         = int.Parse(data[i][1].ToString());
                    success      = int.Parse(data[i][2].ToString());
                    fail         = int.Parse(data[i][3].ToString());
                    hell1        = int.Parse(data[i][4].ToString());
                    hell2        = int.Parse(data[i][5].ToString());
                    hell3        = int.Parse(data[i][6].ToString());


                    refTestCard tc = new refTestCard(cardcode, over, success, fail, hell1, hell2, hell3);                   
                    TestCardDictionary.Add(cardcode, tc);
                }
                

            };
            
    }

    private void loadPassiveCard(string code)
    {
        TextAsset textData;
        JsonData data;

        int cardcode;
        string name;
        int condition;
        int value;
        string keyword;
        string calcultype;
        string type;
        string Explain;
        string Card1;
        string Card2;
        string Card3;
        Addressables.LoadAssetAsync<TextAsset>(code).Completed +=
            (handle) =>
            {
                textData = handle.Result;
                data = JsonMapper.ToObject(textData.text);
                for (int i = 0; i < data.Count; i++)
                {
                    cardcode = int.Parse(data[i][0].ToString());                    
                    name =               data[i][1].ToString();
                    condition =int.Parse(data[i][2].ToString());
                    value    = int.Parse(data[i][3].ToString());
                    keyword            = data[i][4].ToString();
                    calcultype         = data[i][5].ToString();
                    type               = data[i][6].ToString();
                    Explain            = data[i][7].ToString();
                    Card1              = data[i][8].ToString();
                    Card2              = data[i][9].ToString();
                    Card3              = data[i][10].ToString();

                    passiveCard pc = new passiveCard(cardcode,name,condition,value,keyword,calcultype,type,Explain,Card1,Card2,Card3);
                    PassiveDictionary.Add(cardcode,pc);
                }

            };
    }

    private void loadAttackCard(string code)
    {
        TextAsset textData;
        JsonData data;

        int cardcode;
        int maxlevel;
        int minlevel;

        int valuea;
        int valueb;
        int valuec;
        int valued;
      
        string name;
        string explain;
        string damagetype;
        string testtype;
        

        int token1type;
        int token2type;
        int token1count;
        int token2count;
     


        Addressables.LoadAssetAsync<TextAsset>(code).Completed +=
        (handle) =>
        {
            textData = handle.Result;
            data = JsonMapper.ToObject(textData.text);
            for(int i=0; i < data.Count; i++)
            {
                cardcode     = int.Parse(data[i][0].ToString());
                maxlevel     = int.Parse(data[i][1].ToString());
                minlevel     = int.Parse(data[i][2].ToString());

                valuea       = int.Parse(data[i][3].ToString());
                valueb       = int.Parse(data[i][4].ToString());
                valuec       = int.Parse(data[i][5].ToString());
                valued       = int.Parse(data[i][6].ToString());

                name         =           data[i][7].ToString();
                explain      =           data[i][8].ToString();
                damagetype   =           data[i][9].ToString();
                testtype     =           data[i][10].ToString();
                
                token1type   = int.Parse(data[i][11].ToString());
                token2type   = int.Parse(data[i][12].ToString());
                token1count  = int.Parse(data[i][13].ToString());
                token2count  = int.Parse(data[i][14].ToString());


                //attackCard ac = new attackCard(cardcode,maxlevel,minlevel,valuea,valueb,valuec,valued,name,explain,damagetype,testtype,token1type,token2type,token1count, token2count);
                attackCard ac = new attackCard(cardcode, maxlevel, minlevel, valuea, valueb, valuec, valued, name, explain, damagetype, testtype, token1type, token2type, token1count, token2count);
                AttackDictionary.Add(cardcode, ac);
            }


        };
    }

    private void loadGuardCard(string code)
    {
        TextAsset textData;
        JsonData data;

        int cardcode;
        int cost;
        int cooltime;
        int damage;
        string name;
        string explain;
        string keyword;
        string testtype;
        int token1type;
        int token2type;
        int token1count;
        int token2count;
        int phase;

        Addressables.LoadAssetAsync<TextAsset>(code).Completed +=
        (handle) =>
        {
            textData = handle.Result;
            data = JsonMapper.ToObject(textData.text);
            for (int i = 0; i < data.Count; i++)
            {
                cardcode =  int.Parse(data[i][0].ToString());
                cost =      int.Parse(data[i][1].ToString());
                cooltime =  int.Parse(data[i][2].ToString());
                damage =    int.Parse(data[i][3].ToString());
                name =                data[i][4].ToString();
                explain =             data[i][5].ToString();
                testtype =            data[i][6].ToString();
                keyword =             data[i][7].ToString();
                token1type =int.Parse(data[i][8].ToString());
                token2type =int.Parse(data[i][9].ToString());
                token1count=int.Parse(data[i][10].ToString());
                token2count=int.Parse(data[i][11].ToString());
                phase =     int.Parse(data[i][12].ToString());


                guardCard gc = new guardCard(cardcode, cost, cooltime, damage, name, explain, testtype, keyword, token1type, token2type, token1count,token2count,phase);
                
                GuardDictionary.Add(cardcode, gc);
            }

            
        };
    }


    #endregion





    #region 진행 상황 받아오기


    #endregion

    #region 스크립터블 오브젝트 받아오기
    private void LoadScriptableObject<T>(string code ,Dictionary<int,T> dict) where T: ScriptableObject
    {
        int s_code;


        Addressables.LoadAssetsAsync<T>(code,OnDownloadQuestions).Completed +=
            (handle2) =>
             {
                 for(int i=0;i<handle2.Result.Count;i++)
                 {
                     T obj = handle2.Result[i];
                     s_code = int.Parse(obj.name);

                    dict.Add(s_code, obj);

                 }            
                 
                 switch(code)
                 {
                     case "Disease":
                         d_LoadPlayerData(DiseaseData);
                         break;
                     case "Item":
                         i_LoadPlayerData(ItemData);
                         break;
                     case "Nimrod":
                         n_LoadPlayerData(NimrodData);
                         break;
                         
                 }
                 
             };           
    }

    #endregion
    #region 이미지 받아오기
    private void LoadImage(string code , Dictionary<string,Sprite> dict)
    { 
        Sprite sprite;
        string s_name;

        Addressables.LoadAssetsAsync<Sprite>(code, OnDownloadQuestionsCategoryComplete).Completed +=
          (handle2) =>
          {
              for (int i = 0; i < handle2.Result.Count; i++)
              {
                  sprite = handle2.Result[i];
                  s_name = sprite.name;
                 dict.Add(s_name, sprite);
              }
          };
    }

    private void LoadImage(string code , Dictionary<int,Sprite> dict)
    {
        Sprite sprite;
        int s_name;
       

        Addressables.LoadAssetsAsync<Sprite>(code, OnDownloadQuestionsCategoryComplete).Completed +=
          (handle2) =>
          {
              for (int i = 0; i < handle2.Result.Count; i++)
              {
                  
                  sprite = handle2.Result[i];
                  s_name = int.Parse(sprite.name);
                  dict.Add(s_name, sprite);
              }
          };

        
    }
   
  
    #endregion

    #region 텍스트 받아오기
    private void LoadText(string path, Dictionary<int,string> dict)
    {
        TextAsset text;
        int code;
        Addressables.LoadAssetsAsync<TextAsset>(path, OnDownloadEnd).Completed +=
            (handle) =>
            {
                for (int i = 0; i < handle.Result.Count; i++)
                {

                    text = handle.Result[i];
                    code = int.Parse(text.name);

                    dict.Add(code,text.text);

                }
                ChangeArticle(path);
            };
    }

    private void LoadText(string path)
    {
        TextAsset textAsset;
        string longString;
        string Name;
        string[] texts;

        Addressables.LoadAssetsAsync<TextAsset>(path, OnDownloadEnd).Completed +=
            (handle) =>
            {
                for (int i = 0; i < handle.Result.Count; i++)
                {

                    textAsset = handle.Result[i];
                    longString = textAsset.text;
                    Name = textAsset.name;
                    texts = longString.Split('#');
                    AD_Monsters.Add(Name, texts);
                   
                }
            };

        
    }




    #endregion
    #region 도감 정보 로드하기
    private void LoadDictionary(string path ,JsonData data)
    {
        TextAsset textData;
        Addressables.LoadAssetAsync<TextAsset>(path).Completed +=
            (handle) =>
            {
                textData = handle.Result;
                data     = JsonMapper.ToObject(textData.text);
                v_ParsingJsonData(data, vegetalList);
                r_loadPlayerData(vegetalList,ResearchData);
            };
       
    }
    private void LoadDictionary(string path)
    {
        TextAsset textData;
        JsonData data;
        Addressables.LoadAssetAsync<TextAsset>(path).Completed +=
            (handle) =>
            {
                textData = handle.Result;
                data = JsonMapper.ToObject(textData.text);
                b_PasrsingJsonData(data);

            };
    }


    private void LoadDictNum(string path)
    {
        TextAsset textData;
        Addressables.LoadAssetAsync<TextAsset>(path).Completed +=
            (handle) =>
            {
                textData = handle.Result;
                numData = JsonMapper.ToObject(textData.text);
                n_ParsingJsonData();

            };
      
    }


    private void n_ParsingJsonData()
    {
        //bookdata.StartList.Add();
        for(int i=0;i<numData.Count;i++)
        {
            bookdata.AreaNames.Add(numData[i][0].ToString());
            bookdata.StartList.Add(int.Parse(numData[i][1].ToString()));
            bookdata.EndList.Add(int.Parse(numData[i][2].ToString()));
        }
    }

    #endregion

    #region Addressable callback
    private void OnDownloadQuestionsCategoryComplete(
    Sprite obj)
    { }
    private void OnDownloadQuestions<T>(
    T obj)
    { }
    private void OnDownloadEnd(TextAsset end)
    { }
    #endregion

    #endregion 로드
    #region SPAWN_UI

    private void SpawnUI(string path , Transform Parent , int index) 
    {
        Addressables.LoadAssetAsync<GameObject>(path).Completed +=
             (handle) =>
             {
                GameObject ui = Addressables.InstantiateAsync(path).Result;
                 ui.transform.parent = Parent;
                 ui.transform.SetSiblingIndex(index);
                 ui.transform.localPosition = new Vector3(0, 0, 0);
                 ui.transform.localScale = new Vector3(1, 1, 1);
              
             };             
    }

    private void SpawnUI(AssetReferenceGameObject reference)
    {
       Addressables.InstantiateAsync(reference);
    }



    #endregion


    #region 세이브
    public void SaveData()
    {
        #region 질병저장

        List<refDisease> DiseaseList = new List<refDisease>();

        for (int i = 0; i < characterInfo.DiseaseArray.Length; i++)
        {

          
            int code = characterInfo.DiseaseArray[i].DiseaseCode;
            int turn = characterInfo.DiseaseArray[i].DiseaseTurn;
            int value1 = characterInfo.DiseaseArray[i].t_DiseaseValue;
           
            int value2 = characterInfo.DiseaseArray[i].e_DiseaseValue;
         
            refDisease neoref = new refDisease(code, turn, value1, value2);
            DiseaseList.Add(neoref);
        }

        JsonData saveData = JsonMapper.ToJson(DiseaseList);
        File.WriteAllText(filepath + "/Disease.json", saveData.ToString());

        #endregion
        #region SaveItem
        List<refItem> ItemList = new List<refItem>();

        for(int i=0; i <characterInfo.ItemArray.Length; i++)
        {
           
            int decay = characterInfo.ItemArray[i].ItemDecay;
            int smell = characterInfo.ItemArray[i].ItemSmell;
            int count = characterInfo.ItemArray[i].ItemCount;
            int code = characterInfo.ItemArray[i].item.itemcode;

            refItem item = new refItem(code,decay,smell,count);
            ItemList.Add(item);
           
        }


        JsonData saveItemData = JsonMapper.ToJson(ItemList);
        File.WriteAllText(filepath + "/Item.json", saveItemData.ToString());
        #endregion
        #region 
        List<refModule> refModules = new List<refModule>();

        void saveModule(PlayerInfo.CardModule cardModule)
        {
            int code = cardModule.module;
            int str = cardModule.Strength;
            int agi = cardModule.Agility;
            int exa = cardModule.Examine;
            int ste = cardModule.Stealth;

            int at1 = cardModule.AttackTrait[0];
            int at2 = cardModule.AttackTrait[1];
            int at3 = cardModule.AttackTrait[2];
            int at4 = cardModule.AttackTrait[3];
            int at5 = cardModule.AttackTrait[4];

            int gt1 = cardModule.GuardTrait[0];
            int gt2 = cardModule.GuardTrait[1];
            int gt3 = cardModule.GuardTrait[2];
            int gt4 = cardModule.GuardTrait[3];
            int gt5 = cardModule.GuardTrait[4];

            int t1 = cardModule.PassiveTrait[0];
            int t2 = cardModule.PassiveTrait[1];
            int t3 = cardModule.PassiveTrait[2];
            int t4 = cardModule.PassiveTrait[3];
            int t5 = cardModule.PassiveTrait[4];
            int t6 = cardModule.PassiveTrait[5];
            int t7 = cardModule.PassiveTrait[6];
            int t8 = cardModule.PassiveTrait[7];
            int t9 = cardModule.PassiveTrait[8];
            int t10 = cardModule.PassiveTrait[9];

            refModule rm = new refModule(code,str,agi,exa,ste,at1,at2,at3,at4,at5,gt1,gt2,gt3,gt4,gt5,t1,t2,t3,t4,t5,t6,t7,t8,t9,t10);
            refModules.Add(rm);
        }

        saveModule(characterInfo.CardModule1);
        saveModule(characterInfo.CardModule2);
        saveModule(characterInfo.CardModule3);
        saveModule(characterInfo.CardModule4);
        saveModule(characterInfo.CardModule5);
        saveModule(characterInfo.CardModule6);

        JsonData savemoduleData = JsonMapper.ToJson(refModules);
        File.WriteAllText(filepath + "/Module.json", savemoduleData.ToString());


        #endregion

    }

    #endregion
    #region 데이터 파싱
    private void v_ParsingJsonData(JsonData data, List<Vegetal> vlist)
    {
        string tempname;
        string tempsubname;
        string tempexplain;
        string tempknowledge1;
        string tempknowledge2;
        string tempknowledge3 ;
        string tempbonus1 ;
        string tempbonus2 ;
        string tempbonus3 ;

        for (int i = 0; i < data.Count; i++)
        {
            tempname =       data[i][0].ToString();
            tempsubname =    data[i][1].ToString();
            tempexplain =    data[i][2].ToString();
            tempknowledge1 = data[i][3].ToString();
            tempknowledge2 = data[i][4].ToString();
            tempknowledge3 = data[i][5].ToString();
            tempbonus1 =     data[i][6].ToString();
            tempbonus2 =     data[i][7].ToString();
            tempbonus3 =     data[i][8].ToString();

            Vegetal neoVegetal = new Vegetal(tempname,tempsubname, tempexplain, tempknowledge1, tempknowledge2, tempknowledge3, tempbonus1, tempbonus2, tempbonus3);
            vlist.Add(neoVegetal);
        }

    }
    private void b_PasrsingJsonData(JsonData data)
    {
        string nm;
        string snm;
        int hp;
        int maxben;
        int maxpen;
        int str;
        int agi;
        int exa;
        int ste;

        string wea;
        string imm;
        string ben;
        string pen;

        int er;
        int br;
        int ex1;
        int ex2;

        for(int i=0; i<data.Count; i++)
        {
            nm      =           data[i][0].ToString();
            snm      =           data[i][1].ToString();
            hp      = int.Parse(data[i][2].ToString());
            maxben  = int.Parse(data[i][3].ToString());
            maxpen  = int.Parse(data[i][4].ToString());
            str     = int.Parse(data[i][5].ToString());
            agi     = int.Parse(data[i][6].ToString());
            exa     = int.Parse(data[i][7].ToString());
            ste     = int.Parse(data[i][8].ToString());
            wea     =           data[i][9].ToString();
            imm     =           data[i][10].ToString();
            ben     =           data[i][11].ToString();
            pen     =           data[i][12].ToString();
            er      = int.Parse(data[i][13].ToString());
            br      = int.Parse(data[i][14].ToString());
            ex1     = int.Parse(data[i][15].ToString());
            ex2     = int.Parse(data[i][16].ToString());
            beast neoBeast = new beast(nm,snm,hp,maxben,maxpen,str,agi,exa,ste,wea,imm,ben,pen,er,br,ex1,ex2);
            BeastDicitonary.Add(nm, neoBeast);
          
        }
        
    }


    #endregion




    #region 지역 바꾸기

    public void ChangeArticle(string area)
    {
        switch (area)
        {
            case "Intro":
                AD_Current = AD_Intro;
                break;

            case "Tutorial":
                AD_Current = AD_Tutorial;
                break;

            case "YenaGarden":
                AD_Current = AD_Garden;
                break;

            case "Observation":
                AD_Current = AD_Observation;
                break;


        }
    }

    #endregion



    #region 데이터 해제

   

    private void UnloadCache()
    {
        //I_DiseaseDictionary[0].
        Addressables.Release(I_DiseaseDictionary);
    }
   

   

    #endregion

    private JsonData JsonCatcher(string path )
    {
        TextAsset textData;
        textData = Resources.Load(path) as TextAsset;            
        return JsonMapper.ToObject(textData.text);       
    }

    public void SpawnActionCard()
    {
        //AsyncOperationHandle<GameObject> handle;
        ActionCard card;
 /*
        Addressables.InstantiateAsync(CardReference).Completed +=
        (handle) =>
           {
               card = handle.Result.GetComponent<ActionCard>();               
           };       */
    }

    public void GetPlayerInfo(out PlayerInfo player)
    {
        player = characterInfo;
    }
    
    public void GetBookInfo(out BookData book)
    {
        book = bookdata; 
    }


    public void OnApplicationPause(bool pause)
    {
       // if(pause) SaveData();

    }


    public void OnApplicationQuit()
    {
        //SaveData();
    }
    
}
