using Photon.Pun;
using UnityEngine;

public class DestroyCallbacksSample : MonoBehaviourPun, IOnPhotonViewPreNetDestroy
{
    private void OnEnable()
    {
        // PhotonViewのコールバック対象に登録する
        photonView.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        // PhotonViewのコールバック対象の登録を解除する
        photonView.RemoveCallbackTarget(this);
    }

    // ネットワークオブジェクトが破棄される直前に呼ばれるコールバック
    void IOnPhotonViewPreNetDestroy.OnPreNetDestroy(PhotonView rootView)
    {
        Debug.Log($"{rootView.name}({rootView.ViewID}) が破棄されます");
    }
}
