using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) { HandleItem(collision.gameObject); }
        else if (collision.CompareTag("Finish")) { GameManager.instance.NextStage(); }

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
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        if (collision.gameObject.CompareTag("Enemy"))
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

        if (collision.gameObject.CompareTag("Spike"))
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