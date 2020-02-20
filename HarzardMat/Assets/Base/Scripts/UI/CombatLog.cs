using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatLog : MonoBehaviour
{
    [SerializeField] TMP_Text combatlog;
    //public static CombatLog Instance { get; set; }
    // Start is called before the first frame update
    void Start()
    {
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
        combatlog.text += "\n" + text;
    }
}
