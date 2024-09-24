using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using DG.Tweening;

public class UIBattle : MonoBehaviour
{
    [SerializeField] private Text EnemyHp;
   

    [SerializeField] private Image HPImage;
    [SerializeField] private Image weakIcon;
    [SerializeField] private Image immuneIcon;
    

    
    [SerializeField] private TextAnimatorPlayer Victory;
    [SerializeField] private TextAnimatorPlayer bisBonusText;
    [SerializeField] private TextAnimatorPlayer bisPenaltyText;


    [SerializeField] private TextMeshProUGUI powtext;
    [SerializeField] private TextMeshProUGUI agitext;
    [SerializeField] private TextMeshProUGUI exatext;
    [SerializeField] private TextMeshProUGUI stetext;

    [SerializeField] private TextMeshProUGUI card1text;
    [SerializeField] private TextMeshProUGUI card1subText;
    [SerializeField] private TextMeshProUGUI card2text;
    [SerializeField] private TextMeshProUGUI card2subText;

    //[SerializeField] private RectTransform benefitRect;
    //[SerializeField] private RectTransform penaltyRect;

   

    [SerializeField] private Image card1;
    [SerializeField] private Image card2;

    private int MaxHp;
    private int MaxBenefit;
    private int MaxPenalty;
    private Vector2 originBenefitPos;
    private Vector2 originPenaltyPos;


    private BattleManager battleManager;

    private void Start()
    {
        battleManager = GameManager.GetManagerClass<BattleManager>();
        battleManager.BattleUI = this;
        gameObject.SetActive(false);
        //originBenefitPos = benefitRect.anchoredPosition;
        //originPenaltyPos = penaltyRect.anchoredPosition;
       
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        card1subText.gameObject.SetActive(false);
        card2subText.gameObject.SetActive(false);

    }

    public void InitializeUI(int maxhp, int str, int agi, int exa, int ste, Sprite weak, Sprite immune)
    {
        EnemyHp.text = maxhp.ToString();
        //EnemyBenefit.text = maxben.ToString();
        //EnemyPenalty.text = maxpen.ToString();

        MaxHp = maxhp;
        //MaxBenefit = maxben;
        //MaxPenalty = maxpen;


        HPImage.DOFillAmount(1, 0.3f).SetEase(Ease.InElastic);
        //BenefitImage.DOFillAmount(1, 0.3f).SetEase(Ease.InElastic);
        //PenaltyImage.DOFillAmount(1, 0.3f).SetEase(Ease.InElastic);

        //EnemyWeak.ShowText(weak);
        //EnemyImmune.ShowText(immune);

        //BenefitText.ShowText(string.Format("{0} <color=#15AC6C>보너스</color>", benefit));
        //PenaltyText.ShowText(string.Format("{0} <color=#960E0F>패널티</color>", penalty));

        powtext.text = str.ToString();
        agitext.text = agi.ToString();
        exatext.text = exa.ToString();
        stetext.text = ste.ToString();

        weakIcon.sprite = weak;
        immuneIcon.sprite = immune;
    }

    public void SetHPBar(int hp)
    {
        EnemyHp.text = hp.ToString();
        HPImage.rectTransform.DOShakeAnchorPos(1f, strength: 10, vibrato: 30);
        HPImage.DOFillAmount((float)hp / MaxHp, 0.3f).SetEase(Ease.InBounce);
    }

    public void SetHPBar()
    {
        HPImage.rectTransform.DOShakeAnchorPos(3f, strength: 10, vibrato: 30).OnComplete(() => HPImage.DOFillAmount(0, 0.3f).SetEase(Ease.InFlash));
    }
    /*
    public void Win(bool bisBonus, bool bisPenalty, int grade,  refTestCard tc1 = null, refTestCard tc2 = null, Sprite s1= null, Sprite s2 = null , Sprite s3 = null, Sprite s4= null)
    {
        string text1 = "";
        string text2 ="";
        EnemyHp.text = "0";
        HPImage.DOFillAmount(0, 0.3f).SetEase(Ease.InFlash);
        HPImage.rectTransform.DOShakeAnchorPos(0.5f, strength: 30, vibrato: 60).OnComplete(() => {
        

        Victory.ShowText("<color=green>승리</color>");
        benefitRect.DOAnchorPos(new Vector2(0, -60), 0.5f).From(originBenefitPos).SetDelay(1.0f).OnComplete(() =>
         BenefitText.ShowText(string.Format("<wiggle>{0}</wiggle>", BenefitText.textAnimator.text)));
        penaltyRect.DOAnchorPos(new Vector2(0, -222), 0.5f).From(originBenefitPos).SetDelay(1.5f).OnComplete(() =>
        PenaltyText.ShowText(string.Format("<wiggle>{0}</wiggle>", PenaltyText.textAnimator.text)));

       

        HPImage.DOFillAmount(0, 0.1f).SetDelay(3.5f).OnComplete(() => {
             if (bisBonus) bisBonusText.ShowText("<color=green>성공</color>");
             else bisBonusText.ShowText("<color=red>실패</color>");
        });

        HPImage.DOFillAmount(0, 0.1f).SetDelay(4.0f).OnComplete(() => {
             if (!bisPenalty) bisPenaltyText.ShowText("<color=green>성공</color>");
             else bisPenaltyText.ShowText("<color=red>실패</color>");
         });

        HPImage.DOFillAmount(0, 0.1f).SetDelay(4.5f).OnComplete(() => {
            if (grade == 2) Victory.ShowText("<color=green>보너스!</color>");
            else Victory.ShowText("<color=green>일반 보상</color>");

            if (tc1 != null)
            {
                card1.sprite = s1;
                card2.sprite = s2;
                card1.gameObject.SetActive(true);
                card2.gameObject.SetActive(true);
                card1.rectTransform.DOAnchorPos(new Vector2(-285, 227),0.6f).SetEase(Ease.OutBounce).From(new Vector2(-285, -946));
                card2.rectTransform.DOAnchorPos(new Vector2(264, 227),0.6f).SetEase(Ease.OutBounce).From(new Vector2(264, -946));

                card1.rectTransform.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.3f).SetDelay(0.6f).OnComplete(() => {
                    card1.sprite = s3;
                    text1 = " <sprite=5>  ";
                    for (int i = 0; i < tc1.over; i++) text1 += "<sprite=4>";
                    text1 += "\n\n";
                    text1 += " <sprite=6>  ";
                    for (int i = 0; i < tc1.success; i++) text1 += "<sprite=4>";
                    text1 += "\n\n";
                    text1 += " <sprite=7>  ";
                    for (int i = 0; i < tc1.fail; i++) text1 += "<sprite=4>";
                    text1 += "\n\n";
                    text1 += " <sprite=8>  ";
                    for (int i = 0; i < tc1.hell1; i++) text1 += "<sprite=4>";
                    text1 += "\n\n";
                    text1 += " <sprite=9>  ";
                    for (int i = 0; i < tc1.hell2; i++) text1 += "<sprite=4>";
                    card1text.text = text1;
                    card1subText.gameObject.SetActive(true);
                });
                card1.rectTransform.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.3f).SetDelay(0.9f);

                card2.rectTransform.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.3f).SetDelay(0.6f).OnComplete( () =>{
                    card2.sprite = s4;
                    text2 += " <sprite=5>  ";
                    for (int i = 0; i < tc2.over; i++) text2 += "<sprite=4>";
                    text2 += "\n\n";
                    text2 += " <sprite=6>  ";
                    for (int i = 0; i < tc2.success; i++) text2 += "<sprite=4>";
                    text2 += "\n\n";
                    text2 += " <sprite=7>  ";
                    for (int i = 0; i < tc2.fail; i++) text2 += "<sprite=4>";
                    text2 += "\n\n";
                    text2 += " <sprite=8>  ";
                    for (int i = 0; i < tc2.hell1; i++) text2 += "<sprite=4>";
                    text2 += "\n\n";
                    text2 += " <sprite=9>  ";
                    for (int i = 0; i < tc2.hell2; i++) text2 += "<sprite=4>";
                    card2text.text = text2;

                   
                    card2subText.gameObject.SetActive(true);
                });
                card2.rectTransform.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.3f).SetDelay(0.9f);

              
            }
        });


        });
       
        

    }

    */

        /*
    public void SetBenefitBar(int ben)
    {
        EnemyBenefit.text = ben.ToString();
        BenefitImage.DOFillAmount((float)ben / MaxBenefit, 0.3f).SetEase(Ease.InBounce);
    }

    public void SetPenaltyBar(int pen)
    {
        EnemyPenalty.text = pen.ToString();
        PenaltyImage.DOFillAmount((float)pen / MaxPenalty, 0.3f).SetEase(Ease.InBounce);
    }
    */


}
