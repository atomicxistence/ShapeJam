using UnityEngine;

public class TileController : MonoBehaviour, ITile
{
    private Collider2D tileCollider;
    private SpriteRenderer spriteRenderer;
    private AudioManager audioManager;

    private Color32 hiddenColor = new Color32(0, 0, 0, 255);
    private Color32 pathColor = new Color32(120, 120, 120, 255);
    private Color32 markedPathColor = new Color32(78, 202, 86, 255);
    private Color32 markedDangerColor = new Color32(252, 75, 78, 255);
    private Color32 revealColor;

    private string tileSoundName;

    private TileType tileType;

    private void Awake()
    {
        tileCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = pathColor;
        tileSoundName = "Path";
    }

    private void Start()
    {
        var start = new Vector3(-9f, 0f, 0f);
        var finish = new Vector3(9f, 0f, 0f);

        if (transform.position == start || transform.position == finish)
        {
            spriteRenderer.color = revealColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.color = revealColor;
    }

    public void SetTileType(TileType type)
    {
        spriteRenderer.color = hiddenColor;
        tileType = type;
        SetRevealedTileColorAndSound();
    }

    public TileType GetTileType()
    {
        return tileType;
    }

    public void PlayAudio()
    {
        FindObjectOfType<AudioManager>().Play(tileSoundName);
    }

    private void SetRevealedTileColorAndSound()
    {
        switch (tileType)
        {
            case TileType.Path:
                revealColor = pathColor;
                tileSoundName = "Path";
                break;
            case TileType.Wall:
                revealColor = hiddenColor;
                tileSoundName = "Wall";
                break;
            case TileType.Pitfall:
                tileSoundName = "Pitfall";
                revealColor = markedDangerColor;
                break;
            case TileType.Creature:
                tileSoundName = "Creature";
                revealColor = markedDangerColor;
                break;
            case TileType.Water:
                tileSoundName = "Water";
                revealColor = markedDangerColor;
                break;
            case TileType.Danger:
                revealColor = markedDangerColor;
                break;
            default:
                break;
        }
    }
}
