using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    public int id;
    public bool stayOpen = false;
    public bool lockedOpenByLever = false;
    public string description;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //door opens by walking near to it
        if (!lockedOpenByLever)
        {
            if (collision.tag == "Player")
            {
                //Debug.Log(collision.gameObject.name);
                if (description != "")
                {
                    GameEvents.current.UpdateBattleLog(description);
                }

                GameEvents.current.DoorwayTriggerEnter(id);
                //GameEvents.current.UpdateBattleLog("Hey, door is opening!");
            }
            // GameEvents.current.DoorwayTriggerEnter(id);
        }

        if (lockedOpenByLever)
        {

            if(collision.tag == "Pickable")
            {
                //Debug.Log(collision.gameObject.name);
                if (description != "")
                {
                    GameEvents.current.UpdateBattleLog(description);
                }

                GameEvents.current.DoorwayTriggerEnter(id);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!stayOpen)
        {
            if (lockedOpenByLever)
            {

                if (collision.tag == "Pickable") {
                    //Debug.Log(collision.gameObject.name);
                    if (description != "")
                    {
                        GameEvents.current.UpdateBattleLog(description);
                    }
                    GameEvents.current.DoorwayTriggerExit(id);
                }
            }

            if (!lockedOpenByLever)
            {
                if (collision.tag == "Player")
                {
                    GameEvents.current.DoorwayTriggerExit(id); GameEvents.current.UpdateBattleLog("Hey, door is closing!");
                    //Debug.Log(collision.gameObject.name + "exit");
                }
            }
           
                
        }
        
    }
}
