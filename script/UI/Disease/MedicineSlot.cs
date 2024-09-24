using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using FronkonGames.SpritesMojo;
using DG.Tweening;


public class MedicineSlot : MonoBehaviour
{
    [SerializeField] private Image DiseaseImage;
    [SerializeField] private Image DiseaseBackGroundImage;
    [SerializeField] private TextMeshProUGUI DiseaseName;
    [SerializeField] private TextMeshProUGUI DiseaseExplain;
    public MedicineTab medicineTab { get; set; }

   
    private Color newColor;

    private void Awake()
    {

       

       
        ColorUtility.TryParseHtmlString("#9A0026", out newColor);
        //DiseaseBackGroundImage.material = retroMaterial;
        //DiseaseImage.material = retroMaterial;
    }


    private void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
    }

    public void InitializeSlot(Sprite image ,string name, string explain)
    {
        if (name != "")
        {
            DiseaseBackGroundImage.color = newColor;
            DiseaseImage.color = new Color(255, 255, 255, 255);
        }
        else
        {
            DiseaseBackGroundImage.color = Color.white;
            DiseaseImage.color = new Color(255, 255, 255, 0);
        }
        DiseaseImage.sprite = image;
        DiseaseName.text = name;
        DiseaseExplain.text = explain;
    }

    public void OnPointerDown(PointerEventData data)
    {

        if (data.pointerCurrentRaycast.gameObject.CompareTag("MedicineSlot"))
        {
            medicineTab.SelectSlot(int.Parse(gameObject.name));
        }
    }


    public void CureDisease()
    {
        //DiseaseBackGroundImage.material = retroMaterial;
              
        DiseaseName.text = "";
        DiseaseExplain.text = "";
        //DiseaseImage.DOColor(new Color(255, 255, 255, 0), 0.5f).SetDelay(0.4f);
        DiseaseImage.DOFade(0,0.5f).SetDelay(0.2f);
        DiseaseBackGroundImage.DOColor(Color.white, 1f).OnComplete(()=>
        {
            
            medicineTab.EndProduction();
        });
 
            // DiseaseBackGroundImage.material = null;
           
        

    }

}


