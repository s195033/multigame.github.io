using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;�@// LoadScene���g�����߂ɕK�v!!

public class AvatarHitFlag : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�S�[��");
        SceneManager.LoadScene("ClearScene");
    }
}
