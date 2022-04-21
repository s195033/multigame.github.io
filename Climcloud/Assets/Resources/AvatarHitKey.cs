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
            if (other.TryGetComponent<Key>(out var key))
            {
                if (key.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    photonView.RPC(nameof(HitKey), RpcTarget.All, key.Id, key.OwnerId);
                }
            }
        }
        else if (!photonView.IsMine)
        {
            if (other.TryGetComponent<Key>(out var key))
            {
                if (key.OwnerId == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    photonView.RPC(nameof(HitKey), RpcTarget.All, key.Id);
                }
            }
        }
    }

    [PunRPC]
    private void HitKey(int id, int ownerId, PhotonMessageInfo info)
    {
        var keys = FindObjectsOfType<Key>();
        foreach (var key in keys)
        {
            if (key.Equals(id, ownerId))
            {
                // ���g�����˂����e�����������ꍇ�ɂ́A���g�̃X�R�A�𑝂₷
                if (ownerId == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    PhotonNetwork.LocalPlayer.AddScore(-1);
                }

                // ���W�F�l���[�^�Ƀv���C���ƏՓ˂������Ƃ�`����
                GameObject generator = GameObject.Find("FlagGenerator");
                generator.GetComponent<FlagGenerator>().Quantity();

                Destroy(key.gameObject);
                break;
            }
            else if (key.Equals(id, info.Sender.ActorNumber))
            {
                // ���W�F�l���[�^�Ƀv���C���ƏՓ˂������Ƃ�`����
                GameObject generator = GameObject.Find("FlagGenerator");
                generator.GetComponent<FlagGenerator>().Quantity();

                Destroy(key.gameObject);
                break;
            }
        }
    }
}
