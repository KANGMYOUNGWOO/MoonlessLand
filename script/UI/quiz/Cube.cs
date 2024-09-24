using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FronkonGames.SpritesMojo;

public class Cube : MonoBehaviour
{
    [SerializeField] private Image CubeImage;
    private Material CubeMat;
    private Color emptyColor;
    private float randomRange = 0.25f;



    private void Awake()
    {
        CubeMat = Negative.CreateMaterial();
        Negative.ColorChannels.Set(CubeMat, Random.ColorHSV(0.5f - randomRange, 0.5f + randomRange),1.0f);
        ColorUtility.TryParseHtmlString("#545454", out emptyColor);
    }

    
    public void SetColor(int index)
    {
        Negative.ColorChannels.Set(CubeMat, Random.ColorHSV(0.5f - randomRange, 0.5f + randomRange), 1.0f);
        StartCoroutine(waitColorChange());
        IEnumerator waitColorChange()
        {
            WaitForSeconds wait = new WaitForSeconds(1.0f);

            yield return wait;
            switch(index)
            {
                case 0:
                    CubeImage.color = Color.black;
                    break;
                case 1:
                    CubeImage.color = Color.white;
                    break;
                case 2:
                    CubeImage.color = emptyColor;
                    break;

            }

            
        }


    }
    
}
