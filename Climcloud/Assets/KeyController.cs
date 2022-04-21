using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviourPunCallbacks
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Avatar");
    }

    // Update is called once per frame
    void Update()
    {
        // 当たり判定
        Vector2 p1 = transform.position;                // 鍵の中心座標
        Vector2 p2 = this.player.transform.position;    // プレイヤの中心座標
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.3f;    // 鍵の半径
        float r2 = 0.5f;    // プレイヤの半径
        if (d < r1 + r2)
        {
            // 監督スクリプトにプレイヤと衝突したことを伝える
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().Quantity();

            // 旗ジェネレータにプレイヤと衝突したことを伝える
            GameObject generator = GameObject.Find("FlagGenerator");
            generator.GetComponent<FlagGenerator>().Quantity();
            
            // カギを取ったらカギを消す
            Destroy(gameObject);
        }
    }
}
