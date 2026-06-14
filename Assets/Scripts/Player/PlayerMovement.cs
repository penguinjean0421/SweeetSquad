using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    PlayerInput playerInput;

    Rigidbody2D rigid;

    public bool isJumping { get; set; }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // 점프 입력 처리
        if (playerInput.isJumpDown && !isJumping)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }

        // 감속 처리 (키에서 손을 뗐을 때)
        if (playerInput.isHorizontalUp)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.normalized.x * 0.5f, rigid.linearVelocity.y);
        }
    }

    void FixedUpdate()
    {
        // 이동 힘 가하기
        float moveInput = playerInput.horizontal;
        rigid.AddForce(Vector2.right * moveInput, ForceMode2D.Impulse);

        // 최고 속도 제한
        float clampedX = Mathf.Clamp(rigid.linearVelocity.x, -maxSpeed, maxSpeed);
        rigid.linearVelocity = new Vector2(clampedX, rigid.linearVelocity.y);

        // 착지 검사 (하강 중일 때만)
        if (rigid.linearVelocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down * 1.2f, Color.green);
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.2f, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null && rayHit.distance < 1f)
            {
                isJumping = false;
            }
        }
    }

    public void AddStompForce()
    {
        rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0);
        rigid.AddForce(Vector2.up * jumpPower * 0.7f, ForceMode2D.Impulse);
    }

    public void VelocityZero()
    {
        rigid.linearVelocity = Vector2.zero;
    }
}