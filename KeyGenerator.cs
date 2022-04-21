using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGenerator : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Key keyPrefab = default;

    private int nextKeyId = 0;

    // Update is called once per frame
    void Update()
    {
        if (nextKeyId < 5)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = mousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            // 鍵を生成するたびに鍵のIDを1ずつ増やしていく
            photonView.RPC(nameof(MasterKey), RpcTarget.All, nextKeyId++, angle);
        }
    }

    [PunRPC]
    private void MasterKey(int id, float angle)
    {
        var key = Instantiate(keyPrefab);
        Vector3 tmp = GameObject.Find("cloudPrefab (" + Random.Range(26, 100) + ")").transform.position;
        key.Init(id, photonView.OwnerActorNr, transform.position, angle);
        key.transform.position = new Vector3(tmp.x, tmp.y + 0.8f, 0);
    }
}
