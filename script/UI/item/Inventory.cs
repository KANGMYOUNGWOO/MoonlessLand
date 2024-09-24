using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FronkonGames.SpritesMojo;

public class Inventory : MonoBehaviour
{
   

    #region  Components

    #region UIElement
    [SerializeField] private List<slot> ItemSlots = new List<slot>();
    [SerializeField] private List<CombineSlot> CombineSlots = new List<CombineSlot>();
    [SerializeField] private Image BigImage;
    [SerializeField] private Image CombineLock;

    [SerializeField] private Image Company;
    [SerializeField] private Image Brush;
    [SerializeField] private Image Medicine;



    [SerializeField] private TextMeshProUGUI BigName;
    [SerializeField] private TextMeshProUGUI BigSpecify;
    [SerializeField] private TextMeshProUGUI BigDecay;
    [SerializeField] private TextMeshProUGUI BigExplain;

    [SerializeField] private Transform SlotParent;

    [SerializeField] private GameObject UseButton;
    [SerializeField] private GameObject RemoveButton;
     private Canvas can;

    [SerializeField] private CombineEffect combineEffect;

    [SerializeField] private Material combineLockMaterial;

    private Item CurrentItem;

    #region
    private int option = 0;
  
    List<int> optionList = new List<int>();
    #endregion
    private bool bisProduction = false;

    #endregion


    #region Manager

    private CharacterManager characterManager = null;
    private ResourceManager resourceManager = null;
    private UIManager        uiManager = null;


    private PlayerInfo playerInfo;

    #endregion

    #endregion

    #region Vals

    private Item combItem1;
    private Item combItem2;
    private Item combItem3;

    private int CombDecay1;
    private int CombDecay2;
    private int CombDecay3;

    private int combNum = 0;

    private int fadeId;

    #endregion


    private void Awake()
    {
        

        fadeId = Shader.PropertyToID("_DirectionalAlphaFadeNoiseFactor");
        combineLockMaterial.SetFloat(fadeId, 0.2f);
    }

    private void Start()
    {
        can = GameObject.Find("Canvas").GetComponent<Canvas>();
        slot tempslot = null;
        foreach (Transform _slot in SlotParent)
        {
            if (_slot.CompareTag("Slot"))
            {
                tempslot = _slot.GetComponent<slot>();
                ItemSlots.Add(tempslot);
                tempslot.inventory = this;
                tempslot.canvas = can;
            }
        }
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        uiManager        = GameManager.GetManagerClass<UIManager>();
        resourceManager  = GameManager.GetManagerClass<ResourceManager>();
        resourceManager.GetPlayerInfo(out playerInfo);
        characterManager.inventory = this;
        uiManager.inventory        = this;
        combineEffect.inventory    = this;
        this.gameObject.SetActive(false);
       // this.gameObject.SetActive(true);
        

    }
    private void OnEnable()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {           
            ItemSlots[i].Organization(playerInfo.ItemArray[i].item, playerInfo.ItemArray[i].ItemCount);
           // Debug.Log(string.Format("ItemCode : {0} , ItemCount : {1}",playerInfo.ItemArray[i].item.itemcode, playerInfo.ItemArray[i].ItemCount));
           
        }
        EraseBoard();
    }

    #region 정보판 지우기
    private void EraseBoard()
    {
        BigName.text = "";
        BigDecay.text = "";
        BigSpecify.text = "";
        BigExplain.text = "";
        BigImage.color = new Color(0, 0, 0, 0);

        CurrentItem = null;
        UseButton.SetActive(false);
        RemoveButton.SetActive(false);

    }
    #endregion

    #region 아이템 정보 보여주기
    public void SlotClickAction(int num)
    {
        BigName.text    = playerInfo.ItemArray[num].item.ItemName;
        string spec     = "";

        switch (playerInfo.ItemArray[num].type)
        {
            case Item.ItemType.Medicine:
                spec = "의약품";
                UseButton.SetActive(true);
                break;
            case Item.ItemType.Ingredient:
                spec = "생체 물질";
                break;
            case Item.ItemType.Consumable:
                spec = "자연물";
                UseButton.SetActive(true);
                break;
            case Item.ItemType.Trash:
                spec = "부패물";
                break;
            case Item.ItemType.Essence:
                spec = "정수";
                break;
            case Item.ItemType.None:
                spec = "물품";
                break;


        }

        BigSpecify.text = spec + ", " + playerInfo.ItemArray[num].item.ItemCompany;

        int decay = playerInfo.ItemArray[num].ItemDecay;

        if (decay >= 10000)
            BigDecay.text = "유통기한 : 썩지않음";
        else if (decay > 30)
            BigDecay.text = "유통기한 : <color=green>" + decay.ToString() + "</color>";
        else if (decay >10)
            BigDecay.text = "유통기한 : <color=yellow>" + decay.ToString() + "</color>";
        else if (decay > 5)
            BigDecay.text = "유통기한 : <color=red>" + decay.ToString() + "</color>";

        BigExplain.text = playerInfo.ItemArray[num].item.ItemExplain;

        BigImage.color = new Color(255f, 255f, 255f, 132f);
        BigImage.sprite = playerInfo.ItemArray[num].item.ItemSprite;


        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (i == num) continue;
            ItemSlots[i].DeactiveSelectImage();
        }

        CurrentItem = playerInfo.ItemArray[num].item;
       
        RemoveButton.SetActive(true);
        if(bisProduction)
        {
            
            uiManager.Prologues("ItemTutorial2");
        }
    }

    #endregion
    #region 슬롯바꾸기
    public void SlotChange(int before , int after)
    {
        //if(ItemSlots[after].Count == 0)
        
            
            int decay = playerInfo.ItemArray[after].ItemDecay;
            int smell = playerInfo.ItemArray[after].ItemSmell;
            int count = playerInfo.ItemArray[after].ItemCount;

            Item item = playerInfo.ItemArray[after].item;


            playerInfo.ItemArray[after].item      = playerInfo.ItemArray[before].item;
            playerInfo.ItemArray[after].ItemDecay = playerInfo.ItemArray[before].ItemDecay;
            playerInfo.ItemArray[after].ItemSmell = playerInfo.ItemArray[before].ItemSmell;
            playerInfo.ItemArray[after].ItemCount = playerInfo.ItemArray[before].ItemCount;

            playerInfo.ItemArray[before].item      = item;
            playerInfo.ItemArray[before].ItemDecay = decay;
            playerInfo.ItemArray[before].ItemSmell = smell;
            playerInfo.ItemArray[before].ItemCount = count;


            ItemSlots[after].Organization(playerInfo.ItemArray[after].item, playerInfo.ItemArray[after].ItemCount);
            ItemSlots[before].Organization(playerInfo.ItemArray[before].item, playerInfo.ItemArray[before].ItemCount);

        
       


    }
    #endregion

    #region  setcombineslot
    public void SetCombineSlot(int before)
    {
        if (combNum == 3) return;
       
        Item tempItem;
                   
        if (combNum == 0)
        {
            combItem1 = playerInfo.ItemArray[before].item;
            if (combItem1.type == Item.ItemType.Ingredient)
            {
                
                uiManager.SetCardRejectionText("이건 합성할 수 없다. <waitfor=1> <?Disapear>");
                ItemSlots[before].Organization(combItem1,playerInfo.ItemArray[before].ItemCount);
                combItem1 = null;
                return;
            }

            if(combItem1.type == Item.ItemType.None)
            {
                ItemSlots[before].Organization(combItem1, playerInfo.ItemArray[before].ItemCount);                
                uiManager.SetCardRejectionText("이건 합성할 수 없다. <waitfor=1> <?Disapear>");
                combItem1 = null;
                return;
            }
         
            tempItem = characterManager.RemoveItem(playerInfo.ItemArray[before].item.itemcode, 1);
            CombDecay1 = playerInfo.ItemArray[before].ItemDecay;
            CombineSlots[0].SetCombine(tempItem.ItemName, tempItem.itemcode / 1000, tempItem.ItemSprite);
            combNum += 1;
        }
        else if (combNum == 1)
        {           
            combItem2 = playerInfo.ItemArray[before].item;
           // Debug.Log(combItem2.itemcode);
            if (combItem1.type == Item.ItemType.Essence)
            {
                if (combItem2.type != Item.ItemType.Consumable) { uiManager.SetCardRejectionText("자연물을 넣어야해 <waitfor=1> <?Disapear> "); return; }                 
            }
            else if (combItem1.type != combItem2.type)
            {
                ItemSlots[before].Organization(combItem2, playerInfo.ItemArray[before].ItemCount);
                combItem2 = null;

                if(combItem1.type == Item.ItemType.Consumable) 
                uiManager.SetCardRejectionText("자연물을 넣어야해. <waitfor=1> <?Disapear> ");
                if (combItem1.type == Item.ItemType.Medicine)
                uiManager.SetCardRejectionText("의약품을 넣어야해. <waitfor=1> <?Disapear> ");


                return;
            }
            tempItem = characterManager.RemoveItem(playerInfo.ItemArray[before].item.itemcode, 1);
            combNum += 1;
            CombDecay2 = playerInfo.ItemArray[before].ItemDecay;
            CombineSlots[1].SetCombine(tempItem.ItemName, tempItem.itemcode / 1000, tempItem.ItemSprite);
        }
        else if (combNum == 2)
        {
            combItem3 = playerInfo.ItemArray[before].item;
           

            if (combItem1.type == Item.ItemType.Essence)
            {
                if (combItem3.type != Item.ItemType.Consumable) { uiManager.SetCardRejectionText("자연물을 넣어야해 <waitfor=1> <?Disapear> "); return; }
            }
            else if (combItem1.type != combItem3.type)
            {
                ItemSlots[before].Organization(combItem3, playerInfo.ItemArray[before].ItemCount);
                combItem3 = null;

                if (combItem1.type == Item.ItemType.Consumable)
                    uiManager.SetCardRejectionText("자연물을 넣어야해. <waitfor=1> <?Disapear> ");
                if (combItem1.type == Item.ItemType.Medicine)
                    uiManager.SetCardRejectionText("의약품을 넣어야해. <waitfor=1> <?Disapear> ");
                return;

            }
            tempItem = characterManager.RemoveItem(playerInfo.ItemArray[before].item.itemcode, 1);
            combNum += 1;
            CombDecay3 = playerInfo.ItemArray[before].ItemDecay;
            CombineSlots[2].SetCombine(tempItem.ItemName, tempItem.itemcode / 1000, tempItem.ItemSprite);
        }
        else return;
    }

    #endregion

    #region combineButton
    public void CombineButton()
    {      
        if (combNum != 3) return;
        int level = characterManager.MedLevel(combItem1.itemcode,combItem2.itemcode,combItem3.itemcode);

        switch (combItem1.type)
        {
            case Item.ItemType.Consumable:
                combineEffect.ActiveOption(level);

                break;

            case Item.ItemType.Medicine:
               
                break;

            case Item.ItemType.Essence:

                break;
        }
       // combItem1 = null;
       // combItem2 = null;
       // combItem3 = null;
       // combNum = 0;
       // for (int i = 0; i < CombineSlots.Count; i++) CombineSlots[i].ReleaseCombine();

    }

    public void secondOption(int option)
    {
      
        this.option = option;
        int n = 0;
        int end = 0;

        optionList.Clear();
        System.Random rng = new System.Random();
        if(option == 0)
        {

            for (int i = 1; i < 5; i++)
            {
                optionList.Add(i);
            }
            n = optionList.Count;
            end = n;
            while (n > 0)
            {
                n--;
                int k = rng.Next(0, end);
                int value = optionList[k];
                optionList[k] = optionList[n];
                optionList[n] = value;
            }

            combineEffect.SelectOption(optionList[0],optionList[1]);

        }
        else if(option == 1)
        {
            for (int i = 1; i < 6; i++)
            {
                optionList.Add(i);
            }
           
            n = optionList.Count;
            end = n;
            while (n > 0)
            {
                n--;
                int k = rng.Next(0, end);
                int value = optionList[k];
                optionList[k] = optionList[n];
                optionList[n] = value;
            }

            combineEffect.SelectOption(optionList[0],optionList[1],optionList[2]);
        }
        else if(option == 2)
        {
            for (int i = 1; i < 5; i++)
            {
                optionList.Add(i);
            }
            ThirdOption(2);
        }


       
    }


    public void ThirdOption(int detail)
    {
       int result  = characterManager.CombineItem(combItem1.itemcode,combItem2.itemcode,combItem3.itemcode,option,optionList[detail]);
        //characterManager.AddItem(item,1);
        switch (combItem1.type)
        {
            case Item.ItemType.Consumable:
                characterManager.AddItem(result, 1);
                combineEffect.SetMedicineBonus(result);
                break;

            case Item.ItemType.Medicine:
                characterManager.AddItem(result, 1);
                combineEffect.SetMedicineBonus(result);
                break;

            case Item.ItemType.Essence:

                break;
        }

        combItem1 = null;
        combItem2 = null;
        combItem3 = null;
        combNum = 0;
        for (int i = 0; i < CombineSlots.Count; i++) CombineSlots[i].ReleaseCombine();
    }


    #endregion

    #region ResetButton
    public void ReSet()
    {
        if (combItem1 == null) return;
        characterManager.AddItem(combItem1.itemcode, 1 , decay: CombDecay1);
        if (combItem2 != null)  
           characterManager.AddItem(combItem2.itemcode, 1 , decay: CombDecay2);
        if(combItem3 != null)
          characterManager.AddItem(combItem3.itemcode, 1 , decay: CombDecay3);
        combItem1 = null;
        combItem2 = null;
        combItem3 = null;
        CombDecay1 = 0;
        CombDecay2 = 0;
        CombDecay3 = 0;

        for (int i = 0; i < CombineSlots.Count; i++) CombineSlots[i].ReleaseCombine();
        combNum = 0;
    }

    #endregion

    #region Bonus
    public void Bonus(Sprite sprite)
    {
        combineEffect.Bonus(sprite);
    }




    #endregion

    #region 아이템 사용
    public void UseItem()
    {
        if (CurrentItem == null) return;
        if (CurrentItem.type != Item.ItemType.Medicine) return;
        uiManager.UseItem(CurrentItem.ItemSprite,CurrentItem.ItemName,CurrentItem.ItemCompany,CurrentItem.itemcode);
        EraseBoard();
        if(bisProduction)
        {
            bisProduction = false;
            uiManager.Prologues("ItemTutorial3");
        }
    }

    public void Production()
    {
        bisProduction = true;
    }

    #endregion

    #region 아이템 조합 잠금 해제
    public void ReleaseCombineLcok()
    {
        float noiseValue = 0.2f;
       
        IEnumerator releaseRoutine()
        {
            while (noiseValue > -21.0f)
            {
                noiseValue -= 0.15f;
                combineLockMaterial.SetFloat(fadeId, noiseValue);
                yield return new WaitForSeconds(0.01f);
            }
            CombineLock.gameObject.SetActive(false);
            uiManager.Prologues("ItemCombine2");
        }

       
        StartCoroutine(releaseRoutine());

        


    }


    #endregion


    public void AdjustSlot(int slotnum, Item item ,int count)
    {
        ItemSlots[slotnum].Organization(item, count);           
    }

    public Item SenditemInfo(int slotnum)
    {
        return playerInfo.ItemArray[slotnum].item;
    }

    public void GetPlayerData(PlayerInfo data)
    {
        playerInfo = data;
    }
    

}
