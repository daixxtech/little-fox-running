using UnityEngine;

namespace Modules.Scenes {
    ///<summary> 2D 角色控制器 </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour {
        ///<summary> 落地检测半径 </summary>
        private const float GROUND_CHECK_RADIUS = 0.05F;
        ///<summary> 落地检测频率（频率越高，落地检测越准确） </summary>
        private const int GROUND_CHECK_SAMPLE_RATE = 5;
        ///<summary> 平滑时间参数（用于 SmoothDamp 函数） </summary>
        private const float SMOOTH_TIME = 0.05F;

        ///<summary> 移动速度 </summary>
        public float moveSpeed;
        ///<summary> 跳跃力度 </summary>
        public float jumpForce;
        ///<summary> 总跳跃次数 </summary>
        public int jumpCount;
        ///<summary> 剩余可跳跃次数（用于支持多段跳） </summary>
        public int remainingJumpCount;

        ///<summary> 上次检测时是否落地 </summary>
        private bool _preIsGrounded;
        ///<summary> 是否落地 </summary>
        [SerializeField] private bool _isGrounded;
        ///<summary> 是否处于下落状态 </summary>
        [SerializeField] private bool _isFalling;

        ///<summary> 2D 刚体 </summary>
        public Rigidbody2D rig2D;
        ///<summary> 上次计算的速度（用于 SmoothDamp 函数） </summary>
        public Vector2 previousVelocity;
        ///<summary> 当前计算的速度（用于显示在 InspectorWindow 中进行观测，对实际代码无影响，可删除） </summary>
        public Vector2 currentVelocity;
        ///<summary> 落地检测开始点 </summary>
        public Transform groundCheckPointStart;
        ///<summary> 落地检测结束点 </summary>
        public Transform groundCheckPointEnd;
        ///<summary> 地面层级 </summary>
        public LayerMask groundLayer;

        #region 对外接口

        /// <summary> 是否落地 </summary>
        public bool IsGrounded => _isGrounded;

        /// <summary> 是否处于下落状态 </summary>
        public bool IsFalling => _isFalling;

        /// <summary> 是否可跳跃 </summary>
        public bool CanJump => remainingJumpCount > 0;

        /// <summary> 移动 </summary>
        public void Move(float horizontalInput) {
            /* 位移，计算速度 */
            Vector2 curVel = rig2D.velocity;
            Vector2 targetVel = new Vector2(horizontalInput * moveSpeed, curVel.y);
            Vector2 smoothVel = Vector2.SmoothDamp(curVel, targetVel, ref previousVelocity, SMOOTH_TIME, float.PositiveInfinity, Time.fixedDeltaTime);
            rig2D.velocity = smoothVel;
            /* 转向 */
            Vector3 localScale = transform.localScale;
            if (horizontalInput > 0 && localScale.x < 0.0F || horizontalInput < 0 && localScale.x > 0.0F) {
                transform.localScale *= new Vector2(-1.0F, 1.0F);
            }
        }

        /// <summary> 跳跃 </summary>
        public void Jump() {
            /* 剩余可跳跃次数大于 0 */
            if (remainingJumpCount > 0) {
                --remainingJumpCount;
                rig2D.velocity = new Vector2(rig2D.velocity.x, jumpForce);
            }
        }

        #endregion

        private void Start() {
            rig2D = GetComponent<Rigidbody2D>();
            groundCheckPointStart = transform.Find("GroundCheckPointStart");
            groundCheckPointEnd = transform.Find("GroundCheckPointEnd");
            remainingJumpCount = jumpCount;
        }

        private void Update() {
            // 更新当前速度，用于观测
            currentVelocity = rig2D.velocity;
        }

        private void FixedUpdate() {
            // 检查人物是否落地
            CheckGround();
            // 检查人物是否处于下落状态
            CheckFalling();
            // 修正剩余跳跃次数
            CorrectRemainingJumpCount();
        }

        /// <summary> 检查人物是否落地 </summary>
        private void CheckGround() {
            /* 将 start ~ end 均分为多段，对之间的每个点进行检测 */
            Vector3 cur = groundCheckPointStart.position;
            Vector3 end = groundCheckPointEnd.position;
            float step = (end.x - cur.x) / GROUND_CHECK_SAMPLE_RATE; // 分段的步长
            _preIsGrounded = _isGrounded; // 更新上次检测时是否落地
            _isGrounded = false; // 初始化当前落地状态为 false
            for (int i = 0; i < GROUND_CHECK_SAMPLE_RATE; i++) {
                /* 只要有一个点与地面重叠，就表示人物落地 */
                _isGrounded = _isGrounded || Physics2D.OverlapCircle(cur, GROUND_CHECK_RADIUS, groundLayer);
                if (_isGrounded) {
                    break; // 有一个点落地检测为 true 就不用继续检测了
                }
                cur.x += step; // 继续检测下一个点
            }
        }

        /// <summary> 检查人物是否处于下落状态 </summary>
        private void CheckFalling() {
            // 人物在空中且速度大于 -0.1F 则说明处于下落状态
            _isFalling = !_isGrounded && rig2D.velocity.y < -0.1F;
        }

        /// <summary> 修正剩余跳跃次数 </summary>
        private void CorrectRemainingJumpCount() {
            /* 落地：重置可跳跃次数为总跳跃次数 */
            if (_isGrounded) {
                remainingJumpCount = jumpCount;
            }
            /* 未落地但上次检测时落地：重置可跳跃次数为总跳跃次数 - 1 */
            else if (_preIsGrounded) {
                remainingJumpCount = jumpCount - 1;
            }
        }
    }
}