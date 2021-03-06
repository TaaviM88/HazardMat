﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEventTrigger : MonoBehaviour
{
    // public int id;
    [TextArea]
    public string text;
    public bool triggersOnce = false;
    public float cooldownTime = 5f;
    bool isTriggered = false, cooldownOn = false;

    IEnumerator Cooldown()
    {
        cooldownOn = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldownOn = false;
        GetComponent<Collider2D>().enabled = true;
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
