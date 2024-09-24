using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class BattleButton : MonoBehaviour
{
    [SerializeField] private List<Image> Fire = new List<Image>();
    [SerializeField] private Button button;

   

    UIManager uiManager;
    BattleManager battleManager;

    private void Start()
    {
        uiManager = GameManager.GetManagerClass<UIManager>();
        battleManager = GameManager.GetManagerClass<BattleManager>();

        battleManager.battleButton = this;
        uiManager.battleButton = this;


       


        gameObject.SetActive(false);
    }

    public void SetFire(bool bisFire)
    {
        if (bisFire) FireOn();
        else FireOut();
    }


    public void FireOn()
    {
        button.enabled = false;
        button.image.rectTransform.sizeDelta = new Vector2(120,120);
        button.gameObject.SetActive(true);
        for (int i=0;i<3;i++)
        {
            Fire[i].rectTransform.sizeDelta = new Vector2(1,1);
        }
        //button.image.rectTransform.DOSizeDelta(new Vector2(120,120),0.4f).From(new Vector2(30,30)).SetEase(Ease.Flash);
        Fire[0].rectTransform.DOSizeDelta(new Vector2(150, 150), 0.4f).SetEase(Ease.InBounce);
        Fire[1].rectTransform.DOSizeDelta(new Vector2(150,150),0.4f).SetEase(Ease.InBounce).SetDelay(0.4f);
        Fire[2].rectTransform.DOSizeDelta(new Vector2(150,150),0.4f).SetEase(Ease.InBounce).SetDelay(0.8f).OnComplete(()=> {
           
            button.enabled = true;
        });
    }

    public void FireOut()
    {
        button.enabled = false;

        Fire[2].rectTransform.DOSizeDelta(new Vector2(1, 1), 0.2f).SetEase(Ease.InBounce);
        Fire[1].rectTransform.DOSizeDelta(new Vector2(1, 1), 0.2f).SetEase(Ease.InBounce).SetDelay(0.2f);
        Fire[0].rectTransform.DOSizeDelta(new Vector2(1, 1), 0.2f).SetEase(Ease.InBounce).SetDelay(0.4f);
        button.image.rectTransform.DOSizeDelta(new Vector2(1,1),0.2f).SetEase(Ease.OutBounce).From(new Vector2(120,120)).SetDelay(0.6f).OnComplete(() => {

            battleManager.DuelSequence();
            gameObject.SetActive(false);

        }); ;

    }


  
   


    
}
