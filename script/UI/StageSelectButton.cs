using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StageSelectButton : MonoBehaviour
{
    private RectTransform rect;
    private Button button;
    private Image image;
    //private TextMeshPro text;
    private TextMeshProUGUI text;


    private Sequence beatSequence;

    private void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
        button = gameObject.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
       // text = gameObject.GetComponent<TextMeshPro>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        

        transform.localScale =new Vector3(0.01f, 0.01f, 0.01f);
        gameObject.SetActive(false);

        /*
        beatSequence = DOTween.Sequence().AppendCallback(()=>{
            image.DOColor(Color.white, 0.2f);
            rect.DOSizeDelta(new Vector2(120f, 120f), 1.0f).SetLoops(-1, LoopType.Yoyo);
        }
        );
        */
        beatSequence = DOTween.Sequence().Append(rect.DOSizeDelta(new Vector2(120f, 120f), 1.0f));
        beatSequence.SetLoops(-1,LoopType.Yoyo);

        beatSequence.Pause();

    }

    public void SetRectTransform(int _x, int _y , int index ,int timer = 1)
    {
        rect.anchoredPosition = new Vector2(_x, _y);
        //transform.localScale = new Vector3(1f, 1f, 1f);
        rect.sizeDelta = new Vector2(160f,160f);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetDelay((index-1)*0.3f).SetEase(Ease.OutBounce).From(new Vector3(0.01f, 0.01f, 0.01f),true);
      //  transform.DORotate(new Vector3(0, 0, -30), 0.2f).SetLoops(5, LoopType.Restart).SetDelay((index - 1) * 0.3f);
    }

    public void SetRectTransform(int index)
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetDelay((index) * 0.3f).SetEase(Ease.OutBounce).From(new Vector3(0.01f, 0.01f, 0.01f), true);
    }



    public void LiveButton(bool live)
    {
        button.interactable = live;
        text.gameObject.SetActive(true);
        if (live) beatButton();
        
        else DieButton();
    }

    public void StopSequence()
    {
       
        beatSequence.Pause();
    }

    private void beatButton()
    {        
        image.DOColor(Color.white, 0.2f);
        beatSequence.Play();          
    }
    private void DieButton()
    {
        
        image.DOColor(Color.black, 0.5f);
        beatSequence.Pause();       
        button.interactable = false;
    }

    public void Selected()
    {
        button.interactable = false;
        text.gameObject.SetActive(false);

    }

    public void disapear(int index)
    {
        transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.5f).SetDelay((index) * 0.4f).SetEase(Ease.InBounce);
    }

    public void SelectEnd(int index)
    {
        rect.DOAnchorPos(new Vector2(-74, -550), 1.0f).SetDelay((index)*0.4f).SetEase(Ease.InFlash);
    }


    public void DictionaryWork()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.OutBounce).From(new Vector3(0.01f, 0.01f, 0.01f), true);
        beatSequence.Play();
    }

    public void SizeSet()
    {
        transform.localScale = new Vector3(1,1,1);
    }


    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetText(string t)
    {
        text.text = t;
    }

    
}
