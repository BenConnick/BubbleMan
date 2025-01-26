using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 10;
        public float jumpHangForce = 10;

        public SpriteRenderer ColorIndicator;
        public JumpState jumpState = JumpState.Grounded;
        private bool jumpButtonHeldDown;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public float horizontalInertiaInAir = 1;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                if (jumpState == JumpState.Grounded || horizontalInertiaInAir == 0)
                {
                    move.x = Input.GetAxis("Horizontal");
                }
                else
                {
                    float target = Input.GetAxis("Horizontal");
                    move.x = Mathf.Lerp(move.x, target, Time.deltaTime * 60/horizontalInertiaInAir * Mathf.Abs(target - move.x));
                }

                if (jumpState == JumpState.Grounded && Input.GetAxis("Vertical") > 0)
                {
                    jumpState = JumpState.PrepareToJump;
                    jumpButtonHeldDown = true;
                }
                else if (Input.GetAxis("Vertical") <= 0)
                {
                    jumpButtonHeldDown = false;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
            
            var typeComp = GetComponent<RockPaperScissorsComponent>();
            if (typeComp != null && ColorIndicator != null)
            {
                Color color = Color.white;
                switch (typeComp.Value)
                {
                    case TypeRPS.None:
                        break;
                    case TypeRPS.Rock:
                        color = Color.red;
                        break;
                    case TypeRPS.Paper:
                        color = Color.yellow;
                        break;
                    case TypeRPS.Scissors:
                        color = Color.blue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ColorIndicator.color = color;
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    jumpButtonHeldDown = false;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (IsGrounded)
            {
                if (jump)
                {
                    velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                    jump = false;
                }
            }
            else
            {
                if (jumpButtonHeldDown)
                {
                    float gravityAmount = gravityModifier * Physics2D.gravity.y;
                    float hangPower = -Mathf.Clamp(gravityAmount * jumpHangForce, gravityAmount, gravityAmount*.5f);
                    if (velocity.y > .75f*jumpTakeOffSpeed)
                    {
                        velocity.y += hangPower * Time.deltaTime;
                    }
                }
                else
                {
                    base.ComputeVelocity();
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}