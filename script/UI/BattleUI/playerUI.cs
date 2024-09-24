using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class playerUI : MonoBehaviour
{
    private UIManager uiManager;
    [SerializeField] private Image NimrodImage;
    [SerializeField] private Image UserInfoBackGround;
    [SerializeField] private Image ArmorImage;


    [SerializeField] private Image HpBar;
    [SerializeField] private Image MpBar;
    [SerializeField] private Image ArmorBar;

    [SerializeField] private Text  HpText;
    [SerializeField] private Text  MpText;
    [SerializeField] private Text  ArmorText;

    [SerializeField] private TextMeshProUGUI PowerStat;
    [SerializeField] private TextMeshProUGUI ExamineStat;
    [SerializeField] private TextMeshProUGUI AgilityStat;
    [SerializeField] private TextMeshProUGUI StealthStat;

    [SerializeField] private TextMeshProUGUI NimrodName;




    void Start()
    {
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.playerui = this;
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(258,1044,0);
        //transform.localPosition = new Vector3(1, 1, 1);

    }

    public void HPUI(float hp, float mp)
    {
        HpBar.fillAmount = hp / 200f;
        MpBar.fillAmount = mp / 200f;
        HpText.text = hp.ToString();
        MpText.text = mp.ToString();
    }

    public void ArmorUI(float armor, float hp)
    {
        ArmorBar.fillAmount = hp / 200f;
        ArmorText.text = armor.ToString();
    }

    public void AdjustNimrodInfo(Sprite Icon, string name)
    {
        NimrodImage.sprite = Icon;
        NimrodName.text = name;
    }

    public void SetActiveNimrodChanger()
    {
        uiManager.SetActiveNimrodChanger();
    }

    public void ArmorApear(float armor, float hp, bool apea)
    {
        if (apea)
        {
            ArmorImage.gameObject.SetActive(true);
            ArmorImage.rectTransform.DOAnchorPos(new Vector2(-28, 46), 0.1f).From(new Vector2(10, 46)).OnComplete(() =>
            {
                ArmorBar.gameObject.SetActive(true);
                ArmorText.gameObject.SetActive(true);
                ArmorBar.fillAmount = hp / 200f;
                ArmorText.text = armor.ToString();

            });

        }
        else
        {
            ArmorBar.DOFillAmount(0, 0.2f).OnComplete(() =>
            {
                ArmorText.text = "0";
                ArmorText.gameObject.SetActive(false);
                ArmorBar.gameObject.SetActive(false);
                ArmorImage.gameObject.SetActive(false);
                //ArmorIcon.rectTransform.DOAnchorPos(new Vector2(10, 46), 0.1f).From(new Vector2(-28, 46));
            });
        }

    }



}
