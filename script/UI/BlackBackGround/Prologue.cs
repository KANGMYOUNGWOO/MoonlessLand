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
   
    public string article1 { get; set; } = "�η��� ������ ��ó�� �İ���<waitfor=0.8> <?NextArticle> ";
    public string article2 { get; set; } = "���� ����� ���� ǥ����\n�ϱ׷����� �Ű澵 �ܸ� ���� ����<waitfor=0.8> <?NextArticle> ";
    public string article3 { get; set; } = "��� �¸��� �������̰� �� �� ������<waitfor=0.8> <?NextArticle> ";
    public string article4 { get; set; } = "�縻�� �Ƿ� �������� ���� ����\n���������� ���� ����´�<waitfor=0.8> <?NextArticle> ";
    public string article5 { get; set; } = "���� ���� ��� ��� ������\n���ó�� ��������<waitfor=0.8> <?NextArticle> ";
    public string article6 { get; set; } = "�����̶� ������ �� ������\n����Ե� �ٸ����� ���Ӿ��� �����δ�<waitfor=0.8> <?NextArticle> ";
    public string article7 { get; set; } = "����� �ϴ� ����� ���� �޸��� �ߴ� <waitfor=1> <?NextArticle> ";
    public string article8 { get; set; } = "{size}<color=blue>�簡</color> <color=yellow>��</color> <color=blue>��?</color>{/size}";
    public string ar1 { get; set; }      = "{size}<color=green>�� ������ ������ ���� �ϴ� ��</color>{/size}";
    public string ar2 { get; set; }      = "{size}<color=blue>�׷� ��¥ ������... �׷��� �� �����ΰ� ��?</color>{/size}";
    public string ar3 { get; set; }      = "{size}<color=blue>�� ���� ���� ������ ��� ���ͱ� �� ���ݾ�.</color>{/size}";
    public string ar4 { get; set; }      = "{size}<color=green> ���� ��������.�ʵ� �� �� ���� �;�?</color> <waitfor=0.6> <?dialogue> {/size} ";
    public string article9 { get; set; } = "����ϴ� ���ƿ���.\n ���ְ� ���� ���� �ʹ����� ������\n ������ ���� ���� ���� �ƴ� �� ������.\n\n";
    public string sr1 { get; set; }      = "���� ǥ���� ������ ���Ŷ�.\n��� ���ذ��� ���� �������ʴ� ������ ���ðž�.\n�� ���� �츮�ʹ� ���� �ٸ� ����� �̾����ִܴ�.\n";
    public string sr2 { get; set; }      = "�ʸ� ��ĥ���� �װ������� ���� �� �־�.\n\n������ ����Ϸ�\n�װ��� �°� ��������� ������ �߿����̸�\n������ ���µ��� ��ġ�� ������ ���̶���.\n";
    public string sr3 { get; set; }      = "�װ����� ��Ƴ������� �����°͸����� �����ؾ��Ұž�.\n�ʸ� �׷� ���� ������ �ѽ��� ��� �뼭���ַ�.\n�̷� �������� �ʸ� �츱 ����� �������� �ʱ���.\n\n";
    public string sr4 { get; set; }      = "ps. ������ ���� �� ������ ���� ��ȭ���ٰŶ���. ";





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
                    t5.ShowText("<color=red>���̾� �հ����� ��� ������.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 7:
                    t5.ShowText("<color=red>������ �ǰ� �� �ٱ� �帣����</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 8:
                    t5.ShowText("<color=red>���� ������ �ʴ´�.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 9:
                    t5.ShowText("<color=red>��������� �ǽ��� ����� ������</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 10:
                    t5.ShowText("<color=red>�� ���Ҹ� ���� �鸮�� �ʴ´�.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 11:
                    t5.ShowText("<color=red>������ ���� ���� �ӿ���</color><waitfor=1.0f> <?dialogue> ");
                    break;

                case 12:                    
                    t5.ShowText("<color=red>���� ��ȸ�� �ð���,</color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 13:
                    t5.ShowText("<color=red>�ָ����� ���ø� �ð��� �������ߴ�.</color> <waitfor=1.0f> <?dialogue> ");
                    break;
                case 14:
                    t5.ShowText("<color=red>���� ��������� ����� �� </color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 15:
                    t5.ShowText("<color=red>........</color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 16:
                    t5.ShowText("<color=white>�� ������ٸ� ����.</color><waitfor=1.0f> <?dialogue> ");                    
                    break;
                case 17:
                    t5.ShowText("<color=red>��� ��Ҹ�? ����...?</color><waitfor=1.0f> <?dialogue> ");
                    break;
                case 18:
                    t5.ShowText("<waitfor=2.0> <color=white></color>��ħ��, ����<waitfor=2.0f> <?dialogue> ");
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
        t5.ShowText("<color=red>�������� ������ ������.</color> <waitfor=1> <?dialogue> ");
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
