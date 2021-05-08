using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private bool immortal = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !immortal)
        {
            enemyAI.moveAttack = true;
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