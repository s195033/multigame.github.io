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
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }

        PhotonNetwork.NickName = "Player";

        // ���[�J���v���C���[���}�X�^�[�N���C�A���g���ǂ����𔻒肷��
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("���g���}�X�^�[�N���C�A���g�ł�");
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