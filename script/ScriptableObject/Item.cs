using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int itemcode;
    public string ItemName;
    public enum ItemType {Medicine, Ingredient, Consumable,Trash , None ,Essence };
    public Sprite ItemSprite;
    public int ItemDecay;
    //public int ItemSmell;
    public string ItemCompany;
    [TextArea(3,6)]
    public string ItemExplain;
    public string specify1;
    public string specify2;
    public string specify3;


    public ItemType type;
}
