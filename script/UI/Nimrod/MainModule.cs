using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainModule : MonoBehaviour
{
    [SerializeField] private List<Image> cardImages = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> Stats = new List<TextMeshProUGUI>();
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI ModuleName;
    [SerializeField] private int SlotNum;

    public NimrodMain main { get; set; }



    public void InitializeImage(Sprite str, Sprite agi, Sprite exa, Sprite ste ,Sprite icon)
    {
        cardImages[0].sprite = str;
        cardImages[1].sprite = agi;
        cardImages[2].sprite = exa;
        cardImages[3].sprite = ste;

        Icon.sprite = icon;
    }

    public void InitializeStat(string str, string agi, string exa, string ste , string name)
    {
        Stats[0].text   = str;
        Stats[1].text   = agi;
        Stats[2].text   = exa;
        Stats[3].text   = ste;
        ModuleName.text = name;
    }

    private void Awake()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerClick = new EventTrigger.Entry();
        entry_PointerClick.eventID = EventTriggerType.PointerClick;
        entry_PointerClick.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerClick);
    }

    private void OnPointerClick(PointerEventData data)
    {
        main.ChangeToDetail(SlotNum);
    }





}
