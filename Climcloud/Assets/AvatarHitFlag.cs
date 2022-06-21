using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;　// LoadSceneを使うために必要!!

public class AvatarHitFlag : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");
    }
}
