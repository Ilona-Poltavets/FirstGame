using UnityEngine;
using System.Collections;

public class DieSpace : MonoBehaviour
{
    private bool immortal = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !immortal)
        {
            PlayerCtrl.hurt = true;
            hearts.health -= 1;
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        immortal = true;
        yield return new WaitForSeconds(1.0f);
        immortal = false;
    }
}
