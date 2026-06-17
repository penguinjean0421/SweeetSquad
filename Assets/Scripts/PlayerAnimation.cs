using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // 스프라이트 뒤집기
        if (playerInput.isHorizontalPressed)
        {
            spriteRenderer.flipX = playerInput.horizontal == -1;
        }

        // 걷기 애니메이션
        bool isWalking = Mathf.Abs(rigid.linearVelocity.x) > 0.01f;
        anim.SetBool("isWalking", isWalking);

        // 점프 애니메이션
        anim.SetBool("isJumping", playerMovement.isJumping);
    }

    public void PlayDamagedAnimation()
    {
        anim.SetTrigger("doDamaged");
    }
}