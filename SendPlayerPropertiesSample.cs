using Photon.Pun;
using UnityEngine;

public class SendPlayerPropertiesSample : MonoBehaviour
{
    private void LateUpdate()
    {
        PhotonNetwork.LocalPlayer.SendPlayerProperties();
    }
}
