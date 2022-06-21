using System;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label = default;

    private StringBuilder builder;
    private float elapsedTime;

    private void Start()
    {
        builder = new StringBuilder();
        elapsedTime = 0f;
    }

    private void Update()
    {
        // �܂����[���ɎQ�����Ă��Ȃ��ꍇ�͍X�V���Ȃ�
        if (!PhotonNetwork.InRoom) { return; }

        // 0.1�b���Ƀe�L�X�g���X�V����
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.1f)
        {
            elapsedTime = 0f;
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        var players = PhotonNetwork.PlayerList;

        builder.Clear();
        foreach (var player in players)
        {
            if (player.GetScore() > 0)
            {
                builder.AppendLine($"�c��̃J�M�͂���{player.GetScore()}��!!");
            }
            
            if (player.GetScore() == 0)
            {
                builder.AppendLine($"�t���b�O��ڎw����!!");
            }
        }
        label.text = builder.ToString();
    }
}
