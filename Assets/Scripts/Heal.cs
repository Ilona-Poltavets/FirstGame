using UnityEngine;

public class Heal : MonoBehaviour
{
    public AudioSource audio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hearts.health += 1;
            audio.Play();
            Destroy(gameObject);
        }
    }
}
