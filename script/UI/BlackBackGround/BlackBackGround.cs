using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BlackBackGround : MonoBehaviour
{


    private LogicManager logicManager;
    private UIManager uiManager;
    private EventManager eventManager;
 
 
    [SerializeField] private TextMeshProUGUI press2;

    private void Awake()
    {
        logicManager = GameManager.GetManagerClass<LogicManager>();
        uiManager = GameManager.GetManagerClass<UIManager>();
        eventManager = GameManager.GetManagerClass<EventManager>();
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0,154,0);
        uiManager.CameraOn();
        ToFade();
        
    }

    private void Start()
    {
        transform.SetAsLastSibling();
    }

    private void ToFade()
    {
        press2.DOFade(0, 1.0f).OnComplete(FadeTo);
    }

    private void FadeTo()
    {
        press2.DOFade(1, 1.0f).OnComplete(ToFade);
    }

    public void StatScene()
    {
        logicManager.StartMode();
        eventManager.GetBookData();
        uiManager.UIArrange();
        Destroy(gameObject);
    }

    







}
