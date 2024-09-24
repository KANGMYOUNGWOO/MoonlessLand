using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using FronkonGames.SpritesMojo;
using Febucci.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private Image CardImage;
    [SerializeField] private TextMeshProUGUI Explain;
    [SerializeField] private TextMeshProUGUI Name; 
    [SerializeField] private Image           TestType;
    [SerializeField] private Image           DamageType;
    [SerializeField] private Image           Grade;
    [SerializeField] private Animator        HitAnimator;
    [SerializeField] private Animator        BloodAnimator;
    [SerializeField] private TextAnimatorPlayer DamageText;
    private Color InActvieColor;
    private Color HoloColor;

  

    private Material CrashMat;
    private Material CardSetMaterial;
    private UIManager uiManager;

  

    // Start is called before the first frame update
    void Start()
    {      
        ColorUtility.TryParseHtmlString("#959595", out InActvieColor);
        ColorUtility.TryParseHtmlString("#94F5FA ", out HoloColor);

        CrashMat = Edge.CreateMaterial();

        CardSetMaterial = Hologram.CreateMaterial();
        Hologram.Distortion.Set(CardSetMaterial,3.45f);
        Hologram.BlinkStrength.Set(CardSetMaterial,0.05f);
        Hologram.ScanlineStrength.Set(CardSetMaterial,0.9f);
        Hologram.ScanlineCount.Set(CardSetMaterial,15f);
        Hologram.ScanlineSpeed.Set(CardSetMaterial,7);


        Edge.Tint.Set(CrashMat,Color.white);
        HitAnimator.gameObject.SetActive(false);
        BloodAnimator.gameObject.SetActive(false);


        uiManager = GameManager.GetManagerClass<UIManager>();
      

    }

    public void SetPlayerCard(Sprite Card, Sprite damageType, Sprite testType, Sprite grade, string name, string explain)
    {
       

        CardImage.sprite = Card;
        DamageType.gameObject.SetActive(true);
        TestType.gameObject.SetActive(true);
        Grade.gameObject.SetActive(true);
        Name.gameObject.SetActive(true);
        Explain.gameObject.SetActive(true);
       

        DamageType.sprite = damageType;
        TestType.sprite = testType;
        Grade.sprite = grade;
        Name.text = name;
        Explain.text = explain;

        StartCoroutine(holoRoutine());

        IEnumerator holoRoutine()
        {
            float value = 1;
            WaitForSeconds wait = new WaitForSeconds(0.01f);

            CardImage.material = CardSetMaterial;
            Grade.material = CardSetMaterial;
            DamageType.material = CardSetMaterial;
            TestType.material = CardSetMaterial;

            while(value > 0.3f)
            {
                SpriteMojo.Amount.Set(CardSetMaterial,value);
                value -= 0.01f;
                yield return wait;
            }

            CardImage.material = null;

            /*
            CardImage.material = null;
            Grade.material = null;
            DamageType.material = null;
            TestType.material = null;
            */
        }

    }

    public void UnSetPlayerCard(Sprite Card)
    {
        CardImage.sprite = Card;

        DamageType.gameObject.SetActive(false);
        TestType.gameObject.SetActive(false);
        Grade.gameObject.SetActive(false);
        Name.gameObject.SetActive(false);
        Explain.gameObject.SetActive(false);

        CardImage.material = null;
        DamageType.material = null;
        Grade.material = null;
        TestType.material = null;

    }


    public void resetPos()
    {
        CardImage.rectTransform.anchoredPosition = new Vector2(0, -236.22f);
    }

    public void ActionMove(int result , int damage)
    {
        Debug.Log(result);
        switch(result)
        {
            case 1:

                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -707f), 1.4f).SetEase(Ease.InCubic).From(new Vector2(-80, -114)).OnComplete(() => {
                    CardImage.DOColor(Color.white, 0.35f).OnComplete(() =>
                    {
                        HitAnimator.gameObject.SetActive(true);
                        HitAnimator.Play("CardCrash", -1);
                    });
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -88f), 0.2f).SetDelay(0.2f).OnStart(() => CardImage.material = null).OnComplete(() => {
                        uiManager.CardAction("cameraShake");
                    });

                    //CardImage.rectTransform.DOAnchorPos(new Vector2(-80, 387), 0.1f).SetEase(Ease.Flash);
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -114f), 0.4f).SetDelay(2.0f).SetEase(Ease.InExpo).OnComplete(() => uiManager.battleCard.DuelSequence());
                });


                break;
            case 0: 
                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -707f), 1.4f).SetEase(Ease.InCubic).From(new Vector2(-80, -114)).OnComplete(() => {

                    CardImage.DOColor(Color.white, 0.35f).OnComplete(() => {
                        HitAnimator.gameObject.SetActive(true);
                        HitAnimator.Play("CardCrash", -1);
                        BloodAnimator.gameObject.SetActive(true);
                        BloodAnimator.Play("Damage", -1);
                       // DamageText.gameObject.SetActive(true);
                        DamageText.ShowText(damage.ToString());
                    });                    
                    //CardImage.material = CrashMat;
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -88f), 0.2f).SetDelay(0.2f).OnStart(()=>CardImage.material = null).OnComplete(()=> {
                        uiManager.CardAction("cameraShake");                                            
                    });
                    CardImage.DOColor(Color.white, 0.8f).OnComplete(() => {
                        HitAnimator.gameObject.SetActive(false);
                        BloodAnimator.gameObject.SetActive(false);
                        DamageText.StartDisappearingText();

                    });
                });
                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -114f), 0.4f).SetDelay(2.0f).SetEase(Ease.InExpo).OnComplete(()=>uiManager.battleCard.DuelSequence());

                break;
            case -1:
                CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -707f), 1.4f).SetEase(Ease.InCubic).From(new Vector2(-80, -114)).OnComplete(() => {
                    CardImage.DOColor(Color.white, 0.35f).OnComplete(() =>
                    {
                        HitAnimator.gameObject.SetActive(true);
                        HitAnimator.Play("CardCrash", -1);
                    });
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -88f), 0.2f).SetDelay(0.2f).OnStart(() => CardImage.material = null).OnComplete(() => {
                        uiManager.CardAction("cameraShake");
                    });

                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -548), 0.1f).SetEase(Ease.Flash);
                    CardImage.rectTransform.DOAnchorPos(new Vector2(-80, -114f), 0.4f).SetDelay(2.0f).SetEase(Ease.InExpo).OnComplete(() => uiManager.battleCard.DuelSequence());
                });
                break;
        }

        //CardImage.rectTransform.DOAnchorPos(new Vector2(0,-532),0.6f).OnComplete(()=>
        //CardImage.rectTransform.DOAnchorPos(new Vector2(0, -302), 0.1f));        
    }

   public void SetPosition()
    {
        CardImage.rectTransform.anchoredPosition = new Vector2(-80f, -114f);
    }


    public void ChangeState(int index , bool battle)
    {

        float changSpeed = battle ? 0.4f : 0.2f;

        switch (index)
        {

            case -1:
                CardImage.gameObject.SetActive(true);
                CardImage.raycastTarget = false;
                CardImage.rectTransform.DOAnchorPos(new Vector2(-350, -123.22f), changSpeed).SetEase(Ease.InOutSine).From(new Vector2(-700,-123.22f));
               
                break;

            case 0:
                CardImage.gameObject.SetActive(false);
              
                break;
            case 1:
                
                CardImage.gameObject.SetActive(true);
                CardImage.raycastTarget = false;
                CardImage.rectTransform.DOAnchorPos(new Vector2(-350, -123.22f), changSpeed).SetEase(Ease.InOutSine);
                CardImage.rectTransform.DOSizeDelta(new Vector2(169.13f, 242f), changSpeed).OnComplete(() => {

                    CardImage.color = InActvieColor;
                    Grade.color = InActvieColor;
                    TestType.color = InActvieColor;
                    DamageType.color = InActvieColor;

                    Name.fontSize = 12;
                    Explain.fontSize = 12;
               

                });

                TestType.rectTransform.sizeDelta = new Vector2(31f,43f);
                TestType.rectTransform.anchoredPosition = new Vector2(-68,79.5f);

                Grade.rectTransform.sizeDelta = new Vector2(35.28f,35.28f);
                Grade.rectTransform.anchoredPosition = new Vector2(0,-18.3f);

                DamageType.rectTransform.sizeDelta = new Vector2(85.6f,77.4f);
                DamageType.rectTransform.anchoredPosition = new Vector2(4f,39f);

                Name.rectTransform.sizeDelta = new Vector2(122.72f, 25.05f);
                Name.rectTransform.anchoredPosition = new Vector2(1.23f,95.75f);

                Explain.rectTransform.sizeDelta = new Vector2(137.05f,64.3f);
                Explain.rectTransform.anchoredPosition = new Vector2(-0.63f,-73.12f);

              

               


                break;

            case 2:
                CardImage.gameObject.SetActive(true);
                CardImage.rectTransform.DOAnchorPos(new Vector2(0, -236.22f), changSpeed).SetEase(Ease.InOutSine);
                CardImage.rectTransform.DOSizeDelta(new Vector2(266.54f, 382.75f), changSpeed).OnComplete(()=> {

                    CardImage.color = Color.white;
                    Grade.color = Color.white;
                    TestType.color = Color.white;
                    DamageType.color = Color.white;

                    Name.fontSize = 20;
                    Explain.fontSize = 16;
                  
                    CardImage.raycastTarget = true;
                });

              
                TestType.rectTransform.sizeDelta = new Vector2(49.05f, 68.1f);
                TestType.rectTransform.anchoredPosition = new Vector2(-102.8f, 121f);

                Grade.rectTransform.sizeDelta = new Vector2(56f, 56f);
                Grade.rectTransform.anchoredPosition = new Vector2(0, -24.55f);

                DamageType.rectTransform.sizeDelta = new Vector2(135.86f, 123f);
                DamageType.rectTransform.anchoredPosition = new Vector2(4f, 61f);

                Name.rectTransform.sizeDelta = new Vector2(210.6f, 50f);
                Name.rectTransform.anchoredPosition = new Vector2(0.3f, 153.75f);

                Explain.rectTransform.sizeDelta = new Vector2(216.59f, 64.3f);
                Explain.rectTransform.anchoredPosition = new Vector2(-0.68f, -108.12f);

              
                break;

            case 3:
                CardImage.gameObject.SetActive(true);
                CardImage.raycastTarget = false;
                CardImage.rectTransform.DOAnchorPos(new Vector2(350, -123.22f), changSpeed).SetEase(Ease.InOutSine);
                CardImage.rectTransform.DOSizeDelta(new Vector2(169.13f, 242f), changSpeed).OnComplete(() => {

                    CardImage.color = InActvieColor;
                    Grade.color = InActvieColor;
                    TestType.color = InActvieColor;
                    DamageType.color = InActvieColor;

                    Name.fontSize = 12;
                    Explain.fontSize = 12;
                

                });

                TestType.rectTransform.sizeDelta = new Vector2(31f, 43f);
                TestType.rectTransform.anchoredPosition = new Vector2(-68, 79.5f);

                Grade.rectTransform.sizeDelta = new Vector2(35.28f, 35.28f);
                Grade.rectTransform.anchoredPosition = new Vector2(0, -18.3f);

                DamageType.rectTransform.sizeDelta = new Vector2(85.6f, 77.4f);
                DamageType.rectTransform.anchoredPosition = new Vector2(4f, 39f);

                Name.rectTransform.sizeDelta = new Vector2(122.72f, 25.05f);
                Name.rectTransform.anchoredPosition = new Vector2(1.23f, 95.75f);

                Explain.rectTransform.sizeDelta = new Vector2(137.05f, 64.3f);
                Explain.rectTransform.anchoredPosition = new Vector2(-0.63f, -73.12f);

            

                break;

            case 4:
                CardImage.gameObject.SetActive(true);
                CardImage.raycastTarget = false;
                CardImage.rectTransform.DOAnchorPos(new Vector2(350, -123.22f), changSpeed).SetEase(Ease.InOutSine).From(new Vector2(700,-123.22f));
               
                break;

        }



    }

   
}
