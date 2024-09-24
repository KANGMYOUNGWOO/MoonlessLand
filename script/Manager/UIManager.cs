using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using FronkonGames.SpritesMojo;
using Febucci.UI;


public class UIManager : MonoBehaviour,IManager
{

    public GameManager gameManager { get { return GameManager.gameManager; } }
   
    private LogicManager logicManager;
    private CharacterManager characterManager;
    private TextManager textManager;

    [SerializeField] private Material burnout;
    [SerializeField] private Material flameMaterial;
    [SerializeField] private Material readDisolve;
    [SerializeField] private GameObject BookButton;

    [SerializeField] private Camera mainCamera;

    
  
  

    #region burningValues
    private int burnId;
    private bool bisburn;
    private bool bisfirst;
    private bool bissecond;
    private bool bisthird;
    private bool bisend;
    private float burnValue;
    #endregion
    #region fadeValue
    private int fadeId;
    private float fadeValue;
    #endregion
    #region flameValue

    private int flameId;
    private float flameValue;
    private int flamefadeId;
    private float flamefadeValue;

    #endregion


   


    public Inventory        inventory       { get; set; }
    public book             bookObject      { get; set; }
    public NimrodChanger    nimrodChanger   { get; set; }
    public NimrodMain       nimrodMain      { get; set; }
    public MedicineTab      medicineTab     { get; set; }
    public ItemTutorial     itemTutorial    { get; set; }
    public TokenUI          tokenUI         { get; set; }
    public DIseaseTab       diseaseTab      { get; set; }
    public playerUI         playerui        { get; set; }
    public WorldMap         worldMap        { get; set; }
    public ItemAddMessage   itemAddMessage  { get; set; }
    public Prologue         prologue        { get; set; }
    public BackgroundUI     backgroundUI    { get; set; }
    public CardRejection    cardRejection   { get; set; }
    public DiseaseEffect    diseaseEffect   { get; set; }
    public SelectActor      selectActor     { get; set; }
    public BattleCard       battleCard      { get; set; }
    public BattleButton     battleButton    { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
       
    }

    void Start()
    {
      

        burnId    = Shader.PropertyToID("_BurnRadius");
        fadeId    = Shader.PropertyToID("_SourceAlphaDissolveFade");
        flameId   = Shader.PropertyToID("_FlameRadius");
        flamefadeId = Shader.PropertyToID("_FullAlphaDissolveFade");

        logicManager = GameManager.GetManagerClass<LogicManager>();
        textManager = GameManager.GetManagerClass<TextManager>();

        burnValue    = -1+0.29f;
        fadeValue    = 7.5f;
        flameValue   = 1f;
        flamefadeValue = 1f;

        burnout.SetFloat(burnId, burnValue);
        burnout.SetFloat(fadeId, fadeValue);
        flameMaterial.SetFloat(flameId, flameValue);
        readDisolve.SetFloat(flamefadeId,flamefadeValue);

        bisburn = false;

        characterManager = GameManager.GetManagerClass<CharacterManager>();
    }

   

    public void SetCardRejectionText(string text)
    {
        if (!cardRejection.bissEnd) return;
        cardRejection.bissEnd = false;
        cardRejection.gameObject.SetActive(true);
        cardRejection.Apear(text);               
    }

    public void UIArrange()
    {
        diseaseTab.transform.SetSiblingIndex(4);
        playerui.transform.SetSiblingIndex(5);
        tokenUI.transform.SetSiblingIndex(7);

        bookObject.transform.SetSiblingIndex(9);
        nimrodChanger.transform.SetSiblingIndex(10);
        diseaseEffect.transform.SetSiblingIndex(12);
        worldMap.transform.SetSiblingIndex(13);
        cardRejection.transform.SetSiblingIndex(15);
        medicineTab.transform.SetSiblingIndex(16);
        
    }


    #region prologue
    public void Prologues(string message)
    {
        switch (message)
        {
            case "PrologueStart":
                nimrodChanger.bisProhibit = true;
                prologue.gameObject.SetActive(true);
                prologue.startPrologue();
                break;
            case "DictionaryStat":
                bookObject.Productions(0);
                break;
            case "DcitionaryEnd":
                bookObject.Productions(1);
                break;
            case "ItemTutorial0":
                bookObject.Productions(2);
                break;
            case "ItemCombine":
                bookObject.Productions(3);
                break;
            case "ItemCombine2":
                bookObject.Productions(4);
                break;

            case "ItemTutorial1":
                itemTutorial.gameObject.SetActive(true);
                itemTutorial.ChangeBox(0);
                inventory.Production();
                break;
            case "ItemTutorial2":

                itemTutorial.ChangeBox(1);
                break;
            case "ItemTutorial3":
                itemTutorial.ChangeBox(2);
                medicineTab.tutorial();
                break;
            case "ItemTutorial4":
                itemTutorial.gameObject.SetActive(false);
                bookObject.gameObject.SetActive(false);
                logicManager.ButtonActive(0, "확인", "내 몸의 상태를 확인해본다.", true, option: 0);
                break;



            case "cameraShake":
                mainCamera.DOShakePosition(0.7f, strength: 5).SetEase(Ease.InElastic);
                break;

            case "NimrodGlitch1":
                backgroundUI.SetBackGroundMaterial(0);

                break;

            case "NimrodGlitch2":
                prologue.gameObject.SetActive(true);
                prologue.nimrodGlitch();
                break;

            case "NimrodGlitch3":
                prologue.gameObject.SetActive(false);
                logicManager.ButtonActive(0, "속삭임", "목소리의 주인은?", true, option: 0);                
                backgroundUI.SetBackGroundMaterial(flameMaterial);
                backgroundUI.SetBackGroundMaterial(1);
                characterManager.AddModule(2);
                
                nimrodMain.changeModule(0,0);
                break;




        }

    }


    #endregion

    public void CardAction(string message)
    {
        switch(message)
        {
            case "cameraShake":
                mainCamera.DOShakePosition(1.5f, strength: 100).SetEase(Ease.InElastic);
                break;
        }
    }



     public void UseItem(Sprite item,string itemName, string itemCompnay , int itemCode)
    {
        medicineTab.gameObject.SetActive(true);
        medicineTab.ActiveMedTab(item,itemName,itemCompnay,itemCode);
    }
    

  

    public void StartBattle(string text)
    {
        // BlackBackGround.gameObject.SetActive(true);
        diseaseEffect.StartBattle(text);
        
    }

    
    public void AdjustNimrodInfo(Sprite Icon, string name)
    {
        playerui.AdjustNimrodInfo(Icon, name);
        InitializeNimrodChanger(Icon, name);      
    }

    
    public void burning(bool b)
    {
        bisburn = b;
        bisfirst = b;
        bissecond = b;
        bisthird = b;
        bisend = b;

        if(!b)
        {
            burnValue = -1 + 0.29f;
            fadeValue = 7.5f;

            burnout.SetFloat(burnId, burnValue);
            burnout.SetFloat(fadeId, fadeValue);

            selectActor.ActiveBeginingPoint(true);
        }
        else
        {
            StartCoroutine(BurningRoutine());
        }

    }

    #region worldMap

    #region 월드맵 열기
    public void FlameSecne(int level)
    {
        tokenUI.gameObject.SetActive(false);
        diseaseTab.gameObject.SetActive(false);
        nimrodMain.DisActivenimrod();       
        bookObject.disActive();
        nimrodChanger.gameObject.SetActive(false);
        playerui.gameObject.SetActive(false);
        textManager.AreaEnd();

        

        IEnumerator FlameRoutine()
        {
           
            while (flameValue > 0.14f)
            {
                flameValue -= 0.05f;
                flamefadeValue -= 0.3f;
                flameMaterial.SetFloat(flameId, flameValue);
                readDisolve.SetFloat(flamefadeId,flamefadeValue);
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(1.5f);
            while (flameValue > 0.04f)
            {
                flameValue -= 0.05f;               
                flameMaterial.SetFloat(flameId, flameValue);                
                yield return new WaitForSeconds(0.02f);
            }
            worldMap.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            while (flameValue < 1.0f)
            {
                flameValue += 0.05f;               
                flameMaterial.SetFloat(flameId, flameValue);
                yield return new WaitForSeconds(0.02f);
            }

            flamefadeValue = 1.0f;
            readDisolve.SetFloat(flamefadeId,flamefadeValue);
            worldMap.MapStart(level);
        }
        flameValue = 1f;
        flameMaterial.SetFloat(flameId,flameValue);

        
        StartCoroutine(FlameRoutine());

        


    }
    #endregion

    #region 월드맵 닫기

    public void wolrdToSelect()
    {
        worldMap.gameObject.SetActive(false);
        playerui.gameObject.SetActive(true);
        tokenUI.gameObject.SetActive(true);
        diseaseTab.gameObject.SetActive(true);
        bookObject.buttonActivation();

    }


    #endregion
    #endregion

    public void HPUI(float hp, float mp)
    {
        playerui.HPUI(hp,mp);     
    }
    
    public void ArmorUI(float armor,float hp)
    {
        playerui.ArmorUI(armor,hp);
    }

    public void ArmorApear(float armor, float hp, bool apea)
    {
        playerui.ArmorApear(armor,hp,apea);
    }



   public void Gameover()
    {
        prologue.gameObject.SetActive(true);
        prologue.GameOver();
    }


    public IEnumerator BurningRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.007f);

        while(bisburn)
        {
            burnValue += 0.03f;
            fadeValue += 0.03f;
            if (bisfirst) logicManager.FireOut(1);
            bisfirst = false;
            if (fadeValue > 9f)
            {
                //if (bisfirst) logicManager.FireOut(1);
                //bisfirst = false;
            }
            if (fadeValue > 9.2f)
            {
                if (bissecond) logicManager.FireOut(2);
                bissecond = false;
            }
            if (fadeValue > 10.0f)
            {
                if (bisthird)
                {
                    logicManager.FireOut(3);
                    selectActor.ActiveBeginingPoint(false);
                }
                bisthird = false;
            }

            burnout.SetFloat(burnId, burnValue);
            burnout.SetFloat(fadeId, fadeValue);
            if (fadeValue > 11) { logicManager.FireOut(4); fadeValue = 11f; bisburn = false; }

            yield return waitForSeconds;
        }
    }

    public void SetActiveNimrodChanger()
    {

        nimrodChanger.gameObject.SetActive(true);
        nimrodMain.InitializeNimrodChanger();
        nimrodChanger.ActiveSlots();
        
    }

    public void InitializeNimrodChanger(int index, Sprite sprite, string name)
    {
        nimrodChanger.intializeSlot(index, sprite, name);
    }

    public void InitializeNimrodChanger(int index, Sprite sprite, string name, bool bisIn)
    {
        nimrodChanger.intializeSlot(index,sprite,name,bisIn);
    }

    public void InitializeNimrodChanger(Sprite sprite, string name)
    {
        nimrodChanger.initializeSlot(sprite,name);
    }
    public void InitializeNimrodChanger(int index,bool bisSelected)
    {
        nimrodChanger.initializeSlot(index, bisSelected);
    }

    public void ActiveNimrodMain()
    {
        nimrodMain.ActiveNimrod();
    }

    public void NimrodChangeButtonAction(int index)
    {
        nimrodMain.changeModule(index);
    }

    public void BookButtonActive(bool bisButton)
    {
        BookButton.gameObject.SetActive(bisButton);
    }

    public void BookActive()
    {
        bookObject.DisActivation();
    }

    public void CameraOn()
    {
        mainCamera.gameObject.SetActive(true);
    }

}
