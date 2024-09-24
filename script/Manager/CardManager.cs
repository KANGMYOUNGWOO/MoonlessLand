using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour , IManager
{
    public GameManager gameManager { get { return GameManager.gameManager; } }

    private List<int> DeckList = new List<int>();
    private List<int> LeftList = new List<int>();
    private List<int> UsedList = new List<int>();
    private List<int> HandCard = new List<int>();


    private void DrawCard()
    {
        int handCount = 0;

        void CheckHandCards()
        {
            while(handCount < 4)
            {
                if (HandCard[handCount] != 0) handCount += 1;
            }
        }
        #region »Ì±â Àü¿¡ µ¦À» ¼¯´Â´Ù. 
        void ShuffleLeft()
        {
            System.Random rng = new System.Random();
            int value1 = LeftList.Count;
            int end = value1;
            while (value1 > 0)
            {
                value1--;
                int value2 = rng.Next(0, end);
                int value3 = LeftList[value2];
                LeftList[value1] = value3;
            }
        }
        #endregion

        CheckHandCards();
        ShuffleLeft();

        for(int i= handCount;i<4;i++)
        {
            HandCard[i] = LeftList[0];
            UsedList.Add(LeftList[0]);
            LeftList.RemoveAt(0);
        }
    }

    private void DrawCard(int num)
    {
        
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }
}
