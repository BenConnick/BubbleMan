using System.Collections;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ShrimpBounceBehaviour : MonoBehaviour
    {
        public float IntervalSeconds = 1f;
        public AnimationCurve JumpSquishCurve;
        public AnimationCurve RotationCurve;
        public float JumpAnimationDuration = 0.5f;
        public float JumpSquishMagnitude = 2f;
        public float RotationAmount = 360f;

        private float LastJump;

        public void Update()
        {
            if (Time.time > LastJump + IntervalSeconds)
            {
                LastJump = Time.time;
                StartCoroutine(PlayJumpSquish());
                GetComponent<EnemyAnimationController>().jump = true;
            }
        }

        private IEnumerator PlayJumpSquish()
        {
            float startTime = Time.time;
            while (startTime + JumpAnimationDuration > Time.time)
            {
                float t = (Time.time - startTime) / JumpAnimationDuration;
                float yScale = JumpSquishCurve.Evaluate(t) * JumpSquishMagnitude;
                transform.localScale = new Vector3(1, yScale, 1);
                transform.localRotation = Quaternion.Euler(0,0,RotationAmount * RotationCurve.Evaluate(t));
                yield return null;
            }
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}