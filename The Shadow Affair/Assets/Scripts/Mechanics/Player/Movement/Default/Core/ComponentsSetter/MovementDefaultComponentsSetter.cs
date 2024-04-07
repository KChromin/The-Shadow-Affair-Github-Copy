using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultComponentsSetter
    {
        private Rigidbody _rigidBody;
        private Collider _collider;
        private MovementDefaultComponentsSetterData _setterData;

        public void Setup(Rigidbody rigidBody, Collider collider, MovementDefaultComponentsSetterData setterData)
        {
            _rigidBody = rigidBody;
            _collider = collider;
            _setterData = setterData;
        }

        #region Change Methodes

        public void ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials newPhysicsMaterial)
        {
            switch (newPhysicsMaterial)
            {
                case MovementDefaultComponentsSetterData.PhysicsMaterials.Idle:
                    _collider.material = _setterData.materialIdle;
                    break;
                case MovementDefaultComponentsSetterData.PhysicsMaterials.Move:
                    _collider.material = _setterData.materialMove;
                    break;
                case MovementDefaultComponentsSetterData.PhysicsMaterials.InAir:
                    _collider.material = _setterData.materialInAir;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newPhysicsMaterial), newPhysicsMaterial, null);
            }
        }

        public void ChangeDrag(MovementDefaultComponentsSetterData.DragModes newDragMode)
        {
            switch (newDragMode)
            {
                case MovementDefaultComponentsSetterData.DragModes.Grounded:
                    _rigidBody.drag = _setterData.dragGrounded;
                    break;
                case MovementDefaultComponentsSetterData.DragModes.InAir:
                    _rigidBody.drag = _setterData.dragInAir;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newDragMode), newDragMode, null);
            }
        }

        #endregion Change Methodes

    }
}