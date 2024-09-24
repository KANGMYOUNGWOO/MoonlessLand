using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleSet : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> StatTexts  = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI nimrodName;
    [SerializeField] private List<Image> CardImages = new List<Image>();
    [SerializeField] private Image Icon;



    public void SetImage(Sprite str, Sprite agl, Sprite exa, Sprite ste ,Sprite nimrod)
    {
        CardImages[0].sprite = str;
        CardImages[1].sprite = agl;
        CardImages[2].sprite = exa;
        CardImages[3].sprite = ste;
        Icon.sprite          = nimrod;
    }
    public void SetText(int str, int agl, int exa, int ste)
    {
        StatTexts[0].text = str.ToString();
        StatTexts[1].text = agl.ToString();
        StatTexts[2].text = exa.ToString();
        StatTexts[3].text = ste.ToString();
    }
    public void SetName(string name)
    {
        nimrodName.text = name;
    }
    



        
}
