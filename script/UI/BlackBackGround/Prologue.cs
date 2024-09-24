using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using UnityEngine.UI;
using DG.Tweening;
using FronkonGames.SpritesMojo;
using TMPro;


public class Prologue : MonoBehaviour
{
   
    public string article1 { get; set; } = "부러진 발톱이 상처를 파고든다<waitfor=0.8> <?NextArticle> ";
    public string article2 { get; set; } = "발을 디딜때 마다 표정이\n일그러지나 신경쓸 겨를 조차 없다<waitfor=0.8> <?NextArticle> ";
    public string article3 { get; set; } = "사실 온몸이 피투성이가 된 지 오래다<waitfor=0.8> <?NextArticle> ";
    public string article4 { get; set; } = "양말은 피로 눅눅해져 걸음 마다\n진득진득한 즙이 새어나온다<waitfor=0.8> <?NextArticle> ";
    public string article5 { get; set; } = "숨도 끝에 닿아 페는 날숨을\n비명처럼 내지른다<waitfor=0.8> <?NextArticle> ";
    public string article6 { get; set; } = "당장이라도 쓰러질 것 같지만\n놀랍게도 다리만은 끊임없이 움직인다<waitfor=0.8> <?NextArticle> ";
    public string article7 { get; set; } = "살고자 하는 욕망이 나를 달리게 했다 <waitfor=1> <?NextArticle> ";
    public string article8 { get; set; } = "{size}<color=blue>재가</color> <color=yellow>걔</color> <color=blue>야?</color>{/size}";
    public string ar1 { get; set; }      = "{size}<color=green>어 선생님 조순가 뭔가 하는 얘</color>{/size}";
    public string ar2 { get; set; }      = "{size}<color=blue>그럼 진짜 선생님... 그렇게 할 생각인가 봐?</color>{/size}";
    public string ar3 { get; set; }      = "{size}<color=blue>딱 봐도 쟤가 선생님 대신 덤터기 쓴 거잖아.</color>{/size}";
    public string ar4 { get; set; }      = "{size}<color=green> 제발 말조심해.너도 저 꼴 나고 싶어?</color> <waitfor=0.6> <?dialogue> {/size} ";
    public string article9 { get; set; } = "사랑하는 제아에게.\n 해주고 싶은 말이 너무나도 많지만\n 지금은 감상에 빠질 때가 아닌 것 같구나.\n\n";
    public string sr1 { get; set; }      = "내가 표시한 곳으로 가거라.\n경비를 피해가면 끝이 보이지않는 동굴이 나올거야.\n그 굴은 우리와는 전혀 다른 세상과 이어져있단다.\n";
    public string sr2 { get; set; }      = "너를 고칠약은 그곳에서만 구할 수 있어.\n\n하지만 명심하렴\n그곳은 온갖 괴상망측한 질병의 발원지이며\n흉포한 짐승들이 설치는 위험한 곳이란다.\n";
    public string sr3 { get; set; }      = "그곳에서 살아남으려면 숨쉬는것마저도 조심해야할거야.\n너를 그런 곳에 보내는 한심한 어른을 용서해주렴.\n이런 방법말고는 너를 살릴 방법이 떠오르지 않구나.\n\n";
    public string sr4 { get; set; }      = "ps. 동봉한 약이 네 증상을 조금 완화해줄거란다. ";





    [SerializeField] private TextAnimatorPlayer tp1;
    [SerializeField] private TextAnimatorPlayer tp2;
    [SerializeField] private TextAnimatorPlayer tp3;
    [SerializeField] private TextAnimatorPlayer tp4;
    [SerializeField] private TextAnimatorPlayer tp5;
    [SerializeField] private TextAnimatorPlayer tp6;
    [SerializeField] private TextAnimatorPlayer tp7;
    [SerializeField] private Image BackGround;
    [SerializeField] private Image Dialogue;
    [SerializeField] private TextAnimatorPlayer DialogueText;


    [SerializeField] private RectTransform talk1;
    [SerializeField] private RectTransform talk2;
    [SerializeField] private RectTransform talk3;
    [SerializeField] private RectTransform talk4;
    [SerializeField] private RectTransform talk5;

    private TextAnimatorPlayer t1;
    private TextAnimatorPlayer t2;
    private TextAnimatorPlayer t3;
    private TextAnimatorPlayer t4;
    private TextAnimatorPlayer t5;



    [SerializeField] private Image Letter;
    [SerializeField] private TextMeshProUGUI LetterDialogue;
    [SerializeField] private TextMeshProUGUI textmeshT1;
    [SerializeField] private TextMeshProUGUI textmeshT5;
    [SerializeField] private TMP_FontAsset digitalFont;

    [SerializeField] private Image nimrodImage;
    [SerializeField] private Image nimrodName;
    [SerializeField] private TextAnimatorPlayer nimrodText;


    private Material GlitchMat;
    private Material LetterMat;
    private Material nimrodApearMat;
    private Material nimrodGlitchMat;

    private Material shiftMat;
    private Material GlassMaterial;

    private ResourceManager resourceManager;
    private LogicManager logicManager;
    private UIManager uiManager;


    public int articleIndex { get; set; } = 0;
    private int dialogueIndex = 0;
    private bool bisSkiped = false;
    private bool bisEnd = false;
    private Color incolor;
  

    private void Awake()
    {
        tp1.textAnimator.onEvent += PrologueArticle;
        tp2.textAnimator.onEvent += PrologueArticle;
        tp3.textAnimator.onEvent += PrologueArticle;
        tp4.textAnimator.onEvent += PrologueArticle;
        tp5.textAnimator.onEvent += PrologueArticle;
        tp6.textAnimator.onEvent += PrologueArticle;
        tp7.textAnimator.onEvent += PrologueArticle;

        DialogueText.textAnimator.onEvent += OnDialogueEffect;
        
        //LetterDialogue.textAnimator.onEvent += OnDialogueEffect;
        

        GlitchMat = RGBGlitch.CreateMaterial();
        LetterMat = Dissolve.CreateMaterial();
        nimrodApearMat = Dissolve.CreateMaterial();
        nimrodGlitchMat = RGBGlitch.CreateMaterial();
        GlassMaterial = Glass.CreateMaterial();

        RGBGlitch.Amplitude.Set(nimrodGlitchMat,0);
        RGBGlitch.Speed.Set(nimrodGlitchMat,0);
     
        Dissolve.Shape.Set(LetterMat,DissolveShape.Spots);
        Dissolve.Shape.Set(nimrodApearMat,DissolveShape.Mosaic_1);
        Dissolve.Slide.Set(nimrodApearMat, 1f);
        Glass.Distortion.Set(GlassMaterial,0);
        Glass.Refraction.Set(GlassMaterial,0);

        t1 = talk1.GetComponent<TextAnimatorPlayer>();
        t2 = talk2.GetComponent<TextAnimatorPlayer>();
        t3 = talk3.GetComponent<TextAnimatorPlayer>();
        t4 = talk4.GetComponent<TextAnimatorPlayer>();
        t5 = talk5.GetComponent<TextAnimatorPlayer>();


        t5.textAnimator.onEvent += OnDialogueEffect;
        t1.textAnimator.onEvent += OnDialogueEffect;

        article9 += sr1;
        article9 += sr2;
        article9 += sr3;
        article9 += sr4;

        ColorUtility.TryParseHtmlString("#00FFA2", out incolor);

    }

    private void Start()
    {
      resourceManager = GameManager.GetManagerClass<ResourceManager>();
        logicManager = GameManager.GetManagerClass<LogicManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.prologue = this;
        nimrodImage.material = nimrodApearMat;
        nimrodName.material  = nimrodApearMat;

        nimrodImage.gameObject.SetActive(false);
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 154, 0);

        gameObject.SetActive(false);
    }

    private IEnumerator glitchRoutine()
    {       
        BackGround.material = GlitchMat;
        float glitchfloat = 0.1f;

        while(glitchfloat < 1.0f)
        {
            glitchfloat += Time.deltaTime * 0.3f;
            RGBGlitch.Amplitude.Set(GlitchMat,glitchfloat);

            yield return new WaitForSeconds(0.01f);
        }

        logicManager.StartMode(true);
        gameObject.SetActive(false);

                      
    }

    private void StartDialogue()
    {
        
        articleIndex = 0;
        Dialogue.gameObject.SetActive(true);
      
        bisEnd = true;
        tp7.StartDisappearingText();
        //tp7.gameObject.SetActive(false);
        DialogueText.ShowText(resourceManager.AD_Monsters["prologue"][articleIndex]);
        articleIndex += 1;
    }

    public void ShowDialogue()
    {
       

        if (!bisEnd) return;
          
        if (!bisSkiped)
        {
            
            DialogueText.SkipTypewriter();
        }
        else
        {
            DialogueText.ShowText(resourceManager.AD_Monsters["prologue"][articleIndex]);
            articleIndex += 1;
            bisSkiped = false;
        }
    }


    private void Darker()
    {                       
        BackGround.DOColor(Color.white, 1.5f).OnComplete(() => {
            Dialogue.gameObject.SetActive(true);
            
            DialogueText.ShowText(resourceManager.AD_Monsters["prologue"][articleIndex]);
            articleIndex += 1;
            bisEnd = true;
         }
        ).SetEase(Ease.OutElastic);
    }

    public void OnDialogueEffect(string message)
    {
       
       
        IEnumerator tempo()
        {
            yield return new WaitForSeconds(2.0f);
            t1.StartDisappearingText();
            t2.StartDisappearingText();
            t3.StartDisappearingText();
            t4.StartDisappearingText();
            t5.StartDisappearingText();
            bisEnd = true;
        }

        IEnumerator letter()
        {

            float letterFloat = 0.6f;
            Dissolve.Slide.Set(LetterMat, letterFloat);
            Letter.material = LetterMat;
            Letter.gameObject.SetActive(true);
            while (letterFloat > 0.000001f)
            {
                letterFloat -= 0.04f;
                Dissolve.Slide.Set(LetterMat,letterFloat);
                yield return new WaitForSeconds(0.001f);
            }

            LetterDialogue.text = article9;
        }

        IEnumerator shiftRoutine()
        {
            float dissolveFloat = 1;
            float shiftFloat = 0f;
            while (dissolveFloat > 0.1f)
            {
                dissolveFloat -= 0.08f;
                Dissolve.Slide.Set(nimrodApearMat,dissolveFloat);
            }
            nimrodImage.material = nimrodGlitchMat;
            nimrodName.material = nimrodGlitchMat;

            while (shiftFloat < 0.9f)
            {
                shiftFloat += 0.005f;
                RGBGlitch.Speed.Set(nimrodGlitchMat,shiftFloat);
                RGBGlitch.Amplitude.Set(nimrodGlitchMat,shiftFloat);
                yield return new WaitForSeconds(0.01f);
            }
            nimrodImage.sprite = resourceManager.I_NimrodDictionary[2];
            nimrodText.ShowText("{rdir}???{/rdir}");
            while (shiftFloat > 0.1f)
            {
                shiftFloat -= 0.005f;
                RGBGlitch.Speed.Set(nimrodGlitchMat, shiftFloat);
                RGBGlitch.Amplitude.Set(nimrodGlitchMat, shiftFloat);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(1.0f);
            uiManager.Prologues("NimrodGlitch3");
            gameObject.SetActive(false);
        }
        
        if (message == "skip") {  bisSkiped = true; }

        if (message == "dialogue")
        {           
            switch (dialogueIndex)
            {
                case 0:
                    bisEnd = false;
                    BackGround.DOColor(Color.white, 1.0f).OnComplete(()=>
                    {
                        DialogueText.ShowText(" ");
                        Dialogue.gameObject.SetActive(false);
                        BackGround.DOColor(Color.black, 1.0f).SetEase(Ease.Flash).OnComplete(Darker);
                    });
                   
                    break;

                case 1:
                    bisEnd = false;
                    t1.ShowText(article8);
                    talk1.DOAnchorPos(new Vector2(-96f, 521f), 1.0f).From(new Vector2(-96f, 521f)).SetEase(Ease.Linear).OnComplete(()=>t2.ShowText(ar1));
                    talk2.DOAnchorPos(new Vector2(158f, 361f), 1.0f).From(new Vector2(158f, 361f)).SetEase(Ease.Linear).SetDelay(1.5f).OnComplete(()=>t3.ShowText(ar2));
                    talk3.DOAnchorPos(new Vector2(-69f, 190f), 1.7f).From(new Vector2(-69f, 190f)).SetEase(Ease.Linear).SetDelay(2.5f).OnComplete(() => t4.ShowText(ar3)); 
                    talk4.DOAnchorPos(new Vector2(-69f, 16f), 1.7f).From(new Vector2(-69f, 16f)).SetEase(Ease.Linear).SetDelay(3.5f).OnComplete(() => t5.ShowText(ar4)); 
                    talk5.DOAnchorPos(new Vector2(-18f,-222f), 1.5f).From(new Vector2(-18f, -222f)).SetEase(Ease.Linear).SetDelay(4.5f).OnComplete(()=>StartCoroutine(tempo()));                                        
                    //TempDialogue.ShowText(article8);
                   
                    break;

                case 2:
                   
                    
                    break;
                case 3:
                                   
                    StartCoroutine(letter());                   
                    break;
                case 4:
                    Letter.gameObject.SetActive(false);
                    break;
                case 5:
                    StartCoroutine(glitchRoutine());
                    break;
                case 6:
                    t5.ShowText("<color=red>곧이어 손가락이 녹아 내린다.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 7:
                    t5.ShowText("<color=red>눈에서 피가 한 줄기 흐르더니</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 8:
                    t5.ShowText("<color=red>앞이 보이질 않는다.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 9:
                    t5.ShowText("<color=red>희미해지는 의식을 붙잡아 보지만</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 10:
                    t5.ShowText("<color=red>내 숨소리 마저 들리지 않는다.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 11:
                    t5.ShowText("<color=red>질식해 가는 감각 속에서</color><waitfor=1.0f> <?dialogue> ");
                    break;

                case 12:                    
                    t5.ShowText("<color=red>나는 후회할 시간도,</color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 13:
                    t5.ShowText("<color=red>주마등을 떠올릴 시간도 갖지못했다.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 14:
                    t5.ShowText("<color=red>그저 산산조각나 사라질 뿐 </color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 15:
                    t5.ShowText("<color=red>........</color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 16:
                    t5.ShowText("<color=white>그 오랜기다림 끝에.</color><waitfor=1.0f> <?dialogue> ");                    
                    break;
                case 17:
                    t5.ShowText("<color=red>사람 목소리? 누구...?</color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 18:
                    t5.ShowText("<waitfor=2.0> <color=white></color>마침내, 드디어<waitfor=2.0f> <?dialogue> ");
                    break;
                case 19:
                    t5.StartDisappearingText();
                   
                    textmeshT1.color = incolor;
                    textmeshT1.font = digitalFont;
                    talk1.anchoredPosition = new Vector2(-21f, 697f);
                    t1.waitForNormalChars = 0.12f;
                    t1.ShowText("<waitfor=1.0> {horiexp}________SYSTEM ONLINE________{/horiexp} <?dialogue> ");                                   
                    break;

                case 20:
                                   
                    textmeshT5.color = incolor;
                    textmeshT5.font = digitalFont;
                    textmeshT5.fontSize = 50;
                    talk5.anchoredPosition = new Vector2(-147f, 535f);
                    t5.ShowText("{horiexp} metabolism   reset :<waitfor=1.0>     complete\n homeostasis reset :<waitfor=1.0>     complete\n   Mitosis    reset :<waitfor=1.0>     complete {/horiexp}<waitfor=1.0> <?dialogue> ");

                    break;

                case 21:
                    nimrodImage.gameObject.SetActive(true);
                    //nimrodImage.material = shiftMat;
                     //nimrodName.material = shiftMat;
                    StartCoroutine(shiftRoutine());
                    break;


            }

            dialogueIndex += 1;
        }
    }




    public void startPrologue()
    {
        tp1.ShowText(article1);
    }



    public void PrologueArticle(string message)
    {
       
        if(message == "NextArticle")
        {
           
            articleIndex += 1;
            switch (articleIndex)
            {
                case 1:
                    tp2.ShowText(article2);
                    break;
                case 2:
                    tp1.ShowText(article3);
                    break;
                case 3:
                    tp2.ShowText(article4);
                    break;
                case 4:
                    tp1.ShowText(article5);
                    break;
                case 5:
                    tp2.ShowText(article6);
                    break;
                case 6:
                    tp1.StartDisappearingText();
                    tp2.StartDisappearingText();
                    //tp3.StartDisappearingText();
                    //tp4.StartDisappearingText();
                    //tp5.StartDisappearingText();
                    //tp6.StartDisappearingText();
                    BackGround.DOFade(1,0.1f).OnComplete(()=>tp7.ShowText(article7)).SetDelay(2.5f);
                  
                    break;
                case 7:
                    BackGround.DOColor(Color.white, 1f).SetEase(Ease.Linear).OnComplete(StartDialogue);
                    //tp7.StartDisappearingText();
                    

                    break;

            }

           
        }
    }
   
    public void nimrodGlitch()
    {     
        talk5.anchoredPosition = new Vector2(-85f, -54f);
        dialogueIndex = 6;
        t5.textAnimator.tmproText.alignment = TextAlignmentOptions.Center;
        t5.ShowText("<color=red>오른손의 손톱이 빠졌다.</color> <waitfor=1> <?dialogue> ");
    }

    public void GameOver()
    {
        t1.gameObject.SetActive(false);
        t2.gameObject.SetActive(false);
        t3.gameObject.SetActive(false);
        t4.gameObject.SetActive(false);
        t5.gameObject.SetActive(false);
        talk1.gameObject.SetActive(false);
        talk2.gameObject.SetActive(false);
        talk3.gameObject.SetActive(false);
        talk4.gameObject.SetActive(false);
        talk5.gameObject.SetActive(false);
        nimrodImage.gameObject.SetActive(false);

        BackGround.material = GlassMaterial;
        StartCoroutine(glassRoutine());
        IEnumerator glassRoutine()
        {

            float refrection = 0;
            while(refrection < 100)
            {
                Glass.Refraction.Set(GlassMaterial,refrection);
                refrection += 0.3f;

                yield return new WaitForSeconds(0.01f);
            }
            BackGround.material = null;

        }
    }

}
