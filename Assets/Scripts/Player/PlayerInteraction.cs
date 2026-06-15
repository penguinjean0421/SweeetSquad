using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int stageClearScore;

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;

    Rigidbody2D rigid;

    int playerDamagedLayer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();

        playerDamagedLayer = LayerMask.NameToLayer("PlayerDamaged");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) { HandleItem(collision.gameObject); }

        else if (collision.CompareTag("Finish"))
        {
            GameManager.instance.score += stageClearScore;
            UIManager.instance.UpdateScore(GameManager.instance.score);

            GameManager.instance.NextStage();
        }

        if (collision.CompareTag("DeadZone"))
        {
            GameManager.instance.HealthDown();
            if (GameManager.instance.currentHealth >= 1)
            {
                GameManager.instance.PlayerReposition();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer != playerDamagedLayer)
        {
            if (rigid.linearVelocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                playerHealth.OnDamaged(collision.transform.position);
            }
        }

        if (collision.gameObject.CompareTag("Spike") && collision.gameObject.layer != playerDamagedLayer)
        {
            playerHealth.OnDamaged(collision.transform.position);
        }
    }

    void OnAttack(Transform enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.OnDamaged();
            playerMovement.AddStompForce();
        }
    }

    void HandleItem(GameObject item)
    {
        if (item.TryGetComponent<IItemEffect>(out var itemEffect)) { itemEffect.ApplyEffect(); }

        item.SetActive(false);
    }
}