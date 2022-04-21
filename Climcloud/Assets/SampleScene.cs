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
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            Debug.Log($"{photonView.gameObject.name}({photonView.ViewID})");
        }

        PhotonNetwork.NickName = "Player";

        // ローカルプレイヤーがマスタークライアントかどうかを判定する
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("自身がマスタークライアントです");
        }

        // "RoomObject"プレハブからルームオブジェクトを生成する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("RoomObject", Vector3.zero, Quaternion.identity);
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

// IPunPrefabPoolインターフェースを実装する
public class GamePlayerPrefabPool : MonoBehaviour, IPunPrefabPool
{
    [SerializeField]
    private GamePlayer gamePlayerPrefab = default;

    private Stack<GamePlayer> inactiveObjectPool = new Stack<GamePlayer>();

    private void Start()
    {
        // ネットワークオブジェクトの生成・破棄を行う処理を、このクラスの処理に差し替える
        PhotonNetwork.PrefabPool = this;
    }

    GameObject IPunPrefabPool.Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        switch (prefabId)
        {
            case "Avatar":
                GamePlayer player;
                if (inactiveObjectPool.Count > 0)
                {
                    player = inactiveObjectPool.Pop();
                    player.transform.SetPositionAndRotation(position, rotation);
                }
                else
                {
                    player = Instantiate(gamePlayerPrefab, position, rotation);
                    player.gameObject.SetActive(false);
                }
                return player.gameObject;
        }
        return null;
    }

    void IPunPrefabPool.Destroy(GameObject gameObject)
    {
        var player = gameObject.GetComponent<GamePlayer>();
        // PhotonNetworkの内部で既に非アクティブ状態にされているので、以下の処理は不要
        // player.gameObject.SetActive(false);
        inactiveObjectPool.Push(player);
    }
}

public class GamePlayer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        // Object.Instantiateの後に一度だけ必要な初期化処理を行う
    }

    private void Start()
    {
        // 生成後に一度だけ（OnEnableの後に）呼ばれる、ここで初期化処理を行う場合は要注意
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // PhotonNetwork.Instantiateの生成処理後に必要な初期化処理を行う
    }

    public override void OnDisable()
    {
        base.OnDisable();

        // PhotonNetwork.Destroyの破棄処理前に必要な終了処理を行う
    }
}