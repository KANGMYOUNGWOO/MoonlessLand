using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using FronkonGames.SpritesMojo;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class QuizObject : MonoBehaviour
{
    [System.Serializable]
    public class AssetReferenceMaterial : AssetReferenceT<Material>
    {
        public AssetReferenceMaterial(string guid) : base(guid) { }
    }

    #region Components
    [SerializeField] private Image BackGround;
    [SerializeField] private Image SpinDragon;

    [SerializeField] private Image keep;
    [SerializeField] private Image trash;
    [SerializeField] private Image wheel;
    [SerializeField] private Image answer;
    [SerializeField] private Image correctionPad;


    [SerializeField] private Image TextPad;
    [SerializeField] private TextMeshProUGUI quizText;
    [SerializeField] private TextMeshProUGUI correctionText;


    [SerializeField] private Image[] FrameImages = new Image[9];

    [SerializeField] private Button posCorButton;
    [SerializeField] private Button negCorButton;


    #endregion

    #region Material

    private Material BackGroundMat;
    private Material WheelMaterial;
    private Material KeepMaterial;
    private Material TrashMaterial;
    private Material AnswerMaterial;
    public AssetReferenceMaterial OutLineRef;
    private Material DragonMat = null;
    private Material HoloMaterial;

    private Material CorrectionMat;

    private Material AnswerMat;
    private Material WrongMat;

    private Color HoloColor;

    private int StateNum = 0;

    #endregion


    private LogicManager logicManager = null;
    private ResourceManager resourceManager = null;

    static int firstWord = 44032;

    private void Start()
    {
        logicManager = GameManager.GetManagerClass<LogicManager>();
        logicManager.quizObject = this;
        ColorUtility.TryParseHtmlString("#3DD2FF", out HoloColor);

        BackGroundMat  = Dissolve.CreateMaterial();
        WheelMaterial  = Dissolve.CreateMaterial();
        KeepMaterial   = Dissolve.CreateMaterial();
        TrashMaterial  = Dissolve.CreateMaterial();
        AnswerMaterial = Dissolve.CreateMaterial();
        CorrectionMat  = Dissolve.CreateMaterial();
        HoloMaterial   = Hologram.CreateMaterial();
        AnswerMat      = Shift.CreateMaterial();
        WrongMat       = RGBGlitch.CreateMaterial();

        Dissolve.Shape.Set(BackGroundMat,DissolveShape.Board_1);
        Dissolve.Slide.Set(BackGroundMat,1);
        BackGround.material = BackGroundMat;

        Dissolve.Shape.Set(WheelMaterial,DissolveShape.Mosaic_1);
        Dissolve.Shape.Set(KeepMaterial,DissolveShape.Mosaic_2);
        Dissolve.Shape.Set(AnswerMaterial,DissolveShape.Mosaic_1);
        Dissolve.Shape.Set(TrashMaterial,DissolveShape.Mosaic_2);
        Dissolve.Shape.Set(CorrectionMat, DissolveShape.Dissolve);

        Dissolve.Slide.Set(WheelMaterial, 1);
        Dissolve.Slide.Set(KeepMaterial, 1);
        Dissolve.Slide.Set(AnswerMaterial, 1);
        Dissolve.Slide.Set(TrashMaterial, 1);
        Dissolve.Slide.Set(CorrectionMat, 0.5f);

        Hologram.Distortion.Set(HoloMaterial,6.58f);
        Hologram.BlinkStrength.Set(HoloMaterial,0.265f);
        Hologram.BlinkSpeed.Set(HoloMaterial, 30.2f);
        Hologram.ScanlineStrength.Set(HoloMaterial, 0.9f);
        Hologram.ScanlineCount.Set(HoloMaterial,20);
        Hologram.ScanlineSpeed.Set(HoloMaterial,22.5f);
        Hologram.Tint.Set(HoloMaterial,HoloColor);

        Shift.Mode.Set(AnswerMat,1);
        Shift.RadialShift.Set(AnswerMat,0);
        Shift.Noise.Set(AnswerMat,true);
        Shift.NoiseStrength.Set(AnswerMat,0.2f);
        Shift.NoiseSpeed.Set(AnswerMat,0.2f);

        RGBGlitch.Amplitude.Set(WrongMat,0.4f);
        RGBGlitch.Speed.Set(WrongMat,0.4f);

        wheel.material = WheelMaterial;
        keep.material = KeepMaterial;
        answer.material = AnswerMaterial;
        trash.material = TrashMaterial;
        correctionPad.material = CorrectionMat;
        
        correctionPad.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ActiveQuiz(string text)
    {
        quizText.text = text;
    }

    public void ActiveQuiz()
    {
        Dissolve.Slide.Set(BackGroundMat, 1);
        float dissolveSpeed = 0.03f;
        float dissolveSlide = 1;
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.001f);
        WaitForSeconds dragonWait = new WaitForSeconds(0.05f);
        WaitForSeconds dragonWait2 = new WaitForSeconds(0.02f);
       
        bool[] bisAngle = new bool[5] { false, false, false, false ,false};
        float[] dissolveArray = new float[4] { 1, 1, 1, 1 };
        int dragonSize = 4;
      
        float HoloAmount = 1f;

        SpinDragon.gameObject.SetActive(false);
        wheel.gameObject.SetActive(false);
        keep.gameObject.SetActive(false);
        trash.gameObject.SetActive(false);
        answer.gameObject.SetActive(false);
        TextPad.gameObject.SetActive(false);
        for(int i=6;i<9;i++)
        {
            FrameImages[i].gameObject.SetActive(false);
        }

        if(DragonMat == null) OutLineRef.LoadAssetAsync().Completed+=
            ((handle) => 
         {
            //SpinDragon.material = handle.Result;
            DragonMat = handle.Result;
             FronkonGames.SpritesMojo.Outline.Size.Set(DragonMat,4);
            FronkonGames.SpritesMojo.Outline.TextureScale.Set(DragonMat,1.9f);
            FronkonGames.SpritesMojo.Outline.TextureVelocity.Set(DragonMat,new Vector2(0.1f,0));

             StartCoroutine(slide());
         });
        else
        {
            FronkonGames.SpritesMojo.Outline.Size.Set(DragonMat, 4);
            FronkonGames.SpritesMojo.Outline.TextureScale.Set(DragonMat, 1.9f);
            FronkonGames.SpritesMojo.Outline.TextureVelocity.Set(DragonMat, new Vector2(0.1f, 0));

            StartCoroutine(slide());
        }
           
        IEnumerator ImageShow(int index)
        {
            Material currentMat = WheelMaterial;
            switch (index)
            {
                case 0:
                    currentMat = TrashMaterial;
                    break;
                case 1:
                    currentMat = WheelMaterial;
                    break;
                case 2:
                    currentMat = KeepMaterial;
                    break;
                case 3:
                    currentMat = AnswerMaterial;
                    break;

            }

            Dissolve.Slide.Set(currentMat,dissolveArray[index]);

            while(dissolveArray[index] > 0.00001f)
            {
                dissolveArray[index] -= dissolveSpeed;
                Dissolve.Slide.Set(currentMat,dissolveArray[index]);
                yield return waitForSeconds;
            }
        }

        
        IEnumerator slide()
        {
            while (dissolveSlide > 0.000001f)
            {
                dissolveSlide -= 0.03f;
                Dissolve.Slide.Set(BackGroundMat, dissolveSlide);
                yield return waitForSeconds;
            }

            SpinDragon.gameObject.SetActive(true);
            StartCoroutine(dragonspin());
        }
       
        IEnumerator dragonspin()
        {   
           // SpinDragon.material = HoloMaterial;
                     
            quizText.gameObject.SetActive(false);
            /*
            while (HoloAmount > 0.6f)
            {
                HoloAmount -= 0.003f;
                SpriteMojo.Amount.Set(HoloMaterial, HoloAmount);
                yield return waitForSeconds;
            }
              */         
            SpinDragon.material = DragonMat;
            TextPad.gameObject.SetActive(true);
            quizText.gameObject.SetActive(false);
            TextPad.rectTransform.DOAnchorPos(new Vector2(-3, 202), 0.4f).From(new Vector2(-3, 800)).SetEase(Ease.OutQuad);
            for (int i = 0; i < 9; i++)
            {
                FrameImages[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < FrameImages.Length; i++)
            {
                FrameImages[i].material = DragonMat;
            }

            while (dragonSize<25)
            {
                dragonSize += 1;
                FronkonGames.SpritesMojo.Outline.Size.Set(DragonMat,dragonSize);
                yield return dragonWait2;
            }

          

            while (dragonSize>3)
            {
                dragonSize -= 1;
                FronkonGames.SpritesMojo.Outline.Size.Set(DragonMat, dragonSize);
                if(dragonSize == 19 && !bisAngle[0])
                {
                    //StartCoroutine(Holo());
                    bisAngle[0] = true;
                    StartCoroutine(ImageShow(0));
                    trash.gameObject.SetActive(true);
                    trash.rectTransform.DOAnchorPos(new Vector2(351, -669), 0.4f).From(new Vector2(-20f, -669f));                                      
                }
                if(dragonSize == 14 && !bisAngle[1])
                {
                    bisAngle[1] = true;
                    StartCoroutine(ImageShow(1));
                    wheel.gameObject.SetActive(true);
                    wheel.rectTransform.DOAnchorPos(new Vector2(-20f, -895), 0.4f).From(new Vector2(-20f, -669f));
                }
                if(dragonSize == 9 && !bisAngle[2])
                {
                    bisAngle[2] = true;
                    StartCoroutine(ImageShow(2));
                    keep.gameObject.SetActive(true);
                    keep.rectTransform.DOAnchorPos(new Vector2(-387f, -669), 0.4f).From(new Vector2(-20f, -669f));
                }
                if(dragonSize == 4 && !bisAngle[3])
                {
                    bisAngle[3] = true;
                    StartCoroutine(ImageShow(3));
                    answer.gameObject.SetActive(true);
                    answer.rectTransform.DOAnchorPos(new Vector2(-20f, -349), 0.4f).From(new Vector2(-20f, -669f));
                }
                

                yield return dragonWait;
            }

            
            for (int i = 0; i < FrameImages.Length; i++)
            {
                FrameImages[i].material = null;
            }
            quizText.gameObject.SetActive(true);
           
           
            logicManager.SetQuiz();
           
        }


    }

    public void onPosButton()
    {
        //correctionText.gameObject.SetActive(false);
        correctionPad.gameObject.SetActive(false);
        WaitForSeconds holowait = new WaitForSeconds(0.02f);
        WaitForSeconds wait = new WaitForSeconds(1.0f);
        WaitForSeconds kill = new WaitForSeconds(2.0f);
        //float holoAmount = 0.55f;
        int dragonSize = 4;
        IEnumerator holoRoutine()
        {/*
            while (holoAmount < 0.999999f)
            {
                holoAmount += 0.002f;
                SpriteMojo.Amount.Set(HoloMaterial, holoAmount);
                yield return holowait;
            }
            */
            while (dragonSize < 35)
            {
                dragonSize += 1;
                FronkonGames.SpritesMojo.Outline.Size.Set(DragonMat, dragonSize);
                yield return holowait;
                    ;
            }

            yield return wait;

            //wheel.material = null;
            //keep.material = null;
            //trash.material = null;
            //answer.material = null;
            //SpinDragon.material = null;
            /*
            for (int i = 0; i < FrameImages.Length; i++)
            {
                FrameImages[i].material = null;
            }
            */
            //wheel.gameObject.SetActive(false);
            //keep.gameObject.SetActive(false);
            //trash.gameObject.SetActive(false);
            //answer.gameObject.SetActive(false);
            //SpinDragon.gameObject.SetActive(false);
            /*
            for (int i = 0; i < FrameImages.Length; i++)
            {
                FrameImages[i].gameObject.SetActive(false);
            }
            */
            //TextPad.gameObject.SetActive(false);


            bool result = logicManager.QuizAction(0) > 0;

            //extPad.rectTransform.DOAnchorPos(new Vector2(-3,-268),1).From(new Vector2(-3,202));
            Material switching = null; 
            quizText.fontSize = 150;
            if (result) { quizText.text = "<color=green>Success</color>"; switching = AnswerMat; }
            else { quizText.text = "<color=red>Fail</color>"; switching = WrongMat; }

            wheel.material = switching;
            keep.material = switching;
            trash.material = switching;
            answer.material = switching;
            SpinDragon.material = switching;
            
            for (int i = 0; i < FrameImages.Length; i++)
            {
                FrameImages[i].material = switching;
            }
            
            yield return kill;
            wheel.material = null;
            keep.material = null;
            trash.material = null;
            answer.material = null;
            SpinDragon.material = null;
            
            for (int i = 0; i < FrameImages.Length; i++)
            {
                FrameImages[i].material = null;
            }
            

            logicManager.QuizAction();
            quizText.fontSize = 40;
            gameObject.SetActive(false);


        }

        switch (StateNum)
        {
            case 0:
           
                //SpriteMojo.Amount.Set(HoloMaterial,holoAmount);
                SpinDragon.material = DragonMat;
                wheel.material = DragonMat;
                keep.material = DragonMat;
                trash.material = DragonMat;
                answer.material = DragonMat;
                for(int i=0;i<FrameImages.Length;i++)
                {
                    FrameImages[i].material = DragonMat;
                }
                StartCoroutine(holoRoutine());

                break;

            case 1:


                break;
            case 2:
                break;
            case 3:
                break;
        }

    }

    public void onNegButton()
    {
        logicManager.ActionCardcon.ActiveQuizAll();
        correctionPad.gameObject.SetActive(false);
    }
  
    public void ActiveCorrection(int switchnum, string Name)
    {
        correctionPad.gameObject.SetActive(true);
        string josa = "";
        string resultText="";
        char[] temp = Name.ToCharArray();
        
        char Lastword = temp[temp.Length - 1];

        if (Lastword < 44032 || Lastword > 55215) josa = "을";        
        else                                      josa = (Lastword - firstWord) % 28 > 0 ? "을" : "를";

        posCorButton.gameObject.SetActive(false);
        negCorButton.gameObject.SetActive(false);
        correctionText.text = "";

        StateNum = switchnum;

        switch (switchnum)
        {
            case 0:
                resultText = string.Format("<color=blue>{0}</color> {1}\n<color=green>정답으로 제출</color> 하시겠습니까?", Name,josa) ;
                break;

            case 1:
                resultText = string.Format("<color=blue>{0}</color> {1}<color=red>파괴하고</color>\n원하는 키워드의 카드를 뽑습니다.", Name, josa);
                break;

            case 2:
                resultText = string.Format("카드를 모두 섞어놓고 \n 다시 뽑습니다.");
                break;

            case 3:
                resultText = string.Format("<color=blue>{0}</color> {1} 보관합니다", Name, josa);
                break;
        }

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.001f);
        float DissolveSlide = 0.7f;
        StartCoroutine(Dis());
        IEnumerator Dis()
        {
            while(DissolveSlide > 0.000001f)
            {
                DissolveSlide -= 0.01f;
                Dissolve.Slide.Set(CorrectionMat, DissolveSlide);
                yield return waitForSeconds;
            }

            correctionText.text = resultText;
            posCorButton.gameObject.SetActive(true);
            negCorButton.gameObject.SetActive(true);

        }



    }

}
