using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NimrodChanger : MonoBehaviour
{
    [SerializeField] private Image CurrentModule;
    [SerializeField] private Image BackGround;    
    [SerializeField] private Image Slot1;
    [SerializeField] private Image Slot2;
    [SerializeField] private Image Slot3;
    [SerializeField] private Image Slot4;
    [SerializeField] private Image Slot5;
    [SerializeField] private Image Slot6;

    private bool[] moduleNums = new bool[6];
    public bool bisProhibit = false;

    private UIManager uiManager;


    public void ActiveSlots()
    {
        if (bisProhibit) return;

        BackGround.DOFade(0.7f,0.2f).From(0);
      
        CurrentModule.DOFade(1,0.4f).From(0).SetEase(Ease.Linear);
        Slot1.DOFade(1,0.4f).From(0).SetEase(Ease.Linear).SetDelay(0.1f);
        Slot2.DOFade(1,0.4f).From(0).SetEase(Ease.Linear).SetDelay(0.2f);
        Slot3.DOFade(1,0.4f).From(0).SetEase(Ease.Linear).SetDelay(0.3f);
        Slot4.DOFade(1,0.4f).From(0).SetEase(Ease.Linear).SetDelay(0.4f);
        Slot5.DOFade(1,0.4f).From(0).SetEase(Ease.Linear).SetDelay(0.5f);
        Slot6.DOFade(1,0.4f).From(0).SetEase(Ease.Linear).SetDelay(0.6f);
    }

    public void intializeSlot(int index , Sprite sprite , string name , bool bisIn)
    {
        switch(index)
        {
            case 0:
                Slot1.sprite = sprite;
                break;
            case 1:
                Slot2.sprite = sprite;
                break;
            case 2:
                Slot3.sprite = sprite;
                break;
            case 3:
                Slot4.sprite = sprite;
                break;
            case 4:
                Slot5.sprite = sprite;
                break;
            case 5:
                Slot6.sprite = sprite;
                break;
        }

        moduleNums[index] = bisIn;
    }

    public void intializeSlot(int index, Sprite sprite, string name)
    {
        switch (index)
        {
            case 0:
                Slot1.sprite = sprite;
                break;
            case 1:
                Slot2.sprite = sprite;
                break;
            case 2:
                Slot3.sprite = sprite;
                break;
            case 3:
                Slot4.sprite = sprite;
                break;
            case 4:
                Slot5.sprite = sprite;
                break;
            case 5:
                Slot6.sprite = sprite;
                break;
        }

       
    }




    public void initializeSlot(Sprite sprite , string name)
    {
        CurrentModule.sprite = sprite;
    }

    public void initializeSlot(int index, bool bisSelected)
    {
        switch (index)
        {
            case 0:
                if (bisSelected) Slot1.color = Color.red;
                else Slot1.color = Color.white;
                break;
            case 1:
                if (bisSelected) Slot2.color = Color.red;
                else Slot2.color = Color.white;
                break;
            case 2:
                if (bisSelected) Slot3.color = Color.red;
                else Slot3.color = Color.white;
                break;
            case 3:
                if (bisSelected) Slot4.color = Color.red;
                else Slot4.color = Color.white;
                break;
            case 4:
                if (bisSelected) Slot5.color = Color.red;
                else Slot5.color = Color.white;
                break;
            case 5:
                if (bisSelected) Slot6.color = Color.red;
                else Slot6.color = Color.white;
                break;
        }
    }



    private void Start()
    {
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.nimrodChanger = this;

        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 0, 0);
        gameObject.SetActive(false);

    }


    public void ButtonAction(int index)
    {
        if(index !=6)
            if (!moduleNums[index]) return;

        for (int i = 0; i < 6; i++) initializeSlot(i, false);
       
        if(index == 6)
        {
            uiManager.ActiveNimrodMain();
            gameObject.SetActive(false);
        }
        else
        {
          
            initializeSlot(index, true);
            uiManager.NimrodChangeButtonAction(index);
        }
       
    }

    public void QuitnimrodChanger()
    {
        gameObject.SetActive(false);
    }

}
