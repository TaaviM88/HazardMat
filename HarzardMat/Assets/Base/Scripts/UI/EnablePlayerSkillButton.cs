using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayerSkillButton : MonoBehaviour
{
    public PlayerSkillState state = PlayerSkillState.None;
    

    public void EnableSkill()
    {
        GameEvents.current.UpdatePlayerSkillState(state);
    }
}
