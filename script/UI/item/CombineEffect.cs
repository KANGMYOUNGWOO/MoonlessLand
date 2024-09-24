using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FronkonGames.SpritesMojo;
using TMPro;
using DG.Tweening;


public class CombineEffect : MonoBehaviour
{
    [SerializeField] private Image BackGroundImage;
    [SerializeField] private Image ItemFrame;
    [SerializeField] private Image ItemSlotImage;
    [SerializeField] private Image BonusImage;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI BonusText;
    [SerializeField] private List<Image> StartSlots = new List<Image>();
    [SerializeField] private SemiActionCard Sample;
    [SerializeField] private Button QuitButton;

    [SerializeField] private Image SelectbackGround;
    [SerializeField] private Image Company;
    [SerializeField] private Image Brush;
    [SerializeField] private Image Medicine;
    [SerializeField] private List<Image> Icons = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> OptionTexts = new List<TextMeshProUGUI>();
   


    private bool bisBonus = false;
    private bool bisFirstAnim = false;
    private bool bisSecondAnim = false;   
    private Sprite bonusSprite;


    private Material dissolveMat;
    private Material glassMat;

    private float dissolveSlide = 1;
    private float dissolveSpeed = 0.01f;
    public Inventory inventory { get; set; }
    private ResourceManager resourceManager;
    private ObjectPool<SemiActionCard> _actionCardPool = null;

    private void Awake()
    {
        Color incolor;
        Color outcolor;
        ColorUtility.TryParseHtmlString("#87A1E0", out incolor);
        ColorUtility.TryParseHtmlString("#C90500", out outcolor);
        dissolveMat = Dissolve.CreateMaterial();
        glassMat = Glass.CreateMaterial();

        Dissolve.Shape.Set(dissolveMat, DissolveShape.Clock);
        Dissolve.Mode.Set(dissolveMat, 1);
        //Dissolve.


        Dissolve.ColorInside.Set(dissolveMat,incolor);
        Dissolve.ColorOutside.Set(dissolveMat, outcolor);
        Dissolve.BorderSize.Set(dissolveMat, 0.5f);

        Dissolve.Slide.Set(dissolveMat, 1f);
        
        Glass.Refraction.Set(glassMat, 0f);
        ItemFrame.material = glassMat;


        BackGroundImage.material = dissolveMat;        
    }

    private void Start()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        BackGroundImage.gameObject.SetActive(false);
        _actionCardPool = new ObjectPool<SemiActionCard>();
    }




    private IEnumerator Puzzle(int level , string name)
    {
        Dissolve.Slide.Set(dissolveMat, 1f);
        dissolveSpeed = 0.02f;
        

        while (dissolveSlide > 0.001f)
        {
            dissolveSlide -= dissolveSpeed;
            
            Dissolve.Slide.Set(dissolveMat, dissolveSlide);
         
            yield return new WaitForSeconds(0.01f);
        }

        ItemSlotImage.gameObject.SetActive(true);
        ItemFrame.gameObject.SetActive(true);
        ItemFrame.rectTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f).From(new Vector3(1.0f,1.0f,1.0f)).OnComplete(()=>ItemSlotImage.gameObject.SetActive(true));
        NameText.gameObject.SetActive(true);
        NameText.text = name;
        QuitButton.gameObject.SetActive(true);

        for(int i=0;i<level;i++)
        {
            StartSlots[i].gameObject.SetActive(true);
        }

        if(bisBonus)
        {
            BonusImage.sprite = bonusSprite;
            BonusImage.gameObject.SetActive(true);
            BonusText.gameObject.SetActive(true);
        }

    }

    private IEnumerator Puzzle(int bonus1, int bonus2, int bonus3 , string type1 , string type2 , string type3 ,string name)
    {
        Dissolve.Slide.Set(dissolveMat, 1f);
        dissolveSpeed = 1.0f;
        WaitForSeconds wait = new WaitForSeconds(0.005f);
        while (dissolveSlide > 0.001f)
        {
            dissolveSlide -= dissolveSpeed * Time.deltaTime;
            Dissolve.Slide.Set(dissolveMat, dissolveSlide);
            yield return wait;
        }

        ItemSlotImage.gameObject.SetActive(true);
        ItemFrame.gameObject.SetActive(true);
        ItemFrame.rectTransform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f).From(new Vector3(1.0f, 1.0f, 1.0f)).OnComplete(() => ItemSlotImage.gameObject.SetActive(true));
        NameText.gameObject.SetActive(true);
        
        NameText.text = name;

        BonusText.gameObject.SetActive(true);

        
        void initializeTraitCard(int cardcode, string type)
        {
            SemiActionCard newCard = _actionCardPool.GetRecyclableObject() ??
               _actionCardPool.RegisterRecyclableObject(Instantiate(Sample));

            newCard.transform.parent = this.transform;


            switch (type)
            {
                case "Attack":
                    attackCard ac = resourceManager.AttackDictionary[cardcode];
                    //newCard.InitializeCard(true,false,resourceManager.GetActionCardImage("Attack") ,resourceManager.GetActionCardImage("AttackCost") ,ac.Cost,ac.Cooltime,ac.Name,ac.Explain);
                    if (newCard.gameObject.activeSelf)newCard.isActive = true;
                    else newCard.gameObject.SetActive(newCard.isActive = true);
                    break;

                case "Guard":
                    guardCard gc = resourceManager.GuardDictionary[cardcode];
                    newCard.InitializeCard(true, false, resourceManager.I_ActionCardDictionary["Guard"],resourceManager.I_ActionCardDictionary["GuardCost"], gc.Cost, gc.Cooltime, gc.Name, gc.Explain);
                    if (newCard.gameObject.activeSelf) newCard.isActive = true;
                    else newCard.gameObject.SetActive(newCard.isActive = true);
                    break;

                case "Passive":
                    passiveCard pc = resourceManager.PassiveDictionary[cardcode];
                    newCard.InitializeCard(true,false,resourceManager.I_ActionCardDictionary["Passive"],pc.Name,pc.Explain);
                    if (newCard.gameObject.activeSelf) newCard.isActive = true;
                    else newCard.gameObject.SetActive(newCard.isActive = true);

                    break;

            }

        }

        initializeTraitCard(bonus1, type1);
        initializeTraitCard(bonus2, type2);
        initializeTraitCard(bonus3, type3);

    }

    public void ActiveOption(int level)
    {
        bisFirstAnim = false;
        SelectbackGround.gameObject.SetActive(true);
        Company.rectTransform.anchoredPosition = new Vector2(-30,0);
        Brush.rectTransform.anchoredPosition = new Vector2(-30,0);
        Medicine.rectTransform.anchoredPosition = new Vector2(-30,0);

        OptionTexts[2].text = string.Format("무작워 {0}등급 약을\n얻습니다", level);

        Company.gameObject.SetActive(true);
        Brush.gameObject.SetActive(true);
        Medicine.gameObject.SetActive(true);

        Company.rectTransform.DOAnchorPos(new Vector2(-408, 90), 0.5f);
        Brush.rectTransform.DOAnchorPos(new Vector2(-30, 90), 0.5f);
        Medicine.rectTransform.DOAnchorPos(new Vector2(348, 90), 0.5f).OnComplete(() => bisFirstAnim = true); 
      
    }

    public void Selection(int option)
    {
        if (bisFirstAnim)
        {
            inventory.secondOption(option);
            bisFirstAnim = false;
           
        } 
        else if(bisSecondAnim)
        {            
            inventory.ThirdOption(option);
            bisSecondAnim = false;
            SelectbackGround.gameObject.SetActive(false);
        }


    }

    public void SelectOption(int i1, int i2, int i3)
    {
        string s1 = "";
        string s2 = "";
        string s3 = "";
        

        string TypeDevide(int num)
        {
            string result = "";
            switch (num)
            {
                case 1:
                    result = "빨강";
                    break;
                case 2:
                    result = "초록";
                    break;
                case 3:
                    result = "파랑";
                    break;
                case 4:
                    result = "보라";
                    break;
                case 5:
                    result = "노랑";
                    break;
                case 6:
                    result = "검정";
                    break;
            }

            return result;
        }
        s1 = TypeDevide(i1);
        s2 = TypeDevide(i2);
        s3 = TypeDevide(i3);

        Icons[0].sprite = resourceManager.I_IconDictionary[100 + i1];
        Icons[1].sprite = resourceManager.I_IconDictionary[100 + i2];
        Icons[2].sprite = resourceManager.I_IconDictionary[100 + i3];
        
        OptionTexts[0].text = string.Format("{0} 약을 얻습니다",s1);
        OptionTexts[1].text = string.Format("{0} 약을 얻습니다",s2);
        OptionTexts[2].text = string.Format("{0} 약을 얻습니다",s3);
        bisSecondAnim = true;
    }

    public void SelectOption(int i1, int i2)
    {
        string s1 = "";
        string s2 = "";

        string CompDevide(int num)
        {
            string result = "";
            switch (num)
            {
                case 1:
                    result = "안신의";
                    break;
                case 2:
                    result = "리겔의";
                    break;
                case 3:
                    result = "해울의";
                    break;
                case 4:
                    result = "미르의";
                    break;
            }

            return result;
        }
        s1 = CompDevide(i1);
        s2 = CompDevide(i2);

        Icons[0].gameObject.SetActive(false);
        Icons[1].gameObject.SetActive(false);
        Icons[2].gameObject.SetActive(false);

        Medicine.gameObject.SetActive(false);

        Brush.rectTransform.DOAnchorPos(new Vector2(260,90),0.3f);
        Company.rectTransform.DOAnchorPos(new Vector2(-330, 90), 0.3f).OnComplete(() => bisSecondAnim = true); 
       

        OptionTexts[0].text = string.Format("{0} 약을 얻습니다", s1);
        OptionTexts[1].text = string.Format("{0} 약을 얻습니다", s2);
        

    }





    public void SetMedicineBonus(int result)
    {
        Item item;
        BackGroundImage.gameObject.SetActive(true);
        item = resourceManager.ItemDictionary[result];
        ItemSlotImage.sprite = item.ItemSprite;
        NameText.text = "";
        NameText.gameObject.SetActive(false);
        
        for(int i=0; i<4;i++)
        {
            StartSlots[i].gameObject.SetActive(false);
        }

        StartCoroutine(Puzzle(item.itemcode/1000 , item.ItemName));
    }

    public void SetEssenceBonus(int result, int bonus1, int bonus2, int bonus3, string type1, string type2, string type3)
    {
        Sprite sprite;
        BackGroundImage.gameObject.SetActive(true);
        sprite = resourceManager.I_NimrodDictionary[result];
        ItemSlotImage.sprite = sprite;
        NameText.text = "";
        NameText.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            StartSlots[i].gameObject.SetActive(false);
        }
        StartCoroutine(Puzzle(bonus1,bonus2,bonus3,type1,type2,type3,resourceManager.NimrodDictionary[result].Name));
    }


    public void quitButton()
    {
        BackGroundImage.gameObject.SetActive(false);
        ItemSlotImage.gameObject.SetActive(false);
        ItemFrame.gameObject.SetActive(false);
        NameText.gameObject.SetActive(false);
        BonusText.gameObject.SetActive(false);
        BonusImage.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        dissolveSlide = 1;
        NameText.text = "";

    }

    public void Bonus()
    {
        bisBonus = false;
    }

    public void Bonus(Sprite sprite)
    {
        bisBonus = true;
        bonusSprite = sprite;
    }

}
