using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGenerator : MonoBehaviour
{
    public GameObject flagPrefab;
    GameObject quantity;
    float number = 5;
    float count = 0;
    // Update is called once per frame
    void Update()
    {
        if (number == 0)
        {
            if (count == 0)
            {
                GameObject go = Instantiate(flagPrefab);
                go.transform.position = new Vector3(1, 20, 0);
                count++;
            }
                
        }
    }

    public void Quantity()
    {
        number -= 1;
    }
}
