using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Febucci.UI;
using DG.Tweening;
using TMPro;

public class MedicineRejection : MonoBehaviour
{
    [SerializeField] private Image _backGroundImg;
    [SerializeField] private TextAnimatorPlayer taPlayer;
    [SerializeField] private TextMeshProUGUI textMesh;
    public bool bissEnd = true;
    // Start is called before the first frame update
    void Start()
    {
        taPlayer.textAnimator.onEvent += OnEvent;

    }

    public void Apear(string text)
    {
        if (bissEnd) return;
        bissEnd = false;
        gameObject.SetActive(true);       
        taPlayer.ShowText(text);
        _backGroundImg.DOFade(1, 0.1f);
    }


    public void OnEvent(string message)
    {
        switch(message)
        {
            case "MRDisapear":
                textMesh.text = "";
                _backGroundImg.DOFade(0, 0.5f).OnComplete(EndUI).SetEase(Ease.InFlash);
                break;

        }
    }
    private void EndUI()
    {
        bissEnd = true;

        gameObject.SetActive(false);
    }





}
