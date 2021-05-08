using UnityEngine;
using System.Collections;
public class enemyAI : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    private Rigidbody2D rb;
    Animator animator;

    public int health;
    public float speed = 2f;
    public float impulseForce = 500f;

    private bool movingRight = true;
    public static bool moveAttack;
    private bool immortal = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveAttack = false;
        health = 3;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (animator)
        {
            animator.SetBool("walk", true);
        }
        if (moveAttack)
        {
            attack();
            moveAttack = false;
        }
        if (transform.position.y < -10f)
        {
            death();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "block")
        {
            flip();
        }
    }
    public void flip()
    {
        movingRight = !movingRight;
        if (!movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void attack()
    {
        if (animator)
        {
            audio.Play();
            animator.SetTrigger("attack");
            animator.SetBool("walk", true);
        }
    }
    private IEnumerator Wait()
    {
        immortal = true;
        yield return new WaitForSeconds(15.0f);
        immortal = false;
    }
    public void TakeDamage()
    {
        health -= 1;
        animator.SetTrigger("hurt");
        rb.AddForce(Vector2.right * impulseForce);
        if (health <= 0)
        {
            death();
        }
    }

    private void death()
    {
        animator.SetTrigger("death");
        animator.SetBool("walk", false);
        if (!immortal)
        {
            StartCoroutine(Wait());
            Destroy(gameObject);
            Score.score += 1000;
        }
    }
}