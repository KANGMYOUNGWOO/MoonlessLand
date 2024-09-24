using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Febucci.UI;

public class EnemyCard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image CardImage;    
    [SerializeField] private TextMeshProUGUI Name; 
    [SerializeField] private TextMeshProUGUI Explain;
    [SerializeField] private RectTransform NameRect;
    [SerializeField] private RectTransform ExplainRect;
    [SerializeField] private Image TypeImage;
    [SerializeField] private TextAnimatorPlayer DamageText;

    [SerializeField] private Animator BloodAnimator;

   
    private Color InActvieColor;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#959595", out InActvieColor);
        BloodAnimator.gameObject.SetActive(false);
    }

    public void ChangeState(int index, bool battle)
    {

        float changSpeed = battle ? 0.4f : 0.2f;

        switch (index)
        {

            case -1:
                CardImage.gameObject.SetActive(true);
                CardImage.rectTransform.DOAnchorPos(new Vector2(-350, 213.77f), changSpeed).From(new Vector2(-700,213.77f));
                break;


            case 0:
                CardImage.gameObject.SetActive(false);
                //Name.gameObject.SetActive(false);
                //Explain.gameObject.SetActive(false);
                break;


            case 1:
                CardImage.gameObject.SetActive(true);
                Name.gameObject.SetActive(true);
                Explain.gameObject.SetActive(true);
                CardImage.rectTransform.DOAnchorPos(new Vector2(-350,213.77f), changSpeed);
                CardImage.rectTransform.DOSizeDelta(new Vector2(213.2f, 305.1f), changSpeed).OnComplete(() => {
                    CardImage.color = InActvieColor;
                    TypeImage.color = InActvieColor;
                });
                TypeImage.rectTransform.anchoredPosition = new Vector2(-1.37f,45.3f);
                TypeImage.rectTransform.sizeDelta = new Vector2(110.5f,100);
                NameRect.sizeDelta = new Vector2(156.4f,38.4f);
                ExplainRect.sizeDelta = new Vector2(169.6f,97.7f);
           
                Name.fontSize = 21;
                Explain.fontSize = 20;
               
                NameRect.anchoredPosition = new Vector2(-0.7f,123.8f);
                ExplainRect.anchoredPosition = new Vector2(-0.4f,-84.4f);
                //CardImage.rectTransform.Do
                break;

            case 2:
                CardImage.gameObject.SetActive(true);
                Name.gameObject.SetActive(true);
                Explain.gameObject.SetActive(true);
                CardImage.rectTransform.DOAnchorPos(new Vector2(0, 213.77f), changSpeed);
                CardImage.rectTransform.DOSizeDelta(new Vector2(304.6f, 435.8f), changSpeed).OnComplete(() => {
                    CardImage.color = Color.white;
                    TypeImage.color = Color.white;
                });
                TypeImage.rectTransform.anchoredPosition = new Vector2(-1.37f,68);
                TypeImage.rectTransform.sizeDelta = new Vector2(153.52f,138.97f);
               
                Name.fontSize = 31;
                Explain.fontSize = 30;
            
                NameRect.sizeDelta = new Vector2(225.5f, 44.8f);
                ExplainRect.sizeDelta = new Vector2(253.6f, 119.6f);
                NameRect.anchoredPosition = new Vector2(0,175.59f);
                ExplainRect.anchoredPosition = new Vector2(1.4f,-134f);
                break;

            case 3:
                CardImage.gameObject.SetActive(true);
                Name.gameObject.SetActive(true);
                Explain.gameObject.SetActive(true);
                CardImage.rectTransform.DOAnchorPos(new Vector2(350, 213.77f), changSpeed);
                CardImage.rectTransform.DOSizeDelta(new Vector2(213.2f, 305.1f), changSpeed).OnComplete(() => {
                    CardImage.color = InActvieColor;
                    TypeImage.color = InActvieColor;
                });
                TypeImage.rectTransform.anchoredPosition = new Vector2(-1.37f, 45.3f);
                TypeImage.rectTransform.sizeDelta = new Vector2(110.5f, 100);
                Name.fontSize = 21;
                Explain.fontSize = 20;
             
                NameRect.sizeDelta = new Vector2(156.4f, 38.4f);
                ExplainRect.sizeDelta = new Vector2(169.6f, 97.7f);
                NameRect.anchoredPosition = new Vector2(-0.7f, 123.8f);
                ExplainRect.anchoredPosition = new Vector2(-0.4f, -84.4f);
                break;

            case 4:
                CardImage.gameObject.SetActive(true);
                CardImage.rectTransform.DOAnchorPos(new Vector2(350, 213.77f), 0.2f).From(new Vector2(700,213.77f));
                break;

        }


    }

    public void SetPosition()
    {
        CardImage.rectTransform.anchoredPosition = new Vector2(-80f, 336f);
       
    }

    public void ResetPosition()
    {
        CardImage.rectTransform.anchoredPosition = new Vector2(0, 213.77f);
    }


    public void ActionMove(int result , int damage)
    {

        switch (result)
        {
            case 1:
                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 1019), 1.4f).From(new Vector2(-80, 336f)).SetEase(Ease.InCubic).OnComplete(() => {

                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 321), 0.2f).SetEase(Ease.OutBounce).SetDelay(0.2f).OnComplete(()=>
                    {
                        CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 826), 0.1f).SetEase(Ease.Flash);
                        CardImage.DOColor(Color.gray, 0.35f).OnComplete(() =>
                        {
                            BloodAnimator.gameObject.SetActive(true);
                            BloodAnimator.Play("Damage", -1);                      
                            DamageText.ShowText(damage.ToString());
                        });
                        CardImage.DOColor(Color.gray, 0.8f).OnComplete(() => {

                            BloodAnimator.gameObject.SetActive(false);
                            DamageText.StartDisappearingText();

                        });
                    });
                    

                });
                break;

            case 0:

                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 1019), 1.4f).From(new Vector2(-80, 336f)).SetEase(Ease.InCubic).OnComplete(() => {

                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 321), 0.2f).SetEase(Ease.OutBounce).SetDelay(0.2f);
                    CardImage.DOColor(Color.white, 0.35f).OnComplete(() => {
                      
                        BloodAnimator.gameObject.SetActive(true);
                        BloodAnimator.Play("Damage", -1);
                        // DamageText.gameObject.SetActive(true);
                        DamageText.ShowText(damage.ToString());
                    });
                    CardImage.DOColor(Color.white, 0.8f).OnComplete(() => {
                       
                        BloodAnimator.gameObject.SetActive(false);
                        DamageText.StartDisappearingText();

                    });
                });

                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 336), 0.4f).SetDelay(2.0f).SetEase(Ease.InExpo);
                break;

            case -1:
                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 1019), 1.4f).From(new Vector2(-80, 336f)).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 321), 0.2f).SetEase(Ease.OutBounce).SetDelay(0.2f).OnComplete(() => 
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -117), 0.1f).SetEase(Ease.Flash));


                });

                break;

        }

       
        //CardImage.rectTransform.DOAnchorPos(new Vector2(0, 285f), 0.6f).SetEase(Ease.InOutSine).OnComplete(() =>
        //CardImage.rectTransform.DOAnchorPos(new Vector2(0, 108f), 0.1f).SetEase(Ease.Flash)); 
    }



    public void SetEnemyCard(Sprite damageType, string name, string explain)
    {
        TypeImage.sprite = damageType;
        Name.text = name;
        Explain.text = explain;
       
    }

   
}
