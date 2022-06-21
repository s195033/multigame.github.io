using Photon.Pun;
using TMPro;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class AvatarNameDisplay : MonoBehaviourPunCallbacks
{
    float threshold = 0.2f;

    private void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        // プレイヤー名とプレイヤーIDとプレイヤーのランクを表示する
        nameLabel.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
    }
    private void Update()
    {
        float key = 0.0f;
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            if ((Input.acceleration.x > this.threshold) || Input.GetKey(KeyCode.RightArrow)) key = 0.1f;
            if ((Input.acceleration.x < -this.threshold) || Input.GetKey(KeyCode.LeftArrow)) key = -0.1f;
            // 動く方向に応じて反転
            if (key != 0)
            {
                transform.localScale = new Vector3(key, 0.1f, 0.1f);
            }
        }
    }
}
