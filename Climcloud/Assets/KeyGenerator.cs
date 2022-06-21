using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGenerator : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public GameObject keyPrefab;

    private int MaxCounts = 5;

    int start = 11;
    int end = 15;

    List<int> numbers = new List<int>();

    void Start()
    {
        // 数字をリスト化する
        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // マスタークライアントだけ鍵を生成させる
        if (PhotonNetwork.IsMasterClient)
        {
            if (MaxCounts > 0)
            {
                // 鍵を生成する
                GameObject key = Instantiate(keyPrefab);
                // ランダムの数字を取得する
                int index = Random.Range(0, MaxCounts);
                // 指定した数字の中からランダムに数字を取得する
                int cloudNumber = numbers[index];
                // 雲の位置をランダム取得する
                Vector2 tmp = GameObject.Find("cloudPrefab (" + cloudNumber + ")").transform.position;
                // 取得した雲の位置の上にカギを移動させる
                key.transform.position = new Vector2(tmp.x, tmp.y + 0.8f);
                // 重複しないように除外する
                numbers.RemoveAt(index);
                // 除外したため、最大値を減らしていく
                MaxCounts--;
            }
        }
    }
}
