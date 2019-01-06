using UnityEngine;

public class Tile : MonoBehaviour
{
    private Collider2D tileCollider;
    private SpriteRenderer spriteRenderer;

    private Color wallColor = new Color(0f, 0f, 0f);
    private Color pathColor = new Color(120f, 120f, 120f);
    private Color markedPathColor = new Color(78f, 202f, 86f);
    private Color markedDangerColor = new Color(252f, 75f, 78f);
    private Color revealColor;

    [SerializeField]
    private TileType tileType;

    private void Awake()
    {
        tileCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = wallColor;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = revealColor;
    }

    public void SetTileType(TileType type)
    {
        tileType = type;
        SetRevealedTileColor();
    }

    private void SetRevealedTileColor()
    {
        switch (tileType)
        {
            case TileType.Path:
                revealColor = pathColor;
                break;
            case TileType.Wall:
                revealColor = wallColor;
                break;
            case TileType.Pitfall:
            case TileType.Creature:
            case TileType.Water:
            case TileType.Danger:
                revealColor = markedDangerColor;
                break;
            default:
                break;
        }
    }
}
