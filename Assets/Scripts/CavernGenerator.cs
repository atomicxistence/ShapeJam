using System.Collections.Generic;
using UnityEngine;

public class CavernGenerator : MonoBehaviour
{
    private MapParser mapParser;
    [SerializeField]
    private TilePlacer tilePlacer;

    [SerializeField]
    private List<Texture2D> imageMaps = new List<Texture2D>();
    private List<TileType[,]> maps = new List<TileType[,]>();

    private void Awake()
    {
        mapParser = new MapParser();
        foreach (var image in imageMaps)
        {
            maps.Add(mapParser.ParseMap(image));
        }
    }

    private void Start()
    {
        tilePlacer.PlaceTiles(GetRandomMap());
    }

    private TileType[,] GetRandomMap()
    {

        return maps[Random.Range(0,maps.Count)];
    }
}
