using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public GameObject[] childs;
    public Transform child1SpawnPosition, child2SpawnPosition;
    // Start is called before the first frame update
 
    public void Break()
    {
        foreach (GameObject child in childs)
        {
            if (child.gameObject.GetComponents<Sliced>() != null)
            {
                GameObject childClone = Instantiate(child, child1SpawnPosition.position, Quaternion.identity);
                if (!childClone.activeInHierarchy)
                {
                    childClone.SetActive(true);
                }

                childClone.transform.localScale = new Vector3(1, 1, 1);
                childClone.GetComponent<Sliced>().ChanceSprite();
            }

        }
        Destroy(gameObject);
    }
}
