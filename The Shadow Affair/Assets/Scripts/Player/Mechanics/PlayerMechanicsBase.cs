using UnityEngine;

namespace SmugRagGames.Player
{
    public abstract class PlayerMechanicsBase : MonoBehaviour
    {
        #region Enabledness

        //When mechanic is enabled, that means it performs updates//
        [field: Header("Enabled")]
        [field: SerializeField]
        protected bool IsEnabled { get; set; }

        /// <param name="newEnablednessState">New Enabledness State</param>
        public virtual void ChangeEnabledness(bool newEnablednessState)
        {
            IsEnabled = newEnablednessState;
        }

        //For some special states//
        public bool GetCurrentEnabledness()
        {
            return IsEnabled;
        }

        #endregion Enabledness
    }
}