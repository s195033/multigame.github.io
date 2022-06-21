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
        // まだルームに参加していない場合は更新しない
        if (!PhotonNetwork.InRoom) { return; }

        // 0.1秒毎にテキストを更新する
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
                builder.AppendLine($"残りのカギはあと{player.GetScore()}つ!!");
            }
            
            if (player.GetScore() == 0)
            {
                builder.AppendLine($"フラッグを目指そう!!");
            }
        }
        label.text = builder.ToString();
    }
}
