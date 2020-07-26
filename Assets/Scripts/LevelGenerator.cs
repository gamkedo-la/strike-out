using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;

    public ColorToPrefab[] colorMappings;
    void Start()
    {
        GenerateLevel();
        StartCoroutine(Rotate());
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                GenerateTile(x, z);
            }
        }
    }

    void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        if (pixelColor.a == 0)
        {
            //pixel is transparent, ignore it
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, z);
                Instantiate(colorMapping.prefab, position, Quaternion.Euler(-90,0,0), transform);
            }
        }
    }

    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(.25f);
        transform.Rotate(90, 0, 0);
    }
}
