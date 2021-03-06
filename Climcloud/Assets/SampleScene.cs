using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SampleScene : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
{
    private void OnEnable()
    {
        // PUNのコールバック対象に登録する
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        // PUNのコールバック対象の登録を解除する
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        // ローカルプレイヤーオブジェクトを取得する
        var localPlayer = PhotonNetwork.LocalPlayer;

        // ルーム内のプレイヤーオブジェクトの配列（ローカルプレイヤーを含む）を取得する
        var players = PhotonNetwork.PlayerList;

        // ルーム内のプレイヤーオブジェクトの配列（ローカルプレイヤーを含まない）を取得する
        var others = PhotonNetwork.PlayerListOthers;

        // ルーム内のネットワークオブジェクトの名前とIDをコンソールに出力する
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }

        PhotonNetwork.NickName = "Player";

        // ローカルプレイヤーかマスタークライアントかどうかを判定する
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("自身がマスタークライアントです");
        }

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    void IConnectionCallbacks.OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void IMatchmakingCallbacks.OnJoinedRoom()
    {
        var position = new Vector3(Random.Range(-0.5f, 0), 0);
        PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);

        // ルームを作成したプレイヤーは、現在のサーバー時刻をゲームの開始時刻に設定する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }

        // ルームが満員になったら、以降そのルームへの参加を不許可にする
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    void IConnectionCallbacks.OnConnected() { }
    void IConnectionCallbacks.OnDisconnected(DisconnectCause cause) { }
    void IConnectionCallbacks.OnRegionListReceived(RegionHandler regionHandler) { }
    void IConnectionCallbacks.OnCustomAuthenticationResponse(Dictionary<string, object> data) { }
    void IConnectionCallbacks.OnCustomAuthenticationFailed(string debugMessage) { }

    void IMatchmakingCallbacks.OnFriendListUpdate(List<FriendInfo> friendList) { }
    void IMatchmakingCallbacks.OnCreatedRoom() { }
    void IMatchmakingCallbacks.OnCreateRoomFailed(short returnCode, string message) { }
    void IMatchmakingCallbacks.OnJoinRoomFailed(short returnCode, string message) { }
    void IMatchmakingCallbacks.OnJoinRandomFailed(short returnCode, string message) { }
    void IMatchmakingCallbacks.OnLeftRoom() { }
}