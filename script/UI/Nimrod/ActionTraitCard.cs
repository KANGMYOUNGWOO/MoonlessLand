using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ActionTraitCard : MonoBehaviour, IDragHandler, IEndDragHandler
{

    #region Vals

    [SerializeField] private Image Cost;
    [SerializeField] private Image BackGround;
    [SerializeField] private Image CoolTime;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI ExplainText;
    [SerializeField] private TextMeshProUGUI CoolTimeText;
    [SerializeField] private Image FixedImage;
    [SerializeField] private Image DragImage;


    private bool bisDown;

    public bool bisFixed { get; set; }
    public bool bisEmpty { get; set; }

   

    public int slotnum;
    public NimrodDetail nimDetail { get; set; }

    public enum CardType {Attack, Guard, Passive};
    public CardType cardType; 


    private Vector2 DragOriginPos;
    public Canvas canvas { get; set; }

   

    #endregion

    public void OnPointerDown(PointerEventData data)
    {
        if (bisEmpty) return;

        if (!bisDown)
        {
            DragImage.gameObject.SetActive(true);
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            DragImage.transform.parent = canvas.transform;
          

            bisDown = true;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (bisEmpty) return;
        DragImage.rectTransform.anchoredPosition = new Vector2(data.position.x - 600, data.position.y - 1000);
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (bisEmpty) return;

        if (data.pointerCurrentRaycast.gameObject.CompareTag("Attack"))
        {
            if (cardType != CardType.Attack) return;
            int Afterslotnum = int.Parse(data.pointerCurrentRaycast.gameObject.name);
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            DragImage.gameObject.SetActive(false);           
            nimDetail.ChangeAttackCard(Afterslotnum,slotnum);

        }
        else if(data.pointerCurrentRaycast.gameObject.CompareTag("Guard"))
        {
            if (cardType != CardType.Guard) return;
            int Afterslotnum = int.Parse(data.pointerCurrentRaycast.gameObject.name);
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            DragImage.gameObject.SetActive(false);
            nimDetail.ChangeGuardCard(Afterslotnum, slotnum);
        }
        else if(data.pointerCurrentRaycast.gameObject.CompareTag("Passive"))
        {
            if (cardType != CardType.Passive) return;
            int Afterslotnum = int.Parse(data.pointerCurrentRaycast.gameObject.name);
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            DragImage.gameObject.SetActive(false);
            nimDetail.ChangePassiveCard(Afterslotnum, slotnum);
        }
        
        
        DragImage.transform.parent = this.transform;
        DragImage.gameObject.SetActive(false);
        bisDown = false;
    }

    public void ChangeFail()
    {
        FixedImage.gameObject.SetActive(bisFixed = false);
    }
    public void ChangeSuccess()
    {
        FixedImage.gameObject.SetActive(bisFixed = true);
    }
    public void InitializeCard(string name, string explain, int cost, int cooltime, Sprite s_card, Sprite s_cost)
    {
        BackGround.sprite = s_card;
        CostText.text = cost.ToString();
        Cost.gameObject.SetActive(cost > 0);
        if (cost > 0) Cost.sprite = s_cost;

        CoolTimeText.text = cooltime.ToString();
        CoolTime.gameObject.SetActive(cooltime > 0);
        CoolTimeText.gameObject.SetActive(cooltime > 0);
        
        NameText.text = name;
        ExplainText.text = explain;

    }

    public void InitializeCard(Sprite sprite)
    {
        BackGround.sprite = sprite;
        Cost.gameObject.SetActive(false);
        CoolTime.gameObject.SetActive(false);
        NameText.text = "";
        CostText.text = "";
        ExplainText.text = "";
    }


        // Start is called before the first frame update
    private void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);

        bisDown = false;

        DragOriginPos = BackGround.rectTransform.anchoredPosition;

    }

   
}
