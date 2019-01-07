using UnityEngine;

public class Player : MonoBehaviour
{
    private CircleCollider2D collider;
    private SpriteRenderer spriteRenderer;

    private float speed = 2f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 startingPosition = new Vector3(-10f, 0f, 0f);
    private Vector3 currentPosition;
    private Vector3 nextPosition;
    private Vector3 lastPosition;

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
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");

            if (velocity != Vector3.zero)
            {
                nextPosition = new Vector3(Mathf.Clamp(currentPosition.x + velocity.x, -9f, 9f),
                                           Mathf.Clamp(currentPosition.y + velocity.y, -9f, 9f),
                                           0);
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
                case TileType.Wall:
                    nextPosition = lastPosition;
                    break;
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

    private void Dead()
    {
        nextPosition = startingPosition;
        FindObjectOfType<AudioManager>().Play("Death");
    }
}
