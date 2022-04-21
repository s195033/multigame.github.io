using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class OwnerChangeCallbacksSample : MonoBehaviourPun, IOnPhotonViewOwnerChange, IOnPhotonViewControllerChange
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

    // �l�b�g���[�N�I�u�W�F�N�g�̏��L�҂��ύX���ꂽ���ɌĂ΂��R�[���o�b�N
    void IOnPhotonViewOwnerChange.OnOwnerChange(Player newOwner, Player previousOwner)
    {
        string objectName = $"{photonView.name}({photonView.ViewID})";
        string oldName = previousOwner.NickName;
        string newName = newOwner.NickName;
        Debug.Log($"{objectName} �̏��L�҂� {oldName} ���� {newName} �ɕύX����܂���");
    }

    // �l�b�g���[�N�I�u�W�F�N�g�̊Ǘ��҂��ύX���ꂽ���ɌĂ΂��R�[���o�b�N
    void IOnPhotonViewControllerChange.OnControllerChange(Player newController, Player previousController)
    {
        string objectName = $"{photonView.name}({photonView.ViewID})";
        string oldName = previousController.NickName;
        string newName = newController.NickName;
        Debug.Log($"{objectName} �̊Ǘ��҂� {oldName} ���� {newName} �ɕύX����܂���");
    }
}