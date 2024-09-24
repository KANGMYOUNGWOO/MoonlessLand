using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddMessage : MonoBehaviour
{
    UIManager uiManager;
    public List<TraitText> MessageList = new List<TraitText>();


    private void Start()
    {
        uiManager = GameManager.GetManagerClass<UIManager>();
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = new Vector3(0, 0, 0);
        uiManager.itemAddMessage = this;
    }


    public void AddMessage(string input , bool negative = true)
    {
        string[] splited_input = input.Split('#');
        for(int i=0; i<splited_input.Length;i++)
        {
            MessageList[i].ItemSetter(i, splited_input.Length,splited_input[i],negative);
        }


    }




}
