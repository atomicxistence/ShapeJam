using System;
using System.Linq;
using UnityEngine;

public class MapParser
{
    public TileType[,] ParseMap(Texture2D mapImage)
    {
        var imageArray = ChangeMapImageToImageArray(mapImage);
        var integerValueArray = ParseArrayElementsForTileType(imageArray);
        return ConvertIntegerArraysToIntegerMaps(integerValueArray);
    }

    private Color32[] ChangeMapImageToImageArray(Texture2D mapImage)
    {
        var imageArray = mapImage.GetPixels32();
        return imageArray;
    }

    private int[] ParseArrayElementsForTileType(Color32[] imageArray)
    {
        var integerArray = new int[361];

        for (int i = 0; i < imageArray.Length; i++)
        {
            var colorTotal = imageArray[i].r + imageArray[i].b + imageArray[i].g;
            var elementToInteger = 0;

            switch (colorTotal)
            {
                case 0:
                    elementToInteger = 0;
                    break;
                case 393:
                    elementToInteger = 1;
                    break;
                case 547:
                    elementToInteger = 3;
                    break;
                case 403:
                    elementToInteger = 4;
                    break;
                case 509:
                    elementToInteger = 5;
                    break;
                default:
                    break;
            }

            integerArray[i] = elementToInteger;
        }

        return integerArray;
    }

    private TileType[,] ConvertIntegerArraysToIntegerMaps(int[] integerArray)
    {
        var intMap = new int[19, 19];
        Buffer.BlockCopy(integerArray, 0, intMap, 0, integerArray.Length * sizeof(int));
        return ConvertIntegerMapToTileTypeMap(intMap);
    }

    private TileType[,] ConvertIntegerMapToTileTypeMap(int[,] intMap)
    {
        var map = new TileType[19, 19];

        for (int i = 0; i < 19; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                switch (intMap[i,j])
                {
                    case 0:
                        map[i, j] = TileType.Wall;
                        break;
                    case 1:
                        map[i, j] = TileType.Path;
                        break;
                    case 3:
                        map[i, j] = TileType.Pitfall;
                        break;
                    case 4:
                        map[i, j] = TileType.Creature;
                        break;
                    case 5:
                        map[i, j] = TileType.Water;
                        break;
                    default:
                        map[i, j] = TileType.Path;
                        break;
                }
            }
        }
        return map;
    }
}
