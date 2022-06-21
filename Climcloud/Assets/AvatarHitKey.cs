using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarHitKey : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if(other.TryGetComponent<Key>(out var key))
            {
                photonView.RPC(nameof(HitKey), RpcTarget.All);

                Destroy(key.gameObject);
            }
        }
    }

    [PunRPC]
    private void HitKey()
    {
        PhotonNetwork.LocalPlayer.AddScore(-1);
    }
}
