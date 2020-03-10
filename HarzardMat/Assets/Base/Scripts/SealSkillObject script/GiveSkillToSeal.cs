using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSkillToSeal : MonoBehaviour
{
    public SealSkillState skillMode;
    // Start is called before the first frame update
   
    public SealSkillState GetSkill()
    {
        Destroy(gameObject,0.1f);
        return skillMode;
    }
}
