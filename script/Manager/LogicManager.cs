using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour, IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }


    [SerializeField] private Prologue Prologue;
    #region variable

    //Enum
    public enum GameMode { None, Read, Battle, Select, Cutscene, Map, Test, Mini, Setup };
    public GameMode CurGameMode { get; set; } = GameMode.Read;

    public enum BattlePhase { Attack, Guard, Support };
    public BattlePhase CurPhase { get; set; } = BattlePhase.Attack;

    #region SingleTonManager
    //Component
    private EventManager eventManager;
    private TextManager textManager;
    private UIManager uiManager;
    private CharacterManager characterManager;
    private ResourceManager resourceManager;
    private BattleManager battleManager;
    #endregion


    public PlayerInfo playerInfo { get; set; }

    public int[] connection = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] code = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public bool[] exception = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };


   
    #region Position
    private int currentPosition = 0;
    public bool _selectProgressfirst { get; set; } = true;
    private int CurrentEventCode = 0;
    private int currentOption = 0;
    private bool bisEnd = false;
    #endregion

 

    #region  판정
    //판정
    private int testChance = 0;
    private int testPrevResult = 0;
    private int testAfterResult = 0;
    private int[] ResultArray = new int[10];
    private int testDifficult = 0;
    private int testCardType = 0;
    #endregion

    private attackCard currentAttackCard;
    private guardCard currentGuardCard;

  
    private bool testResult = false;


    #region 
    private bool[] bIsTestArray = new bool[5];
    private string[] testTypeArray = new string[5];
    private int[] testDifficulty = new int[5];

    private Item[] itemQuizArray = new Item[4];
    private Item CurrentItem = null;
    private Item KeepItem = null;

    #endregion

    public TestCardSet TestUI { get; set; }
    public ActioncardController ActionCardcon { get; set; }
    public QuizObject quizObject { get; set; }

    public SelectActor selectActor { get; set; }

    #endregion

    


    #region Initialize

    private void Awake()
    {
        eventManager = GameManager.GetManagerClass<EventManager>();
        textManager = GameManager.GetManagerClass<TextManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        battleManager = GameManager.GetManagerClass<BattleManager>();

       // ReadArea.gameObject.SetActive(false);
    }

    private void Start()
    {
        
    }

    #endregion


    #region 게임모드 변경하기

    #region 읽기
    private void ReadFunction(bool loaded)
    {
        TurnProgress();
        eventManager.CurrentEvent.Excute_Event(CurrentEventCode, isStart: loaded);
    
        textManager.ReadReadTextAsset(CurrentEventCode);

    }
    #endregion
    #region 선택 단계
    private void SelectFunction()
    {
        
        selectActor.SelectSceneActive(true);
        uiManager.burning(false);
        ActionCardcon.RemoveAll();

        if (currentPosition >= 0)
        {
            int _mid = 0;
            int _pro = 0;
            int _standard = 0;

            for (int i = 0; i < 4; i++)
            {
                _mid = connection[i + 4] > 0 ? _mid + 1 : _mid;
                _pro = connection[i + 8] > 0 ? _pro + 1 : _pro;
            }

            _standard = _pro > 0 ? 7 : 3;
          
            if (currentPosition > _standard)
            {
                SelectTableOut();
                _selectProgressfirst = true;
                currentPosition = 0;
                eventManager.CurrentEvent.Select_Event(0);
            }
        }

        

        if (_selectProgressfirst)
        {
            SetSelectButton();
        }
        else ReShowSelectPage();


        ButtonSetter();
        _selectProgressfirst = false;
    }
    #endregion
    #region 전투

   


    #region 전투 시작
    private void BattleFunction()
    {
        int _subconnection = 0;
        int _midconnection = 0;
        int _proconnection = 0;        
        for (int i = 0; i < connection.Length; i++)
        {
            if (i < 4) _subconnection = connection[i] > 0 ? _subconnection + 1 : _subconnection;           
            else if (i < 8) _midconnection = connection[i] > 0 ? _midconnection + 1 : _midconnection;
            else if (i < 12) _proconnection = connection[i] > 0 ? _proconnection + 1 : _proconnection;
        }

       
        for (int i = 0; i < 8; i++) selectActor.ActiveLine(false, i, 2);
    
        for (int i = 0; i < 8; i++) selectActor.ActiveLine(false, i, 1);
     
        for (int i = 0; i < 4; i++) selectActor.ActiveLine(false, i, 0);

    
        selectActor.SelectSceneActive(false);
       
    
        textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(), battleManager.GetEnemyName());

        #region 폐기할건데 혹시 모르니까 
        /*
        //textManager.ReadReadTextAsset(CurrentEventCode);

        
        //nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        //attackCard ac;

        //ac = resourceManager.AttackDictionary[0];
        //ButtonActive(0, 1, testDifficulty[0], ac.TestType, ac.Name, ac.Explain, ac.Cost, ac.Cooltime, option: 0);
        //ButtonActive(0, testDifficulty[0], ac.TestType, ac.Name, ac.Explain, 1, ac.DamageType, "s", 1, 1, 1, ac.Token1Type, ac.Token2Type, ac.Token1Count, ac.Token2Count, option: 0);
        
        for (int i = 0; i < module.EssentialAttack.Count; i++) {
            ac = resourceManager.AttackDictionary[module.EssentialAttack[i]];
            testDifficulty[i+1] = battleManager.GetEnemyStat(ac.TestType);

            int tempCool = 0;
            
            if (ac.Cooltime > 0)
            {
                tempCool = characterManager.GetIsCoolTime(ac.Code) ? characterManager.GetCoolTime(ac.Code) : ac.Cooltime;
            }
            
        ButtonActive(i+1, testDifficulty[i+1], ac.TestType, ac.Name, ac.Explain, 1, ac.DamageType, "s", 1, 1, tempCool, ac.Token1Type, ac.Token2Type, ac.Token1Count, ac.Token2Count, option: 0);            
        }
        for(int i = module.EssentialAttack.Count; i<module.maxAttackTrait; i++)
        {
            if (playerInfo.CurrentModule.AttackTrait[i - module.EssentialAttack.Count] != 0)
            {
                ac = resourceManager.AttackDictionary[playerInfo.CurrentModule.AttackTrait[i - module.EssentialAttack.Count]];
                testDifficulty[i + 1] = battleManager.GetEnemyStat(ac.TestType);
                //ButtonActive(i + 1, 1, testDifficulty[i+1], ac.TestType, ac.Name, ac.Explain, ac.Cost, ac.Cooltime, option: 0);

                //ActionCardcon.AddCard(1, i+1, ac.Name, ac.Explain, cost: ac.Cost, cooltime: ac.Cooltime , option:0);
            }
        }
        */
        #endregion
      
        battleManager.DrawAttackCards(playerInfo.CurrentModule.module);
        battleManager.Monsterbehaviour();
    }
    #endregion

    public void Battle()
    {       
        ChangeGameMode(GameMode.Battle);        
    }
    #region 방어태세일때 호출 ㄱ
    public void GuardFunction()
    {
        
        textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(), battleManager.GetEnemyName());

        nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        guardCard gc;
        int tempCool=0;


        gc = resourceManager.GuardDictionary[0];
        //ButtonActive(0,0, gc.TestType, gc.Name, gc.Explain, gc.Damage, gc.Keyword, gc.Cost, gc.Cooltime, gc.Cooltime, gc.Token1Type, gc.Token2Type, gc.Token1Count, gc.Token2Count);

        
        for(int i=0;i<module.EssentialGuard.Count;i++)
        {
           
            gc = resourceManager.GuardDictionary[module.EssentialGuard[i]];       
            testDifficulty[i + 1] = battleManager.GetEnemyStat(gc.TestType);        
            tempCool = characterManager.GetIsCoolTime(gc.Code) ? characterManager.GetCoolTime(gc.Code) : gc.Cooltime;          
            //ButtonActive(i + 1, testDifficulty[i + 1], gc.TestType, gc.Name, gc.Explain, gc.Damage, gc.Keyword, gc.Cost, gc.Cooltime, tempCool, gc.Token1Type, gc.Token2Type, gc.Token1Count, gc.Token2Count);        
            if (gc.Cooltime > 0)
            {
                if (characterManager.GetIsCoolTime(gc.Code)) ActionCardcon.SetCoolTime(i + 1);
                if (tempCool == 0) ActionCardcon.CoolingUI(i + 1, tempCool, gc.Cooltime);
            }
            
        }
        
    }

    public void GuardCardCool()
    {
        nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        guardCard gc;
        int tempCool = 0;


        for (int i = 0; i < module.EssentialGuard.Count; i++)
        {
            gc = resourceManager.GuardDictionary[module.EssentialGuard[i]];          
            tempCool = characterManager.GetIsCoolTime(gc.Code) ? characterManager.GetCoolTime(gc.Code) : gc.Cooltime; 
            if (gc.Cooltime > 0)
            {
                if (characterManager.GetIsCoolTime(gc.Code)) ActionCardcon.SetCoolTime(i + 1);
                if (tempCool == 0) ActionCardcon.CoolingUI(i + 1, tempCool, gc.Cooltime);
            }

        }
      
    }


    #endregion

    #region 2번째 공격 부터 호출
    public void BattleFunction2()
    {
        characterManager.Cooling();

        testDifficulty[0] = battleManager.GetEnemyStat("strength");

        textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(), battleManager.GetEnemyName());

        nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        attackCard ac;
        int tempCool = 0;
        ac = resourceManager.AttackDictionary[0];
        //ButtonActive(0, testDifficulty[0], ac.TestType, ac.Name, ac.Explain, ac.Damage, ac.DamageType, ac.Keyword, ac.Cost, ac.Cooltime, ac.Cooltime, ac.Token1Type, ac.Token2Type, ac.Token1Count, ac.Token2Count, option: 0);

        
        for (int i = 0; i < module.EssentialAttack.Count; i++)
        {
            ac = resourceManager.AttackDictionary[module.EssentialAttack[i]];
            testDifficulty[i + 1] = battleManager.GetEnemyStat(ac.TestType);
            //tempCool = characterManager.GetIsCoolTime(ac.Code) ? characterManager.GetCoolTime(ac.Code) : ac.Cooltime;

            //Debug.Log(string.Format("반복 횟수 : {0}",i));
            //ButtonActive(i + 1, testDifficulty[i + 1], ac.TestType, ac.Name, ac.Explain, ac.Damage, ac.DamageType, ac.Keyword, ac.Cost, ac.Cooltime, tempCool, ac.Token1Type, ac.Token2Type, ac.Token1Count, ac.Token2Count, option: 0);
            /*
            if (ac.Cooltime > 0)
            {
                //Debug.Log(string.Format("{0}의 쿨타임 :{1}", ac.Code, characterManager.GetCoolTime(ac.Code)));
                if (characterManager.GetIsCoolTime(ac.Code)) ActionCardcon.SetCoolTime(i + 1);
                if (tempCool == 0) ActionCardcon.CoolingUI(i + 1, tempCool, ac.Cooltime);
            }
            */
        }
       
        for (int i = module.EssentialAttack.Count; i < module.maxAttackTrait; i++)
        {
            if (playerInfo.CurrentModule.AttackTrait[i - module.EssentialAttack.Count] != 0)
            {
                ac = resourceManager.AttackDictionary[playerInfo.CurrentModule.AttackTrait[i - module.EssentialAttack.Count]];
                testDifficulty[i + 1] = battleManager.GetEnemyStat(ac.TestType);
                               
                //ButtonActive(i + 1, testDifficulty[i + 1], ac.TestType, ac.Name, ac.Explain, ac.Damage, ac.DamageType, ac.Keyword, ac.Cost, ac.Cooltime, tempCool, ac.Token1Type, ac.Token2Type, ac.Token1Count, ac.Token2Count, option: 0);
                //ActionCardcon.AddCard(1, i+1, ac.Name, ac.Explain, cost: ac.Cost, cooltime: ac.Cooltime , option:0);
            }
        }    
    }


    public void AttackCardCool()
    {
        nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        attackCard ac;
        int tempCool = 0;


        for (int i = 0; i < module.EssentialAttack.Count; i++)
        {
            ac = resourceManager.AttackDictionary[module.EssentialAttack[i]];
            //tempCool = characterManager.GetIsCoolTime(ac.Code) ? characterManager.GetCoolTime(ac.Code) : ac.Cooltime;
            /*
            if (ac.Cooltime > 0)
            {
                if (characterManager.GetIsCoolTime(ac.Code)) ActionCardcon.SetCoolTime(i + 1);
                if (tempCool == 0) ActionCardcon.CoolingUI(i + 1, tempCool, ac.Cooltime);
            }
            */
        }

    }
    #endregion 

    public void SetBattle(string name)
    {
        battleManager.initializeBattleSetting(name);
    }

    public void EndBattle(int essential, int bonus = 0, int extra1 = 0 , int extra2 = 0)
    {
        characterManager.AddItem(essential,1);
        if (bonus > 0) characterManager.AddItem(bonus, 1);
        if (extra1 > 0) characterManager.AddItem(extra1, 1);
        if (extra2 > 0) characterManager.AddItem(extra2, 1);
    }



   
    #endregion


    private void ChangeGameMode(GameMode gm,int code = 0, bool loaded = false)
    {
        CurGameMode = gm;
        switch (CurGameMode)
        {
           
            case GameMode.Read:
                ReadFunction(loaded);             
                break;

            case GameMode.Select:
                SelectFunction();
                break;

            case GameMode.Battle:
                BattleFunction();
                break;

            case GameMode.Cutscene:
                break;
            case GameMode.Map:
                break;
            
            case GameMode.Mini:
                break;
            case GameMode.Setup:
                break;
        }



    }

    #endregion
    #region 셀렉트 
    #region 처음 단계
    void SetSelectButton()
    {      
        int _subConnection = 0;
        int _midConnection = 0;
        int _proConnection = 0;
        
        eventManager.CurrentEvent.GetTableData(ref connection);
        eventManager.CurrentEvent.GetEventCode(ref code);
        eventManager.CurrentEvent.GetException(ref exception);
        selectActor.SelectTableOut();
        for (int i = 0; i < 4; i++)
        {            
            _subConnection = connection[i] > 0 ? i+1 : _subConnection;          
        }

        for (int i = 4; i < 8; i++)
        {
            _midConnection = connection[i] > 0 ? _midConnection+1 : _midConnection;
          
        }

        for (int i = 8; i < 12; i++)
        {
            _proConnection = connection[i] > 0 ? _proConnection +1 : _proConnection;
           
        }

       
        selectActor.positioning(_subConnection,0,_subConnection,_midConnection,_proConnection);
        selectActor.positioning(_midConnection,1,_subConnection,_midConnection,_proConnection);
        if(_proConnection > 0)selectActor.positioning(_proConnection,2,_subConnection,_midConnection,_proConnection);
        
        selectActor.SetSubLine(_subConnection,0, _subConnection, _midConnection, _proConnection);
        selectActor.SetLine(_subConnection, _midConnection, 1, _subConnection, _midConnection, _proConnection);
        if (_proConnection > 0) selectActor.SetLine(_midConnection, _proConnection, 2, _subConnection, _midConnection, _proConnection);

        selectActor.SetImage(_subConnection,0, playerInfo.ProgressData.current_Area_Level);
        selectActor.SetImage(_midConnection,4, playerInfo.ProgressData.current_Area_Level);
        if(_proConnection > 0)selectActor.SetImage(_proConnection,8, playerInfo.ProgressData.current_Area_Level);
        
    }

    #endregion

    #region 단게에 따른 버튼 활성화

    
    private void ReShowSelectPage()
    {
        int _subconnection = 0;
        int _midconnection = 0;
        int _proconnection = 0;
        
        for (int i = 0; i < connection.Length; i++)
        {
            if (i < 4)
            {
                _subconnection = connection[i] > 0 ? _subconnection + 1 : _subconnection;
                
            }
            else if (i < 8)
            {
                _midconnection = connection[i] > 0 ? _midconnection + 1 : _midconnection;
                
            }
            else if (i < 12)
            {
                _proconnection = connection[i] > 0 ? _proconnection + 1 : _proconnection;
                
            }

        }
       


        for(int i=0; i<_subconnection;i++)
        {
            selectActor.ActiveButton(true,i,0);
            //subList[i].gameObject.SetActive(true);          
        }
        for(int i=0; i<_midconnection;i++)
        {
            selectActor.ActiveButton(true, i, 1);
            //midList[i].gameObject.SetActive(true);            
        }
        for (int i = 0; i < _proconnection; i++)
        {
            selectActor.ActiveButton(true, i, 2);
            //proList[i].gameObject.SetActive(true);            
        }
        
        for(int i=0; i<4;i++)
        {
            selectActor.ActiveLine(true,i,0);
            //subLine[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < 8; i++)
        {
            selectActor.ActiveLine(true, i, 1);
            //midLine[i].gameObject.SetActive(true);            
        }

        if (_proconnection > 0) {
            for (int i =0; i<8; i++)
            {
                selectActor.ActiveLine(true, i, 2);
                // proLine[i].gameObject.SetActive(true);
            }
        }
    }

    public void ButtonSetter()
    {
        int connect = 0;

        if (_selectProgressfirst)
        {
            for (int i = 0; i < 4; i++)
            {
                if (connection[i] > 0) selectActor.LiveButton(true, i, 0);
                selectActor.LiveButton(false, i, 1);
                selectActor.LiveButton(false, i, 2);               
            }
        }
        else if (currentPosition < 4)
        {
            connect = connection[currentPosition] / 10;
            
            for (int i = 0; i < 4; i++)
            {
                selectActor.LiveButton(false, i, 0);
                selectActor.LiveButton(false, i, 2);
                selectActor.StopSequence(i,0);
                selectActor.StopSequence(i,2);
                //subList[i].StopSequence();
                //proList[i].StopSequence();
            }


            for (int j = 5; j > 1; j--)
            {

                if (connect % j == 0)
                {
                    
                    connect = connect / j;
                    selectActor.LiveButton(true, j-2, 1);                 
                    //midList[j-2].LiveButton(true);
                }
            }

        }
        else if (currentPosition < 8)
        {
            connect = connection[currentPosition] / 10;
         
            for (int i = 0; i < 4; i++)
            {
               
                //subList[i].LiveButton(false);
                //midList[i].LiveButton(false);
                selectActor.LiveButton(false, i, 0);
                selectActor.LiveButton(false, i, 1);
                selectActor.StopSequence(i, 1);
                selectActor.StopSequence(i, 2);
                //midList[i].StopSequence();
                //subList[i].StopSequence();
            }
            for (int j = 5; j > 1; j--)
            {

                if (connect % j == 0)
                {
                    connect = connect / j;
                    //Debug.Log(string.Format("currentposition :  {0}\n connect : {1}\nactivation  : {2}", currentPosition - 4,connect,j-2));
                    selectActor.LiveButton(true, j-2, 2);
                    //proList[j-2].LiveButton(true);
                }
            }

        }
        else if(currentPosition<12)
        {for (int i = 0; i < 4; i++)
            {
                //subList[i].LiveButton(false);
                //proList[i].LiveButton(false);
                //midList[i].LiveButton(false);
                selectActor.LiveButton(false, i, 0);
                selectActor.LiveButton(false, i, 1);
                selectActor.LiveButton(false, i, 2);
                selectActor.StopSequence(i, 0);
                selectActor.StopSequence(i, 1);
                selectActor.StopSequence(i, 2);
                //subList[i].StopSequence();
                //proList[i].StopSequence();
                //midList[i].StopSequence();
            }
        }

    }


    #endregion


    #region 버튼을 눌렀을 떄
    public void StageSelect(int index)
    {
        bool bisTemp = false;
        currentPosition = index;
        CurrentEventCode = code[currentPosition];
        bisTemp = exception[currentPosition];

        if (bisTemp) {
            eventManager.CurrentEvent.Excute_Event(CurrentEventCode);           
            uiManager.StartBattle(resourceManager.AD_Monsters[battleManager.GetEnemyName()][0]);            
        }
        else uiManager.burning(true);
        


        for(int i=0;i<4;i++)
        {
            selectActor.SelectButton(i, 0);
            selectActor.SelectButton(i, 1);
            selectActor.SelectButton(i, 2);                       
        }
    }

    public void FireOut(int num)
    {
        int _subconnection = 0;
        int _midconnection = 0;
        int _proconnection = 0;
        for(int i=0;i<connection.Length;i++)
        {
            if (i < 4)
            {
                _subconnection = connection[i] > 0 ? _subconnection + 1 : _subconnection; 
            } 
            else if(i < 8)
            {
                _midconnection = connection[i] > 0 ? _midconnection + 1 : _midconnection;
            }
            else if(i < 12)
            {
                _proconnection = connection[i] > 0 ? _proconnection + 1 : _proconnection;
            }

        }

        selectActor.FireOut(num,_subconnection,_midconnection,_proconnection);

        switch (num)
        {
            case 1:              
                ChangeGameMode(GameMode.Read, CurrentEventCode);
                break;          
        }

       

    }


    public void SelectDesolve()
    {        
        selectActor.SelectDesolve();
        ChangeGameMode(GameMode.Read, CurrentEventCode);
    }

  
    #endregion

    public void SetWorldMapToSelectScene(int index)
    {
        string area = "";
        switch(index)
        {
            case 2:
                area = "YenaGarden";
                break;
            case 3:
                area = "Observation";
                break;

        }


        eventManager.LoadArea(area);
        playerInfo.ProgressData.current_Area = area;
        playerInfo.ProgressData.current_Area_Level = index;
        uiManager.wolrdToSelect();
        resourceManager.ChangeArticle(area);
        _selectProgressfirst = true;
        eventManager.CurrentEvent.progress_index = 0;
        currentPosition = 0;
        ChangeGameMode(GameMode.Select);

    }


    private void SelectTableOut()
    {
             
        for(int i=0; i<4;i++)
        {
            selectActor.ResetLine(i, 0);
        }

        for(int i= 0; i< 8;i++)
        {
            selectActor.ResetLine(i, 1);
        }

        for(int i=0; i<8;i++)
        {
            selectActor.ResetLine(i, 2);
        }

    }

    #endregion
    #region 테스트와 결과 종합
    
    public bool Test(int index ,int difficulty)
    {
        refTestCard tc;
        int maxCard;
        nimrod module = null;
        module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        tc = resourceManager.TestCardDictionary[0];
        switch (testTypeArray[index])
        {
            #region 힘
            case "strength":
                //testChance = playerInfo.currentStrength;
                testChance = module.strength;
                 tc = resourceManager.TestCardDictionary[playerInfo.CurrentModule.Strength];
              
                 break;
            #endregion
            #region  예민함
            case "examine":
                testChance = module.examine;
                //testChance = playerInfo.currentExamine;
                tc = resourceManager.TestCardDictionary[playerInfo.CurrentModule.Examine];
               
                break;
            #endregion
            #region  날렵함
            case "agility":
                //testChance = playerInfo.currentAgility;
                testChance = module.agility;
                tc = resourceManager.TestCardDictionary[playerInfo.CurrentModule.Agility];

                break;
            #endregion
            #region 은밀함
            case "stealth":
                //testChance = playerInfo.currentStealth;
                testChance = module.stealth;
                tc = resourceManager.TestCardDictionary[playerInfo.CurrentModule.Stealth];
                break;
                #endregion
               
        }
        maxCard = tc.success + tc.fail + tc.hell1 + tc.hell2 + tc.hell3 + tc.over;

        for (int i = 0; i < ResultArray.Length; i++) ResultArray[i] = 2;

        for (int i = 0; i < testChance; i++)
        {
            int random = Random.Range(1, maxCard+1);
            //대성공
            
            if (random <= tc.over) ResultArray[i] = 4;
                        
            if(random <= tc.success + tc.over) //성공
            {
                if (random > tc.over) ResultArray[i] = 1;
            }

            if(random <= tc.over + tc.success + tc.fail) // 실패
            {
                if (random > tc.over + tc.success) ResultArray[i] = 2;
            }

            if(random <= tc.over+ tc.success + tc.fail + tc.hell1) // 
            {
                if (random > tc.over + tc.success + tc.fail) ResultArray[i] = 3;
            }

            if(random <= tc.over + tc.success + tc.fail + tc.hell1 + tc.hell2)
            {
                if (random > tc.over + tc.success + tc.fail + tc.hell1) ResultArray[i] = 5;
            }

            if(random <= tc.over + tc.success + tc.fail + tc.hell1 + tc.hell2 + tc.hell3)
            {
                if (random > tc.over + tc.success + tc.fail + tc.hell1 + tc.hell2) ResultArray[i] = 3;
            }                    
        }
        return calculateResult(testChance, difficulty, characterManager.CalculateResult(testTypeArray[index]));
        
    }


    

    public bool calculateResult(int chance,int difficulty , int bonus)
    {
        int compare = 0;
        for(int i=0; i<chance; i++)
        {
            //compare = (ResultArray[i] == 1) ? compare + 1 : compare;
           switch(ResultArray[i])
            {
                case 0:
                    compare += 2;
                    break;
                case 1:
                    compare += 1;
                    break;
                case 2:                    
                    break;
                case 3:
                    compare -= 1;
                    break;
                case 4:
                    compare -= 2;
                    break;
                case 5:
                    compare -= 3;
                    break;

            }
        }
        
        if (compare < 0) compare = 0;
        testPrevResult = compare;
        testAfterResult = compare + bonus;
        return difficulty <= (compare + bonus);
    }
    
    #endregion
    #region TurnLogic

    private void TurnProgress()
    {
        characterManager.AdjustTurn();
        //uiManager.HPUI(playerInfo.hp,playerInfo.mp);
    }



    #endregion
    #region 카드냈을떄

    #region 테스트 기다리기
    

    public void WaitForEnd()
    {
       
        switch(CurGameMode)
        {
            case GameMode.Read:
                eventManager.CurrentEvent.Excute_Event(testResult: testAfterResult , option:currentOption);
                textManager.ReadReadTextAsset(CurrentEventCode);
                break;

            case GameMode.Battle:
               
                switch (testCardType)
                {                                           
                    case 1:
                        //AttackCardFunction();
                        break;
                    case 2:
                        //GuardCardFunction();
                        break;
                    case 3:

                        battleManager.Monsterbehaviour(option:currentOption, result:testAfterResult);

                        textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(),battleManager.GetEnemyName());
                        break;
                }
                
                break;
        }
    
    }


    #endregion

    #region 카드 낼수 있는지 확인
    public bool CardInspection(int type, int num)
    {
        nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        int tempcode = 0;
        bool bisAvaliable = true;
        bool A_Cardinspection(int code)
        {
            currentAttackCard = resourceManager.AttackDictionary[code];

            int tokenCount = 0;
            if (currentAttackCard.Token1Count > 0)
            {
                for (int i = 0; i < playerInfo.tokenArray.Length; i++)
                {
                    if (currentAttackCard.Token1Type == playerInfo.tokenArray[i]) tokenCount += 1;
                }
                if (tokenCount < currentAttackCard.Token1Count)
                {
                    switch(currentAttackCard.Token1Type)
                    {
                        case 1:
                            uiManager.SetCardRejectionText("<color=green>초록 주화</color>가 부족하군.<?Disapear> ");
                            break;
                        case 2:
                            uiManager.SetCardRejectionText("<color=red>붉은 주화</color>가 부족하군.<?Disapear> ");
                            break;
                        case 3:
                            uiManager.SetCardRejectionText("<color=red>푸른 주화</color>가 부족하군.<?Disapear> ");
                            break;
                        case 4:
                            uiManager.SetCardRejectionText("<color=red>갈색 주화</color>가 부족하군.<?Disapear> ");
                            break;
                    }                  
                    return false;
                }
            }

            tokenCount = 0;
            if (currentAttackCard.Token2Count > 0)
            {
                for (int i = 0; i < playerInfo.tokenArray.Length; i++)
                {
                    if (currentAttackCard.Token2Type == playerInfo.tokenArray[i]) tokenCount += 1;
                }
                if (tokenCount < currentAttackCard.Token2Count)
                {
                    switch (currentAttackCard.Token2Type)
                    {
                        case 1:
                            uiManager.SetCardRejectionText("<color=green>초록 주화</color>가 부족하군.<?Disapear> ");
                            break;
                        case 2:
                            uiManager.SetCardRejectionText("<color=red>붉은 주화</color>가 부족하군.<?Disapear> ");
                            break;
                        case 3:
                            uiManager.SetCardRejectionText("<color=red>푸른 주화</color>가 부족하군.<?Disapear> ");
                            break;
                        case 4:
                            uiManager.SetCardRejectionText("<color=red>갈색 주화</color>가 부족하군.<?Disapear> ");
                            break;
                    }
                    return false;
                }
            }

            if (characterManager.GetIsCoolTime(currentAttackCard.Code))
            {
                uiManager.SetCardRejectionText("이 기술은 아직 준비되지않았다.<?Disapear> ");
                return false;
            }

            return true;

        }
        bool G_Cardinspection(int code)
        {
            currentGuardCard = resourceManager.GuardDictionary[code];

            int tokenCount = 0;
            if (currentGuardCard.Token1Count > 0)
            {
                for (int i = 0; i < playerInfo.tokenArray.Length; i++)
                {
                    if (currentGuardCard.Token1Type == playerInfo.tokenArray[i]) tokenCount += 1;
                }
                if (tokenCount < currentGuardCard.Token1Count)
                {
                    switch (currentGuardCard.Token1Type)
                    {
                        case 1:
                            uiManager.SetCardRejectionText("<color=green>초록 주화</color>가 부족하군.<?Disapear>");
                            break;
                        case 2:
                            uiManager.SetCardRejectionText("<color=red>붉은 주화</color>가 부족하군.<?Disapear>");
                            break;
                        case 3:
                            uiManager.SetCardRejectionText("<color=red>푸른 주화</color>가 부족하군.<?Disapear>");
                            break;
                        case 4:
                            uiManager.SetCardRejectionText("<color=red>갈색 주화</color>가 부족하군.<?Disapear>");
                            break;
                    }
                    return false;
                }
            }

            tokenCount = 0;
            if (currentGuardCard.Token2Count > 0)
            {
                for (int i = 0; i < playerInfo.tokenArray.Length; i++)
                {
                    if (currentGuardCard.Token2Type == playerInfo.tokenArray[i]) tokenCount += 1;
                }
                if (tokenCount < currentGuardCard.Token2Count)
                {
                    switch (currentGuardCard.Token2Type)
                    {
                        case 1:
                            uiManager.SetCardRejectionText("<color=green>초록 주화</color>가 부족하군.<?Disapear>");
                            break;
                        case 2:
                            uiManager.SetCardRejectionText("<color=red>붉은 주화</color>가 부족하군.<?Disapear>");
                            break;
                        case 3:
                            uiManager.SetCardRejectionText("<color=red>푸른 주화</color>가 부족하군.<?Disapear>");
                            break;
                        case 4:
                            uiManager.SetCardRejectionText("<color=red>갈색 주화</color>가 부족하군.<?Disapear>");
                            break;
                    }
                    return false;
                }
            }

            if (characterManager.GetIsCoolTime(currentGuardCard.Code))
            {
                uiManager.SetCardRejectionText("이 기술은 아직 준비되지않았다.<?Disapear>");
                return false;
            }

            return true;
        }
        bool I_Cardinspection(int code)
        {
            bool bisItem = false;            
            int i = 0;

            while(i< playerInfo.ItemArray.Length)
            {
                if (playerInfo.ItemArray[i].item.itemcode == code)
                {
                    return true;
                }
                i++;
            }
            uiManager.SetCardRejectionText("물건이 없어.<?Disapear> ");
            return false;
        }


        switch (type)
        {
            #region 일반 상황
            case 0:
                //bisAvaliable = bisItemArray[num] ? I_Cardinspection(ItemCodeArray[num]) : true;
                break;
            #endregion
            #region 공격
            case 1:

                if      (num == 0)                               tempcode = 0;    // 기본카드                                    
                else if (num + 1 < module.EssentialAttack.Count) tempcode = playerInfo.CurrentModule.AttackTrait[num - 1 - module.EssentialAttack.Count];  // 1더하는 이유 기본 카드 한 장이 들어가기 떄문                    
                else                                             tempcode = module.EssentialAttack[num - 1];

                bisAvaliable = A_Cardinspection(tempcode);
               

                //공                
                break;
            #endregion
            #region 방어
            case 2:

                if      (num == 0)                              tempcode = 0;    // 기본카드                                    
                else if (num + 1 < module.EssentialGuard.Count) tempcode = playerInfo.CurrentModule.GuardTrait[num - 1 - module.EssentialGuard.Count];  // 1더하는 이유 기본 카드 한 장이 들어가기 떄문                    
                else                                            tempcode = module.EssentialGuard[num - 1];

                bisAvaliable = G_Cardinspection(tempcode);


                //방                
                break;
                #endregion

        }
        return bisAvaliable;
    }

    #endregion

    public void ButtonAction(int type, int num)
    {

    
        int tempcode = 0;
       
        nimrod module = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];
        testCardType = type;
        testDifficult = testDifficulty[num];

        switch (type)
        {

            #region 일반 상황
            case 0:
                if (!bIsTestArray[num])
                {                 
                        if (bisEnd)
                        {
                            ActionCardcon.RemoveAll();

                            uiManager.FlameSecne(playerInfo.ProgressData.current_Area_Level);
                        }
                        else
                        {
                            if (eventManager.CurrentEvent.GetBisOption())
                            {
                                ActionCardcon.RemoveAll();

                                eventManager.CurrentEvent.Excute_Event(option: num);

                                //textManager.ReadReadTextAsset(CurrentEventCode);
                            }
                            else ChangeGameMode(GameMode.Select);
                        }
                }
                   
                
                else
                {

                    TestUI.gameObject.SetActive(true);
                    testResult = Test(num, testDifficulty[num]);
                    currentOption = num;


                    ActionCardcon.RemoveAll();
                    switch (testTypeArray[num])
                    {
                        #region 힘
                        case "strength":
                           
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult , playerInfo.CurrentModule.Strength, ResultArray, testResult);
                            break;
                        #endregion

                        #region 날렵함
                        case "agility":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Agility, ResultArray, testResult);
                            break;
                        #endregion

                        #region 예민함
                        case "examine":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Examine, ResultArray, testResult);
                            break;
                        #endregion

                        #region 은밀함
                        case "stealth":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Stealth, ResultArray, testResult);
                            break;
                            #endregion
                    }

                }


                break;
            #endregion
            #region 공격
            case 1:

                //if      (num == 0)                               tempcode = 0;    // 기본카드                                    
                //else if (num + 1 < module.EssentialAttack.Count) tempcode = playerInfo.CurrentModule.AttackTrait[num - 1 - module.EssentialAttack.Count];  // 1더하는 이유 기본 카드 한 장이 들어가기 떄문                    
                //else                                             tempcode = module.EssentialAttack[num - 1];
                
                CardAction(tempcode, 1, num);

                //공                
                break;

            #endregion
            #region 방어
            case 2:
                if      (num == 0)                              tempcode = 0;    // 기본카드                                    
                else if (num + 1 < module.EssentialGuard.Count) tempcode = playerInfo.CurrentModule.GuardTrait[num - 1 - module.EssentialGuard.Count];  // 1더하는 이유 기본 카드 한 장이 들어가기 떄문                    
                else                                            tempcode = module.EssentialGuard[num - 1];

                CardAction(tempcode, 2, num);
                //방

                break;
            #endregion
            #region 대응
            case 3: // 전투시 공격 대응
                ActionCardcon.RemoveAll();
                if (!bIsTestArray[num])
                {
                    
                    if (battleManager.GetbisOption())
                    {
                        
                        battleManager.Monsterbehaviour(option: num);
                        textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(), battleManager.GetEnemyName());
                    }
                    else
                    {
                        battleManager.Monsterbehaviour();
                        textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(), battleManager.GetEnemyName());
                    }
                                       
                }
                else
                {
                    TestUI.gameObject.SetActive(true);
                    testResult = Test(num, testDifficulty[num]);

                   
                    switch (testTypeArray[num])
                    {
                        #region 힘
                        case "strength":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult , playerInfo.CurrentModule.Strength, ResultArray, testResult);
                            break;
                        #endregion

                        #region 날렵함
                        case "agility":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Agility, ResultArray, testResult);
                            break;
                        #endregion

                        #region 예민함
                        case "examine":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Examine, ResultArray, testResult);
                            break;
                        #endregion

                        #region 은밀함
                        case "stealth":
                            TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Stealth, ResultArray, testResult);
                            break;
                            #endregion
                    }

                }
                break;


            #endregion
            #region 아이템
            case 4:
                //eventManager
                if (checkItem())
                {
                    quizObject.gameObject.SetActive(true);
                    quizObject.ActiveQuiz();
                    ActionCardcon.RemoveAll();
                    currentOption = num;
                }
                else uiManager.SetCardRejectionText("가진 물건이 없어.<?Disapear> ");

                break;
            #endregion
        }

      

    }


 
    #endregion

   

    #region 카드 냈을 떄 첫번쨰 분류
    private void CardAction(int code , int situation , int num)
    {

        switch(situation)
        {
            case 0:

                break;

            case 1:

                battleManager.SetCard(situation,num);
                ActionCardcon.RemoveCard(num);
               
                break;
        }

     
        if (situation == 1)
        {
            currentAttackCard = resourceManager.AttackDictionary[code];
            #region
            /*
            if (currentAttackCard.Cooltime > 0)
            {
                characterManager.A_Cooltimed(num, currentAttackCard.Code, currentAttackCard.Cooltime);
                ActionCardcon.SetCoolTime(num);
            }
            */
            /*
            if (currentAttackCard.TestType != "")
            {
                ActionCardcon.RemoveAll();
                TestUI.gameObject.SetActive(true);               
                testResult = Test(num, battleManager.GetEnemyStat(currentAttackCard.TestType));
                switch (testTypeArray[num])
                {
                    #region 힘
                    case "strength":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Strength, ResultArray, testResult);
                        break;
                    #endregion

                    #region 날렵함
                    case "agility":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Agility, ResultArray, testResult);
                        break;
                    #endregion

                    #region 예민함
                    case "examine":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Examine, ResultArray, testResult);
                        break;
                    #endregion

                    #region 은밀함
                    case "stealth":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Stealth, ResultArray, testResult);
                        break;
                        #endregion
                }
            }
            else
            {
                AttackCardFunction();
            }
            */
            #endregion
            

        }
        else
        {
            currentGuardCard = resourceManager.GuardDictionary[code];
            if (currentGuardCard.Cooltime > 0)
            {
                characterManager.A_Cooltimed(num, currentGuardCard.Code, currentGuardCard.Cooltime);
                ActionCardcon.SetCoolTime(num);
            }

            if (currentGuardCard.TestType != "")
            {
                ActionCardcon.RemoveAll();
                TestUI.gameObject.SetActive(true);
                testResult = Test(num, battleManager.GetEnemyStat(currentGuardCard.TestType));
                switch (testTypeArray[num])
                {
                    #region 힘
                    case "strength":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Strength, ResultArray, testResult);
                        break;
                    #endregion

                    #region 날렵함
                    case "agility":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Agility, ResultArray, testResult);
                        break;
                    #endregion

                    #region 예민함
                    case "examine":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Examine, ResultArray, testResult);
                        break;
                    #endregion

                    #region 은밀함
                    case "stealth":
                        TestUI.SetTestCard(testChance, 2, testDifficulty[num], testPrevResult, testAfterResult, playerInfo.CurrentModule.Stealth, ResultArray, testResult);
                        break;
                        #endregion
                }
            }
            else
            {
                GuardCardFunction();
            }

        }
           
    }




    #endregion
    #region 카드 펑션
    private void AttackCardFunction()
    {
        int tempInt = 0;
        /*
       if(currentAttackCard.Damage > 0)
            if (currentAttackCard.Type != "")
                if(testResult) battleManager.GetDamage(currentAttackCard.Damage, currentAttackCard.Key);

        */

        //characterManager.playerUseMana(currentAttackCard.Cost);
       
        switch(currentAttackCard.Code)
        {
            case 0:
                if (testResult) {
                    //battleManager.GetDamage(currentAttackCard.Damage, currentAttackCard.DamageType);
                    
                }
               
                break;

            case 1111:
               
               // battleManager.GetDamage(currentAttackCard.Damage, currentAttackCard.DamageType);
                characterManager.UseArmor(15);
                //if (playerInfo.armor > 0) characterManager.CoolTimeDecrease(1112,currentAttackCard.Cooltime); 
                break;

            case 1112:
               
                //battleManager.GetDamage(currentAttackCard.Damage, currentAttackCard.DamageType);
                characterManager.UseArmor(15);
                //if (playerInfo.armor > 0) characterManager.CoolTimeDecrease(1111, currentAttackCard.Cooltime);
                break;

            case 1113:
                tempInt = playerInfo.armor;
                if (tempInt >= 60) tempInt = 0; 
                //battleManager.GetDamage(currentAttackCard.Damage + tempInt, currentAttackCard.DamageType);
                characterManager.UseArmor(playerInfo.armor);

                break;


        }

        /*
       switch(currentAttackCard.Phase)
        {
            case 0:
                break;
            case 1:
                //Debug.Log("phase");
                ActionCardcon.RemoveAll();
                //Debug.Log("phase2");
                battleManager.Monsterbehaviour();
                //Debug.Log("phase3");
                textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(),battleManager.GetEnemyName());
                //Debug.Log("phase4");
                break;
            case 2:
                ActionCardcon.EndStunAll();
                AttackCardCool();
                break;
        }
      */

    }

    private void GuardCardFunction()
    {
        characterManager.playerUseMana(currentGuardCard.Cost);

        switch(currentGuardCard.Code)
        {
            case 1611:
                
               
                if(testResult)
                {
                    characterManager.playerGetArmor(30);
                }
                else
                {
                    characterManager.playerGetArmor(20);
                }
                break;
        }

        

        switch (currentGuardCard.Phase)
        {
            case 0:

                break;
            case 1:
               
                ActionCardcon.RemoveAll();
                battleManager.Monsterbehaviour();
                textManager.ReadMonsterAsset(battleManager.GetEnemyBehaviour(), battleManager.GetEnemyName());
                break;
            case 2:
                ActionCardcon.EndStunAll();
                GuardCardCool();
                break;
        }

    }



    #endregion
    #region 버튼 선택지 오버로드

    #region 일반 선택 확인(select 화면으로 보냄)
    public void ButtonActive(int index, string input, string input2, bool active, int option = 1, bool bisEnd = false)
    {        
        bIsTestArray[index] = false;
     
        ActionCardcon.AddCard(0,index,input,input2,  option:option);
        this.bisEnd = bisEnd;

    }
    #endregion
    #region 싸우는 중일때 선택 확인
    public void ButtonActive(int index , int type, string name, string explain)
    {
        bIsTestArray[index] = false;
     
        ActionCardcon.AddCard(3,index,name,explain,option:0);
    }

    #endregion

    #region 아이템 있으면(잠시 주석처리)

    public void ButtonActive(int index, string name, int itemcode, string itemname , string explain , int option = 1)
    {
        bIsTestArray[index] = false;
      

        for (int i = 0; i < playerInfo.ItemArray.Length; i++)
        {
            if (playerInfo.ItemArray[i].item.itemcode == itemcode)
            {
                //buttonPreText = "<color=green>" + itemname + "</color>\n" + explain;
                
                break;
            }
        }

        //ActionCardcon.AddCard(0, index, name, buttonPreText,option:option);
    
   }

    #endregion
    
    public void ButtonActive(int index, int difficulty, string abilityname, string name, string input, int option = 1)
    {
     
        bIsTestArray[index]     = true;
        testDifficulty[index]   = difficulty;
        testTypeArray[index]    = abilityname;
        string buttonPreText = "";
        switch (abilityname)
        {
            case "strength":                
                buttonPreText = string.Format("<sprite=0> <color=#FA60B2>힘 테스트\n 난이도 {0} </color>\n{1}", difficulty, input);
                break;
            case "examine":               
                buttonPreText = string.Format("<sprite=2> <color=#39A3D9> 예민함 테스트\n 난이도 {0} </color>\n{1}", difficulty, input);
                break;
            case "agility":                
                buttonPreText = string.Format("<sprite=1> <color=green> 날렵함 테스트\n 난이도 {0} </color>\n{1}", difficulty, input);
                break;
            case "stealth":               
                buttonPreText = string.Format("<sprite=3> <color=#BA2622> 은밀함 테스트\n 난이도 {0} </color>\n{1}", difficulty, input);
                break;
        }
      

        ActionCardcon.AddCard(0,index,name,buttonPreText);
      
    }

    

    public void ButtonActive(int index ,string situtext)
    {
        //quizObject.ActiveQuiz(situtext);
       

        ActionCardcon.AddCard(4,index,"물품 활용","도움이 될만한 물건을\n찾아본다.");
    }

    public void ButtonActive(int index, int value, string name, int grade, string damageType, string testType , string explain, int tokentype1, int tokentype2, int tokencount1, int tokencount2, int option =1)
    {
        string directTest = "";
        string tokenRequire1 = "";
        string tokenRequire2 = "";
        string CardExplain = "";
        string s_grade = "";
        string s_damage = "";

        switch (tokentype1)
        {
            case 1:
                tokenRequire1 = string.Format("초록 주화 사용 : {0}\n", tokencount1);
                break;
            case 2:
                tokenRequire1 = string.Format("붉은 주화 사용 : {0}\n", tokencount1);
                break;
            case 3:
                tokenRequire1 = string.Format("푸른 주화 사용 : {0}\n", tokencount1);
                break;
            case 4:
                tokenRequire1 = string.Format("갈색 주화 사용 : {0}\n", tokencount1);
                break;
        }

        switch (tokentype2)
        {
            case 1:
                tokenRequire2 = string.Format("초록 주화 사용 : {0}\n", tokencount2);
                break;
            case 2:
                tokenRequire2 = string.Format("붉은 주화 사용 : {0}\n", tokencount2);
                break;
            case 3:
                tokenRequire2 = string.Format("푸른 주화 사용 : {0}\n", tokencount2);
                break;
            case 4:
                tokenRequire2 = string.Format("갈색 주화 사용 : {0}\n", tokencount2);
                break;
        }

        switch (testType)
        {
            case "d_strength":
                bIsTestArray[index] = true;
                directTest = string.Format("<sprite=0> <color=#FA60B2>힘</color>을 시험 합니다.\n");
                testType = "strength";
                break;
            case "d_examine":
                bIsTestArray[index] = true;
                directTest = string.Format("<sprite=2> <color=#39A3D9>예민함</color>을 시험 합니다.\n");
                testType = "examine";
                break;
            case "d_agility":
                bIsTestArray[index] = true;
                directTest = string.Format("<sprite=1> <color=green>날렵함</color>을 시험 합니다.\n");
                testType = "agility";
                //buttonPreText = string.Format("<sprite=1> <color=green> 날렵함 테스트\n 난이도 {0} </color>\n{1}", difficulty, input);
                break;
            case "d_stealth":
                bIsTestArray[index] = true;
                //buttonPreText = string.Format("<sprite=3> <color=#BA2622> 은밀함 테스트\n 난이도 {0} </color>\n{1}", difficulty, input);
                directTest = string.Format("<sprite=3> <color=#BA2622>은밀함</color>을 시험 합니다.\n");
                testType = "stealth";
                break;

        }
        switch(grade)
        {
            case 0:
                s_grade = "D";
                break;
            case 1:
                s_grade = "C";
                break;
            case 2:
                s_grade = "B";
                break;
            case 3:
                s_grade = "A";
                break;
        }
        if(value > 0)
        {
            s_damage = string.Format("{0} 피해\n",value);
        }

        CardExplain = string.Format("{0}{1}{2}{3}{4}",tokenRequire1,tokenRequire2,directTest,s_damage,explain);

        ActionCardcon.AddCard(index,name,s_grade,damageType,testType,CardExplain,option);
    }


    #endregion
    
    
    #region 아이템 카드 제출시
    public void QuizAction(string slot, int index)
    {

        ActionCardcon.RemoveQuizAll();
        switch (slot)
        {
            case "Keep":
                quizObject.ActiveCorrection(3, itemQuizArray[index].ItemName);
                CurrentItem = itemQuizArray[index];
                break;

            case "Wheel":
                quizObject.ActiveCorrection(2, itemQuizArray[index].ItemName);
                break;

            case "Moon":
                quizObject.ActiveCorrection(0,itemQuizArray[index].ItemName);
                CurrentItem = itemQuizArray[index];
                break;

            case "Trash":
                quizObject.ActiveCorrection(1, itemQuizArray[index].ItemName);
                CurrentItem = itemQuizArray[index];
                break;
        }
    }
    
    public int QuizAction(int stateNum)
    {
        int answer = 0;
        switch(stateNum)
        {
            case 0:               
                answer = eventManager.CurrentEvent.compareItemQuiz(CurrentItem.specify1,CurrentItem.specify2,(CurrentItem.itemcode/1000));
               
                break;
        }


        return answer;
    }

    public void QuizAction()
    {
        int result = 0;
        result = eventManager.CurrentEvent.compareItemQuiz(CurrentItem.specify1, CurrentItem.specify2, (CurrentItem.itemcode / 1000));
        eventManager.CurrentEvent.Excute_Event(testResult: result,option:currentOption);

    }
   

    #endregion

    private bool checkItem()
    {
        bool bisExist = false;
        for (int i = 0; i < playerInfo.ItemArray.Length; i++)
        {
            if (playerInfo.ItemArray[i].item.type == Item.ItemType.Consumable)
            {
                bisExist = true;
                break;
            }
        }
        return bisExist;
    }

    public void SetQuiz()
    {
        List<int> IndexList = new List<int>();
        List<Item> ItemList = new List<Item>();
        int itemlackCount = 0;

        for(int i=0;i<playerInfo.ItemArray.Length;i++)
        {
            if(playerInfo.ItemArray[i].item.type == Item.ItemType.Consumable)
            {
                IndexList.Add(i);               
            }
        }

        itemlackCount = IndexList.Count;
        if (itemlackCount > 4) itemlackCount = 4;       

        System.Random rng = new System.Random();
        int n = IndexList.Count;
        int end = n;
        while (n > 0)
        {
            n--;
            int k = rng.Next(0, end);
            int value = IndexList[k];
            IndexList[k] = IndexList[n];
            IndexList[n] = value;
        }
       
       
        for (int i = 0; i < itemlackCount; i++)
        {
           ItemList.Add(playerInfo.ItemArray[IndexList[i]].item);
            itemQuizArray[i] = ItemList[i];
           ActionCardcon.AddQuizSlot(i, ItemList[i]);
        }
        

    }

    public void battleVictory()
    {
        
    }


    public void StartMode(bool start = false)
    {
        uiManager.bookObject.gameObject.SetActive(false);
        if (start) playerInfo.ProgressData.bisPrologue = false;
        if (playerInfo.ProgressData.bisPrologue)
        {
            uiManager.Prologues("PrologueStart");
        }
        else
        {
            //eventManager.LoadArea(playerInfo.ProgressData.current_Area);
            // ChangeGameMode(playerInfo.ProgressData.gameMode,playerInfo.ProgressData.Current_EventCode,true);

            
            //eventManager.LoadArea("Tutorial");
            //playerInfo.ProgressData.current_Area = "Tutorial";
            //playerInfo.ProgressData.current_Area_Level = 1;
            //resourceManager.ChangeArticle("Tutorial");
            

            /*
            eventManager.LoadArea("YenaGarden");
            playerInfo.ProgressData.current_Area = "YenaGarden";
            playerInfo.ProgressData.current_Area_Level = 2;
            resourceManager.ChangeArticle("YenaGarden");
            */

            
            eventManager.LoadArea("Observation");
            playerInfo.ProgressData.current_Area = "Observation";
            playerInfo.ProgressData.current_Area_Level = 3;
            resourceManager.ChangeArticle("Observation");
            uiManager.prologue.gameObject.SetActive(false);
            
          


            //uiManager.prologue.gameObject.SetActive(false);
            characterManager.AdjustDiseaseSlot();
            //TestUI.GetResultData();
            TestUI.gameObject.SetActive(false);
            uiManager.HPUI(playerInfo.hp, playerInfo.mp);
            characterManager.InitializeTrait();
            ChangeGameMode(GameMode.Select);
            
        }
    }

    #region 강제 eventcode변경

    public void ForceChangeEventCode(int code)
    {
        CurrentEventCode = code;
        textManager.ReadReadTextAsset(code);
        
    }

    #endregion

}
