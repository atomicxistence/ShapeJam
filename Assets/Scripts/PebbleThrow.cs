using UnityEngine;

public class PebbleThrow : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D target = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            var tile = target.transform.gameObject.GetComponent<ITile>();
            if (tile != null)
            {
                tile.PlayAudio();
            }
        }
    }
}
