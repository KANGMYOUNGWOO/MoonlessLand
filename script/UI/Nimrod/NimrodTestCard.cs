using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NimrodTestCard : MonoBehaviour, IDragHandler, IEndDragHandler, IRecyclableGameObject
{
    public bool isActive { get; set; }

    [SerializeField]private Image cardImage; 
    [SerializeField]private Image DragImage;
    public int slotnum;


    public Canvas canvas { get; set; }
    public NimrodDetail nimrodDetail { get; set; }

    private bool bisDown;
    private Vector3 OriginPos;


    void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);

        bisDown = false;
        // DragRect = DragImage.GetComponent<RectTransform>();
        // DragOriginPos = DragRect.anchoredPosition;
    }

    public void initializeTestCard(Sprite sprite, Canvas canvas, int slotnum)
    {
        cardImage.sprite = sprite;
        cardImage.color = new Color(255, 255, 255, 255);
        DragImage.sprite = sprite;
        DragImage.gameObject.SetActive(false);
        this.canvas = canvas;
        this.slotnum = slotnum;
        OriginPos = DragImage.rectTransform.anchoredPosition;

    }

    public void AdjustSlotnum(int index)
    {
        if (slotnum > index) slotnum -= 1;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    #region IPointer

    public void OnDrag(PointerEventData data)
    {        //// DragImage.transform.position = data.position;
       DragImage.rectTransform.anchoredPosition = new Vector2(data.position.x - 600, data.position.y - 1000);
    }


    public void OnPointerDown(PointerEventData data)
    {
        if(!bisDown)
        {
            cardImage.color = new Color(0f,0f,0f,0f);
            DragImage.gameObject.SetActive(true);
            DragImage.transform.parent = canvas.transform;
            bisDown = true;

        }
    }
   
    public void OnEndDrag(PointerEventData data)
    {

        if (bisDown)
        {
            Debug.Log(data.pointerCurrentRaycast.gameObject.name);

            if (data.pointerCurrentRaycast.gameObject.CompareTag("TestCardSlot"))
            {
                string Statname = data.pointerCurrentRaycast.gameObject.name;
                nimrodDetail.EquipTestCard(Statname,slotnum);
                DragImage.transform.parent = this.transform;
                DragImage.rectTransform.anchoredPosition = OriginPos;
                DragImage.gameObject.SetActive(false);
                gameObject.SetActive(false);
               
            }
            else
            {
                DragImage.transform.parent = this.transform;
                DragImage.rectTransform.anchoredPosition = OriginPos;
                DragImage.gameObject.SetActive(false);
                cardImage.color = new Color(255, 255, 255, 255);
               
            }

        }
        bisDown = false;
        
    }



    #endregion
}
