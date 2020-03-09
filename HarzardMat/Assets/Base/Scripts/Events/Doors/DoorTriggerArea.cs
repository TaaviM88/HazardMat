using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    public int id;
    public bool stayOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Debug.Log(collision.gameObject.name);
            GameEvents.current.DoorwayTriggerEnter(id);
            //GameEvents.current.UpdateBattleLog("Hey, door is opening!");
        }
       // GameEvents.current.DoorwayTriggerEnter(id);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!stayOpen)
        {
            if (collision.tag == "Player")
            {
                GameEvents.current.DoorwayTriggerExit(id); GameEvents.current.UpdateBattleLog("Hey, door is closing!");
                //Debug.Log(collision.gameObject.name + "exit");
            }
                
        }
        
    }
}
