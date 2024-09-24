using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class TestCard : MonoBehaviour
{

    //public TestCardSet paren { get; set; }

    [SerializeField] private RectTransform CardRect;
    [SerializeField] private Image cardimage;
   
    [SerializeField] private Image ResultImage;
    
   
  
    [SerializeField] private Sprite Front;
    [SerializeField] private Sprite Back;

    private Sprite Result;


    private static float originPos_x = -55.0f;
    private static float originPos_y = -708.0f;

    public bool bisReward = false;
    public bool bisTrick  = false;
    

    

    public void SetCardImage(Sprite front, Sprite back , Sprite result)
    {
        Front = front;
        Back = back;
        Result = result;


        ResultImage.sprite = result;
        cardimage.sprite = back;

        ResultImage.enabled = false;
        CardRect.anchoredPosition = new Vector2(originPos_x, originPos_y);
        cardimage.DOFade(1, 0.1f);
        ResultImage.DOFade(1, 0.1f);


    }

    public void SetCardImage(Sprite result)
    {
        ResultImage.sprite = result;
        CardRect.anchoredPosition = new Vector2(originPos_x, originPos_y);
        cardimage.DOFade(1, 0.1f);
        ResultImage.DOFade(1, 0.1f);
    }



    public void FlyCard(int index , int maxCount)
    {
        ResultImage.enabled = false;
        float pos_x =0, pos_y=0;

        switch(maxCount)
        {
            case 1:
                pos_x = -72f;
                pos_y = 156f;
                break;
            case 2:
                pos_x = -180f + index * 216;
                pos_y = 156f;
                break;
            case 3:
                pos_x = -288f + index * 216;
                pos_y = 156f;
                break;
            case 4:
                pos_x = index < 2 ?  -180f + index * 216 : -180f + (index- 2 )* 216;
                pos_y = index < 2 ? 318 : -6;
                break;

            case 5:
                pos_x = index < 3 ? -288f + index * 216 : -180f + (index-3) * 216;
                pos_y = index < 3 ? 318 : -6;
                break;
            case 6:
                pos_x = index < 3 ? -288f + index * 216 : -288f + (index -3) * 216;
                pos_y = index < 3 ? 318 : -6;
                break;        
            case 7:           
                pos_x = index < 4 ? -396f + index * 216 : -288f + (index - 4) * 216;
                pos_y = index < 4 ? 318 : -6;
                break;        
                              
            case 8:           
                pos_x = index < 4 ? -396f + index * 216 : -396f + (index-4) * 216;
                pos_y = index < 4 ? 318 : -6;
                break;       
            case 9:          
                pos_x = index < 5 ? -504 + 216 * index : -396f + (index - 5) * 216;
                pos_y = index < 5 ? 318 : -6;
                break;

            case 10:
                pos_x = index < 5 ? -504f + 216 * index : -504f + (index - 5) * 216;
                pos_y = index < 5 ? 318 : -6;
                break;

           
        }

        CardRect.DOAnchorPos(new Vector2(pos_x, pos_y), (0.6f)).SetDelay(0.15f * index).From(new Vector2(originPos_x,originPos_y));
        CardRect.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.3f).From(new Vector3(0,0,0)).SetDelay(0.6f + (index * 0.15f))
            .OnComplete(OpenCard);
        //float pos_x = index >= 5 ? -504 + 216 * (index - 5) : -504 + 216 * index;
        //float pos_y = index >= 5 ? -6 : 318;
        
        
    }


   public void OpenCard()
    {
        cardimage.sprite = Front;
        CardRect.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.3f);
        
        ResultImage.enabled = true;
    }

    public void cardEnd()
    {
        cardimage.DOFade(0, 1.0f).SetEase(Ease.InFlash).SetDelay(0.5f);
        ResultImage.DOFade(0, 1.0f).SetEase(Ease.InFlash).SetDelay(0.5f);

    }
    public void ResetPosition(int index, int maxCount, int result)
    {
        
        float pos_x = 0;
        float pos_y = 0;
        switch (maxCount)
        {
            case 1:
                pos_x = -72f;
                pos_y = 156f;
                break;
            case 2:
                pos_x = -180f + index * 216;
                pos_y = 156f;
                break;
            case 3:
                pos_x = -288f + index * 216;
                pos_y = 156f;
                break;
            case 4:
                pos_x = index < 2 ? -180f + index * 216 : -180f + (index - 2) * 216;
                pos_y = index < 2 ? 318 : -6;
                break;

            case 5:
                pos_x = index < 3 ? -288f + index * 216 : -180f + (index - 3) * 216;
                pos_y = index < 3 ? 318 : -6;
                break;
            case 6:
                pos_x = index < 3 ? -288f + index * 216 : -288f + (index - 3) * 216;
                pos_y = index < 3 ? 318 : -6;
                break;
            case 7:
                pos_x = index < 4 ? -396f + index * 216 : -288f + (index - 4) * 216;
                pos_y = index < 4 ? 318 : -6;
                break;

            case 8:
                pos_x = index < 4 ? -396f + index * 216 : -396f + (index - 4) * 216;
                pos_y = index < 4 ? 318 : -6;
                break;
            case 9:
                pos_x = index < 5 ? -504 + 216 * index : -396f + (index - 5) * 216;
                pos_y = index < 5 ? 318 : -6;
                break;

            case 10:
                pos_x = index < 5 ? -504f + 216 * index : -504f + (index - 5) * 216;
                pos_y = index < 5 ? 318 : -6;
                break;



        }

        CardRect.gameObject.transform.rotation = Quaternion.identity;
        cardimage.sprite = Front;
        CardRect.DOAnchorPos(new Vector2(originPos_x,originPos_y), 0.4f)
            .OnComplete(() => {
                CardRect.DOAnchorPos(new Vector2(pos_x, pos_y), (0.6f)).SetDelay(0.15f * index);

                CardRect.gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.3f).SetDelay(0.6f + (index * 0.15f))
          .OnComplete(OpenCard);
            });

       


        
    }


}
