using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using FronkonGames.SpritesMojo;
using DG.Tweening;

public class QuizSlot : MonoBehaviour,IRecyclableGameObject, IDragHandler, IEndDragHandler
{
    public bool isActive { get; set; }


    [SerializeField] private Image Icon;
    [SerializeField] private Image DragIcon;
    [SerializeField] private TextMeshProUGUI DragName;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI DragSpecify;
    [SerializeField] private TextMeshProUGUI ItemSpeicfy;
    [SerializeField] private TextMeshProUGUI DragExplain;
    [SerializeField] private TextMeshProUGUI ItemExplain;
    [SerializeField] private List<Image> Stars = new List<Image>();
    [SerializeField] private Image BackGround;
    [SerializeField] private Image DragBackGround;

    [SerializeField] private GameObject OriginObject;
    [SerializeField] private GameObject DragObject;
    
    public Canvas canvas { get; set; }
    public LogicManager logicManager { get; set; }
    public ActioncardController actionCardCon { get; set; }

    private bool bisDown;
    private bool bisEndDraw;
    private Vector3 DragOriginPos;

    private Material DissolveMat;
    
    public int Index { get; set; }
   

    private void Start()
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

        DissolveMat = Dissolve.CreateMaterial();
        Dissolve.Shape.Set(DissolveMat,DissolveShape.Spiral_1);
    }


    public void OnPointerDown(PointerEventData data)
    {       
        if (!bisEndDraw) return;
        if(!bisDown)
        {           
            OriginObject.SetActive(false);
            DragBackGround.rectTransform.anchoredPosition = DragOriginPos;
            DragObject.SetActive(true);
            DragObject.transform.parent = canvas.transform;
            transform.localScale = new Vector3(1, 1, 1);
            isActive = false;
            
            bisDown = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log(data.pointerCurrentRaycast.gameObject.name);
        if (data.pointerCurrentRaycast.gameObject.CompareTag("QuizTab"))
        {
            string optionName = data.pointerCurrentRaycast.gameObject.name;

            //logicManager.QuizAction(optionName,Index);
            StartCoroutine(Action(optionName));
            //logicManager.QuizAction(int.Parse(data.pointerCurrentRaycast.gameObject.name),Level,item);
        }
        else BackToOriginPos();
    }

    IEnumerator Action(string option)
    {
        DragBackGround.material = DissolveMat;
        DragIcon.material = DissolveMat;
        DragName.gameObject.SetActive(false);
        DragSpecify.gameObject.SetActive(false);
        DragExplain.gameObject.SetActive(false);
        WaitForSeconds wait = new WaitForSeconds(0.00001f);
        float dissolveSlide = 0f;
        while(dissolveSlide<0.99f)
        {
            dissolveSlide += 0.011f;
            Dissolve.Slide.Set(DissolveMat,dissolveSlide);
            yield return wait;
        }

        DragObject.transform.parent = this.transform;
        transform.localScale = new Vector3(1, 1, 1);

        DragBackGround.rectTransform.anchoredPosition = DragOriginPos;
        DragBackGround.material = null;
        DragIcon.material = null;
        DragName.gameObject.SetActive(true);
        DragSpecify.gameObject.SetActive(true);
        DragExplain.gameObject.SetActive(true);
        

        OriginObject.gameObject.SetActive(true);
        DragObject.gameObject.SetActive(false);

        logicManager.QuizAction(option, Index);
        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData data)
    {       
        DragBackGround.rectTransform.anchoredPosition = new Vector2(data.position.x - 600, data.position.y - 1000);
    }

    public void OnEndDrag(PointerEventData data)
    {
       
    }

    public void InitializeCard(int index,Item item)
    {

        Icon.sprite = item.ItemSprite;                
        ItemSpeicfy.text = string.Format("{0},{1},{2}", item.specify1, item.specify2, item.specify3);
        ItemName.text = item.ItemName;
        ItemExplain.text = item.ItemExplain;

        DragIcon.sprite = item.ItemSprite;
        DragSpecify.text = string.Format("{0},{1},{2}", item.specify1, item.specify2, item.specify3);
        DragName.text = item.ItemName;
        DragExplain.text = item.ItemExplain;
        
        Index = index;

        for(int i=0;i<4;i++)
        {
            Stars[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < item.itemcode / 1000; i++)
        {
            Stars[i].gameObject.SetActive(true);
        }
        BackGround.gameObject.SetActive(false);
        DragOriginPos = DragBackGround.rectTransform.anchoredPosition;
        DragBackGround.gameObject.SetActive(true);
        bisEndDraw = false;
        DragBackGround.rectTransform.DOAnchorPos(DragOriginPos, 0.3f).SetDelay(index * 0.1f).OnComplete(EndDraw).From(new Vector2(1136, DragOriginPos.y)).SetEase(Ease.Flash);
    }
    private void EndDraw()
    {
        DragBackGround.rectTransform.anchoredPosition = DragOriginPos;
        BackGround.gameObject.SetActive(true);
        DragBackGround.gameObject.SetActive(false);
      
        bisEndDraw = true;

    }

    private void BackToOriginPos()
    {
        DragObject.transform.parent = this.transform;
        DragObject.transform.localScale = new Vector3(1, 1, 1);
        DragBackGround.rectTransform.DOAnchorPos(DragOriginPos,0.4f).SetEase(Ease.InOutCirc).OnComplete(SetActiveOrigin); ;

    }

    private void SetActiveOrigin()
    {
        bisDown = false;
        OriginObject.SetActive(true);
        DragObject.SetActive(false);
        // Cost.gameObject.SetActive(true);
        //if (biscost) CostText.gameObject.SetActive(true);
       
        //if (bisCool) CoolTimeText.gameObject.SetActive(true);
       

      
    }
}
