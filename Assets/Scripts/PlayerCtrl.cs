using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject respawn;

    [SerializeField] private AudioSource jump;

    public float speed = 10f;
    public float jumpForce=30f;
    public static int attempts = 3;

    private bool faceRight = true;
    private bool isGrounded;
    public static bool hurt;

    void Start()
    {
        //Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.transform.position = respawn.transform.position;
        hurt = false;
        in_gameMenu.GameIsPaused = false;
    }
    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);
        if (animator)
            animator.SetBool("run", Mathf.Abs(moveX) >= 0.1f);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (moveX > 0 && !faceRight)
            flip();
        else if (moveX < 0 && faceRight)
            flip();
        if (hurt)
        {
            if (animator)
            {
                animator.SetTrigger("hurt");
            }
            hurt = false;
        }
        if (transform.position.y < -8f && attempts != 0)
        {
            gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));
            hearts.health -= 1;
        }
        if (hearts.health == 0 && attempts!=0)
        {
            attempts -= 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Jump()
    {
        if (animator)
        {
            animator.SetTrigger("jump");
        }
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jump.Play();
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            PlayerPrefs.SetFloat("xPos", transform.position.x);
            PlayerPrefs.SetFloat("yPos", transform.position.y);
        }
    }
}