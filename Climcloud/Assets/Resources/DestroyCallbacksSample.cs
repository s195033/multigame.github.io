using Photon.Pun;
using UnityEngine;

public class DestroyCallbacksSample : MonoBehaviourPun, IOnPhotonViewPreNetDestroy
{
    private void OnEnable()
    {
        // PhotonView�̃R�[���o�b�N�Ώۂɓo�^����
        photonView.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        // PhotonView�̃R�[���o�b�N�Ώۂ̓o�^����������
        photonView.RemoveCallbackTarget(this);
    }

    // �l�b�g���[�N�I�u�W�F�N�g���j������钼�O�ɌĂ΂��R�[���o�b�N
    void IOnPhotonViewPreNetDestroy.OnPreNetDestroy(PhotonView rootView)
    {
        Debug.Log($"{rootView.name}({rootView.ViewID}) ���j������܂�");
    }
}
