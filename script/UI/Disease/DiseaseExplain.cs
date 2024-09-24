using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class DiseaseExplain : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI DiseaseName;
    [SerializeField] private TextMeshProUGUI Explain;
    private RectTransform rectTransform;

    public DIseaseTab dIseaseTab { get; set; }

    public void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }


    public void ActiveExplain(string name,string line)
    {
        DiseaseName.text = name;
        Explain.text = line;
    }

    public void ActiveEffect(string name, string line)
    {
        DiseaseName.text = "";
        Explain.text = "";
        rectTransform.DOScale(new Vector3(1, 1, 1), 0.6f).SetEase(Ease.InOutCubic).From(new Vector3(0.1f, 0.1f, 0.1f)).OnComplete(()=>Test(name,line));
        
    }

    public void Test(string name, string line)
    {
        DiseaseName.text = name;
        Explain.text = line;
    }

    public void Out()
    {
        gameObject.SetActive(false);
    }

}
