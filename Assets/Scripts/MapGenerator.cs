using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Object containers")]
    [SerializeField]
    Transform ocean;
    [SerializeField]
    Transform land;

    [Header("Tile Sprites")]
    [SerializeField]
    GameObject[] oceanTile;
    [SerializeField]
    GameObject[] middleTile;
    [SerializeField]
    GameObject[] topLeftTile;
    [SerializeField]
    GameObject[] topTile;
    [SerializeField]
    GameObject[] topRightTile;
    [SerializeField]
    GameObject[] rightTile;
    [SerializeField]
    GameObject[] bottomRightTile;
    [SerializeField]
    GameObject[] bottomTile;
    [SerializeField]
    GameObject[] bottomLeftTile;
    [SerializeField]
    GameObject[] leftTile;
    [SerializeField]
    GameObject[] innerTopLeftTile;
    [SerializeField]
    GameObject[] innerTopRightTile;
    [SerializeField]
    GameObject[] innerBottomLeftTile;
    [SerializeField]
    GameObject[] innerBottomRightTile;

    [Header("Settings")]
    [SerializeField]
    int mapSize;
    [SerializeField]
    int minIslandWidth;
    [SerializeField]
    int maxIslandWidth;
    [SerializeField]
    int minIslandHeight;
    [SerializeField]
    int maxIslandHeight;
    [SerializeField]
    [Range(0, 100)]
    float islandVariationFrequency;

    // Start is called before the first frame update
    void Start()
    {
        for(int c = 0; c < mapSize; c++)
        {
            for(int i = 0; i < mapSize; i++)
            {
                Instantiate(oceanTile[Random.Range(0, oceanTile.Length)], new Vector3(c, i), Quaternion.identity, ocean);
                Instantiate(oceanTile[Random.Range(0, oceanTile.Length)], new Vector3(-c, i), Quaternion.identity, ocean);
                Instantiate(oceanTile[Random.Range(0, oceanTile.Length)], new Vector3(c, -i), Quaternion.identity, ocean);
                Instantiate(oceanTile[Random.Range(0, oceanTile.Length)], new Vector3(-c, -i), Quaternion.identity, ocean);
            }
        }
        CreateIsland(new Vector3(10, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateIsland(Vector3 position)
    {
        Transform islandContainer = Instantiate(new GameObject("island")).GetComponent<Transform>();
        int width = Random.Range(minIslandWidth, maxIslandWidth);
        int height = Random.Range(minIslandHeight, maxIslandHeight);
        bool[,] isLand = new bool[width,height];
        for (int c = 0; c < width; c++)
        {
            for (int i = 0; i < height; i++)
            {
                if (c > 1 && i > 1 && c < width - 2 && i < height - 2) isLand[c, i] = true;
                else if (c > 2 && isLand[c - 1, i] && !isLand[c - 2, i] || c > 2 && isLand[c - 1, i] && !isLand[c - 3, i] || c > 2 && isLand[c - 1, i] && c > width - 3 || c == 1 && isLand[c - 1, i] || c == 2 && isLand[c - 1, i]) isLand[c, i] = true;
                else if (i > 2 && isLand[c, i - 1] && !isLand[c, i - 2] || i > 2 && isLand[c, i - 1] && !isLand[c, i - 3] || i > 2 && isLand[c, i - 1] && i > height - 3 || i == 1 && isLand[c, i - 1] || i == 2 && isLand[c, i - 1]) isLand[c, i] = true;
                else if (c > width - 3) isLand[c, i] = isLand[c - 1, i];
                else if (i > height - 3) isLand[c, i] = isLand[c, i - 1];
                else isLand[c, i] = Random.value * 100 > islandVariationFrequency;
            }
        }
        for (int c = 1; c < width - 1; c++)
        {
            for (int i = 1; i < height - 1; i++)
            {
                if (isLand[c - 1, i] && isLand[c + 1, i] && isLand[c, i - 1] && isLand[c, i + 1]) isLand[c, i] = true;
            }
        }
        for (int c = 0; c < width; c++)
        {
            for (int i = 0; i < height; i++)
            {
                if(isLand[c,i])
                {
                    if(c == 0 && i == 0 || c == 0 && !isLand[c, i - 1] ||i == 0 && !isLand[c - 1, i] || c > 0 && i > 0 && !isLand[c - 1, i] && !isLand[c, i - 1])
                        Instantiate(bottomLeftTile[Random.Range(0, bottomLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (i == 0 && c > 0 && c < width-1 && isLand[c + 1, i] && isLand[c - 1, i] || i > 0 && c > 0 && c < width - 1 && !isLand[c,i - 1] && isLand[c + 1, i] && isLand[c - 1, i])
                        Instantiate(bottomTile[Random.Range(0, bottomTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if(c == width-1 && i == 0 || i > 0 && c == width-1 && !isLand[c, i - 1] || i == 0 && c < width - 1 && !isLand[c + 1, i] || c < width-1 && i > 0 && !isLand[c + 1, i] && !isLand[c, i - 1])
                        Instantiate(bottomRightTile[Random.Range(0, bottomRightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c == width-1 && i > 0 && i < height - 1 && isLand[c, i + 1] && isLand[c, i - 1] || c < width-1 && i > 0 && i < height-1 && !isLand[c + 1, i] && isLand[c, i + 1] && isLand[c, i - 1])
                        Instantiate(rightTile[Random.Range(0, rightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c == width-1 && i == height-1 || c == width-1 && !isLand[c, i + 1] || i == height-1 && !isLand[c + 1, i] || c > width - 1 && i > height - 1 && !isLand[c + 1, i] && !isLand[c, i + 1])
                        Instantiate(topRightTile[Random.Range(0, topRightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c < width - 1 && c > 0 && i == height - 1 && isLand[c + 1, i] && isLand[c - 1, i] || i < height -1 && c > 0 && c < width-1 && !isLand[c, i + 1] && isLand[c + 1, i] && isLand[c - 1, i])
                        Instantiate(topTile[Random.Range(0, topTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c == 0 && i == height - 1 || c == 0 && !isLand[c, i + 1] || i == height - 1 && !isLand[c - 1, i] || c > 0 && i < height -1 && !isLand[c - 1, i] && !isLand[c, i + 1])
                        Instantiate(topLeftTile[Random.Range(0, topLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c == 0 && isLand[c, i + 1] && isLand[c, i - 1] || c > 0 && i > 0 && i < height - 1 && !isLand[c - 1, i] && isLand[c, i + 1] && isLand[c, i - 1])
                        Instantiate(leftTile[Random.Range(0, leftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c > 0 && c < width - 1 && i > 0 && i < height - 1 && !isLand[c + 1, i + 1])
                        Instantiate(innerTopRightTile[Random.Range(0, innerTopRightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c > 0 && c < width - 1 && i > 0 && i < height - 1 && !isLand[c - 1, i + 1])
                        Instantiate(innerTopLeftTile[Random.Range(0, innerTopLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c > 0 && c < width - 1 && i > 0 && i < height - 1 && !isLand[c + 1, i - 1])
                        Instantiate(innerBottomRightTile[Random.Range(0, innerBottomRightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else if (c > 0 && c < width - 1 && i > 0 && i < height - 1 && !isLand[c - 1, i - 1])
                        Instantiate(innerBottomLeftTile[Random.Range(0, innerBottomLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);

                    else
                        Instantiate(middleTile[Random.Range(0, middleTile.Length)], new Vector3(c, i) + position, Quaternion.identity, islandContainer);
                }
            }
        }
    }
}

/* if(c == 0 && i == 0)
                        Instantiate(topLeftTile[Random.Range(0, topLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);
                    else if (c == 0 && !isLand[c, i - 1])
                        Instantiate(topLeftTile[Random.Range(0, topLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);
                    else if (i == 0 && !isLand[c - 1, i])
                        Instantiate(topLeftTile[Random.Range(0, topLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);
                    else if(i > 0 && c > 0 && !isLand[c - 1, i] && !isLand[c, i - 1])
                        Instantiate(topLeftTile[Random.Range(0, topLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);
                        */
                    
