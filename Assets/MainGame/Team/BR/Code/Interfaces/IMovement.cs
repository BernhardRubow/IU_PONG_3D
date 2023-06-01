using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

namespace Assets.MainGame.Team.BR.Code.Interfaces
{
    public interface IMovement
    {
        Vector3 Direction { get; set; }
    
        void SwitchMoveDirection(MoveDirections moveDirection);
    }
}