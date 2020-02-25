using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool hasCheckpoint = false;
    private Vector2 checkpointPos;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PlayerManager.Instance.gameObject.transform.position = checkpointPos;
        //PlayerManager.Instance.HealPlayer(PlayerManager.Instance.maxHealth);
        //PlayerManager.Instance.isAlive = true;

        //UIFade.Instance.FadeFromBlack();
        PlayerManager.Instance.SetCameraToFollowPlayer();
        //PlayerManager.Instance.move.canMove = true;
    }

    public void LoadNewScene(int levelId)
    {
        if (SceneManager.sceneCountInBuildSettings > levelId)
        {
            SceneManager.LoadScene(levelId);
        }
        else
        {
            Debug.Log($"Can't find levelid in the build settings {levelId}");
        }
    }


    public void StartNewLevel()
    {
        //If we don't have checkpoint go to beginning of the level
        if (!hasCheckpoint)
        {
            PlayerManager.Instance.GoToPosition(GameObject.Find("LevelStartPoint").transform.position);
            UpdateCheckpoint(PlayerManager.Instance.gameObject.transform.position, false);
        }
        //Go to Checkpoint
        else
        {
            PlayerManager.Instance.GoToPosition(checkpointPos);
        }
        PlayerManager.Instance.SetCameraToFollowPlayer();
        //PlayerManager.Instance.GenerateGhost();
        //UIFade.Instance.FadeFromBlack();
    }

    public void UpdateCheckpoint(Vector2 newPos, bool isCheckpoint)
    {
        hasCheckpoint = isCheckpoint;
        checkpointPos = newPos;
    }

    public Vector2 GoToCheckPoint()
    {
        return checkpointPos;
    }
}
