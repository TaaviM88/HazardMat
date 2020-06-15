using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliced : MonoBehaviour
{
    public enum WhichPart { Top, Bottom, Right, Left };
    public WhichPart whichpart;
    SpriteRenderer spriteRenderer;
    [Header("Sprite borders")]
    [Header(" ")]
    public float leftBorder = 0;
    public float bottomBorder = 0;
    public float rightBorder = 0;
    public float topBorder = 0;

    [Header("Sprite slice offset from middle")]
    public float leftOffset = 0;
    public float bottomOffset = 0;
    public float rightOffset = 0;
    public float topOffset = 0;

    [Header("How much force when sprite is sliced")]
    public float force = 4;
    Rigidbody2D _rb2D;
    // Start is called before the first frame update
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public void ChanceSprite(Sprite currentSprite)
    {
        _rb2D = GetComponent<Rigidbody2D>();
        PolygonCollider2D pc;
        switch (whichpart)
        {
            case WhichPart.Top:

                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = currentSprite;
                Vector4 border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                var rect = new Rect(0, spriteRenderer.sprite.rect.height / 2 + topOffset, spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height / 2);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.zero, 100, 32, SpriteMeshType.Tight, border, true);
                pc = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                _rb2D.AddForce(Vector2.right * force, ForceMode2D.Impulse);

                break;

            case WhichPart.Bottom:
                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = currentSprite;
                border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                rect = new Rect(0, 0, spriteRenderer.sprite.rect.width, spriteRenderer.sprite.rect.height / 2 + bottomOffset);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 100, 32, SpriteMeshType.Tight, border, true);
                pc = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                _rb2D.AddForce(-Vector2.right * force, ForceMode2D.Impulse);
                break;
            case WhichPart.Right:

                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = currentSprite;
                border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                rect = new Rect(spriteRenderer.sprite.rect.width / 2 + rightOffset, 0, spriteRenderer.sprite.rect.width / 2, spriteRenderer.sprite.rect.height);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 100, 32, SpriteMeshType.Tight, border, true);
                pc = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                _rb2D.AddForce(-Vector2.right * force, ForceMode2D.Impulse);
              
               
                break;
            case WhichPart.Left:
                spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = currentSprite;
                border = new Vector4(leftBorder, bottomBorder, rightBorder, topBorder);
                rect = new Rect(0, 0, spriteRenderer.sprite.rect.width / 2 + leftOffset, spriteRenderer.sprite.rect.height);

                spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, rect, Vector2.one * 0.5f, 100, 32, SpriteMeshType.Tight, border, true);
                pc = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
                _rb2D.AddForce(-Vector2.right * force, ForceMode2D.Impulse);
                
                break;
        }
    }
}
