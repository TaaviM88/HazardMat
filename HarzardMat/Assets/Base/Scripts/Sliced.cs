using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliced : MonoBehaviour
{
    public enum WhichPart { Top, Bottom, Right, Left };
    public WhichPart whichpart;
    SpriteRenderer spriteRenderer;
    public float leftBorder = 0, bottomBorder = 0, rightBorder = 0, topBorder = 0;

    Rigidbody2D _rb2D;
    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public void ChanceSprite()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        switch (whichpart)
        {
            case WhichPart.Top:

                spriteRenderer = GetComponent<SpriteRenderer>();
                Vector4 border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                var rect = new Rect(0, spriteRenderer.sprite.rect.height / 2, spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height / 2);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.zero, 32, 32, SpriteMeshType.Tight, border, true);
                _rb2D.AddForce(Vector2.right * 2, ForceMode2D.Impulse);

                break;

            case WhichPart.Bottom:
                spriteRenderer = GetComponent<SpriteRenderer>();
                border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                rect = new Rect(0, 0, spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height / 2);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 32, 32, SpriteMeshType.Tight, border, true);
                _rb2D.AddForce(-Vector2.right * 2, ForceMode2D.Impulse);
                break;
            case WhichPart.Right:

                spriteRenderer = GetComponent<SpriteRenderer>();
                border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                rect = new Rect(spriteRenderer.sprite.rect.width / 2, 0, spriteRenderer.sprite.rect.width / 2, spriteRenderer.sprite.rect.height);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 32, 32, SpriteMeshType.Tight, border, true);
                _rb2D.AddForce(-Vector2.right * 2, ForceMode2D.Impulse);
                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 32, 32, SpriteMeshType.Tight, border, true);
                _rb2D.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
                break;
            case WhichPart.Left:
                spriteRenderer = GetComponent<SpriteRenderer>();
                border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                rect = new Rect(0, 0, spriteRenderer.sprite.rect.width / 2, spriteRenderer.sprite.rect.height);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 32, 32, SpriteMeshType.Tight, border, true);
                _rb2D.AddForce(-Vector2.right * 2, ForceMode2D.Impulse);
                break;
        }
    }
}
