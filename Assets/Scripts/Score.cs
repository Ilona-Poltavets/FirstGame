using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    
    [SerializeField] public int cost;

    public AudioSource audio;
    public GameObject ScoreText;

    public static int score;

    void Start()
    {
        score = 0;
    }
    private void Update()
    {
        ScoreText.GetComponent<Text>().text = "Score: " + score;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            audio.Play();
            Count();
            Destroy(gameObject);
        }
    }

    private void Count()
    {
        score += cost;
    }
}
