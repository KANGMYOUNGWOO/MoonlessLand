using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DiseaseSlot : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{

    [SerializeField] private Image DiseaseImage;
    [SerializeField] private Image BackGroundImage;

    [SerializeField] private Animator B1;
    [SerializeField] private Animator B2;
    [SerializeField] private Animator B3;


    private Sequence InfectionSequence;
    private Sequence AnimSeq1;
    private Sequence AnimSeq2;
    private Sequence AnimSeq3;
    
    private Color newColor;

    public DIseaseTab diseaseTab { get; set; }
    public Sprite DiseaseSprite { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("DiseaseSlot")) diseaseTab.ActiveExplain(int.Parse(transform.name));
        else diseaseTab.gameObject.SetActive(false);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
       // diseaseTab.DeactiveExplain();
    }
    

    public void SetImage(Sprite sprite)
    {
        DiseaseImage.sprite = sprite;
        DiseaseImage.color = new Color(255, 255, 255, 255);
        BackGroundImage.color = newColor;
    }
    public void SetImage()
    {
        BackGroundImage.color = Color.white;
        DiseaseImage.color = new Color(255, 255, 255, 0);
    }

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#9A0026", out newColor);      
    }


    public void Infected()
    {
        InfectionSequence = DOTween.Sequence().OnStart(()
              =>
        {

            BackGroundImage.color = Color.white;

            B1.speed = 0;
            B2.speed = 0;
            B3.speed = 0;

            B1.gameObject.SetActive(false);
            B2.gameObject.SetActive(false);
            B3.gameObject.SetActive(false);

            B1.gameObject.SetActive(true);
            B1.speed = 1;
            B1.Play("blood", -1, 0f);
            
        });
        AnimSeq1 = DOTween.Sequence().OnStart(()
           => {

               B2.gameObject.SetActive(true);
               B2.speed = 1;
               B2.Play("blood", -1, 0f);
              
           }).SetDelay(0.2f);
        AnimSeq2 = DOTween.Sequence().OnStart(()
          => {
              B3.gameObject.SetActive(true);
              B3.speed = 1;
              B3.Play("blood", -1, 0f);
              
          }).SetDelay(0.4f);
        AnimSeq3 = DOTween.Sequence().OnStart(()
            =>
        {
            BackGroundImage.DOColor(newColor, 0.5f).From(Color.white);

        }).SetDelay(0.6f).OnComplete(ImageChange);


        InfectionSequence.Append(AnimSeq1);
        InfectionSequence.Append(AnimSeq2);
        InfectionSequence.Append(AnimSeq3);

        InfectionSequence.Play();

    }

    private void ImageChange()
    {
        B1.gameObject.SetActive(false);
        B2.gameObject.SetActive(false);
        B3.gameObject.SetActive(false);
        DiseaseImage.sprite = DiseaseSprite;
        DiseaseImage.DOFade(1, 0.2f).From(0);
        //DiseaseImage.sprite = sprite;
    }
    

    



}
