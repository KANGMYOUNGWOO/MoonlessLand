using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour, IManager
{
    public struct BattleActionCard
    {
        public int code;
        public int grade;
        public int value;
        public string name;
        public string explain;
        public string damageType;
        public string testType;

        public int type;

        public BattleActionCard(int cd, int tp, int gd, int va, string nm, string ep, string dt, string tt)
        {
            code = cd;
            type = tp;
            grade = gd;
            value = va;
            name = nm;
            explain = ep;
            damageType = dt;
            testType = tt;
        }
    }


    #region Singleton Instaances
    public GameManager gameManager { get { return GameManager.gameManager; } }
    private LogicManager    logicManager;
    private ResourceManager resourceManager;
    private CharacterManager characterManager;
    private TextManager textManager;
    private UIManager uiManager;
#endregion

    private Dictionary<string, MonsterBase> monsters = new Dictionary<string, MonsterBase>();
   
    [SerializeField] private Transform monster;

    public MonsterBase CurrentAI { get; set; }
    public UIBattle BattleUI { get; set; }
    public BattleButton battleButton { get; set; }


   static BattleActionCard empty = new BattleActionCard(0, 0, 0, 0, "empty", "empty", "empty", "empty");

    private List<BattleActionCard> SettedCards  = new List<BattleActionCard>() { empty, empty, empty };
    private List<BattleActionCard> HandAttackCards = new List<BattleActionCard>() {empty,empty,empty,empty };
    private List<BattleActionCard> HandGuardCards = new List<BattleActionCard>() { empty, empty, empty, empty };
    private List<BattleActionCard> EnemyBattleCards = new List<BattleActionCard>() { empty, empty, empty };

    private int[] MatchResult = new int[3];
  


    #region EnemyStat

    private string EnemyName;
    private int    EnemyHp;
    private int    EnemyBen;
    private int    EnemyMaxBen;
    private int    EnemyPen;
    private int    EnemyMaxPen;
    private int    EnemyStrength;
    private int    EnemyAgility;
    private int    EnemyExamine;
    private int    EnemyStealth;
    private int    EssentialReward; 
    private int    Reward1; 
    private int    Reward2; 
    private string EnemyWeakness;
    private string EnemyImmune;
    private string EnemyBenefit;
    private string EnemyPenalty;


    #endregion

    #region initialize
    void Start()
    {
        logicManager = GameManager.GetManagerClass<LogicManager>();
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        textManager = GameManager.GetManagerClass<TextManager>();

        MonsterBase[] mon = monster.GetComponents<MonsterBase>();
        
        for (int i = 0; i < mon.Length; i++) {
            mon[i].SetManager(this,logicManager);
            monsters.Add(mon[i].getName(), mon[i]);
        }

    
    }
    #endregion

    #region 내 손에 카드가 4장이 될 때까지 카드를 뽑습니다 
    public void DrawAttackCards(int moduleNum)
    {
        #region
        
      
        nimrod module = resourceManager.NimrodDictionary[moduleNum];

        int[] grade = new int[4];
        int[] AttackArray = new int[4];
      
        attackCard ac = resourceManager.AttackDictionary[0];
        attackCard[] acs = new attackCard[] { ac, ac, ac, ac };
        int perc = 0;
        int value = 0;
        int LeftCards = 0;

        #region 손에 들고 있는 카드의 장수를 셉니다
        void CountLeftCards()
        {            
            while(LeftCards  <  4)
            {
                if (HandAttackCards[LeftCards].code == 0) break;
                LeftCards += 1;
            }
        }
        #endregion
            
        #region 등급에 맞게 카드를 조정합니다
        void GradeSetter(int index)
        {
            while (true)
            {
                perc = Random.Range(0, module.EssentialAttack.Count);
                acs[index] = resourceManager.AttackDictionary[module.EssentialAttack[perc]];
                if (grade[index] >= acs[index].MinLevel && grade[index] <= acs[index].MaxLevel)
                {
                    break;
                }

            }
        }
        #endregion
        CountLeftCards();

        for (int i = LeftCards; i < 4; i++)
        {            
            perc = Random.Range(1, 101);
            if (perc <= 50) grade[i] = 0;
            else if (perc <= 80) grade[i] = 1;
            else if (perc <= 95) grade[i] = 2;
            else if (perc <= 100) grade[i] = 3;
        }

      

        for (int i = LeftCards; i < 4; i++)
        {
            GradeSetter(i);
        }

        for (int i = LeftCards; i < 4; i++)
        {
            AttackArray[i] = acs[i].Code;
        }

       

      
        for (int i = LeftCards; i < 4; i++)
        {
            switch (grade[i])
            {
                case 0:
                    value = acs[i].ValueD;
                    break;
                case 1:
                    value = acs[i].ValueC;
                    break;
                case 2:
                    value = acs[i].ValueB;
                    break;
                case 3:
                    value = acs[i].ValueA;
                    break;
            }
            HandAttackCards[i] = new BattleActionCard(acs[i].Code, 1, grade[i], value, acs[i].Name, acs[i].Explain, acs[i].DamageType, acs[i].TestType);
            logicManager.ButtonActive(i, value, acs[i].Name, grade[i], acs[i].DamageType, acs[i].TestType, acs[i].Explain, acs[i].Token1Type, acs[i].Token2Type, acs[i].Token1Count, acs[i].Token2Count, option: 0);
        }
      
        
        #endregion 
    }
    #endregion

    #region 카드를 중앙에 배치합니다.
    public void SetCard(int type, int num)
    {
       
        string damage = "";
        string s_damage = "";
        string grade = "";
        string CardExplain = "";
        int direction = 0;

        switch (type)
        {
            case 1:
                switch (HandAttackCards[num].grade)
                {
                    case 0:
                        grade = "D";
                        break;
                    case 1:
                        grade = "C";
                        break;
                    case 2:
                        grade = "B";
                        break;
                    case 3:
                        grade = "A";
                        break;
                }

                switch (HandAttackCards[num].damageType)
                {
                    case "절단":
                        damage = "Cut";
                        break;
                    case "관통":
                        damage = "Pierece";
                        break;
                    case "타격":
                        damage = "Hit";
                        break;
                }

                Sprite curBackSprite = resourceManager.I_ActionCardDictionary["AttackCard"];
                Sprite curDamageType = resourceManager.I_ActionCardDictionary[damage];
                Sprite curTestIcon = resourceManager.I_ActionCardDictionary[HandAttackCards[num].testType];
                Sprite curGrade = resourceManager.I_ActionCardDictionary[grade];
                if(HandAttackCards[num].value > 0)
                {
                    s_damage = string.Format("{0} 피해\n", HandAttackCards[num].value);
                }
                CardExplain = string.Format("{0},{1}",s_damage,HandAttackCards[num].explain);
                direction = uiManager.battleCard.SetPlayerCard(curBackSprite, curDamageType, curTestIcon, curGrade, HandAttackCards[num].name, CardExplain, HandAttackCards[num].damageType);
                SettedCards[direction] = new BattleActionCard(HandAttackCards[num].code,1,HandAttackCards[num].grade,HandAttackCards[num].value,HandAttackCards[num].name,HandAttackCards[num].explain,HandAttackCards[num].damageType,HandAttackCards[num].testType);

                HandAttackCards.RemoveAt(num);
                HandAttackCards.Add(empty);

                break;
            case 2:
                HandAttackCards.RemoveAt(num);
                HandAttackCards.Add(empty);
                break;
        }

       


        if(SettedCards[0].code != 0)
            if(SettedCards[1].code != 0)
                if(SettedCards[2].code != 0)
                    ActiveButton();

      
    }

    

    private void ActiveButton()
    {
        battleButton.gameObject.SetActive(true);
        battleButton.SetFire(true);
    }

    public void ResetSettedCard()
    {

        SettedCards[0] = empty;
        SettedCards[1] = empty;
        SettedCards[2] = empty;

        Sprite ready = resourceManager.I_ActionCardDictionary["Ready"];

        uiManager.battleCard.UnSetPlayerCard(ready,0);
        uiManager.battleCard.UnSetPlayerCard(ready,1);
        uiManager.battleCard.UnSetPlayerCard(ready,2);

        logicManager.ActionCardcon.gameObject.SetActive(true);
    }

    #endregion

    #region 적의 카드를 배치합니다
    public void SetEnmeyCard(int index, int type, int value, string name, string damagetype, string explain)
    {
        BattleActionCard bc = new BattleActionCard(1,type,3,value,name,explain,damagetype,"");
        EnemyBattleCards[index] = bc;

        string damage = "";
        string s_Explain = "";
        switch (damagetype)
        {
            case "절단":
                damage = "Cut";
                break;
            case "관통":
                damage = "Pierece";
                break;
            case "타격":
                damage = "Hit";
                break;
        }

        s_Explain = string.Format("{0} 피해\n{1}",value,explain);

       
        Sprite curDamageType = resourceManager.I_ActionCardDictionary[damage];


        uiManager.battleCard.SetEnemyCard(index,curDamageType,name,s_Explain);
        

    }
    #endregion


    private void CompareStat()
    {
        int tempSet = 0;
        int tempEnemy = 0;
        for (int i = 0; i < 3; i++)
        {
            switch (SettedCards[i].damageType)
            {
                case "절단":
                    tempSet = 2;
                    break;

                case "관통":
                    tempSet = 3;
                    break;

                case "타격":
                    tempSet = 1;
                    break;
            }

            switch (EnemyBattleCards[i].damageType)
            {
                case "절단":
                    tempEnemy = 2;
                    break;

                case "관통":
                    tempEnemy = 3;
                    break;

                case "타격":
                    tempEnemy = 1;
                    break;
            }

            if (SettedCards[i].value > EnemyBattleCards[i].value)
            {
                if (tempSet == tempEnemy) MatchResult[i] = 0;
                else
                {
                    if (tempSet == 1) { MatchResult[i] = tempEnemy == 2 ? 0 : 1; Debug.Log(string.Format("첫번째 조건")); }
                    else { MatchResult[i] = tempSet > tempEnemy ? 1 : 0; Debug.Log(string.Format("두번째 조건")); }
                }

            }

            else if (SettedCards[i].value == EnemyBattleCards[i].value)
            {
                if (tempSet == tempEnemy) MatchResult[i] = 2;
                else
                {
                    if (tempSet == 1) { MatchResult[i] = tempEnemy == 2 ? -1 : 1; Debug.Log(string.Format("세번째 조건")); }
                    else { MatchResult[i] = tempSet > tempEnemy ? 1 : -1; Debug.Log(string.Format("네번째 조건")); }
                }

            }
            else
            {
                if (tempSet == tempEnemy) MatchResult[i] = 0;
                else
                {
                    if (tempSet == 1) { MatchResult[i] = tempEnemy == 2 ? -1 : 0; Debug.Log(string.Format("다섯번째 조건")); }
                    else { MatchResult[i] = tempSet > tempEnemy ? 0 : -1; Debug.Log(string.Format("여섯번째 조건")); }
                }
            }
                



             
        }
    }

    public int GetEnemyStat(string stat)
    {
        int num = 0;
        switch(stat)
        {
            case "strength":
                num = EnemyStrength;
                break;
            case "agility":
                num = EnemyAgility;
                    break;
            case "examine":
                num = EnemyStealth;
                break;
            case "stealth":
                num = EnemyExamine;
                break;
        }
        return num;
    }

    public void DataSend(out int result1, out int result2, out int result3, out int pd1, out int pd2, out int pd3 , out int ed1, out int ed2, out int ed3)
    {
        result1 = MatchResult[0];
        result2 = MatchResult[1];
        result3 = MatchResult[2];

        pd1 = SettedCards[0].value;
        pd2 = SettedCards[1].value;
        pd3 = SettedCards[2].value;

        ed1 = EnemyBattleCards[0].value;
        ed2 = EnemyBattleCards[1].value;
        ed3 = EnemyBattleCards[2].value;
    }


    public string GetEnemyName()
    {
        return EnemyName;
    }
    public int GetEnemyBehaviour()
    {
        return CurrentAI.GetCurrentBehaviour();
    }
    public bool GetbisOption()
    {
        return CurrentAI.bisOption;
    }

    #region 전투 세팅
    public void initializeBattleSetting(string Name)
    {
        
        beast be = resourceManager.BeastDicitonary[Name];
        string enemyWeak = "";
        string enemyImmune = "";

        EnemyName     = be.name;
        EnemyHp       = be.hp;
        EnemyStrength = be.strength;
        EnemyAgility  = be.agility;
        EnemyExamine  = be.examine;
        EnemyStealth  = be.stealth;
        EnemyWeakness = be.weak;
        EnemyImmune   = be.immune;
       
        EssentialReward = be.EssentialReward;
        
        Reward1       = be.ExtraReward1;
        Reward2       = be.ExtraReward2;
        
        switch(EnemyWeakness)
        {
            case "절단":
                enemyWeak = "Cut";
                break;
            case "관통":
                enemyWeak = "Pierece";
                break;
            case "타격":
                enemyWeak = "Hit";
                break;
        }

        switch (EnemyImmune)
        {
            case "절단":
                enemyImmune = "Cut";
                break;
            case "관통":
                enemyImmune = "Pierece";
                break;
            case "타격":
                enemyImmune = "Hit";
                break;
        }

        Sprite weak = resourceManager.I_ActionCardDictionary[enemyWeak];
        Sprite immune = resourceManager.I_ActionCardDictionary[enemyImmune];

        BattleUI.gameObject.SetActive(true);
        BattleUI.InitializeUI(EnemyHp,EnemyStrength,EnemyAgility,EnemyExamine,EnemyStealth,weak,immune);

        CurrentAI = monsters[Name];
       CurrentAI.SelectFirstBehaviour();

    }
    #endregion

    public void Monsterbehaviour(int option = 0, int result = 0)
    {
        CurrentAI.Action(option , result);
    }

    public void DuelSequence()
    {
        logicManager.ActionCardcon.gameObject.SetActive(false);
        CompareStat();
        uiManager.battleCard.DuelSequence();
    }


    #region 적에게 피해를 줍니다.
    public void GetDamage(int hp , string attackType)
    {
        if (attackType == EnemyWeakness) hp *= 2;
        else if (attackType == EnemyImmune) hp /= 2;
        EnemyHp -= hp;
        /*
        if (attackType == EnemyBenefit)
        {
            EnemyBen -= hp;
            if (EnemyBen <= 0) EnemyBen = 0;
            BattleUI.SetBenefitBar(EnemyBen);
        }
        if (attackType == EnemyPenalty)
        {
            EnemyPen -= hp;
            if (EnemyPen <= 0) EnemyPen = 0;
            BattleUI.SetPenaltyBar(EnemyPen);
        }
        */
        if (EnemyHp <= 0)
        {
            logicManager.ActionCardcon.RemoveAll();
            textManager.Clear();

            int grade = 0;
            List<int> CardList = new List<int>();
           
            bool bisbonus   = EnemyBen <= 0 ? true : false; 
            bool bispenalty = EnemyPen > 0 ? false : true;
           

            refTestCard tc1;
            refTestCard tc2;
          
          
            Sprite s1;
            Sprite s2;
            Sprite s3;
            Sprite s4;
          
          

            if (bisbonus) grade += 1;
            if (bispenalty) grade -= 1;
            else grade += 1;
           
            if (grade <= 0) grade = 0;
            if (grade == 2)
            {
                Debug.Log(Reward1);
                Debug.Log(Reward2);
                for(int i = Reward1; i<Reward2+1;i++)
                {
                    CardList.Add(i);
                }

                System.Random rng = new System.Random();
                int n = CardList.Count;
                int end = n;
                while (n > 0)
                {
                    n--;
                    int k = rng.Next(0, end);
                    int value = CardList[k];
                    CardList[k] = CardList[n];
                    CardList[n] = value;
                }
              

                
                tc1 = resourceManager.TestCardDictionary[CardList[0]];
                tc2 = resourceManager.TestCardDictionary[CardList[1]];
                
                
              

              s1 = resourceManager.I_CardBackDictionary[tc1.code];
              s2 = resourceManager.I_CardBackDictionary[tc2.code];              
              s3 = resourceManager.I_CardFrontDictionary[tc1.code];
              s4 = resourceManager.I_CardFrontDictionary[tc2.code];
            
              //BattleUI.Win(bisbonus,bispenalty,grade,tc1,tc2,s1,s2,s3,s4);
              characterManager.AddItem(EssentialReward,1);
                Item item = resourceManager.ItemDictionary[EssentialReward];
                uiManager.itemAddMessage.AddMessage(item.ItemName, true);
                //logicManager.TestUI.EndBattle(EnemyBenefit, EnemyPenalty, bisbonus, bispenalty, grade, EnemyMaxBen, EnemyMaxPen, item,tc1,tc2,tc3,s1,s2,s3);
            }
            else
            {
                //BattleUI.Win(bisbonus, bispenalty, grade);
            }
         
            
            EnemyHp = 0;
            

           
        }
       
        else BattleUI.SetHPBar(EnemyHp);
    }
    #endregion

    public void DamageToPlayer(int hp, string attackType)
    {
        characterManager.playerGetDamage(hp);
    }



    




}
