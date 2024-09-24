using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TraitText : MonoBehaviour
{
    [SerializeField] private Image Line;
    [SerializeField] private RectTransform rect;

    [SerializeField] private Sprite GreenLine;
    [SerializeField] private Sprite RedLine;

    [SerializeField] private Text Explanation;

    private string exp;
    
    private float length;
    private int indexer = 0;

   
    
   public void ItemSetter(int index, int maxCount, string explain , bool negative)
    {
        float pos_x = 0;
        float pos_y = 0;
        pos_x = 600.0f - (25 * 3);
        pos_y = 528.0f - (150 * index);

        Explanation.text = ""; 
        rect.anchoredPosition = new Vector2(894.0f, pos_y);

        rect.DOAnchorPos(new Vector2(pos_x, pos_y), 0.5f).SetDelay(1.0f + index * 0.4f).SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                if(negative) Explanation.DOText(string.Format("<color=green>+{0}</color>",explain), 0.2f).SetDelay(0.2f);
                else         Explanation.DOText(string.Format("<color=red>-{0}</color>",explain), 0.2f).SetDelay(0.2f);
                //Line.DOFillAmount(length * 0.07f, 0.2f).SetDelay(0.2f);
                rect.DOAnchorPos(new Vector2(894.0f, pos_y), 0.5f).SetDelay((maxCount - index) * 0.4f + 1).SetEase(Ease.OutBounce);
            });

    }



    public void LineSetter(int index, int maxCount , string explain )
    {
        exp = explain;
        length = exp.Length;
        indexer = index;
       

        float pos_x = 0;
        float pos_y = 0;
        pos_x = 600.0f - (25 * length);
        pos_y =528.0f -(150 * index);


        //Explanation.DoText("", 1.0f).SetDelay(4 + index);
        /*
        rect.DOAnchorPos(new Vector2(pos_x, pos_y), 0.5f).SetDelay(3.5f + index * 1.0f).SetEase(Ease.InExpo)
            //.OnComplete(()=> Explanation.DOText(exp, 1.0f).SetDelay(0.5f).SetRelative())
            .OnComplete(()=> Line.DOFillAmount(1 * length * 0.05f, 1.0f).SetDelay(0.5f).SetRelative());
            */
        rect.anchoredPosition = new Vector2(894.0f,pos_y);

        rect.DOAnchorPos(new Vector2(pos_x, pos_y), 0.5f).SetDelay(1.0f + index * 0.4f).SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                Explanation.DOText(exp, 0.2f).SetDelay(0.2f);
                Line.DOFillAmount( length * 0.07f, 0.2f).SetDelay(0.2f);
                rect.DOAnchorPos(new Vector2(894.0f, pos_y), 0.5f).SetDelay((maxCount - index)* 0.4f+1).SetEase(Ease.OutBounce);
            });
            
        //Explanation.DOText(exp,1.0f).SetDelay(4.5f+index*0.5f).SetRelative();
        //Line.DOFillAmount(1 *length *0.05f ,1.0f).SetDelay(4.5f+index*0.5f).SetRelative();
    }

    public void LineSetter(int index, int maxCount, string explain, bool negative)
    {
        exp = explain;
        length = exp.Length;
        indexer = index;


        float pos_x = 0;
        float pos_y = 0;
        pos_x = 600.0f - (25 * length);
        pos_y = 528.0f - (150 * index);


        //Explanation.DoText("", 1.0f).SetDelay(4 + index);
        /*
        rect.DOAnchorPos(new Vector2(pos_x, pos_y), 0.5f).SetDelay(3.5f + index * 1.0f).SetEase(Ease.InExpo)
            //.OnComplete(()=> Explanation.DOText(exp, 1.0f).SetDelay(0.5f).SetRelative())
            .OnComplete(()=> Line.DOFillAmount(1 * length * 0.05f, 1.0f).SetDelay(0.5f).SetRelative());
            */
        rect.anchoredPosition = new Vector2(894.0f, pos_y);

        rect.DOAnchorPos(new Vector2(pos_x, pos_y), 0.5f).SetDelay(1.0f + index * 0.4f).SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                if (negative) Explanation.DOText(string.Format("<color=green>{0}</color>", explain), 0.2f).SetDelay(0.2f);
                else Explanation.DOText(string.Format("<color=red>{0}</color>", explain), 0.2f).SetDelay(0.2f);
                rect.DOAnchorPos(new Vector2(894.0f, pos_y), 0.5f).SetDelay((maxCount - index) * 0.4f + 1).SetEase(Ease.OutBounce);
            });

        //Explanation.DOText(exp,1.0f).SetDelay(4.5f+index*0.5f).SetRelative();
        //Line.DOFillAmount(1 *length *0.05f ,1.0f).SetDelay(4.5f+index*0.5f).SetRelative();
    }





    public void ClearSetter(int index)
    {
        float pos_y = 528.0f - (150 * index);
        rect.DOAnchorPos(new Vector2(894.0f, pos_y),0.2f);
    }
   

}
