using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ActionCard : MonoBehaviour, IDragHandler, IEndDragHandler, IRecyclableGameObject
{

  
    #region Variables

    [SerializeField] private Image CardImage;
    [SerializeField] private Image CardGrade;
    [SerializeField] private Image CardTestIcon;
    [SerializeField] private Image CardDamageType;
    [SerializeField] private Image CardLock;
    [SerializeField] private TextMeshProUGUI CardName;
   
    [SerializeField] private TextMeshProUGUI CardExplain;
    [SerializeField] private GameObject CardFire;

    [SerializeField] private Image DragImage;
    [SerializeField] private Image DragGrade;
    [SerializeField] private Image DragLock;
    [SerializeField] private Image DragTestIcon;
    [SerializeField] private Image DragDamageType;
    [SerializeField] private TextMeshProUGUI DragName;
    
    [SerializeField] private TextMeshProUGUI DragExplain;
    [SerializeField] private GameObject DragFire;

   

    [SerializeField] private GameObject OriginCard;
    [SerializeField] private GameObject DragCard;

    public ActioncardController actionCardCon { get; set; }
    public LogicManager logicManager { get; set; }
    public int index;
    public Canvas canvas { get; set; }
    public bool isActive { get; set; }
    private int type;
    private bool bisGrade;
    private bool bisTest;
    private bool bisType;

    private Vector2 DragOriginPos;
    private bool bisDown;
    private bool bisEndDraw;

    #endregion


    #region IPointer



    public void OnPointerDown(PointerEventData data)
    {
        if (!bisEndDraw) return;

        if (!bisDown)
        {
            /*
            Fire.gameObject.SetActive(false);
            //Cost.gameObject.SetActive(false);
            CoolTime.gameObject.SetActive(false);
            CostText.gameObject.SetActive(false);
            NameText.gameObject.SetActive(false);
            ExplainText.gameObject.SetActive(false);
            CoolTimeText.gameObject.SetActive(false);
            TagText.gameObject.SetActive(false);

            OriginImage.gameObject.SetActive(false);

            BackGround.color = Color.black;
            DragRect.anchoredPosition = DragOriginPos;
            DragImage.SetActive(true);
            DragImage.transform.parent = canvas.transform;
            isActive = false;
            bisDown = true;
            */

            OriginCard.SetActive(false);            
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            DragCard.SetActive(true);
            DragCard.transform.parent = canvas.transform;
            transform.localScale = new Vector3(1, 1, 1);
            isActive = false;
            bisDown = true;

        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (bisDown)
        {
            switch(type)
            {
                case 0:
                    if (data.position.y < 880f)
                    {
                        BackToOriginPos();
                    }
                    else
                    {
                        SetSpell();
                    }
                    break;

                case 1:

                    if (data.pointerCurrentRaycast.gameObject.CompareTag("ActioncardTab"))
                    {
                        DragCard.transform.parent = this.transform;
                        DragCard.transform.localScale = new Vector3(1, 1, 1);
                        DragImage.rectTransform.anchoredPosition = DragOriginPos;
                        DragCard.gameObject.SetActive(false);

                        logicManager.ButtonAction(type, index);
                        //logicManager
                    }
                    else BackToOriginPos();
                    break;

                case 4:
                    if (data.position.y < 880f)
                    {
                        BackToOriginPos();
                    }
                    else
                    {
                        SetSpell();
                    }
                    break;
            }



           

        }
    }



    public void OnDrag(PointerEventData data)
    {
        // DragImage.transform.position = data.position;
        DragImage.rectTransform.anchoredPosition = new Vector2(data.position.x - 600, data.position.y - 1000);
    }


    public void OnEndDrag(PointerEventData data)
    {
        /*
        if (bisDown)
        {
            if (data.position.y < 880f)
            {
                BackToOriginPos();
            }
            else
            {
                Debug.Log(type);
                switch (type)
                {
                    
                    case 0:
                        SetSpell();
                        break;
                    case 1:

                        if (data.pointerCurrentRaycast.gameObject.CompareTag("ActioncardTab"))
                        {
                            DragCard.transform.parent = this.transform;
                            DragCard.transform.localScale = new Vector3(1, 1, 1);
                            logicManager.ButtonAction(type, index);
                            //logicManager
                        }
                        break;
                }
               
            }

        }
        */
    }
    #endregion

    #region DOTWeen

    private void BackToOriginPos()
    {
        DragCard.transform.parent = this.transform;
        DragCard.transform.localScale = new Vector3(1,1,1);
        DragImage.rectTransform.DOAnchorPos(DragOriginPos, 0.4f).SetEase(Ease.InOutCirc).OnComplete(SetActiveOrigin);
    }

    private void NDT_BackToOriginPos()
    {
        /*
        DragImage.transform.parent = this.transform;
        DragRect.anchoredPosition = DragOriginPos;
        OriginImage.SetActive(true);
        DragImage.SetActive(false);

        //Fire.gameObject.SetActive(true);
        // Cost.gameObject.SetActive(true);
        CostText.gameObject.SetActive(true);
        NameText.gameObject.SetActive(true);
        CoolTimeText.gameObject.SetActive(true);
        ExplainText.gameObject.SetActive(true);
        TagText.gameObject.SetActive(true);

     


        BackGround.color = Color.white;

        bisDown = false;

        logicManager.ButtonAction(type, index);
        */
        DragCard.transform.parent = this.transform;
        DragImage.rectTransform.anchoredPosition = DragOriginPos;
        OriginCard.SetActive(true);
        DragCard.SetActive(false);

        CardName.gameObject.SetActive(true);
      
        CardExplain.gameObject.SetActive(true);
        //CardGrade.gameObject.SetActive(true);
        CardGrade.gameObject.SetActive(bisGrade);
        CardDamageType.gameObject.SetActive(bisType);
        CardTestIcon.gameObject.SetActive(bisTest);

        CardImage.color = Color.white;
        bisDown = false;
        logicManager.ButtonAction(type, index);

    }


    private void SetSpell()
    {
        bisDown = false;
        actionCardCon.stunAll(index);
        if (logicManager.CardInspection(type, index))
        {
            DragImage.rectTransform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).SetLoops(3, LoopType.Yoyo).From(new Vector3(1.0f, 1.0f, 1.0f)).OnComplete(NDT_BackToOriginPos);
        }
        else
        {
            actionCardCon.EndStunAll();

            DragCard.transform.parent = this.transform;
            
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            OriginCard.SetActive(true);
            DragCard.SetActive(false);
            CardName.gameObject.SetActive(true);
           
            CardExplain.gameObject.SetActive(true);
            CardGrade.gameObject.SetActive(bisGrade);
            CardDamageType.gameObject.SetActive(bisType);
            CardTestIcon.gameObject.SetActive(bisTest);

            CardImage.color = Color.white;
            bisDown = false;        
        }
        //DragRect.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).SetLoops(3,LoopType.Yoyo).From(new Vector3(1.0f,1.0f,1.0f)).OnComplete(NDT_BackToOriginPos);

    }


    private void SetActiveOrigin()
    {
        bisDown = false;
        OriginCard.SetActive(true);
        DragCard.SetActive(false);
        // Cost.gameObject.SetActive(true);
        //if (biscost) CostText.gameObject.SetActive(true);
        CardName.gameObject.SetActive(true);
        //if (bisCool) CoolTimeText.gameObject.SetActive(true);
        CardExplain.gameObject.SetActive(true);
      
        CardImage.color = Color.white;
        if (bisGrade) CardGrade.gameObject.SetActive(true);
    }


    public void AdjustIndex(int num)
    {
        if (num < index) index -= 1;
       

    }

    #endregion

    #region 朝球持失
    public void InitializeCard(Sprite backGround, Sprite grade, string name, string Explain, int type, int costText, int coolTime, string tag, bool isFire, int option)
    {
        #region
        /*
        BackGround.sprite = backGround;
        DragBack.sprite = backGround;
        Cost.sprite = cost;
        DragCost.sprite = cost;
        NameText.text = name;
        DragNameText.text = name;
        ExplainText.text = Explain;
        DragExplainText.text = Explain;
        TagText.text = tag;
        DragTagText.text = tag;
      
        this.type = type;
        //Cost.gameObject.SetActive(costText > 0);
        CostText.text = costText > 0 ? costText.ToString() : " ";
        //DragCost.gameObject.SetActive(costText > 0);
        DragCostText.text = costText > 0 ? costText.ToString() : " ";

        CoolTime.gameObject.SetActive(coolTime > 0);
        Locked.gameObject.SetActive(false);
        DragLocked.gameObject.SetActive(false);
        DragCoolTime.gameObject.SetActive(coolTime > 0);
        CoolTimeText.text = coolTime > 0 ? coolTime.ToString() : " ";
        DragCoolTimeText.text = coolTime > 0 ? coolTime.ToString() : " ";

        Fire.gameObject.SetActive(false);

        biscost = costText > 0;
        bisCool = coolTime > 0;
        bisFire = isFire;

        OriginImage.SetActive(false);
        if (DragRect == null)
        {
            DragRect = DragImage.GetComponent<RectTransform>();
            DragOriginPos = DragRect.anchoredPosition;

        }

        DragImage.SetActive(true);
        bisEndDraw = false;
        DragRect.DOAnchorPos(DragOriginPos, 0.3f).SetDelay(2 * (option + 0.1f) + index * 0.1f).OnComplete(EndDraw).From(new Vector2(1136, DragOriginPos.y)).SetEase(Ease.Flash);


        // DragRect.anchoredPosition = DragOriginPos;
        // DragImage.SetActive(false);
        */
        #endregion
       
    }


    public void InitializeCard(Sprite cardImage, Sprite grade, Sprite testIcon, Sprite typeicon, string name ,string explain,int type, int option)
    {
        CardImage.sprite = cardImage;
        CardGrade.sprite = grade;
        CardTestIcon.sprite = testIcon;
        CardDamageType.sprite = typeicon;
        CardName.text = name;
      
        CardExplain.text = explain;

        DragImage.sprite = cardImage;
        DragGrade.sprite = grade;
        DragTestIcon.sprite = testIcon;
        DragDamageType.sprite = typeicon;
        DragName.text = name;
      
        DragExplain.text = explain;

        this.type = type;

        bisGrade = (grade == null) ? false : true;
        CardGrade.gameObject.SetActive(bisGrade);
        DragGrade.gameObject.SetActive(bisGrade);
        bisTest = (testIcon == null) ? false : true;
        CardTestIcon.gameObject.SetActive(bisTest);
        DragTestIcon.gameObject.SetActive(bisTest);
        bisType = (typeicon == null) ? false : true;
        CardDamageType.gameObject.SetActive(bisType);
        DragDamageType.gameObject.SetActive(bisType);

        CardImage.gameObject.SetActive(false);
        DragImage.gameObject.SetActive(true);
        DragOriginPos = DragImage.rectTransform.anchoredPosition;
        DragImage.transform.localScale = new Vector3(1, 1, 1);
        DragImage.rectTransform.DOAnchorPos(DragOriginPos, 0.3f).SetDelay(2 * (option + 0.1f) + index * 0.1f).OnComplete(EndDraw).From(new Vector2(1136, DragOriginPos.y)).SetEase(Ease.Flash);
    }

    #endregion

    #region で

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);


        EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
        entry_PointerUp.eventID = EventTriggerType.PointerUp;
        entry_PointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerUp);
        

        bisDown = false;
        // DragRect = DragImage.GetComponent<RectTransform>();
        // DragOriginPos = DragRect.anchoredPosition;





    }

    #endregion

    public void CoolTimed()
    {
        /*
        Locked.gameObject.SetActive(true);
        DragLocked.gameObject.SetActive(true);
        DragLocked.DOFade(1, 0.5f).SetEase(Ease.OutElastic).From(0);
        Locked.DOFade(1, 0.5f).SetEase(Ease.OutElastic).From(0);
        */
    }

    public void Cooling(int cool , int maxcool = 1)
    {/*
        if(cool == 0)
        {
            CoolTimeText.text = maxcool.ToString();
            Locked.DOFade(0, 0.4f).SetEase(Ease.InElastic).From(1).OnComplete(()=>Locked.gameObject.SetActive(false));            
            DragLocked.DOFade(0, 0.4f).SetEase(Ease.InElastic).From(1).OnComplete(()=>DragLocked.gameObject.SetActive(false));
            
        }

        CoolTimeText.text = cool.ToString();
        */
    }

    



    private void OnDisable()
    {
        isActive = false;
    }

    public void Stun()
    {
        bisEndDraw = false;
    }


    public void EndStun()
    {
        bisEndDraw = true;
    }

    private void EndDraw()
    {
        /*
        DragRect.anchoredPosition = DragOriginPos;
        OriginImage.SetActive(true);
        DragImage.SetActive(false);
        
        BackGround.DOFade(1, 0.01f);
        if (biscost)
        {
            Cost.DOFade(1, 0.01f);
            CostText.DOFade(1, 0.01f);

        }
        if (bisCool)
        {
            CoolTime.DOFade(1, 0.01f);
            CoolTimeText.DOFade(1, 0.01f);
        }
        Fire.SetActive(bisFire);
        NameText.gameObject.SetActive(true);
        ExplainText.gameObject.SetActive(true);
        bisEndDraw = true;
        */

        DragImage.rectTransform.anchoredPosition = DragOriginPos;
        OriginCard.SetActive(true);
        DragCard.SetActive(false);
        CardImage.DOFade(1, 0.01f);
        if(bisGrade)
        {
            CardGrade.DOFade(1, 0.01f);
        }
        CardName.gameObject.SetActive(true);
      
        CardExplain.gameObject.SetActive(true);
        bisEndDraw = true;
    }

    public void ActiveFire(bool bisf)
    {/*
        bisFire = bisf;
        Fire.gameObject.SetActive(bisf);
        */
    }

   
}
