using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
    


public class AdventureDict : MonoBehaviour
{
    public GameManager gameManager { get { return GameManager.gameManager; } }


    [SerializeField] List<Image> Bonus = new List<Image>();
    [SerializeField] private GameObject MapState;
    [SerializeField] private GameObject DetailState;


    #region 사전 외부 

    [SerializeField] private List<StageSelectButton> Buttons = new List<StageSelectButton>();
    [SerializeField] private List<NameButton> NameButtons = new List<NameButton>();
    [SerializeField] private TextMeshProUGUI AreaName;

    [SerializeField] private GameObject OutPrevButton;
    [SerializeField] private GameObject OutNextButton;
    [SerializeField] private TextMeshProUGUI OutPage;

    #endregion

    #region 사전 내부

    [SerializeField] private TextMeshProUGUI Name; 
    [SerializeField] private TextMeshProUGUI SubName; 
    [SerializeField] private TextMeshProUGUI Explain;

    [SerializeField] private List<TextMeshProUGUI> KnowledgeList = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> ProfitList = new List<TextMeshProUGUI>();

    [SerializeField] private GameObject MemoPad;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI examineText;
    [SerializeField] private TextMeshProUGUI stealthText;
    [SerializeField] private TextMeshProUGUI weakText;
    [SerializeField] private TextMeshProUGUI immuneText;
    [SerializeField] private TextMeshProUGUI bonusText;
    [SerializeField] private TextMeshProUGUI penaltyText;
    [SerializeField] private TextMeshProUGUI hpGaugeText;
    [SerializeField] private TextMeshProUGUI bonusGaugeText;
    [SerializeField] private TextMeshProUGUI penaltyGaugeText;


    #endregion



    private CharacterManager characterManager;
    private ResourceManager resourceManager;
    private BookData bookdata;

    private int Page = 0;
    private int CurrentPage = 0;
    private int startNum = 0;
    private int endNum = 0;


    private enum mode {Mapstate, DetailState};    
    private mode CurrentMode = mode.DetailState;  
    private int CurrentArea = 0;


    private void Awake()
    {
        //gameManager.bookData += GetBookData;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        resourceManager.GetBookInfo(out bookdata);
        gameObject.SetActive(false);
      
    }

   

    private void ChangeMode()
    {
        switch (CurrentMode)
        {
            case mode.DetailState:
                MapState.SetActive(false);
                DetailState.SetActive(true);

               

                break;
            case mode.Mapstate:
                DetailState.SetActive(false);
                MapState.SetActive(true);

                SetArea();
                for (int i = 0; i < Buttons.Count; i++)
                {
                    Buttons[i].gameObject.SetActive(true);
                    Buttons[i].DictionaryWork();                   
                }
                break;

        }
    }

    public void SetArea(int num = 0)
    {
        CurrentArea   = num;
        AreaName.text = bookdata.AreaNames[CurrentArea];

        startNum = bookdata.StartList[CurrentArea];
        endNum   = bookdata.EndList[CurrentArea];
        CurrentPage = 0;
        string tempName;

        Page = endNum - startNum % 10 == 0 ? (endNum - startNum)/ 10  : (endNum - startNum) / 10 + 1;
        
        for(int i=0;i<10;i++)
        {
            bookdata.GetDictData(startNum + i, out tempName);
            if (bookdata.vegetalDict[tempName].bisFirst == 0) tempName = "???";
            NameButtons[i].DisApear(tempName);
        }

        OutPrevButton.SetActive(false);
        OutNextButton.SetActive(Page>0);
        OutPage.text = string.Format("{0} / {1}",CurrentPage+1,Page);

    }

    public void ClickOutPrev()
    {
        if (CurrentPage == 0) return;

        OutNextButton.SetActive(true);
        CurrentPage -= 1;
        OutPage.text = string.Format("{0} / {1}", CurrentPage+1, Page);
        if (CurrentPage == 0) OutPrevButton.SetActive(false);
        string tempName;

        int Count = 0;
        Count = (endNum - startNum) - (CurrentPage * 10);
        Count = Mathf.Clamp(Count, 1, 10);

        for (int i = 0; i < Count; i++)
        {
            bookdata.GetDictData(startNum + i + CurrentPage * 10, out tempName);
            NameButtons[i].gameObject.SetActive(true);
            if (bookdata.vegetalDict[tempName].bisFirst == 0) tempName = "???";
            NameButtons[i].DisApear(tempName);
        }

        for (int s = Count; s < 10;s++)
        {
            NameButtons[s].gameObject.SetActive(false);
        }

    }


    public void ClickOutNect()
    {
        if (CurrentPage+1 == Page) return;

        OutPrevButton.SetActive(true);
        CurrentPage += 1;
        OutPage.text = string.Format("{0} / {1}", CurrentPage+1, Page);
        if (CurrentPage+1 == Page) OutNextButton.SetActive(false);
        string tempName;

        int Count = 0;
        Count = (endNum - startNum) - (CurrentPage * 10);
        Count = Mathf.Clamp(Count, 1, 10);

        for (int i = 0; i < Count; i++)
        {
            bookdata.GetDictData(startNum + i + CurrentPage * 10, out tempName);
            NameButtons[i].gameObject.SetActive(true);
            if (bookdata.vegetalDict[tempName].bisFirst == 0) tempName = "???";
            NameButtons[i].DisApear(tempName);
        }
        for (int s = Count; s < 10; s++)
        {
            NameButtons[s].gameObject.SetActive(false);
        }

    }

  

    private void OnEnable()
    {
        ChangeMode();
    }

    public void PreviousButton()
    {
        CurrentMode = mode.Mapstate;
        ChangeMode();
    }


    public void EnterDetail(int index)
    {
        int tempnum = startNum + index + CurrentPage * 10;
        string tempName;
        string subname;
        string tempExplain;
        string tempknowledge1;
        string tempknowledge2;
        string tempknowledge3;
        string tempBonus1;
        string tempBonus2;
        string tempBonus3;

        bookdata.GetDictData(tempnum, out tempName, out subname, out tempExplain, out tempknowledge1, out tempknowledge2, out tempknowledge3, out tempBonus1, out tempBonus2, out tempBonus3);

        if (resourceManager.BeastDicitonary.ContainsKey(tempName))
        {
            beast be = resourceManager.BeastDicitonary[tempName];
            MemoPad.SetActive(true);

            strengthText.text = be.strength.ToString();
            agilityText.text  = be.agility.ToString();
            examineText.text  = be.examine.ToString();
            stealthText.text  = be.stealth.ToString();

            weakText.text     = be.weak;
            immuneText.text   = be.immune;
            bonusText.text    = be.benefit;
            penaltyText.text  = be.penalty;

            hpGaugeText.text  = be.hp.ToString();
            bonusGaugeText.text = be.MaxBenefit.ToString();
            penaltyGaugeText.text = be.MaxPenalty.ToString();

        }
        else
        {
            strengthText.text = "?";
            agilityText.text  = "?";
            examineText.text  = "?";
            stealthText.text  = "?";

            weakText.text = "-";
            immuneText.text = "-";
            bonusText.text = "-";
            penaltyText.text = "-";

            hpGaugeText.text = "-";
            bonusGaugeText.text = "-";
            penaltyGaugeText.text = "-";
        }


        if (bookdata.vegetalDict[tempName].bisBonus1 == 0) { tempknowledge1 = "알려진 바가 없다"; tempBonus1 = " "; }
        else if(bookdata.vegetalDict[tempName].bisBonus1 == 2) { tempknowledge1 = "-"; tempBonus1 = " "; };
        if (bookdata.vegetalDict[tempName].bisBonus2 == 0) { tempknowledge2 = "알려진 바가 없다"; tempBonus2 = " "; }
        else if (bookdata.vegetalDict[tempName].bisBonus2 == 2) { tempknowledge2 = "-"; tempBonus2 = " "; };
        if (bookdata.vegetalDict[tempName].bisBonus3 == 0) { tempknowledge3 = "알려진 바가 없다"; tempBonus3 = " "; }
        else if (bookdata.vegetalDict[tempName].bisBonus3 == 2) { tempknowledge3 = "-"; tempBonus3 = " "; };
        if (bookdata.vegetalDict[tempName].bisFirst == 0) { tempName = "???"; subname = "-미지의 존재-"; tempExplain = ""; }

        CurrentMode = mode.DetailState;

        ChangeMode();
      
        Name.text = tempName;
        Explain.text = tempExplain;
        SubName.text = subname;
        KnowledgeList[0].text = tempknowledge1;
        KnowledgeList[1].text = tempknowledge2;
        KnowledgeList[2].text = tempknowledge3;
        ProfitList[0].text = tempBonus1;
        ProfitList[1].text = tempBonus2;
        ProfitList[2].text = tempBonus3;
    }








}
