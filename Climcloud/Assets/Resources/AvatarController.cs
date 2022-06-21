using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // LoadSceneを使うために必要!!

public class AvatarController : MonoBehaviourPunCallbacks
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        // ジャンプする
        if ((Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0) || Input.GetKeyDown(KeyCode.Space))
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 左右移動
        int key = 0;
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            if ((Input.acceleration.x > this.threshold) || Input.GetKey(KeyCode.RightArrow)) key = 1;
            if ((Input.acceleration.x < -this.threshold) || Input.GetKey(KeyCode.LeftArrow)) key = -1;
        }
        
        // プレイヤの速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // スピード制限
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // プレイヤの速度に応じてアニメーション速度を変える
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // 画面外に出た場合は最初から
        if (transform.position.y < -10)
        {
            // 加速度を初期化
            this.rigid2D.velocity = Vector2.zero;
            // 初期位置を設定
            transform.position = new Vector3(Random.Range(-0.5f, 0), 0);
        }

        PhotonNetwork.SendRate = 20;            // 1秒間にメッセージ送信を行う回数
        PhotonNetwork.SerializationRate = 10;   // 1秒間にオブジェクト同期を行う回数
    }
}
