using UnityEngine;

public class PebbleThrow : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D target = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            var tile = target.transform.gameObject.GetComponent<ITile>();
            if (tile != null)
            {
                if (IsClickingOnSurroundingTile(target.transform))
                {
                    tile.PlayAudio();
                }
            }
        }
    }

    private bool IsClickingOnSurroundingTile(Transform tile)
    {
        return Vector3.Distance(tile.position, player.position) <= 1;
    }
}
