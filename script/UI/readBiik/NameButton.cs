using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NameButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI NameText;
    public string s_name { get; set; }


    public void DisApear(string name)
    {
        button.interactable = false;
        s_name = name;
        NameText.DOFade(0, 0.3f).SetEase(Ease.Flash).OnComplete(Apear);
    }

    private void Apear()
    {
        button.interactable = true;
        NameText.DOFade(1, 0.3f).SetEase(Ease.Flash);
        NameText.text = s_name;
    }

}
