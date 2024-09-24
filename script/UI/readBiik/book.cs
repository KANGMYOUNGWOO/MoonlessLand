using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using UnityEngine.UI;
using DG.Tweening;

public class book : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private AdventureDict dictionary;
  


    //[SerializeField] private Inventory;
    //[SerializeField] private Inventory;


    [SerializeField] private GameObject BookMarkObject;
    [SerializeField] private List<GameObject> BookMarkList = new List<GameObject>();
  
    [SerializeField] private TextAnimatorPlayer messageTap;
    private UIManager uiManager;
    [SerializeField] private BookReader reader;
    [SerializeField] private BookReader reader2;
    [SerializeField] private BookReader reader3;
    [SerializeField] private BookReader reader4;
    private bool isreading = false;

    private int Index = 0;
    private bool bisProduction = false;
    private bool bisProduction2 = false;


    private void Start()
    {       
        reader.b = this;
        reader2.b = this;
        reader3.b = this;
        reader4.b = this;

        transform.SetSiblingIndex(9);
        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.bookObject = this;
        messageTap.textAnimator.onEvent += endEvent;
        //gameObject.SetActive(false);
    }




    public void TouchBook(int index)
    {
        this.Index = index;
        isreading = true;
        inventory.gameObject.SetActive(false);
        dictionary.gameObject.SetActive(false);
      

        for (int i = 0; i < BookMarkList.Count; i++)
        {
            BookMarkList[i].SetActive(false);
        }

        StartCoroutine(bookRoutine());

        //reader.StartFlipping();
        //reader2.StartFlipping();
        //reader3.StartFlipping();
        //reader4.StartFlipping();

        IEnumerator bookRoutine()
        {
            WaitForSeconds flip1 = new WaitForSeconds(0.15f);
            WaitForSeconds flip2 = new WaitForSeconds(0.15f);
            WaitForSeconds flip3 = new WaitForSeconds(0.15f);

            reader.StartFlipping();            
            yield return flip1;
            reader2.StartFlipping();
            yield return flip2;
            reader3.StartFlipping();
            yield return flip3;
            reader4.StartFlipping();
           
        }

    }

    public void DisActivation()
    {
        if (isreading) return;
        gameObject.SetActive(!gameObject.activeSelf);

    }

    public void disActive()
    {
        uiManager.BookButtonActive(false);
        gameObject.SetActive(false);
    }

    public void buttonActivation()
    {
        uiManager.BookButtonActive(true);
    }

    #region
  

   

    public void endEvent(string mes)
    {
        IEnumerator Disapearing()
        {
            yield return new WaitForSeconds(0.7f);
            messageTap.gameObject.SetActive(false);
            bisProduction = false;
            gameObject.SetActive(false);
        }

        if (mes == "endEvent")
        {
            messageTap.StartDisappearingText();
            StartCoroutine(Disapearing());
        }

    }


    public void Productions(int message)
    {
        switch (message)
        {
            case 0:
                gameObject.SetActive(true);
                bisProduction = true;
                TouchBook(0);
                for (int i = 0; i < BookMarkList.Count; i++)
                {
                    BookMarkList[i].SetActive(false);
                }
                break;
            case 1:
                gameObject.SetActive(true);
                TouchBook(1);
                uiManager.BookButtonActive(true);
              
                break;
            case 2:
                gameObject.SetActive(true);
                TouchBook(2);
                bisProduction = true;
                break;

            case 3:
                gameObject.SetActive(true);
                bisProduction2 = true;
                bisProduction = false;
                TouchBook(2);
                uiManager.BookButtonActive(false);
                break;
            case 4:
                uiManager.BookButtonActive(true);
                break;


        }



    }


    

   

   
    #endregion


    public void ActivePage()
    {
        for (int i = 0; i < BookMarkList.Count; i++)
        {
            BookMarkList[i].SetActive(true);
            BookMarkList[i].transform.SetParent(this.transform);
            BookMarkList[i].transform.SetAsFirstSibling();
        }

        BookMarkList[Index].transform.SetParent(BookMarkObject.transform);

        switch (Index)
        {
            case 0:
                #region 튜토리얼
                if (bisProduction)
                {
                    for (int i = 0; i < BookMarkList.Count; i++)  BookMarkList[i].SetActive(false);
                    messageTap.gameObject.SetActive(true);
                    messageTap.ShowText("<shake a=1>준비된 탐험가에게는 죽음 마저도 모험일 뿐</shake> <waitfor=1.0> <?endEvent> ");
                    
                }
                #endregion
                break;
            case 1:
                dictionary.gameObject.SetActive(true);                
                break;
            case 2:
                inventory.gameObject.SetActive(true);
                
                #region 튜토리얼
                if (bisProduction)
                {
                    uiManager.Prologues("ItemTutorial1");
                    bisProduction = false;
                }
                #endregion
                #region  합성 튜토리얼
                if (bisProduction2)
                {
                    inventory.ReleaseCombineLcok();
                    bisProduction2 = false;
                }
                #endregion
                break;
           
        }

        
        isreading = false;

    }


}
