using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimrodBackGroundUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(64,166,0);
        //gameObject.SetActive(false);

    }

    
}
