using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // LoadScene���g�����߂ɕK�v!!

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
        // �W�����v����
        if ((Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0) || Input.GetKeyDown(KeyCode.Space))
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // ���E�ړ�
        int key = 0;
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            if ((Input.acceleration.x > this.threshold) || Input.GetKey(KeyCode.RightArrow)) key = 1;
            if ((Input.acceleration.x < -this.threshold) || Input.GetKey(KeyCode.LeftArrow)) key = -1;
        }
        
        // �v���C���̑��x
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // �X�s�[�h����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // ���������ɉ����Ĕ��]
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // �v���C���̑��x�ɉ����ăA�j���[�V�������x��ς���
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // ��ʊO�ɏo���ꍇ�͍ŏ�����
        if (transform.position.y < -10)
        {
            // �����x��������
            this.rigid2D.velocity = Vector2.zero;
            // �����ʒu��ݒ�
            transform.position = new Vector3(Random.Range(-0.5f, 0), 0);
        }

        PhotonNetwork.SendRate = 20;            // 1�b�ԂɃ��b�Z�[�W���M���s����
        PhotonNetwork.SerializationRate = 10;   // 1�b�ԂɃI�u�W�F�N�g�������s����
    }
}
