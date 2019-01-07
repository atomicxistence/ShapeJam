using UnityEngine;

public class TilePlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;

    public void PlaceTiles(TileType[,] map)
    {
        var startingPoint = new Vector3(-9f, -9f, 0f);

        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                var offsetTilePosition = new Vector3(j, i, 0);
                var currentTilePosition = startingPoint + offsetTilePosition;

                InitializeTile(map[i, j], currentTilePosition);
            }
        }
    }

    private void InitializeTile(TileType tileType, Vector3 placementPosition)
    {
        var currentTile = Instantiate(tilePrefab, placementPosition, Quaternion.identity);
        currentTile.GetComponent<ITile>().SetTileType(tileType);
    }
}
