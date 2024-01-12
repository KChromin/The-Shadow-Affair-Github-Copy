using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGrounded : MovementDefaultStateBase
    {
        public MovementDefaultStateGrounded(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            isRootState = true;
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.MovementBase.CurrentStatesInfo.generalState = MovementDefault.CurrentStatesInfoClass.GeneralStates.Grounded;
#endif

            #endregion Set State Info

            //Grounding event//
            Ctx.MovementBase.InvokeOnGroundingAction();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            Ctx.Execution.MoveBasic();
            Ctx.Execution.BidaGravity();
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.MovementBase.CurrentStatesInfo.generalState = MovementDefault.CurrentStatesInfoClass.GeneralStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.MovementDefaultCheckers.CheckIsGrounded())
            {
                SwitchStates(Factory.InAir());
            }
        }
    }
}