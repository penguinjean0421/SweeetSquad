using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [Header("VFX References")]
    public GameObject clearParticlePrefab;
    public float particleScaleMultiplier = 1f;

    [Header("Clear Score")]
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

        if (collision.CompareTag("Finish"))
        {
            collision.enabled = false;
            StartCoroutine(FinishSequenceRoutine(collision.gameObject));
        }

        if (collision.CompareTag("DeadZone"))
        {
            GameManager.instance.HealthDown();
            GameManager.instance.PlayerReposition();
        }
    }

    IEnumerator FinishSequenceRoutine(GameObject finishObj)
    {
        if (clearParticlePrefab != null)
        {
            int spawnCount = 4;
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 0.6f), 0f);
                GameObject particleGo = Instantiate(clearParticlePrefab, finishObj.transform.position + randomOffset, Quaternion.identity);
                particleGo.transform.localScale *= particleScaleMultiplier;
                DontDestroyOnLoad(particleGo);
                Destroy(particleGo, 3.0f);
            }
        }

        yield return new WaitForSeconds(1.0f);

        GameManager.instance.score += stageClearScore;
        UIManager.instance.UpdateScore(GameManager.instance.score);

        GameManager.instance.NextStage();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerDamagedLayer) { return; }

        if (collision.gameObject.CompareTag("Spike")) { TryTakeDamage(collision.transform.position); }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 normal = collision.GetContact(0).normal;

            if (rigid.linearVelocity.y < 0 && normal.y > 0.5f)
            {
                OnAttack(collision.transform);
            }
            else
            {
                TryTakeDamage(collision.transform.position);
            }
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

    void TryTakeDamage(Vector3 targetPosition)
    {
        if (!playerHealth.isInvincible) { playerHealth.OnDamaged(targetPosition); }
    }

    void HandleItem(GameObject item)
    {
        // 파티클 실행

        if (item.TryGetComponent<IItemEffect>(out var itemEffect)) { itemEffect.ApplyEffect(); }

        item.SetActive(false);
    }
}