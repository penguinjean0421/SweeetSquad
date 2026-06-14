using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyAI enemyAI;
    private EnemyMovement enemyMovement;
    private EnemyAnimation enemyAnimation;
    private CapsuleCollider2D capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        enemyAI = GetComponent<EnemyAI>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    public void OnDamaged()
    {
        enemyAI.StopAI();

        capsuleCollider.enabled = false;

        enemyAnimation.PlayDeathVisuals();
        enemyMovement.AddKnockbackForce(Vector2.up * 5f);

        Invoke(nameof(DeActive), 5f);
    }

    private void DeActive()
    {
        gameObject.SetActive(false);
    }
}