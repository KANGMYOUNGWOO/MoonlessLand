using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    public Sprite sprite { get; set; }
 
    public int Count;


    public int slotnum = 0;

    [SerializeField] private Sprite empty;
    [SerializeField] private TextMeshProUGUI CountText;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Image DragImage;
    [SerializeField] private GameObject NameTagObject;
    [SerializeField] private GameObject SelectObject;
    [SerializeField] private List<GameObject> Stars = new List<GameObject>();

    private RectTransform DragRect;
    private float clickTime = 0f;
    private bool bisClicked = false;

    public Inventory inventory { get; set; }
    public Canvas canvas { get; set; }


    private bool bisDown = false;


    public Vector2 OriginPos;

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

        EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
        entry_PointerUp.eventID = EventTriggerType.PointerUp;
        entry_PointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerUp);


    }

    private void Start()
    {
        
        DragRect = DragImage.GetComponent<RectTransform>();
        SelectObject.SetActive(false);
        OriginPos = DragRect.anchoredPosition;
    }

    void OnPointerClick(PointerEventData data)
    {

     
        if (Count != 0)
        {
            SelectObject.SetActive(true);
           
            inventory.SlotClickAction(slotnum);
            
        }
        
        
    }

   public void OnPointerUp(PointerEventData data)
    {
        if (bisDown)
        {
          

            if (data.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
            {
                int Afterslotnum = int.Parse(data.pointerCurrentRaycast.gameObject.name);
                inventory.SlotChange(slotnum, Afterslotnum);
                bisDown = false;
                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
                //DragImage.gameObject.SetActive(false);

            }
            else if (data.pointerCurrentRaycast.gameObject.CompareTag("CombineSlot"))
            {

                inventory.SetCombineSlot(slotnum);

                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
            }
            else
            {
               
                NameText.text = "";
                NameTagObject.SetActive(true);
                CountText.gameObject.SetActive(true);
                ItemImage.sprite = sprite;

                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
                //DragImage.gameObject.SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    //이거 고쳐야함 기억해 놔라~~~
                    Stars[i].SetActive(true);
                }
                bisDown = false;
            }


            bisDown = false;
            SelectObject.SetActive(false);
        }
        
    }

    public void OnPointerDown(PointerEventData data)
    {
       
        if (Count == 0) return;
       
        if (!bisDown)
        {
            //SelectObject.SetActive(false);
            NameText.text = "";
            NameTagObject.SetActive(false);
            CountText.gameObject.SetActive(false);
            ItemImage.sprite = empty;
            DragImage.gameObject.SetActive(true);
            DragImage.transform.parent = canvas.transform;
            DragImage.sprite = sprite;
            //
            for (int i = 0; i < 4; i++)
            {
                Stars[i].SetActive(false);
            }

            bisDown = true;
         

        }

    }
    public void OnDrag(PointerEventData data)
    {
       
        DragRect.anchoredPosition = new Vector2(data.position.x - 600f, data.position.y -950f);
    }
    public void OnEndDrag(PointerEventData data)
    {
        //if (Count == 0) return;

        if (bisDown)
        {
           

            if (data.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
            {
                int Afterslotnum = int.Parse(data.pointerCurrentRaycast.gameObject.name);
                inventory.SlotChange(slotnum,Afterslotnum);
                bisDown = false;
                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
                //DragImage.gameObject.SetActive(false);

            }
            else if(data.pointerCurrentRaycast.gameObject.CompareTag("CombineSlot"))
            {
              
                inventory.SetCombineSlot(slotnum);
                
                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
            }
            else
            {
                
                NameText.text = "";
                NameTagObject.SetActive(true);
                CountText.gameObject.SetActive(true);
                ItemImage.sprite = sprite;
                          
                DragImage.sprite = empty;
                DragImage.transform.parent = this.transform;
                DragRect.anchoredPosition = OriginPos;
                //DragImage.gameObject.SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    //이거 고쳐야함 기억해 놔라~~~
                    Stars[i].SetActive(true);
                }
                bisDown = false;
            }


            bisDown = false;
            SelectObject.SetActive(false);
        }
    }

   
   
    public void SwitchSlotImage(Sprite sprite)
    {
        ItemImage.sprite = sprite;
       
    }

    public void Organization(Item item,int count)
    {
        
        //SelectObject.SetActive(false);
        
        if (item.itemcode != 0)
        {
            NameTagObject.SetActive(true);
            NameText.text = item.ItemName;
            for(int i=0; i<4;i++)
            {
                Stars[i].SetActive(false);
            }
            for(int i=0;i<item.itemcode/1000;i++)
            {
                Stars[i].SetActive(true);
            }

            ItemImage.sprite = item.ItemSprite;
            if (count > 0)
            {
                Count = count;
                CountText.gameObject.SetActive(true);
                CountText.text = count.ToString();
            }
        }
        else if(item.itemcode == 0)
        {
            NameTagObject.SetActive(false);
            NameText.text = "";
            for (int i = 0; i < 4; i++)
            {
                Stars[i].SetActive(false);
            }
            ItemImage.sprite = empty;

            CountText.text = "";
            Count = count;
            CountText.gameObject.SetActive(false);
        }
        


        sprite = item.ItemSprite;
    }
   
    public void Organization(int count)
    {
        Count = count;
        CountText.gameObject.SetActive(true);
        CountText.text = count.ToString();
    }

    public void DeactiveSelectImage()
    {
        SelectObject.SetActive(false);
    }

    private void Update()
    {
        if(bisClicked)
        {
            clickTime += Time.deltaTime;
            if(clickTime > 0.5f)
            {
                bisClicked = false;
                bisDown = true;

            }
        }
    }

    
}
