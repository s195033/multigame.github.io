using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    public class CameraController : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        [Tooltip("The distance in the local x-z plane to the target")]
        [SerializeField]
        private float distance = 10.0f;

        [Tooltip("The height we want the camera to be above the target")]
        [SerializeField]
        private float height = 0.0f;

        [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        // cached transform of the target
        Transform cameraTransform;

        // maintain a flag internally to reconnect if target is lost or camera is switched
        bool isFollowing;

        // Cache for camera offset
        Vector3 cameraOffset = Vector3.zero;

        #endregion

        #region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
        {
            // Start following the target if wanted.
            if (photonView.IsMine)
            {
                OnStartFollowing();
            }
        }


        void LateUpdate()
        {
            // The transform target may not destroy on level load,
            // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }

            // only follow is explicitly declared
            if (isFollowing)
            {
                Follow();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the start following event.
        /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
        /// </summary>
        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
            // we don't smooth anything, we go straight to the right camera shot
            Cut();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Follow the target smoothly
        /// </summary>
        void Follow()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10.0f);

            cameraTransform.LookAt(this.transform.position + centerOffset);
        }


        void Cut()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset.x, cameraOffset.y, -10.0f);

            cameraTransform.LookAt(this.transform.position + centerOffset);
        }
        #endregion
    }
}
