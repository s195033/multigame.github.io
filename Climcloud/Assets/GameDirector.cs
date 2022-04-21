using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject key;
    GameObject quantity;

    int number = 5;
    // Start is called before the first frame update
    void Start()
    {
        this.key = GameObject.Find("keyPrefab");
        this.quantity = GameObject.Find("Quantity");
    }
    void Update()
    {   
        if (number == 0)
        {
            this.quantity.GetComponent<Text>().text = "フラッグを目指そう!!";
        }
    }
    public void Quantity()
    {
        number -= 1; 
        this.quantity.GetComponent<Text>().text = "残りのカギはあと" + number + "つ!!"; 
    }
}
