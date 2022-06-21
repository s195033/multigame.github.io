using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGenerator : MonoBehaviourPunCallbacks
{
    public GameObject flagPrefab;
    GameObject quantity;
    float number = 5;
    float count = 0;
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.GetScore() == 0)
        {
            if (count == 0)
            {
                GameObject go = Instantiate(flagPrefab);
                go.transform.position = new Vector2(0, 1);
                count++;
            }
        }
    }
}
