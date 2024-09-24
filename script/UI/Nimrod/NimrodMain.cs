using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimrodMain : MonoBehaviour
{

    #region Choo Seok
    //���� �ּ� �ȴ޸� �ڻ���
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
        #region ����
        /*         
         �� ����� �����ܰ� ī�� �̹���, �׸��� ������ ǥ�����ִ� �Լ� �Դϴ�.        
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

        #region ����
        /*
         
         ���� ���� ui�� Ȱ��ȭ��Ű�� 
         ���� ���� �� �Լ��� ����Ǿ� 
         ����� ������ ���� ui�� �����մϴ�.  

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
         ��� ����(Ȱ��ȭ �����ʰ� �κ��丮���� ����ϰ� �ִ� ��)�� �巡�� �ؼ�
         ����ĭ�� �־��� �� Ȱ��ȭ �Ǵ� �Լ��Դϴ�.
         ù��° ���ڴ� ���õǾ� Ȱ��ȭ �� ��� ������ �ε���
         �ι�° ���ڴ� ����Ǿ� ��Ȱ��ȭ�� ��� ������ �ε��� �Դϴ�.
      
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
        // ��Ȱ��ȭ �� ����� �ʼ� �нú긦 ��� ���� ��ŵ�ϴ�.


        for(int i=0; i <playerInfo.CurrentModule.PassiveTrait.Length;i++)
        {
            if(playerInfo.CurrentModule.PassiveTrait[i] != 0)
            {
                pc = resourceManager.PassiveDictionary[playerInfo.CurrentModule.PassiveTrait[i]];
                characterManager.DisActiveTrait( pc.code);
            }
        }
        // ��Ȱ��ȭ �� ����� ���� �нú긦 ��� ���� ��ŵ�ϴ�.


        characterManager.CooltimeStopper(changedModule);
        // ����� ��Ȱ��ȭ ��Ű�� �� ����� ������� ��Ÿ���� �帣�� �ʰ� �մϴ�. 


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
        // ����� ������ ui�� ǥ���ϴ� �κ��Դϴ�.


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
         Ȱ��ȭ�� 6���� ��� �� ���ڷ� ���� ���� currentmodule�� ����մϴ�.  
         */
        #endregion
        passiveCard pc;        
        nimrod nim = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];

        for (int i=0; i< nim.EssentialPassive.Count ;i++)
        {
            pc = resourceManager.PassiveDictionary[nim.EssentialPassive[i]];
            characterManager.DisActiveTrait(pc.code);
        }
        //�ʼ� �нú� ����

        for(int i=0;i<playerInfo.CurrentModule.PassiveTrait.Length;i++)
        {
            if (playerInfo.CurrentModule.PassiveTrait[i] != 0)
            {
                pc = resourceManager.PassiveDictionary[playerInfo.CurrentModule.PassiveTrait[i]];
                characterManager.DisActiveTrait(pc.code);
            }
        }
        //���� �нú� ����

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
        // currentmodule ����

        nimrod currentModule = resourceManager.NimrodDictionary[playerInfo.CurrentModule.module];

        uiManager.AdjustNimrodInfo(resourceManager.I_NimrodDictionary[currentModule.code], currentModule.Name);
        //���� ui���� �����ܰ� �̸��� �����մϴ�.

        for (int i = 0; i < nim.EssentialPassive.Count; i++)
        {            
            pc = resourceManager.PassiveDictionary[nim.EssentialPassive[i]];            
            characterManager.ActiveTrait( pc.code);
        }  //�ʼ� �нú� Ȱ��

        for (int i = 0; i < playerInfo.CurrentModule.PassiveTrait.Length; i++)
        {
            if (playerInfo.CurrentModule.PassiveTrait[i] != 0)
            {
                pc = resourceManager.PassiveDictionary[playerInfo.CurrentModule.PassiveTrait[i]];
                characterManager.ActiveTrait( pc.code);
            }
        }  //���� �нú� ��Ȱ��

        characterManager.ReCalculateDiseaseDamage();
        // Ư���� ���� ����� ���� ������� �ٽ� ����մϴ�.
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