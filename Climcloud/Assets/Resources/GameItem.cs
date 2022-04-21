using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class GameItem : MonoBehaviourPunCallbacks
{
    private bool isAvailable; // アイテムが取得可能かどうか
    private string id; // アイテムID

    public void Spawn()
    {
        isAvailable = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(
            new Hashtable() { { id, 0 } }
        );
    }

    public void TryGetItem()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(
            new Hashtable() { { id, PhotonNetwork.LocalPlayer.ActorNumber } },
            new Hashtable() { { id, 0 } }
        );
        photonView.RPC(nameof(RPCTryGetItem), RpcTarget.AllViaServer);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        foreach (var entry in propertiesThatChanged)
        {
            string k = (string)entry.Key;
            int v = (int)entry.Value;

            if (k == id && v == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("アイテムの取得に成功しました");
            }
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.OpResponseReceived += OnOpResponseReceived;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.OpResponseReceived -= OnOpResponseReceived;
    }

    private void OnOpResponseReceived(OperationResponse response)
    {
        if (response.OperationCode == OperationCode.SetProperties
            && response.ReturnCode == ErrorCode.InvalidOperation)
        {
            OnPropertiesUpdateFailed(response.DebugMessage);
        }
    }

    public virtual void OnPropertiesUpdateFailed(string message) { }

    [PunRPC]
    private void RPCTryGetItem(PhotonMessageInfo info)
    {
        if (isAvailable)
        {
            isAvailable = false;

            if (info.Sender.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("アイテムの取得に成功しました");
            }
        }
        else
        {
            // 既にアイテムが取得済みなら、何もしない
        }
    }
}