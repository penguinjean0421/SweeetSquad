using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private EnemyAI enemyAI;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(enemyAI.NextMove, rigid.linearVelocity.y);
    }

    public void AddKnockbackForce(Vector2 force)
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }
}