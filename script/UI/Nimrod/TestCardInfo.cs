using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestCardInfo : MonoBehaviour
{
    [SerializeField] private Image TestCardImage;
    [SerializeField] private TextMeshProUGUI percentageText;
    [SerializeField] private TextMeshProUGUI StatText;
   
    
    public void InitializeInfo(Sprite sprite, string explain, string stat)
    {
        TestCardImage.sprite = sprite;
        percentageText.text  = explain;
        StatText.text        = stat;
   
    }
    
}
