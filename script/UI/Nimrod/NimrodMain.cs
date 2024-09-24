using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimrodMain : MonoBehaviour
{

    #region Choo Seok
    //이제 주석 안달면 자살함
    #endregion

    [SerializeField] private List<MainModule> Modules = new List<MainModule>();
    [SerializeField] private Transform moduleSlotParent;
    [SerializeField] private GameObject Parent;
    private List<ModuleSlot> moduleSlots = new List<ModuleSlot>();

    #region Component
   

    private PlayerInfo playerInfo;
    private ResourceManager resourceManager;
    private CharacterManager characterManager;
    private UIManager uiManager;

    [SerializeField] private NimrodDetail detail;
    private Canvas canvas;

   

    #endregion
    private bool bisFirst = true;

    private void Awake()
    {
        
       
        foreach(Transform tr in moduleSlotParent)
        {
            int index = 0;
            if(tr.gameObject.CompareTag("ModuleDragSlot"))
            {
               ModuleSlot ms = tr.GetComponent<ModuleSlot>();
                ms.slotnum = index;
                index += 1;

                moduleSlots.Add(ms);

            }
        }


    }

    private void Start()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        resourceManager.GetPlayerInfo(out playerInfo);
        uiManager = GameManager.GetManagerClass<UIManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager.nimrodMain = this;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        for(int i =0; i<moduleSlots.Count;i++)
        {
            moduleSlots[i].canvas = canvas;
        }
        for (int i=0;i<Modules.Count;i++)
        {
            Modules[i].main = this;
        }

        
        //detail.gameObject.SetActive(false);
        //Parent.SetActive(false);
    }


    public void GetPlayerData(PlayerInfo info)
    {
        this.playerInfo = info;
    }

    private void InitializeModule(PlayerInfo.CardModule module , int index)
    {
        #region 설명
        /*         
         각 모듈의 아이콘과 카드 이미지, 그리고 스탯을 표시해주는 함수 입니다.        
        */
        #endregion


        Sprite Str;
        Sprite Agi;
        Sprite Exa;
        Sprite Ste;
        Sprite Icon;

        string power;
        string fast;
        string eye;
        string hide;
        string name;

        Str = resourceManager.I_CardBackDictionary[module.Strength];
        Agi = resourceManager.I_CardBackDictionary[module.Agility];
        Exa = resourceManager.I_CardBackDictionary[module.Examine];
        Ste = resourceManager.I_CardBackDictionary[module.Stealth];
        Icon = resourceManager.I_NimrodDictionary[module.module];

        power = playerInfo.StatArray[index].strength.ToString();
        fast  = playerInfo.StatArray[index].agility.ToString();
        eye   = playerInfo.StatArray[index].examine.ToString();
        hide  = playerInfo.StatArray[index].stealth.ToString();
        name = resourceManager.NimrodDictionary[module.module].Name;
      
        Modules[index].InitializeImage(Str,Agi,Exa,Ste,Icon);
        Modules[index].InitializeStat(power, fast, eye, hide, name);
        
    }


    public void InitialieModules()
    {

        #region 설명
        /*
         
         스탯 변경 ui를 활성화시키면 
         가장 먼저 이 함수가 실행되어 
         저장된 정보에 따라 ui를 구성합니다.  

        */

        #endregion

        InitializeModule(playerInfo.CardModule1, 0);
        InitializeModule(playerInfo.CardModule2, 1);
        InitializeModule(playerInfo.CardModule3, 2);
        InitializeModule(playerInfo.CardModule4, 3);
        InitializeModule(playerInfo.CardModule5, 4);
        InitializeModule(playerInfo.CardModule6, 5);

        if(bisFirst)
        {
            for(int i=0; i<moduleSlots.Count;i++)
            {
                moduleSlots[i].nimrodMain = this;
                moduleSlots[i].empty = resourceManager.I_NimrodDictionary[0];
                moduleSlots[i].canvas = this.canvas;
            }
            bisFirst = false;
        }

        for(int i = 0; i < moduleSlots.Count; i++)
        {if(playerInfo.ModuleArray[i] > 1)
            moduleSlots[i].InitializeSlot(true, resourceManager.I_NimrodDictionary[playerInfo.ModuleArray[i]]);
        }

    }


    public void changeModule(int selectedModule, int changedModule)
    {

        #region 
        /*
         모듈 슬롯(활성화 되지않고 인벤토리에서 대기하고 있는 것)을 드래그 해서
         착용칸에 넣었을 때 활성화 되는 함수입니다.
         첫번째 인자는 선택되어 활성화 될 모듈 슬롯의 인덱스
         두번째 인자는 변경되어 비활성화될 모듈 슬롯의 인덱스 입니다.
      
         */
        #endregion


        nimrod currentModule = resourceManager.NimrodDictionary[playerInfo.ModuleArray[selectedModule]];
        nimrod prevModule = null;
        passiveCard pc;
        
        switch(changedModule)
        {
            case 0:
                prevModule = resourceManager.NimrodDictionary[playerInfo.CardModule1.module];
                break;
            case 1:
                prevModule = resourceManager.NimrodDictionary[playerInfo.CardModule2.module];
                break;
            case 2:
                prevModule = resourceManager.NimrodDictionary[playerInfo.CardModule3.module];
                break;
            case 3:
                prevModule = resourceManager.NimrodDictionary[playerInfo.CardModule4.module];
                break;
            case 4:
                prevModule = resourceManager.NimrodDictionary[playerInfo.CardModule5.module];
                break;
            case 5:
                prevModule = resourceManager.NimrodDictionary[playerInfo.CardModule6.module];
                break;
            
        }



        for (int i=0; i<prevModule.EssentialPassive.Count;i++)
        {
            pc = resourceManager.PassiveDictionary[prevModule.EssentialPassive[i]];
            characterManager.DisActiveTrait( pc.code);
            
        }
        // 비활성화 될 모듈의 필수 패시브를 모두 종료 시킵니다.


        for(int i=0; i <playerInfo.CurrentModule.PassiveTrait.Length;i++)
        {
            if(playerInfo.CurrentModule.PassiveTrait[i] != 0)
            {
                pc = resourceManager.PassiveDictionary[playerInfo.CurrentModule.PassiveTrait[i]];
                characterManager.DisActiveTrait( pc.code);
            }
        }
        // 비활성화 될 모듈의 선택 패시브를 모두 종료 시킵니다.


        characterManager.CooltimeStopper(changedModule);
        // 모듈을 비활성화 시키면 그 모듈의 기술들의 쿨타임이 흐르지 않게 합니다. 


        switch (changedModule)
        {
            case 0:
                
                AdjustModuleInfo(playerInfo.CardModule1, currentModule);                
                InitializeModule(playerInfo.CardModule1, 0);
                if(playerInfo.CurrentModule == playerInfo.CardModule1) uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);

                break;
            case 1:
               
                AdjustModuleInfo(playerInfo.CardModule2, currentModule);               
                InitializeModule(playerInfo.CardModule2, 1);
                if (playerInfo.CurrentModule == playerInfo.CardModule2) uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);

                break;
            case 2:
               
                AdjustModuleInfo(playerInfo.CardModule3, currentModule);               
                InitializeModule(playerInfo.CardModule3, 2);
                if (playerInfo.CurrentModule == playerInfo.CardModule3) uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);

                break;
            case 3:
               
                AdjustModuleInfo(playerInfo.CardModule4, currentModule);                
                InitializeModule(playerInfo.CardModule4, 3);
                if (playerInfo.CurrentModule == playerInfo.CardModule4) uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);

                break;
            case 4:
               
                AdjustModuleInfo(playerInfo.CardModule5, currentModule);              
                InitializeModule(playerInfo.CardModule5, 4);
                if (playerInfo.CurrentModule == playerInfo.CardModule5) uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);

                break;
            case 5:
               
                AdjustModuleInfo(playerInfo.CardModule6, currentModule);                
                InitializeModule(playerInfo.CardModule6, 5);
                if (playerInfo.CurrentModule == playerInfo.CardModule6) uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);

                break;
        }
        // 변경된 정보를 ui에 표시하는 부분입니다.


        for (int i = 0; i < currentModule.EssentialAttack.Count; i++)
        {
            //if (resourceManager.AttackDictionary[currentModule.EssentialAttack[i]].Cooltime > 0) characterManager.A_CooltimeReset(changedModule, i, currentModule.EssentialAttack[i]);
        }

        for (int i = 0; i < currentModule.EssentialPassive.Count; i++)
        {
            pc = resourceManager.PassiveDictionary[currentModule.EssentialPassive[i]];
            characterManager.ActiveTrait( pc.code);
          
        }

        if (prevModule.code == 1 || prevModule.code == 0)
        {
            
            playerInfo.ModuleArray[selectedModule] = 0;
            moduleSlots[selectedModule].InitializeSlot(true,resourceManager.I_NimrodDictionary[0]);
            moduleSlots[selectedModule].InitializeSlot(false);
          
        }
        else
        {
            Debug.Log(string.Format("bb : {0}",selectedModule));
            playerInfo.ModuleArray[selectedModule] = prevModule.code;
           moduleSlots[selectedModule].InitializeSlot(true,resourceManager.I_NimrodDictionary[prevModule.code]);
            
          
        }
        
       
    }

    public void changeModule(int index)
    {
        #region 
        /*
         활성화된 6개의 모듈 중 인자로 받은 것을 currentmodule로 사용합니다.  
         */
        #endregion
        passiveCard pc;        
        nimrod nim = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];

        for (int i=0; i< nim.EssentialPassive.Count ;i++)
        {
            pc = resourceManager.PassiveDictionary[nim.EssentialPassive[i]];
            characterManager.DisActiveTrait(pc.code);
        }
        //필수 패시브 종료

        for(int i=0;i<playerInfo.CurrentModule.PassiveTrait.Length;i++)
        {
            if (playerInfo.CurrentModule.PassiveTrait[i] != 0)
            {
                pc = resourceManager.PassiveDictionary[playerInfo.CurrentModule.PassiveTrait[i]];
                characterManager.DisActiveTrait(pc.code);
            }
        }
        //선택 패시브 종료

        switch (index)
        {
            case 0:
                playerInfo.CurrentModule = playerInfo.CardModule1;               
                break;
            case 1:
                playerInfo.CurrentModule = playerInfo.CardModule2;
                break;
            case 2:
                playerInfo.CurrentModule = playerInfo.CardModule3;
                break;
            case 3:
                playerInfo.CurrentModule = playerInfo.CardModule4;
                break;
            case 4:
                playerInfo.CurrentModule = playerInfo.CardModule5;
                break;
            case 5:
                playerInfo.CurrentModule = playerInfo.CardModule6;
                break;
        }
        // currentmodule 변경

        nimrod currentModule = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];

        uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);
        //명함 ui에서 아이콘과 이름을 변경합니다.

        for (int i = 0; i < nim.EssentialPassive.Count; i++)
        {            
            pc = resourceManager.PassiveDictionary[nim.EssentialPassive[i]];            
            characterManager.ActiveTrait( pc.code);
        }  //필수 패시브 활성

        for (int i = 0; i < playerInfo.CurrentModule.PassiveTrait.Length; i++)
        {
            if (playerInfo.CurrentModule.PassiveTrait[i] != 0)
            {
                pc = resourceManager.PassiveDictionary[playerInfo.CurrentModule.PassiveTrait[i]];
                characterManager.ActiveTrait( pc.code);
            }
        }  //선택 패시브 비활성

        characterManager.ReCalculateDiseaseDamage();
        // 특성에 따라 변경된 질병 대미지를 다시 계산합니다.
    }

    public void InitializeNimrodChanger()
    {
        int selected = 5;
        Sprite resSprite;
        bool bisIn = false;
       
        if (playerInfo.CurrentModule == playerInfo.CardModule1) selected = 0;
        else if (playerInfo.CurrentModule == playerInfo.CardModule2) selected = 1;
        else if (playerInfo.CurrentModule == playerInfo.CardModule3) selected = 2;
        else if (playerInfo.CurrentModule == playerInfo.CardModule4) selected = 3;
        else if (playerInfo.CurrentModule == playerInfo.CardModule5) selected = 4;
        else if (playerInfo.CurrentModule == playerInfo.CardModule6) selected = 5;

        void manager(int index)
        {            
            switch(index)
            {
                case 0:
                    resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule1.module];
                    bisIn = playerInfo.CardModule1.module > 0;
                    break;
                case 1:
                    resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule2.module];
                    bisIn = playerInfo.CardModule2.module > 0;
                    break;
                case 2:
                    resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule3.module];
                    bisIn = playerInfo.CardModule3.module > 0;
                    break;
                case 3:
                    resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule4.module];
                    bisIn = playerInfo.CardModule4.module > 0;
                    break;
                case 4:
                    resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule5.module];
                    bisIn = playerInfo.CardModule5.module > 0;
                    break;
                case 5:
                    resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule6.module];
                    bisIn = playerInfo.CardModule6.module > 0;
                    break;
                default:
                    resSprite = resourceManager.I_NimrodDictionary[0];
                    break;
            }
            uiManager.InitializeNimrodChanger(index,resSprite," ",bisIn);
        }

        for (int i = 0; i < 6; i++) uiManager.InitializeNimrodChanger(i, resourceManager.I_NimrodDictionary[0], " ");

       for(int i=0;i<6;i++)
        {   if (i == selected) uiManager.InitializeNimrodChanger(i,true);
            manager(i);
        }
        switch (selected)
        {
            case 0:
                resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule1.module];
                break;
            case 1:
                resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule2.module];
                break;
            case 2:
                resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule3.module];
                break;
            case 3:
                resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule4.module];
                break;
            case 4:
                resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule5.module];
                break;
            case 5:
                resSprite = resourceManager.I_NimrodDictionary[playerInfo.CardModule6.module];
                break;
            default:
                resSprite = resourceManager.I_NimrodDictionary[0];
                break;
        }
        uiManager.InitializeNimrodChanger(resSprite," ");        
    }



    private void AdjustModuleInfo(PlayerInfo.CardModule module ,nimrod nim)
    {
        module.module = nim.code;
        module.Strength = 0;
        module.Agility = 0;
        module.Examine = 0;
        module.Stealth = 0;        
    }

    public void ChangeToDetail(int index)
    {
        PlayerInfo.CardModule temp = playerInfo.CardModule1; ;
        switch(index)
        {
            case 0:
                temp = playerInfo.CardModule1;
                break;
            case 1:
                temp = playerInfo.CardModule2;
                break;
            case 2:
                temp = playerInfo.CardModule3;
                break;
            case 3:
                temp = playerInfo.CardModule4;
                break;
            case 4:
                temp = playerInfo.CardModule5;
                break;
            case 5:
                temp = playerInfo.CardModule6;
                break;
        }
        if (temp.module == 0) return;

        detail.gameObject.SetActive(true);
        detail.InitializeModule(index);
        gameObject.SetActive(false);
    }

    public void Temp()
    {
        Parent.SetActive(false);
    }


    public void ActiveNimrod()
    {
        Parent.SetActive(true);
        detail.gameObject.SetActive(false);
        gameObject.SetActive(true);
        InitialieModules();
    }

    public void DisActivenimrod()
    {
        Parent.SetActive(false);
        detail.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}