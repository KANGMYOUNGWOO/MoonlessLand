using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SemiActionCard : MonoBehaviour,IRecyclableGameObject
{
    [SerializeField] private Image BackGround;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image CostImage;
    [SerializeField] private Image CoolTimeImage;
    [SerializeField] private Image Fixed;

    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI ExplainText;
    [SerializeField] private TextMeshProUGUI CoolTimeText;
    [SerializeField] private TextMeshProUGUI CostText;

    public bool isActive { get; set; }

    public void InitializeCard(bool bisActive,bool isFixed, int cost, int cool, string name, string explain)
    {
        cardImage.gameObject.SetActive(bisActive);
        if(CostText != null) CostText.text = cost.ToString();
        if(CoolTimeText != null) CoolTimeText.text = cool.ToString();
        if (CoolTimeImage != null) CoolTimeImage.gameObject.SetActive(cool > 0);

        NameText.text = name;
        ExplainText.text = explain;
        Fixed.gameObject.SetActive(isFixed);
       

    }

    public void InitializeCard(bool bisActive, bool isFixed, Sprite backGround , Sprite Cost , int cost, int cool, string Name, string explain)
    {
        cardImage.gameObject.SetActive(bisActive);
        cardImage.sprite = backGround;
        CostImage.sprite = Cost;
        if (CostText != null) CostText.text = cost.ToString();
        if (CoolTimeText != null) CoolTimeText.text = cool.ToString();
        if (CoolTimeImage != null) CoolTimeImage.gameObject.SetActive(cool > 0);

        NameText.text = Name;
        ExplainText.text = explain;
        Fixed.gameObject.SetActive(isFixed);
       

    }

    
    public void InitializeCard(bool bisActive , Sprite sprite)
    {
        cardImage.gameObject.SetActive(bisActive);
        BackGround.sprite = sprite;
        Fixed.gameObject.SetActive(false);
       
    }
    

    public void InitializeCard(bool bisActive, bool isFixed, string Name, string explain)
    {
        cardImage.gameObject.SetActive(bisActive);
        NameText.text = Name;
        ExplainText.text = explain;
        Fixed.gameObject.SetActive(isFixed);
       
    }

    public void InitializeCard(bool bisActive, bool isFixed,Sprite Background,  string Name, string explain)
    {
        cardImage.gameObject.SetActive(bisActive);
        NameText.text = Name;
        ExplainText.text = explain;
        Fixed.gameObject.SetActive(isFixed);
       
    }


    public void InitializeCard(Sprite sprite)
    {
        cardImage.gameObject.SetActive(false);
        Fixed.gameObject.SetActive(false);
        BackGround.sprite = sprite;
       
    }

    

}
