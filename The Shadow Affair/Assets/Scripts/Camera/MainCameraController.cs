using Unity.Cinemachine;
using UnityEngine;

namespace SmugRagGames.Camera
{
    [SelectionBase]
    public class MainCameraController : MonoBehaviour
    {
        [Header("Main Camera")]
        [SerializeField]
        private UnityEngine.Camera mainCamera;

        [Header("Cinemachine Cameras")]
        [SerializeField]
        private CinemachineCamera cinemachineDefault;

        #region Setup Follow Target

        private void SetupFollowTarget()
        {
            CameraTarget newTarget = new()
            {
                TrackingTarget = GetPlayerFollowTarget()
            };

            cinemachineDefault.Target = newTarget;
        }

        private static Transform GetPlayerFollowTarget()
        {
            return GameObject.FindWithTag("PlayerHeadPivot").transform;
        }

        #endregion Setup Follow Target

        private void OnEnable()
        {
            SetupFollowTarget();
        }
    }
}