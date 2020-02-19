using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveSkillToSeal : MonoBehaviour
{
    public SealSkillModeEnum.SkillMode skillMode;
    // Start is called before the first frame update
   
    public SealSkillModeEnum.SkillMode GetSkill()
    {
        Destroy(gameObject,0.1f);
        return skillMode;
    }
}
