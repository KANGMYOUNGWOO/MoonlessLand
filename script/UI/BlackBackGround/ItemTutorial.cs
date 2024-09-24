using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ItemTutorial : MonoBehaviour
{
    private Vector3 FirstPos_0 = new Vector3(-272.7f, 48.1f, 0f);
    private Vector3 FirstPos_1 = new Vector3(412f, 6.5f, 0f);
    private Vector3 FirstPos_2 = new Vector3(101.23f, -291f, 0f);
    private Vector3 FirstPos_3 = new Vector3(101.23f, 886.2f, 0f);

    private Vector3 FirstPos_4 = new Vector3(101.2f, 207.2f, 0f);

    private Vector3 SecondPos_0 = new Vector3(-20f, 580f, 0f);
    private Vector3 SecondPos_1 = new Vector3(38.04f, -575.7f, 0f);
    private Vector3 SecondPos_2 = new Vector3(-482.6f, -43.8f, 0f);
    private Vector3 SecondPos_3 = new Vector3(131.3f, -43.8f, 0f);

    private Vector3 SecondPos_4 = new Vector3(-327f, -158f, 0f);

    private Vector3 ThirdPos_0 = new Vector3(432.3f, 925.17f, 0f);
    private Vector3 ThirdPos_1 = new Vector3(38.04f, 625.8f, 0f);
    private Vector3 ThirdPos_2 = new Vector3(-453.9f, 883.67f, 0f);
    private Vector3 ThirdPos_3 = new Vector3(-27.7f, -411f, 0f);

    private Vector3 ThirdPos_4 = new Vector3(-314, 283f, 0f);

    private Vector3 ForthPos_0 = new Vector3(-129.2f, 721.1f, 0);
    private Vector3 ForthPos_1 = new Vector3(540.12f, 880.5f, 0);
    private Vector3 ForthPos_2 = new Vector3(540.12f, 539.25f, 0);
    private Vector3 ForthPos_3 = new Vector3(584.88f, 698.63f, 0);

    private Vector3 ForthPos_4 = new Vector3(453f, 573f, 0f);

    private Vector3 FifthPos_0 = new Vector3(-54.4f, 457f, 0);
    private Vector3 FifthPos_1 = new Vector3(273.49f, -262.75f, 0);
    private Vector3 FifthPos_2 = new Vector3(-44f, -758.9f, 0);
    private Vector3 FifthPos_3 = new Vector3(-589f, -262.75f, 0);

    private Vector3 FifthPos_4 = new Vector3(-221f, -548f, 0f);

    private Vector2 FirstScale_0 = new Vector2(645.4f, 2274.2f);
    private Vector2 FirstScale_1 = new Vector2(519.04f, 2190.9f);
    private Vector2 FirstScale_2 = new Vector2(102.46f, 1138.9f);
    private Vector2 FirstScale_3 = new Vector2(102.5f, 997f);

    private Vector2 SecondScale_0 = new Vector2(1150.7f, 1210.4f);
    private Vector2 SecondScale_1 = new Vector2(1266.9f, 1026.6f);
    private Vector2 SecondScale_2 = new Vector2(225.6f, 37.205f);
    private Vector2 SecondScale_3 = new Vector2(822.2f, 37.2f);


    private Vector2 ThirdScale_0 = new Vector2(246.2f, 519.95f);
    private Vector2 ThirdScale_1 = new Vector2(1266.9f, 78.8f);
    private Vector2 ThirdScale_2 = new Vector2(283f, 437f);
    private Vector2 ThirdScale_3 = new Vector2(1241.1f, 1492.8f);

    private Vector2 ForthScale_0 = new Vector2(1075.9f, 606.2f);
    private Vector2 ForthScale_1 = new Vector2(262.74f, 287.5f);
    private Vector2 ForthScale_2 = new Vector2(262.74f, 242.5f);
    private Vector2 ForthScale_3 = new Vector2(173.35f, 76.25f);

    private Vector2 FifthScale_0 = new Vector2(1225.5f, 1154.8f);
    private Vector2 FifthScale_1 = new Vector2(569.7f, 284f);
    private Vector2 FifthScale_2 = new Vector2(1204.7f, 708.3f);
    private Vector2 FifthScale_3 = new Vector2(173.4f, 284f);

    [SerializeField] private RectTransform black1;
    [SerializeField] private RectTransform black2;
    [SerializeField] private RectTransform black3;
    [SerializeField] private RectTransform black4;

    [SerializeField] private RectTransform Arrow;
    [SerializeField] private Image whiteBox;
    [SerializeField] private TextMeshProUGUI HepText;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Arrow.DOSizeDelta(new Vector2(92.5f, 138.1f),0.5f).SetLoops(-1);
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.itemTutorial = this;
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(-77, 187, 0);

        gameObject.SetActive(false);
    }

    public void ChangeBox(int i)
    {
        switch(i)
        {
            case 0:
                black1.anchoredPosition = FirstPos_0;
                black2.anchoredPosition = FirstPos_1;
                black3.anchoredPosition = FirstPos_2;
                black4.anchoredPosition = FirstPos_3;
                black1.sizeDelta = FirstScale_0;
                black2.sizeDelta = FirstScale_1;
                black3.sizeDelta = FirstScale_2;
                black4.sizeDelta = FirstScale_3;

                Arrow.anchoredPosition = FirstPos_4;
                whiteBox.gameObject.SetActive(true);
                HepText.text = "<color=green>약품</color>을 사용하기 위해 터치";

                break;

            case 1:
                black1.anchoredPosition = SecondPos_0;
                black2.anchoredPosition = SecondPos_1;
                black3.anchoredPosition = SecondPos_2;
                black4.anchoredPosition = SecondPos_3;
                black1.sizeDelta = SecondScale_0;
                black2.sizeDelta = SecondScale_1;
                black3.sizeDelta = SecondScale_2;
                black4.sizeDelta = SecondScale_3;

                Arrow.anchoredPosition = SecondPos_4;

                whiteBox.gameObject.SetActive(false);
                break;

            case 2:
                black1.anchoredPosition = ThirdPos_0;
                black2.anchoredPosition = ThirdPos_1;
                black3.anchoredPosition = ThirdPos_2;
                black4.anchoredPosition = ThirdPos_3;
                black1.sizeDelta = ThirdScale_0;
                black2.sizeDelta = ThirdScale_1;
                black3.sizeDelta = ThirdScale_2;
                black4.sizeDelta = ThirdScale_3;

                Arrow.anchoredPosition = ThirdPos_4;
                whiteBox.gameObject.SetActive(true);
                HepText.text 
                = "<color=green>약</color>의 <color=yellow>등급</color>이 <color=red>질병</color>의 등급 이상이고,\n<color=green>약</color>과<color=red>질병</color>의 <color=blue>색</color>이 같다면 치료 가능";

                
                break;

            case 3:
                black1.anchoredPosition = ForthPos_0;
                black2.anchoredPosition = ForthPos_1;
                black3.anchoredPosition = ForthPos_2;
                black4.anchoredPosition = ForthPos_3;
                black1.sizeDelta = ForthScale_0;
                black2.sizeDelta = ForthScale_1;
                black3.sizeDelta = ForthScale_2;
                black4.sizeDelta = ForthScale_3;

                Arrow.anchoredPosition = ForthPos_4;
                whiteBox.gameObject.SetActive(false);

          
                break;
         
        }
    }

}
