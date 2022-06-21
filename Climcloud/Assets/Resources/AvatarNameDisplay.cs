using Photon.Pun;
using TMPro;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarNameDisplay : MonoBehaviourPunCallbacks
{
    float threshold = 0.2f;

    private void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // �v���C���[���ƃv���C���[ID�ƃv���C���[�̃����N��\������
        nameLabel.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
    }
    private void Update()
    {
        float key = 0.0f;
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            if ((Input.acceleration.x > this.threshold) || Input.GetKey(KeyCode.RightArrow)) key = 0.1f;
            if ((Input.acceleration.x < -this.threshold) || Input.GetKey(KeyCode.LeftArrow)) key = -0.1f;
            // ���������ɉ����Ĕ��]
            if (key != 0)
            {
                transform.localScale = new Vector3(key, 0.1f, 0.1f);
            }
        }
    }
}
