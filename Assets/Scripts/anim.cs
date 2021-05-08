using UnityEngine;

public class anim : MonoBehaviour
{
    Animator animator;
    public AudioSource audio;
    private bool flag;

    private void Start()
    {
        animator = GetComponent<Animator>();
        flag = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player" && !flag)
        {
            audio.Play();
            if (animator)
            {
                animator.SetTrigger("check");
                animator.SetBool("off", true);
            }
            flag = true;
        }
    }
}
