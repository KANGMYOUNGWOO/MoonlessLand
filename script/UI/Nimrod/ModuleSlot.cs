using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModuleSlot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public NimrodMain nimrodMain {get; set;}
    public Sprite     empty      {get; set;}
    public Canvas     canvas { get; set; }


    [SerializeField] private Image ModuleImage;
    [SerializeField] private Image DragImage;


    private RectTransform DragRect;

    private bool bisDown;
    private Vector3 OriginPos;

    public bool bisEmpty = false;
    public int slotnum;

    private void Awake()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerClick = new EventTrigger.Entry();
        entry_PointerClick.eventID = EventTriggerType.PointerClick;
        entry_PointerClick.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerClick);

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
    }

    private void Start()
    {
        DragRect = DragImage.rectTransform;

        OriginPos = DragRect.anchoredPosition;
        DragImage.gameObject.SetActive(false);
    }




    public void OnPointerClick(PointerEventData data)
    {

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!bisEmpty) return;

        if(!bisDown)
        {
            ModuleImage.color = new Color(0, 0, 0, 0);
            DragImage.gameObject.SetActive(true);
            DragImage.sprite = ModuleImage.sprite;
            DragImage.transform.parent = canvas.transform;

        }

        bisDown = true;
    }

    public void InitializeSlot(bool bisEmpty, Sprite sprite = null)
    {
        ModuleImage.color = new Color(255, 255, 255, 255);
        this.bisEmpty = bisEmpty;
        if (bisEmpty)
        {
            ModuleImage.sprite = sprite;
            DragImage.sprite = sprite;
        }
    }


    public void OnDrag(PointerEventData data)
    {
        DragImage.rectTransform.anchoredPosition = new Vector2(data.position.x - 600f, data.position.y - 930f);
    }

    public void OnEndDrag(PointerEventData data)
    { 
        if(bisDown)
        {
          

            if (data.pointerCurrentRaycast.gameObject.CompareTag("ModuleSlot"))
            {
                int Afterslotnum = int.Parse(data.pointerCurrentRaycast.gameObject.name);
                nimrodMain.changeModule(int.Parse(gameObject.name), Afterslotnum);
                
                bisDown = false;
                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
                DragImage.gameObject.SetActive(false);
                //ModuleImage.color = new Color(255, 255, 255, 255);
            }
            else
            {
                bisDown = false;
                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
                DragImage.gameObject.SetActive(false);
                ModuleImage.color = new Color(255, 255, 255, 255);
            }
        }
    }

    


}
