using DG.Tweening;
using UnityEngine;

namespace MainModule
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private static readonly int FlashTimeProperty = Shader.PropertyToID("_WhitePercent");

        public Collider2D HitCollider;
        [SerializeField] private SpriteRenderer SpriteRenderer;

        private Material _material;
        private float _whitePercent;
        private Sequence _sequence;

        public void Awake()
        {
            _material = SpriteRenderer.material;
        }

        public void Flash(EnemyBlinkSettings enemyBlinkSettings)
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            _sequence.Append(DOTween.To(
                    () => _whitePercent,
                    value =>
                    {
                        _whitePercent = value;
                        _material.SetFloat(FlashTimeProperty, value);
                    },
                    1f,
                    enemyBlinkSettings.StartBlinkDuration)
                .SetEase(enemyBlinkSettings.StartEase));

            _sequence.Append(DOTween.To(
                    () => _whitePercent,
                    value =>
                    {
                        _whitePercent = value;
                        _material.SetFloat(FlashTimeProperty, value);
                    },
                    0f,
                    enemyBlinkSettings.EndBlinkDuration)
                .SetEase(enemyBlinkSettings.EndEase));
        }
    }
}