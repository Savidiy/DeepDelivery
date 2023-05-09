using UnityEngine;
using UnityEngine.EventSystems;

namespace UiModule
{
    public class MoveStickBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler,
        IPointerExitHandler
    {
        [SerializeField] private float MaxVisualStickShiftDistance = 50;
        [SerializeField] private float MaxPointerMoveDistance = 70;
        [SerializeField] private float MinPointerMoveDistance = 15;
        [SerializeField] private RectTransform StickBody;
        [SerializeField] private RectTransform StickJoy;

        private bool IsPressed { get; set; }
        public Vector2 InputDirection { get; private set; }
        private Vector2 _startMovePosition;

        private void Awake()
        {
            _startMovePosition = StickBody.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            _startMovePosition = eventData.position;
            UpdateJoy(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopMove(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopMove(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!IsPressed)
                return;

            UpdateJoy(eventData);
        }

        private void StopMove(PointerEventData eventData)
        {
            IsPressed = false;
            _startMovePosition = eventData.position;
            UpdateJoy(eventData);
        }

        private void UpdateJoy(PointerEventData eventData)
        {
            Vector2 movePosition = eventData.position;
            MoveStartPointTo(movePosition);
            InputDirection = CalcInput(movePosition);
            UpdateJoyPosition();
        }

        private void MoveStartPointTo(Vector2 movePosition)
        {
            float distance = Vector2.Distance(movePosition, _startMovePosition);
            if (distance <= MaxPointerMoveDistance)
                return;

            Vector2 delta = movePosition - _startMovePosition;
            Vector2 shift = delta * (distance - MaxPointerMoveDistance) / distance;
            _startMovePosition += shift;
        }

        private Vector2 CalcInput(Vector2 movePosition)
        {
            Vector2 delta = movePosition - _startMovePosition;
            float magnitude = delta.magnitude;
            if (magnitude < MinPointerMoveDistance)
                return Vector2.zero;

            delta.Normalize();
            Vector2 input = delta * (magnitude - MinPointerMoveDistance) / MaxPointerMoveDistance;
            return input;
        }

        private void UpdateJoyPosition()
        {
            StickJoy.anchoredPosition = InputDirection * MaxVisualStickShiftDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 position = StickBody.position;

            Vector3 minRadius = StickBody.TransformVector(Vector3.forward * MinPointerMoveDistance);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, minRadius.magnitude);

            Vector3 maxRadius = StickBody.TransformVector(Vector3.forward * (MaxPointerMoveDistance + MinPointerMoveDistance));
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, maxRadius.magnitude);
        }
    }
}