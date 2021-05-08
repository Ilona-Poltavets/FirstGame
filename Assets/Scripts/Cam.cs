using UnityEngine;

public class Cam : MonoBehaviour
{
    private Transform player;
    public Vector2 offset = new Vector2(2f, 1f);
    
    private int lastX;

    public bool isLeft;
    public float dumping = 1.5f;
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, -100f);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -100f);
        }
    }

    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > lastX)
                isLeft = false;
            else if (currentX < lastX)
                isLeft = true;
            lastX = Mathf.RoundToInt(player.position.x);
            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y - offset.y, -100f);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -100f);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}