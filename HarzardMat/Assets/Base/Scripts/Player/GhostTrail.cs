using UnityEngine;
using DG.Tweening;
public class GhostTrail : MonoBehaviour
{
    private PlayerMovement move;
    private SpriteRenderer sr;
    public Transform ghostsParent;
    private PlayerAnimationScript anime;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        move = PlayerManager.Instance.gameObject.GetComponent<PlayerMovement>();
        anime = PlayerManager.Instance.gameObject.GetComponent<PlayerAnimationScript>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void ShowGhost()
    {
        Sequence s = DOTween.Sequence();
        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            currentGhost.localScale = new Vector3(PlayerManager.Instance.side, transform.localScale.y, transform.localScale.z); 
            s.AppendCallback(() => currentGhost.position = move.transform.position);
            //s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = anime.sr.flipX);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = anime.sr.sprite);
            s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(ghostInterval);
        }
    }
    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
