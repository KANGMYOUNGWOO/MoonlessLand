using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NimrodDetail : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI ModuleName;
   private Canvas canvas;

    [SerializeField] private GameObject Test;
    [SerializeField] private GameObject Trait;

    [SerializeField] private List<Button> TraitButtons = new List<Button>();
    [SerializeField] private List<Button> ModeButtons = new List<Button>();
    [SerializeField] private GameObject TraitNext;


    [SerializeField] private List<SemiActionCard> Attacks = new List<SemiActionCard>();
    [SerializeField] private List<SemiActionCard> Guards = new List<SemiActionCard>();
    [SerializeField] private List<SemiActionCard> Passive = new List<SemiActionCard>();
    [SerializeField] private List<ActionTraitCard> TraitCards = new List<ActionTraitCard>();

    [SerializeField] private List<TestCardInfo> TestCardInfos = new List<TestCardInfo>();


    private List<NimrodTestCard> TestCards = new List<NimrodTestCard>();
    private ObjectPool<NimrodTestCard> _TestcardPool = null;
    

    [SerializeField] private NimrodTestCard Sample;

    [SerializeField] private NimrodMain nimrodMain;
    [SerializeField] private Transform CardFrame;

    private PlayerInfo playerinfo;
    private ResourceManager resourceManager = null; 



    private PlayerInfo.CardModule selectedModule;

    private enum CardType {Attack,Guard,Passive }
    public enum Mode {Trait, Test }
    CardType CurrentType = CardType.Attack;
    public Mode CurrentMode = Mode.Trait;

   

    private delegate void adjustSlot(int index);
    adjustSlot RemoveSlot;
    
    

    public void GetPlayerData(PlayerInfo info)
    {
        playerinfo = info;
    }


    private void Awake()
    {
        
         for(int i=0; i<TraitCards.Count;i++)
        {
            TraitCards[i].canvas = canvas;
        }
        Test.SetActive(false);
    }

    private void Start()
    {
        _TestcardPool = new ObjectPool<NimrodTestCard>();
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        resourceManager.GetPlayerInfo(out playerinfo);
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }


    #region InitializeTestCard

    private void InitializeTestCard()
    {
        Sprite s_testCard;
        //string s_percentage;
        //refTestCard testCard;

        string explain = "";

        void AddExplain(string ex, int maxnum)
        {
            explain += ex;
            for (int i = 0; i < maxnum; i++)
            {
                explain += "<sprite=4>";
            }
            explain += "\n\n";
        }

        void instanceFunction(int index, int stat)
        {
            explain = "";
            Debug.Log(stat);
            s_testCard = resourceManager.I_CardBackDictionary[stat];
            refTestCard testCard = resourceManager.TestCardDictionary[stat];
            AddExplain("<sprite=5>  ", testCard.over);
            AddExplain("<sprite=6>  ", testCard.success);
            AddExplain("<sprite=7>  ", testCard.fail);
            AddExplain("<sprite=8>  ", testCard.hell1);
            AddExplain("<sprite=9>  ", testCard.hell2);

            TestCardInfos[index].InitializeInfo(s_testCard, explain, "2");
           
        }

        instanceFunction(0, selectedModule.Strength);
        instanceFunction(1, selectedModule.Agility);
        instanceFunction(2, selectedModule.Examine);
        instanceFunction(3, selectedModule.Stealth);

        for (int i = 0; i < playerinfo.TestCardList.Count; i++)
        {

            NimrodTestCard newCard = _TestcardPool.GetRecyclableObject() ??
                _TestcardPool.RegisterRecyclableObject(Instantiate(Sample));

            Sprite testSprite = resourceManager.I_CardBackDictionary[playerinfo.TestCardList[i]];

            newCard.initializeTestCard(testSprite, canvas, i);

            if (newCard.gameObject.activeSelf)
            {
                newCard.isActive = true;
                RemoveSlot += newCard.AdjustSlotnum;
            }
            else
                newCard.gameObject.SetActive(newCard.isActive = true);

           
            newCard.transform.parent = CardFrame;
            newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            newCard.nimrodDetail = this;
        }




    }


    #endregion

    #region 카드 바꾸기
    public void ChangeAttackCard(int prev, int after)
    {
        nimrod nim = resourceManager.NimrodDictionary[selectedModule.module];

        if (nim.EssentialAttack.Count > prev)
        {
            TraitCards[after].ChangeFail();           
        }
        else if (nim.maxAttackTrait < prev) TraitCards[after].ChangeFail();
        else
        {
            TraitCards[after].ChangeSuccess();
            attackCard ACard =
            resourceManager.AttackDictionary[playerinfo.AttackCardList[after]];
            //Attacks[prev].InitializeCard(true, true, ACard.Cost, ACard.Cooltime, ACard.Name, ACard.Explain);
        }
    }

    public void ChangeGuardCard(int prev, int after)
    {
        nimrod nim = resourceManager.NimrodDictionary[selectedModule.module];
        if (nim.EssentialGuard.Count > prev)
        {
            TraitCards[after].ChangeFail();          
        }
        else if (nim.maxGuardTrait < prev) TraitCards[after].ChangeFail();
        else
        {
            TraitCards[after].ChangeSuccess();
            guardCard GCard =
            resourceManager.GuardDictionary[playerinfo.GuardCardList[after]];
            Guards[prev].InitializeCard(true, true, GCard.Cost, GCard.Cooltime, GCard.Name, GCard.Explain);
        }
    }

    public void ChangePassiveCard(int prev, int after)
    {
        nimrod nim = resourceManager.NimrodDictionary[selectedModule.module];
        if (nim.EssentialPassive.Count > prev)
        {
            TraitCards[after].ChangeFail();          
        }
        else if (nim.maxPassiveTrait < prev) TraitCards[after].ChangeFail();
        else
        {
            TraitCards[after].ChangeSuccess();
            passiveCard PCard =
            resourceManager.PassiveDictionary[playerinfo.PassiveCardList[after]];
            Passive[prev].InitializeCard(true, false, PCard.Name, PCard.Explain);
        }
    }
    #endregion

    #region 모듈 세팅
    private void SetModules(PlayerInfo.CardModule cardModule)
    {
        nimrod nim;
        attackCard  ACard;
        guardCard   GCard;
        passiveCard PCard;

        

        selectedModule = cardModule;
        nim = resourceManager.NimrodDictionary[cardModule.module];
        Icon.sprite = resourceManager.I_NimrodDictionary[cardModule.module];
        ModuleName.text = nim.Name;


        for (int i = 0; i < 5; i++)
        {
            Attacks[i].InitializeCard(false, resourceManager.I_ActionCardDictionary["empty"]);
             Guards[i].InitializeCard(false, resourceManager.I_ActionCardDictionary["empty"]);          
        }

        for(int i=0; i<10; i++)
        {
            Passive[i].InitializeCard(false, resourceManager.I_ActionCardDictionary["empty"]);
        }



        for (int i = 0; i < nim.EssentialAttack.Count; i++)
        {           
            ACard = resourceManager.AttackDictionary[nim.EssentialAttack[i]]; 
            //Attacks[i].InitializeCard(true, true, ACard.Cost, ACard.Cooltime, ACard.Name, ACard.Explain);
        }

        
        for(int i=nim.EssentialAttack.Count; i<nim.maxAttackTrait; i++)
        {
            
            ACard = resourceManager.AttackDictionary[cardModule.AttackTrait[i - nim.EssentialAttack.Count]];
            //if (ACard.Code != 0)
            //Attacks[i].InitializeCard(true, false, ACard.Cost, ACard.Cooltime, ACard.Name, ACard.Explain);
        }

        for (int i = nim.maxAttackTrait; i < 5; i++)
        {
            Attacks[i].InitializeCard(false, resourceManager.I_ActionCardDictionary["locked"]);
        }

        for (int i = 0; i < nim.EssentialGuard.Count; i++)
        {
            
            GCard = resourceManager.GuardDictionary[nim.EssentialGuard[i]];
            Guards[i].InitializeCard(true, true, GCard.Cost, GCard.Cooltime, GCard.Name, GCard.Explain);
        }

        for (int i = nim.EssentialGuard.Count; i < nim.maxGuardTrait; i++)
        {
           
            GCard = resourceManager.GuardDictionary[cardModule.GuardTrait[i - nim.EssentialGuard.Count]];
            if(GCard.Code != 0)
            Guards[i].InitializeCard(true, false, GCard.Cost, GCard.Cooltime, GCard.Name, GCard.Explain);
        }

        for (int i = nim.maxGuardTrait; i < 5; i++)
        {
           Guards[i].InitializeCard(false, resourceManager.I_ActionCardDictionary["locked"]);
        }

        for (int i = 0; i < nim.EssentialPassive.Count; i++)
        {
            PCard = resourceManager.PassiveDictionary[nim.EssentialPassive[i]];
            Passive[i].InitializeCard(true, true, PCard.Name, PCard.Explain);
            
        }

        for (int i = nim.EssentialPassive.Count; i < nim.maxPassiveTrait; i++)
        {
            PCard = resourceManager.PassiveDictionary[cardModule.PassiveTrait[i - nim.EssentialPassive.Count]];
            if(PCard.code != 0)
            Passive[i].InitializeCard(true, false, PCard.Name, PCard.Explain);
        }


       
        for (int i = nim.maxPassiveTrait; i < 10; i++)
        {
            Passive[i].InitializeCard(false, resourceManager.I_ActionCardDictionary["locked"]);
        }
        
        InitializeCard();

    }
    #endregion

    #region 모듈 스위치
    
    public void InitializeModule(int index)
    {
       
   
        switch (index)
        {
            case 0:
                SetModules(playerinfo.CardModule1);
                break;

            case 1:
                SetModules(playerinfo.CardModule2);
                break;

            case 2:
                SetModules(playerinfo.CardModule3);
                break;

            case 3:
                SetModules(playerinfo.CardModule4);
                break;

            case 4:
                SetModules(playerinfo.CardModule5);
                break;

            case 5:
                SetModules(playerinfo.CardModule6);
                break;
        }

    }
    #endregion

    #region 카드 세팅
    public void InitializeCard()
    {
        attackCard ACard;
        guardCard GCard;
        passiveCard PCard;

        Sprite s_card;
        Sprite s_cost;

        for(int i=0;i<TraitCards.Count;i++)
        {
            TraitCards[i].InitializeCard(resourceManager.I_ActionCardDictionary["empty"]);
            TraitCards[i].bisEmpty = true;
        }

        switch (CurrentType)
        {
            case CardType.Attack:

                for(int i=0; i<playerinfo.AttackCardList.Count; i++)
                {
                    s_card = resourceManager.I_ActionCardDictionary["attack"];
                    s_cost = resourceManager.I_ActionCardDictionary["attackCost"];
                    ACard = resourceManager.AttackDictionary[playerinfo.AttackCardList[i]];
                   // TraitCards[i].InitializeCard(ACard.Name,ACard.Explain,ACard.Cost,ACard.Cooltime,s_card,s_cost);
                    TraitCards[i].bisEmpty = false;
                }
                break;

            case CardType.Guard:
                for(int i=0; i<playerinfo.GuardCardList.Count; i++ )
                {
                    s_card = resourceManager.I_ActionCardDictionary["guard"];
                    s_cost = resourceManager.I_ActionCardDictionary["guardCost"];
                    GCard = resourceManager.GuardDictionary[playerinfo.GuardCardList[i]];
                    TraitCards[i].InitializeCard(GCard.Name, GCard.Explain, GCard.Cost, GCard.Cooltime, s_card, s_cost);
                    TraitCards[i].bisEmpty = false;
                }
                break;

            case CardType.Passive:
                for(int i=0; i<playerinfo.PassiveCardList.Count; i++)
                {
                    s_card = resourceManager.I_ActionCardDictionary["passive"];
                    s_cost = resourceManager.I_ActionCardDictionary["passiveCost"];
                    PCard = resourceManager.PassiveDictionary[playerinfo.PassiveCardList[i]];
                    TraitCards[i].InitializeCard(PCard.Name, PCard.Explain, 0, 0, s_card, s_cost);
                    TraitCards[i].bisEmpty = false;
                }
                break;
        }
    }




    #endregion

    #region 테스트 카드 장비
    public void EquipTestCard(string stat , int index)
    {
        
        int cardNum = playerinfo.TestCardList[index];
        int previous;
        string explain;
        refTestCard testcard = resourceManager.TestCardDictionary[cardNum];
        explain = "";

        void AddExplain(string ex, int maxnum)
        {
            explain += ex;
            for (int i = 0; i < maxnum; i++)
            {
                explain += "<sprite=4>";
            }
            explain += "\n\n";
        }

        AddExplain("<sprite=5>  ", testcard.over);
        AddExplain("<sprite=6>  ", testcard.success);
        AddExplain("<sprite=7>  ", testcard.fail);
        AddExplain("<sprite=8>  ",testcard.hell1);
        AddExplain("<sprite=9>  ",testcard.hell2);

        playerinfo.TestCardList.RemoveAt(index);
        RemoveSlot(index);

        switch(stat)
        {
            case "Power":               
                if (selectedModule.Strength > 0)
                {
                    previous = selectedModule.Strength;
                   
                    playerinfo.TestCardList.Add(previous);
                  
                    NimrodTestCard newCard = _TestcardPool.GetRecyclableObject() ??
               _TestcardPool.RegisterRecyclableObject(Instantiate(Sample));

                    Sprite testSprite = resourceManager.I_CardBackDictionary[previous];

                    newCard.initializeTestCard(testSprite, canvas, playerinfo.TestCardList.Count-1);

                    if (newCard.gameObject.activeSelf)
                        newCard.isActive = true;
                    else
                        newCard.gameObject.SetActive(newCard.isActive = true);

                    newCard.transform.parent = CardFrame;
                    newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    newCard.nimrodDetail = this;
                }                
                selectedModule.Strength = cardNum;
                TestCardInfos[0].InitializeInfo(resourceManager.I_CardBackDictionary[cardNum],explain,resourceManager.NimrodDictionary[selectedModule.module].strength.ToString());               
                
                break;
            case "Agility":
                if (selectedModule.Agility > 0)
                {
                    previous = selectedModule.Agility;

                    playerinfo.TestCardList.Add(previous);

                    NimrodTestCard newCard = _TestcardPool.GetRecyclableObject() ??
               _TestcardPool.RegisterRecyclableObject(Instantiate(Sample));

                    Sprite testSprite = resourceManager.I_CardBackDictionary[previous];

                    newCard.initializeTestCard(testSprite, canvas, playerinfo.TestCardList.Count - 1);

                    if (newCard.gameObject.activeSelf)
                        newCard.isActive = true;
                    else
                        newCard.gameObject.SetActive(newCard.isActive = true);

                    newCard.transform.parent = CardFrame;
                    newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    newCard.nimrodDetail = this;
                }
                selectedModule.Agility  = cardNum;
                TestCardInfos[1].InitializeInfo(resourceManager.I_CardBackDictionary[cardNum],explain, resourceManager.NimrodDictionary[selectedModule.module].agility.ToString());
                break;
            case "Examine":
                if (selectedModule.Examine > 0)
                {
                    previous = selectedModule.Examine;

                    playerinfo.TestCardList.Add(previous);

                    NimrodTestCard newCard = _TestcardPool.GetRecyclableObject() ??
               _TestcardPool.RegisterRecyclableObject(Instantiate(Sample));

                    Sprite testSprite = resourceManager.I_CardBackDictionary[previous];

                    newCard.initializeTestCard(testSprite, canvas, playerinfo.TestCardList.Count - 1);

                    if (newCard.gameObject.activeSelf)
                        newCard.isActive = true;
                    else
                        newCard.gameObject.SetActive(newCard.isActive = true);

                    newCard.transform.parent = CardFrame;
                    newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    newCard.nimrodDetail = this;
                }
                selectedModule.Examine  = cardNum;
                TestCardInfos[2].InitializeInfo(resourceManager.I_CardBackDictionary[cardNum],explain, resourceManager.NimrodDictionary[selectedModule.module].examine.ToString());
                break;
            case "Stealthe":
                if (selectedModule.Stealth > 0)
                {
                    previous = selectedModule.Stealth;

                    playerinfo.TestCardList.Add(previous);

                    NimrodTestCard newCard = _TestcardPool.GetRecyclableObject() ??
               _TestcardPool.RegisterRecyclableObject(Instantiate(Sample));

                    Sprite testSprite = resourceManager.I_CardBackDictionary[previous];

                    newCard.initializeTestCard(testSprite, canvas, playerinfo.TestCardList.Count - 1);

                    if (newCard.gameObject.activeSelf)
                        newCard.isActive = true;
                    else
                        newCard.gameObject.SetActive(newCard.isActive = true);

                    newCard.transform.parent = CardFrame;
                    newCard.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    newCard.nimrodDetail = this;
                }
                selectedModule.Stealth  = cardNum;
                TestCardInfos[3].InitializeInfo(resourceManager.I_CardBackDictionary[cardNum],explain, resourceManager.NimrodDictionary[selectedModule.module].stealth.ToString());
                break;
        }
      


    }

    #endregion

    #region buttons

    public void SwitchModeButton(int index)
    {

        ModeButtons[index].interactable = false;
        
        switch (index)
        {
            case 0:
                CurrentMode = Mode.Trait;
                ModeButtons[1].interactable = true;
                Test.gameObject.SetActive(false);
                Trait.gameObject.SetActive(true);
                SetModules(selectedModule);
                InitializeCard();
                break;

            case 1:
                CurrentMode = Mode.Trait;
                ModeButtons[0].interactable = true;
                Test.gameObject.SetActive(true);
                Trait.gameObject.SetActive(false);
                InitializeTestCard();
                break;
        }

    }

    public void SwitchCardButton(int index)
    {
        TraitButtons[index].interactable = false;
        switch(index)
        {
            case 0:
                TraitButtons[1].interactable = true;
                TraitButtons[2].interactable = true;
                CurrentType = CardType.Attack;
                InitializeCard();
                break;
            case 1:
                TraitButtons[0].interactable = true;
                TraitButtons[2].interactable = true;
                CurrentType = CardType.Guard;
                InitializeCard();
                break;
            case 2:
                TraitButtons[0].interactable = true;
                TraitButtons[1].interactable = true;
                CurrentType = CardType.Passive;
                InitializeCard();
                break;
        }
    }


    #endregion



}
