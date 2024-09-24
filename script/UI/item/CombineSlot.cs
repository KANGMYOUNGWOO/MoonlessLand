using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombineSlot : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private Image ItemImage;
   [SerializeField] private GameObject NameTag;
   [SerializeField] private TextMeshProUGUI ItemName;
   [SerializeField] private List< Image> StarList = new List<Image>();

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            StarList[i].gameObject.SetActive(false);
        }
    }


    public void SetCombine(string name, int starNum ,Sprite sprite)
    {
        NameTag.SetActive(true);
        ItemName.text = name;

        for (int i=0;i<4;i++)
        {
            StarList[i].gameObject.SetActive(false);
        }

        for (int i=0; i<starNum;i++)
        {
            StarList[i].gameObject.SetActive(true);
        }
        ItemImage.color = new Color(255f, 255f, 255f, 255f);
        ItemImage.sprite = sprite;

    }

    public void ReleaseCombine()
    {
        NameTag.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            StarList[i].gameObject.SetActive(false);
        }
        ItemImage.color = new Color(255f, 255f, 255f, 0f);
    }

}
