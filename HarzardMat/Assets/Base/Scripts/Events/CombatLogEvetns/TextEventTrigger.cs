using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    // public int id;
    public string text;
    public bool triggersOnce = false;
    bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            if (collision.tag == "Player")
            {
                GameEvents.current.UpdateBattleLog(text);
                if (triggersOnce && !isTriggered)
                {
                    isTriggered = true;

                }
            }
        }
    }
}
