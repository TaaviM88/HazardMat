using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    SpriteRenderer sprite;
    public GameObject[] childs;
    public Transform child1SpawnPosition, child2SpawnPosition;
    // Start is called before the first frame update
 
    public void Break()
    {
       

        sprite = GetComponent<SpriteRenderer>();
        //foreach (GameObject child in childs)
        //{
        //    if (child.gameObject.GetComponents<Sliced>() != null)
        //    {
        //        GameObject childClone = Instantiate(child, child1SpawnPosition.position, Quaternion.identity);
        //        if (!childClone.activeInHierarchy)
        //        {
        //            childClone.SetActive(true);
        //        }
               
        //        childClone.transform.localScale = new Vector3((int)transform.localScale.x, 1, 1);
        //        //facing = mihin päin vihollinen katsoo kun se halkaistaan
        //        childClone.GetComponent<Sliced>().ChanceSprite(sprite.sprite);
        //    }

        //}
        Destroy(gameObject);
    }
}
