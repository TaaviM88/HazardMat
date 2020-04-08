using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class CombatLog : MonoBehaviour
{
    [SerializeField] TMP_Text combatlog;
    public float typingSpeed = .05f;
    bool istyping = false;
    public List<string> logQueueList = new List<string>();
    //public static CombatLog Instance { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.updateBattleLog += Log;

       /* if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
            Instance = this;
            */
    }

    public void Log(string text)
    {
        if(!istyping)
        {
            if(logQueueList.Count == 0)
            {
                StartCoroutine(PlayText(text));
            }
            else
            {
                StartCoroutine(PlayText(logQueueList[0]));
                logQueueList.RemoveAt(0);
            }
        }
        else
        {
            logQueueList.Add(text);
        }
        //combatlog.text += "\n" + text;
    }

    IEnumerator PlayText(string story)
	{
        istyping = true;
		foreach (char c in story) 
		{
			combatlog.text +=   c;
			yield return new WaitForSeconds (typingSpeed);
		}
        combatlog.text +="\n";

        if(logQueueList.Count > 0)
        {
            StartCoroutine(PlayText(logQueueList[0]));
            logQueueList.RemoveAt(0);
        }
        else
        {
            istyping = false;
        }
    }
}
