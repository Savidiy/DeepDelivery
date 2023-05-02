using UnityEngine;

namespace MainModule
{
    public class MobileInput
    {
        public Vector2 InputDirection { get; private set; }
        public bool IsFirePressed { get; private set; }

        public void SetInputDirection(Vector2 inputDirection, bool isFirePressed)
        {
            InputDirection = inputDirection;
            IsFirePressed = isFirePressed;
        }
    }
}