using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public Transform summonPoint;
    public GameObject SummonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InstantiateSummon();
            //PlayerManager.Instance.canChangeAttackMode = false;
        }
    }

    public void InstantiateSummon()
    {
        GameObject clone = Instantiate(SummonPrefab, summonPoint.position, Quaternion.identity);
        //clone.transform.localScale = new Vector3( PlayerManager.Instance.side * clone.transform.localScale.x, clone.transform.localScale.y, clone.transform.localScale.z);
        //clone.transform.localScale = new Vector3 (PlayerManager.Instance.side,  clone.transform.localScale.y, clone.transform.localScale.z);
        clone.transform.localScale = new Vector3(PlayerManager.Instance.side, clone.transform.localScale.y, clone.transform.localScale.z);
        //clone.GetComponent<SealMovement>().UpdateOriginalScaleX(PlayerManager.Instance.side);
        PlayerManager.Instance.Summoning();
    }

}
