using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    Animator animator;
    public float attackRange;
    public LayerMask DamageableLayerMask;
    public float TimeBtwAttack;
    public float timer;
    [SerializeField] private AudioSource hit;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attack();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void attack()
    {
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                animator.SetTrigger("attack");
                timer = TimeBtwAttack;
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, DamageableLayerMask);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<enemyAI>().TakeDamage();
                        hit.Play();
                    }
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
