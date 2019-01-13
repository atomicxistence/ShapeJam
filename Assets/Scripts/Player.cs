using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CircleCollider2D collider;
    private SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private Vector3 startingPosition = new Vector3(-9f, 0f, 0f);
    private Vector3 currentPosition;
    private Vector3 nextPosition;
    private Vector3 lastPosition;

    private TileType[,] currentMap;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPosition = startingPosition;
        nextPosition = currentPosition;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            //Back to Main Menu
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }

            var horizontalMove = Input.GetAxisRaw("Horizontal");
            var verticalMove = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(Vector3.Distance(currentPosition, nextPosition)) < 1)
            {
                if (Mathf.Abs(horizontalMove) > 0)
                {
                    velocity.x = horizontalMove;
                    velocity = WallCheck() ? Vector3.zero : velocity;
                    nextPosition += velocity;
                }
                else if (Mathf.Abs(verticalMove) > 0)
                {
                    velocity.y = verticalMove;
                    velocity = WallCheck() ? Vector3.zero : velocity;
                    nextPosition += velocity;
                }

                BoundsCheck();
                velocity = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (nextPosition != currentPosition)
        {
            transform.position = nextPosition;
            lastPosition = currentPosition;
            currentPosition = nextPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ITile>() != null)
        {
            var tileType = collision.GetComponent<ITile>().GetTileType();
            switch (tileType)
            {
                case TileType.Creature:
                case TileType.Pitfall:
                case TileType.Water:
                    Dead();
                    break;
                default:
                    break;
            }
        }
    }

    public void SetCurrentMap(TileType[,] map)
    {
        currentMap = map;
    }

    private bool WallCheck()
    {
        var tile = currentMap[(int)(currentPosition.y + velocity.y) + 9 , (int)(currentPosition.x + velocity.x) + 9];
        if (tile == TileType.Wall)
        {
            FindObjectOfType<AudioManager>().Play("Oof");
            return true;
        }
        return false;
    }

    private void BoundsCheck()
    {
        nextPosition.x = Mathf.Clamp(nextPosition.x, -9f, 9f);
        nextPosition.y = Mathf.Clamp(nextPosition.y, -9f, 9f);
    }

    private void Dead()
    {
        nextPosition = startingPosition;
        FindObjectOfType<AudioManager>().Play("Death");
    }
}
