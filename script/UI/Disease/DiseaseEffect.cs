using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using DG.Tweening;
using FronkonGames.SpritesMojo;


public class DiseaseEffect : MonoBehaviour
{

    [SerializeField] private Image Image1;
    [SerializeField] private Image Image2;
    [SerializeField] private Image Image3;
    [SerializeField] private Image BlackBackGround;
    [SerializeField] private DiseaseSlot EffectSlot;
    [SerializeField] private Button EndButton;
    [SerializeField] private TextAnimatorPlayer BattleText;
    [SerializeField] private DiseaseExplain EffectExplain;
    


    private UIManager uiManager = null;
    private ResourceManager resourceManager;
    private LogicManager logicManager;
    private int TempIndex = 0;
    private Sprite TempSprite = null;

    private PlayerInfo characterInfo;

    #region blackBackGround
    private Material dissolveMat;
    private float dissolveSlide = 0.3f;
    private float dissolveSpeed = 1.0f;
    private bool bisBattle;

    #endregion

    private void Awake()
    {
        dissolveMat = Dissolve.CreateMaterial();
        Dissolve.Shape.Set(dissolveMat, DissolveShape.FoggySpiral);
        Dissolve.Slide.Set(dissolveMat, 0);
        Dissolve.UVScale.Set(dissolveMat, 1.0f);
        bisBattle = false;
        BlackBackGround.material = dissolveMat;
    }

    void Start()
    {
        Image1.gameObject.SetActive(false);
        Image2.gameObject.SetActive(false);
        Image3.gameObject.SetActive(false);
        BlackBackGround.gameObject.SetActive(false);
        EndButton.gameObject.SetActive(false);
        BattleText.gameObject.SetActive(false);

        EffectSlot.gameObject.SetActive(false);      
        BlackBackGround.gameObject.SetActive(false);

        resourceManager = GameManager.GetManagerClass<ResourceManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        logicManager = GameManager.GetManagerClass<LogicManager>();
        resourceManager.GetPlayerInfo(out characterInfo);
        uiManager.diseaseEffect = this;
        gameObject.SetActive(false);
    }
      
    public void ActiveEffectExplain(int index)
    {
        int value = 0;
        int turn = 0;
        string diseaseLine = "";
        string name;
        string explain = "";
        string stat = "";

        Disease disease = characterInfo.DiseaseArray[index].disease;
        name = disease.DiseaseName;

        if (characterInfo.DiseaseArray[index].DiseaseCode == 0) return;


        if (characterInfo.DiseaseArray[index].disease.isTurnDm)
        {
            turn = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = characterInfo.DiseaseArray[index].t_DiseaseValue;
            explain = disease.red_DiseaseExplain;
            diseaseLine = string.Format("<color=yellow>{0}</color> 턴 동안 매 턴 마다 {1} <color=red>{2}</color> 피해", turn, explain, value);
        }

        if (characterInfo.DiseaseArray[index].disease.isEndDm)
        {
            turn = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = characterInfo.DiseaseArray[index].e_DiseaseValue;
            explain = disease.yellow_DiseaseExplain;
            diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 후 {1} <color=red>{2}</color> 피해", turn, explain, value)
                : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 후 {1} <color=red>{2}</color> 피해", turn, explain, value);

        }
        if (characterInfo.DiseaseArray[index].disease.isStat1)
        {
            turn = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue1;
            stat = disease.statType1;


            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value);


            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value);




        }

        if (characterInfo.DiseaseArray[index].disease.isStat2)
        {
            turn = characterInfo.DiseaseArray[index].DiseaseTurn;
            value = disease.s_PenaltyValue2;
            stat = disease.statType2;


            if (turn > 0)
                diseaseLine = diseaseLine == "" ? string.Format("<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value)
               : diseaseLine + string.Format("\n<color=yellow>{0}</color> 턴 동안  <color=green>{1}</color> 이 <color=red>{2}</color> 만큼 감소", turn, stat, value);


            else
                diseaseLine = diseaseLine == "" ? string.Format("<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value)
               : diseaseLine + string.Format("\n<color=green>{0}</color> 이  <color=red>{1}</color> 만큼 감소", stat, value);
        }




        EffectExplain.gameObject.SetActive(true);
        EffectExplain.ActiveEffect(name, diseaseLine);
    }

    private void D_Effect()
    {
        Image1.gameObject.SetActive(true);
        Image2.gameObject.SetActive(true);
        Image3.gameObject.SetActive(true);

        Image1.rectTransform.DOAnchorPos(new Vector2(-361f, 480f), 0.3f).SetEase(Ease.Flash).From(new Vector2(-2123f, -1624.6f));
        Image2.rectTransform.DOAnchorPos(new Vector2(-361, -1246), 0.2f).SetDelay(0.4f).SetEase(Ease.Flash).From(new Vector2(-1976f, -2215.6f));
        Image3.rectTransform.DOAnchorPos(new Vector2(-47, 197), 0.1f).SetDelay(0.8f).SetEase(Ease.Flash).OnComplete(Effect).From(new Vector2(1109.3f, -2742.6f));
    }

    private void Effect()
    {
        BlackBackGround.gameObject.SetActive(true);
        EffectSlot.gameObject.SetActive(true);
        EffectSlot.DiseaseSprite = TempSprite;
        EffectSlot.Infected();
        ActiveEffectExplain(TempIndex);
        Image1.DOFade(1, 0.6f).OnComplete(() => EndButton.gameObject.SetActive(true));

    }


    public void GetDiseaseUI(int index, Sprite sprite)
    {
        EffectExplain.gameObject.SetActive(false);
        TempSprite = sprite;
        TempIndex = index;
        D_Effect();
    }

    public void disActive()
    {
        Image1.gameObject.SetActive(false);
        Image2.gameObject.SetActive(false);
        Image3.gameObject.SetActive(false);
        BlackBackGround.gameObject.SetActive(false);
        EffectSlot.gameObject.SetActive(false);
        EffectExplain.gameObject.SetActive(false);
        EndButton.gameObject.SetActive(false);


    }


    public void StartBattle(string text)
    {
        gameObject.SetActive(true);

        Image1.gameObject.SetActive(false);
        Image2.gameObject.SetActive(false);
        Image3.gameObject.SetActive(false);
        EndButton.gameObject.SetActive(false);
        EffectSlot.gameObject.SetActive(false);
        EffectExplain.gameObject.SetActive(false);

        BattleText.gameObject.SetActive(true);
        BlackBackGround.gameObject.SetActive(true);
        StartCoroutine(BattleUI(text));

    }

    private IEnumerator BattleUI(string text)
    {

        Dissolve.Slide.Set(dissolveMat, 0.3f);
        WaitForSeconds wait = new WaitForSeconds(0.001f);
        BlackBackGround.gameObject.SetActive(true);

        dissolveSpeed = 0.005f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.001f);

        while (dissolveSlide > 0.000001f)
        {
            dissolveSlide -= dissolveSpeed ;
            Dissolve.Slide.Set(dissolveMat, dissolveSlide);
            yield return waitForSeconds;
        }
        BattleText.gameObject.SetActive(true);
        BattleText.ShowText(text);

        bisBattle = true;
        //BlackBackGround.gameObject.SetActive(false);

    }

    public void cutsceneQuit()
    {
        if (bisBattle)
        {
            Dissolve.Slide.Set(dissolveMat, 0f);
            dissolveSlide = 0.3f;
            //BlackBackGround.gameObject.SetActive(false);
            logicManager.Battle();
            BattleText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
