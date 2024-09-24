using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FronkonGames.SpritesMojo;
using Febucci.UI;
using TMPro;


public class BackgroundUI : MonoBehaviour
{
    #region component
    [SerializeField] private Image readBackGround;
    [SerializeField] private Image diseaseBackGround;
    [SerializeField] private Image tokenBackGround;
    [SerializeField] private Image infoBackGround;
    [SerializeField] private Image NimrodChangeButton;

    [SerializeField] private TextMeshProUGUI MainArticle;
    [SerializeField] private Text TraitText;
    [SerializeField] private Text NameText;
    

    #endregion

    #region  Material

    Material backroundGlitch;
    Material backGroundEdge;
    Material backGroundShift;
    [SerializeField] private Material backGroundHologram;

    #endregion



    UIManager uiManager;
    TextManager textManager;

    private void Awake()
    {   //Color incolor;
        //ColorUtility.TryParseHtmlString("#87A1E0", out incolor);
        backroundGlitch = RGBGlitch.CreateMaterial();
        backGroundEdge = Edge.CreateMaterial();
        backGroundShift = Swirl.CreateMaterial();
        Edge.Tint.Set(backGroundEdge,Color.red);
        Edge.Sobel.Set(backGroundEdge, SobelFunction.Standard);
        Edge.Mode.Set(backGroundEdge, EdgeMode.TrueColor);
        RGBGlitch.Speed.Set(backroundGlitch, 0.1f);
        RGBGlitch.Amplitude.Set(backroundGlitch, 0.1f);
        Shift.NoiseStrength.Set(backGroundShift, 0.2f);
        Shift.NoiseSpeed.Set(backGroundShift, 0.2f);


    }

    private void Start()
    {
        uiManager = GameManager.GetManagerClass<UIManager>();
        textManager = GameManager.GetManagerClass<TextManager>();
        TextAnimatorPlayer tap = MainArticle.GetComponent<TextAnimatorPlayer>();
        textManager.initializeTextManager(tap , NameText , TraitText);
        uiManager.backgroundUI = this;
    }


    public void SetBackGroundMaterial(Material mat)
    {
        readBackGround.material     = mat;         
    }

    public void SetBackGroundMaterial(int num)
    {
        IEnumerator edgeRoutine()
        {
            float edgeFloat = 0;
            int index = 0;
            float edgeSpeed = 0.05f;

            WaitForSeconds wait = new WaitForSeconds(0.01f);
            while (edgeFloat < 0.9f)
            {
                edgeFloat += 0.05f;
                SpriteMojo.Amount.Set(backGroundEdge, edgeFloat);
                yield return wait;
            }

            while (edgeFloat > 0.1f)
            {
                edgeFloat -= 0.01f;
                SpriteMojo.Amount.Set(backGroundEdge, edgeFloat);
                yield return wait;
            }

            while (edgeFloat < 0.9f)
            {
                edgeFloat += 0.05f;
                SpriteMojo.Amount.Set(backGroundEdge, edgeFloat);
                yield return wait;
            }

            while (edgeFloat > 0.1f)
            {
                edgeFloat -= 0.01f;
                SpriteMojo.Amount.Set(backGroundEdge, edgeFloat);
                yield return wait;
            }
            diseaseBackGround.material = backroundGlitch;
            tokenBackGround.material = backroundGlitch;
            infoBackGround.material = backroundGlitch;


            while (index  < 25)
            {

                while (edgeFloat < 0.9f)
                {
                    edgeFloat += edgeSpeed;
                    SpriteMojo.Amount.Set(backGroundEdge, edgeFloat);
                    yield return wait;
                }

                while (edgeFloat > 0.1f)
                {
                    edgeFloat -= edgeSpeed;
                    SpriteMojo.Amount.Set(backGroundEdge, edgeFloat);
                    yield return wait;
                }

                index += 1;
                edgeSpeed += 0.03f;
            }


            //readBackGround.material = backroundGlitch;
            readBackGround.material    = backGroundHologram;
           
            StartCoroutine(glitchRoutine());
        }

        IEnumerator glitchRoutine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
            float glitchFloat = 0.1f;

            while (glitchFloat < 1.0f)
            {
                glitchFloat += 0.005f;
                RGBGlitch.Speed.Set(backroundGlitch, glitchFloat);
                RGBGlitch.Amplitude.Set(backroundGlitch, glitchFloat);

                yield return waitForSeconds;
            }
            RGBGlitch.Speed.Set(backroundGlitch, 0);
            uiManager.Prologues("NimrodGlitch2");
        }
       
        



        switch (num)
        {
            case 0:
                readBackGround.material = backGroundEdge;
                StartCoroutine(edgeRoutine());
               
                break;

            case 1:
                diseaseBackGround.material = null;
                tokenBackGround.material = null;
                infoBackGround.material = null;
                break;

            case 3:

                break;


        }



    }

  


}
