using System;
using UnityEngine;

namespace Modules.Scenes {
    ///<summary> 玩家控制器 </summary>
    public class PlayerController : MonoBehaviour {
        ///<summary> 水平输入 </summary>
        public float xInput;
        ///<summary> 是否按下跳跃键 </summary>
        public bool isJumpPressed;

        ///<summary> 2D 人物控制器 </summary>
        public CharacterController2D controller;
        ///<summary> 动画控制器 </summary>
        public Animator animator;

        private void Awake() {
            controller = GetComponent<CharacterController2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            // 先切换动画状态再处理数据
            UpdateAnimatorState();
            // 输入为 0 也要调用，保证主角平地不会滑动
            controller.Move(xInput);
            // 检测在 Update 中是否按下了跳跃键
            if (isJumpPressed) {
                isJumpPressed = false;
                controller.Jump();
            }
        }

        private void Update() {
            // 读取 X 轴的输出
            xInput = Input.GetAxis("Horizontal");
            // 是否按下跳跃键
            if (Time.timeScale != 0 && Input.GetButtonDown("Jump")) {
                isJumpPressed = true;
            }
        }

        ///<summary> 更新动画控制器状态 </summary>
        private void UpdateAnimatorState() {
            if (isJumpPressed && controller.CanJump && (controller.IsGrounded || controller.IsFalling) /* 防止多段跳时多次触发 Jump 动画 */) {
                animator.SetTrigger("Jump"); // 放在 Update 里会导致重复触发
            }
            animator.SetBool("isRunning", Math.Abs(xInput) > 0.1F);
            animator.SetBool("isGrounded", controller.IsGrounded);
            animator.SetBool("isFalling", controller.IsFalling);
        }
    }
}