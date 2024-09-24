using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Febucci.UI;


public class TestCardSet : MonoBehaviour
{
    private LogicManager logicManager;
    private ResourceManager resourceManager;
    private CharacterManager characterManager;

    public List<TestCard> CardList = new List<TestCard>();
    public List<TraitText> TraitList = new List<TraitText>();





    public bool isOver = true;

    [SerializeField] private RectTransform PadRect;
    [SerializeField] private Text DifficultyText;
    [SerializeField] private Text ResultText;
    [SerializeField] private RectTransform ResultRect;
    [SerializeField] private GameObject ConfirmButton;
    [SerializeField] private TextAnimatorPlayer text;



    //public List<Sprite> ResultLiist = new List<Sprite>();
   

    private int stat;
    private int trait;

    private int prevResult;
    private int afterResult;
    private int difficulty;
    private bool bisSuccess;
    private Vector2 Originpos_testPad;
    private Vector2 Originpos_resultRect;



    //Sequence ResultSequence;



    public void Start()
    {
        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        logicManager = GameManager.GetManagerClass<LogicManager>();
        characterManager = GameManager.GetManagerClass<CharacterManager>();
        logicManager.TestUI = this;
        Originpos_testPad = PadRect.anchoredPosition;
        Originpos_resultRect = ResultRect.anchoredPosition;
        gameObject.SetActive(false);
    }




    public void TempGo()
    {
        //stat = num;
        //DifficultyText.text = "난이도 : " + Difficulty.ToString();
        stat = 7;
        trait = 3;
        DifficultyText.text = "난이도 : 3";
        PadRect.DOAnchorPos(new Vector2(-75, 193), 1f).SetEase(Ease.OutBounce).From(Originpos_testPad).OnComplete(CardGo);



    }

    public void SetTestCard(int stat, int trait, int difficulty, int prevResult, int afterResult, int code, int[] resultArray, bool b_result)
    {

        this.stat = stat;
        this.trait = characterManager.bonusExplainList.Count;

        this.prevResult = prevResult;
        this.afterResult = afterResult;
        this.difficulty = difficulty;


        DifficultyText.text = "난이도 : " + difficulty.ToString();
        DifficultyText.DOFade(1, 0.1f);
        ResultText.DOText("결과값 : " + prevResult.ToString(), 0.0f).From("");
        text.ShowText(" ");
        text.gameObject.SetActive(false);

        Sprite front = resourceManager.I_CardFrontDictionary[code];
        Sprite back = resourceManager.I_CardBackDictionary[code];




        for (int i = 0; i < stat; i++)
        {
            // CardList[i].SetCardImage(front,back, resourceManager.I_ResultDictionary[resultArray[i]]);
            CardList[i].SetCardImage(front, back, resourceManager.I_ResultDictionary[resultArray[i]]);

        }



        bisSuccess = b_result;


        PadRect.DOAnchorPos(new Vector2(-75, 193), 1f).SetEase(Ease.OutBounce).From(Originpos_testPad).OnComplete(CardGo);
    }




    public void TextSet()
    {

        ConfirmButton.SetActive(false);
        ResultRect.DOAnchorPos(new Vector2(-271, 348), 1.0f).SetEase(Ease.InOutBack).SetDelay(0.2f).From(Originpos_resultRect);

        for (int i = 0; i < stat; i++)
        {
            CardList[i].cardEnd();
        }

        for (int i = 0; i < trait; i++)
        {
            TraitList[i].LineSetter(i, trait, characterManager.bonusExplainList[i]);
        }

        ResultText.DOFade(0, 1.0f).SetDelay(0.3f + trait * 0.4f)
            .OnComplete(() =>
            {
               
                ResultText.DOText("결과값 : " + afterResult.ToString(), 0.0f).From("");
                ResultText.DOFade(1, 1.0f).From(0);
                ResultRect.DOAnchorPos(new Vector2(0, 348), 0.5f).SetDelay(1.7f);
                //ResultRect.DOAnchorPos(new Vector2(0,504),0.5f).SetDelay(2.2f);
                DifficultyText.DOFade(0, 1.0f).SetDelay(2.3f);
                ResultText.DOFade(0, 1.0f).SetDelay(2.3f).OnComplete(() =>
                {
                    text.gameObject.SetActive(true);

                    string tex;
                    tex = afterResult >= difficulty ? "<color=green>SUCCESS</color>" : "<color=red>FAIL</color>";
                    text.ShowText(tex);

                    DifficultyText.DOFade(0, 1.0f).OnComplete(() =>
                    {


                        text.gameObject.SetActive(false);

                        logicManager.WaitForEnd();

                        characterManager.bonusExplainList.Clear();

                        gameObject.SetActive(false);

                       
                    });
                });
            }
          );
        // ResultRect.DOAnchorPos(new Vector2(),1.0f);

        // shake.DOShakeAnchorPos(1, new Vector2(100f,0),10,0,false,false).SetDelay(2.0f);
    }

    public void CardGo()
    {
        ResultText.DOFade(1, 1.0f).SetDelay(0.4f + 0.15f * stat).SetEase(Ease.InExpo).From(0).OnComplete(() => ConfirmButton.SetActive(true));

        for (int i = 0; i < stat; i++)
        {
            CardList[i].FlyCard(i, stat);
        }

    }

  

    
}
