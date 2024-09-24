using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;


public class CubeCard : MonoBehaviour, IDragHandler, IEndDragHandler, IRecyclableGameObject
{
    #region Components
    [SerializeField] private TextMeshProUGUI OriginName;
    [SerializeField] private TextMeshProUGUI DragName;

    [SerializeField] private TextMeshProUGUI OriginExplain;
    [SerializeField] private TextMeshProUGUI DragExplain;

    [SerializeField] private Image OriginImage;
    [SerializeField] private Image DragImage;
    #endregion

    #region variables

    private bool bisDown = false;
    private bool bisEndDraw = false;
    public bool isActive { get; set; }

    private Vector2 DragOriginPos;
    #endregion

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
    }

    public void InitalizeCard(string name, string explain)
    {
        OriginName.text = name;
        DragName.text = name;
        OriginExplain.text = explain;
        DragExplain.text = explain;
    }

    private void OnPointerDown(PointerEventData data)
    {
        if (!bisEndDraw) return;
        if (!bisDown)
        {
            OriginImage.gameObject.SetActive(false);
            DragImage.rectTransform.anchoredPosition = DragOriginPos;
            DragImage.gameObject.SetActive(true);
            //DragImage.transform.parent = canvas.transform;
            transform.localScale = new Vector3(1, 1, 1);
            isActive = false;
            bisDown = true;

        }

    }

    private void OnPointerUp(PointerEventData data)
    {

    }

    public void OnDrag(PointerEventData data)
    {
        DragImage.rectTransform.anchoredPosition = new Vector2(data.position.x - 600, data.position.y - 1000);
    }

    public void OnEndDrag(PointerEventData data)
    {


    }
}
