using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    // public int id;
    public string text;
    public bool triggersOnce = false;
    public float cooldownTime = 5f;
    bool isTriggered = false, cooldownOn = false;

    IEnumerator Cooldown()
    {
        cooldownOn = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldownOn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            if (collision.tag == "Player")
            {
                if (!cooldownOn)
                {
                    GameEvents.current.UpdateBattleLog(text);
                    StartCoroutine(Cooldown());
                    if (triggersOnce && !isTriggered)
                    {
                        isTriggered = true;

                    }
                }
            }
        }
    }
}
