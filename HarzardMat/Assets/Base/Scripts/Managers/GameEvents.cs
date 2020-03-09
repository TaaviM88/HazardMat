using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }
    #region DoorActions
    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if(onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> onDoorwayTriggerExit;
    public void DoorwayTriggerExit(int id)
    {
        if(onDoorwayTriggerExit != null)
        {
            onDoorwayTriggerExit(id);
        }
    }
    #endregion

    public event Action<string> updateBattleLog;
    public void UpdateBattleLog(string newText)
    {
        if(updateBattleLog != null)
        {
            updateBattleLog(newText);
        }
    }


}
