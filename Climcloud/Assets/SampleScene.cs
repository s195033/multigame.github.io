using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SampleScene : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
{
    private void OnEnable()
    {
        // PUN�̃R�[���o�b�N�Ώۂɓo�^����
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        // PUN�̃R�[���o�b�N�Ώۂ̓o�^����������
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        // ���[�J���v���C���[�I�u�W�F�N�g���擾����
        var localPlayer = PhotonNetwork.LocalPlayer;

        // ���[�����̃v���C���[�I�u�W�F�N�g�̔z��i���[�J���v���C���[���܂ށj���擾����
        var players = PhotonNetwork.PlayerList;

        // ���[�����̃v���C���[�I�u�W�F�N�g�̔z��i���[�J���v���C���[���܂܂Ȃ��j���擾����
        var others = PhotonNetwork.PlayerListOthers;

        // ���[�����̃l�b�g���[�N�I�u�W�F�N�g�̖��O��ID���R���\�[���ɏo�͂���
        foreach (var photonView in PhotonNetwork.PhotonViewCollection)
        {
            Debug.Log($"{photonView.gameObject.name}({photonView.ViewID})");
        }

        PhotonNetwork.NickName = "Player";

        // ���[�J���v���C���[���}�X�^�[�N���C�A���g���ǂ����𔻒肷��
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("���g���}�X�^�[�N���C�A���g�ł�");
        }

        // "RoomObject"�v���n�u���烋�[���I�u�W�F�N�g�𐶐�����
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("RoomObject", Vector3.zero, Quaternion.identity);
        }

        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
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

        // ���[�����쐬�����v���C���[�́A���݂̃T�[�o�[�������Q�[���̊J�n�����ɐݒ肷��
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
        }

        // ���[���������ɂȂ�����A�ȍ~���̃��[���ւ̎Q����s���ɂ���
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

// IPunPrefabPool�C���^�[�t�F�[�X����������
public class GamePlayerPrefabPool : MonoBehaviour, IPunPrefabPool
{
    [SerializeField]
    private GamePlayer gamePlayerPrefab = default;

    private Stack<GamePlayer> inactiveObjectPool = new Stack<GamePlayer>();

    private void Start()
    {
        // �l�b�g���[�N�I�u�W�F�N�g�̐����E�j�����s���������A���̃N���X�̏����ɍ����ւ���
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
        // PhotonNetwork�̓����Ŋ��ɔ�A�N�e�B�u��Ԃɂ���Ă���̂ŁA�ȉ��̏����͕s�v
        // player.gameObject.SetActive(false);
        inactiveObjectPool.Push(player);
    }
}

public class GamePlayer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        // Object.Instantiate�̌�Ɉ�x�����K�v�ȏ������������s��
    }

    private void Start()
    {
        // ������Ɉ�x�����iOnEnable�̌�Ɂj�Ă΂��A�����ŏ������������s���ꍇ�͗v����
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // PhotonNetwork.Instantiate�̐���������ɕK�v�ȏ������������s��
    }

    public override void OnDisable()
    {
        base.OnDisable();

        // PhotonNetwork.Destroy�̔j�������O�ɕK�v�ȏI���������s��
    }
}