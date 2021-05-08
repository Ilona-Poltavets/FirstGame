using UnityEngine;

public class movePlatform : MonoBehaviour
{
    float speed = 2f;
    public float max;
    public float min;

    bool moveUp = true;
    void Update()
    {
        if (transform.position.y > max)
        {
            moveUp = false;
        }
        else if (transform.position.y < min)
        {
            moveUp = true;
        }
        if (moveUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}