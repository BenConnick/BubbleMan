using UnityEngine;

namespace Platformer.Mechanics
{
    public class ColoredAnimations : MonoBehaviour
    {
        private RockPaperScissorsComponent _Type;
        private Animator _Animator;
        public RuntimeAnimatorController[] AnimatorsByType;

        protected void Awake()
        {
            _Type = GetComponent<RockPaperScissorsComponent>();
            _Animator = GetComponent<Animator>();
            _Animator.runtimeAnimatorController = AnimatorsByType[(int)_Type.Value - 1];
        }
    }
}