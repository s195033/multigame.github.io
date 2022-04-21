using Photon.Pun;
using UnityEngine;

public class AvatarTransformView : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float InterpolationPeriod = 0.1f; // ��Ԃɂ����鎞��

    private Vector3 p1;
    private Vector3 p2;
    private Vector3 v1;
    private Vector3 v2;
    private float elapsedTime;

    private void Start()
    {
        p1 = transform.position;
        p2 = p1;
        v1 = Vector3.zero;
        v2 = v1;
        elapsedTime = Time.deltaTime;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // ���g�̃l�b�g���[�N�I�u�W�F�N�g�́A���t���[���̈ړ��ʂƌo�ߎ��Ԃ��L�^����
            p1 = p2;
            p2 = transform.position;
            elapsedTime = Time.deltaTime;
        }
        else
        {
            // ���v���C���[�̃l�b�g���[�N�I�u�W�F�N�g�́A��ԏ������s��
            elapsedTime += Time.deltaTime;
            if (elapsedTime < InterpolationPeriod)
            {
                transform.position = HermiteSpline.Interpolate(p1, p2, v1, v2, elapsedTime / InterpolationPeriod);
            }
            else
            {
                transform.position = Vector3.LerpUnclamped(p1, p2, elapsedTime / InterpolationPeriod);
            }
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            // ���t���[���̈ړ��ʂƌo�ߎ��Ԃ���A�b�������߂đ��M����
            stream.SendNext((p2 - p1) / elapsedTime);
        }
        else
        {
            var networkPosition = (Vector3)stream.ReceiveNext();
            var networkVelocity = (Vector3)stream.ReceiveNext();
            var lag = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - info.SentServerTimestamp) / 1000f);

            // ��M���̍��W���A��Ԃ̊J�n���W�ɂ���
            p1 = transform.position;
            // ���ݎ����ɂ�����\�����W���A��Ԃ̏I�����W�ɂ���
            p2 = networkPosition + networkVelocity * lag;
            // �O��̕�Ԃ̏I�����x���A��Ԃ̊J�n���x�ɂ���
            v1 = v2;
            // ��M�����b�����A��Ԃɂ����鎞�Ԃ�����̑��x�ɕϊ����āA��Ԃ̏I�����x�ɂ���
            v2 = networkVelocity * InterpolationPeriod;
            // �o�ߎ��Ԃ����Z�b�g����
            elapsedTime = 0f;
        }
    }
}

