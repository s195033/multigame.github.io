using Photon.Pun;
using UnityEngine;

public class InstantiateCallbackSample : MonoBehaviour, IPunInstantiateMagicCallback
{
    // �l�b�g���[�N�I�u�W�F�N�g���������ꂽ���ɌĂ΂��R�[���o�b�N
    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
        {
            Debug.Log("���g���l�b�g���[�N�I�u�W�F�N�g�𐶐����܂���");
        }
        else
        {
            Debug.Log("���v���C���[���l�b�g���[�N�I�u�W�F�N�g�𐶐����܂���");
        }
    }
}
