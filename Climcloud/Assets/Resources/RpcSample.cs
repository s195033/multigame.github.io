using Photon.Pun;
using UnityEngine;

public class RpcSample : MonoBehaviourPunCallbacks
{
    private void Update()
    {
        // �}�E�X�N���b�N���ɁA���[�����̃v���C���[�S���Ƀ��b�Z�[�W�𑗐M����
        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC(nameof(RpcSendMessage), RpcTarget.All, "����ɂ���");
        }
    }

    [PunRPC]
    private void RpcSendMessage(string message, PhotonMessageInfo info)
    {
        // ���b�Z�[�W�𑗐M�����v���C���[�����\������
        Debug.Log($"{info.Sender.NickName}: {message}");
    }
}
