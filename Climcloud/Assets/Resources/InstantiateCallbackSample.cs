using Photon.Pun;
using UnityEngine;

public class InstantiateCallbackSample : MonoBehaviour, IPunInstantiateMagicCallback
{
    // ネットワークオブジェクトが生成された時に呼ばれるコールバック
    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
        {
            Debug.Log("自身がネットワークオブジェクトを生成しました");
        }
        else
        {
            Debug.Log("他プレイヤーがネットワークオブジェクトを生成しました");
        }
    }
}
