using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OwnershipSample : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // ���g���Ǘ��҂��ǂ����𔻒肷��
        if (photonView.IsMine)
        {
            // ���L�҂��擾����
            Player owner = photonView.Owner;
            // ���L�҂̃v���C���[����ID���R���\�[���ɏo�͂���
            Debug.Log($"{owner.NickName}({photonView.OwnerActorNr})");
        }
    }
}