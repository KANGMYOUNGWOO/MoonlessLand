using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Beatbutton : MonoBehaviour
{

    // [SerializeField] private Image image;



    void Start()
    {
        //image = gameObject.GetComponent<Image>();
        transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 1.0f).SetLoops(-1, LoopType.Yoyo);
    }

}
