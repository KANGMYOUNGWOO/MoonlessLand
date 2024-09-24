using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using UnityEngine.UI;
using DG.Tweening;


    public class CardRejection : MonoBehaviour
    {
       
        [SerializeField] private Image _backGroundImg;
        private RectTransform rect;
        public TextMeshProUGUI textMesh;        
        public bool bissEnd = true;
        [SerializeField] private TextAnimatorPlayer rejectionPlayer;
       UIManager uiManager = null;



    private void Start()
    {
        //_TAnimator.onEvent += OnEvent;        
        //rect = _backGroundImg.rectTransform;
        //rect.anchoredPosition = new Vector2(-93, 265);

        uiManager = GameManager.GetManagerClass<UIManager>();
        uiManager.cardRejection = this;
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(-68,298,0);
        gameObject.SetActive(false);
          rejectionPlayer.textAnimator.onEvent += OnEvent;
    }
    public void Apear(string text)
    {
        _backGroundImg.DOFade(1, 0.1f);
        rejectionPlayer.ShowText(text);
    }

    

    public void OnEvent(string message)
    {
        switch (message)
        {
            case "Disapear":
                _backGroundImg.DOFade(1, 1.5f).OnComplete(() =>
                {
                    textMesh.text = "";
                    _backGroundImg.DOFade(0, 0.5f).OnComplete(EndUI).SetEase(Ease.InFlash);                    
                });

               
                break;
        }
    }

    private void EndUI()
    {
        bissEnd = true;
        
        gameObject.SetActive(false);
    }

       



       




    }
