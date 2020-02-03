using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameManager;
    public GameObject audioManager;

    // Start is called before the first frame update
    void Awake()
    {
       // InstantiateUICanvas();
       // InstantiatePlayer();
        InstantiateGameManager();
        InstantiateAudioManager();
    }

    private void InstantiateAudioManager()
    {
        if (AudioManager.Instance == null)
        {
            Instantiate(audioManager);
        }
    }

    private void InstantiateGameManager()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
    }

    private void InstantiateUICanvas()
    {
       /* if (UIManager.Instance == null)
        {
            //UIFade.Instance = Instantiate(UIScreen).GetComponent<UIFade>();
            Instantiate(UIScreen);
        }*/
    }

    private void InstantiatePlayer()
    {
        if (PlayerManager.Instance == null)
        {
            GameObject start = GameObject.Find("LevelStartPoint");

            GameObject clone = Instantiate(player, start.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("player found");
        }
    }
}
