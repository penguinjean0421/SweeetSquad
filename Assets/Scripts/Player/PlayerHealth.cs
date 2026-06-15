using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isInvincible { get; private set; } = false;

    [Header("Damaged")]
    public float invincibilityTime = 3f;
    public float damageCooldown = 1f;

    PlayerAnimation playerAnimation;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    readonly int playerLayer = 10;
    readonly int damagedLayer = 11;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void OnDamaged(Vector2 targetPos)
    {
        if (isInvincible) { return; }

        StartCoroutine(InvincibleCooldownCo());

        GameManager.instance.HealthDown();
        gameObject.layer = damagedLayer; // 무적 레이어로 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 투명화

        // 튕겨나가는 리액션 힘
        int direction = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(direction, 1) * 7, ForceMode2D.Impulse);

        playerAnimation.PlayDamagedAnimation();

        Invoke(nameof(OffDamaged), invincibilityTime);
    }

    IEnumerator InvincibleCooldownCo()
    {
        isInvincible = true;

        yield return new WaitForSeconds(damageCooldown);

        isInvincible = false;
    }

    void OffDamaged()
    {
        gameObject.layer = playerLayer;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        capsuleCollider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}