using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    CombatLog combatLog;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        combatLog = GetComponent<CombatLog>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCombatLog(string text)
    {
        combatLog.Log(text);
    }
}
