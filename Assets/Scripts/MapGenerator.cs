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
            }
        }
        CreateIsland(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateIsland(Vector3 position)
    {
        int width = Random.Range(minIslandWidth, maxIslandWidth);
        int height = Random.Range(minIslandHeight, maxIslandHeight);
        bool[,] isLand = new bool[width,height];
        for (int c = 0; c < width; c++)
        {
            for (int i = 0; i < height; i++)
            {
                if (c > 2 && isLand[c - 1, i] && !isLand[c - 3, i] || c > 2 && isLand[c - 1, i] && c > width - 3 || c == 1 && isLand[c - 1, i] || c == 2 && isLand[c - 1, i]) isLand[c, i] = true;
                else if (i > 2 && isLand[c, i - 1] && !isLand[c, i - 3] || i > 2 && isLand[c, i - 1] && i > height - 3 || i == 1 && isLand[c, i - 1] || i == 2 && isLand[c, i - 1]) isLand[c, i] = true;
                else if (c > width - 3 && !isLand[c - 1, i]) isLand[c, i] = false;
                else if (i > height - 3 && !isLand[c, i - 1]) isLand[c, i] = false;
                else isLand[c, i] = Random.value * 100 < islandVariationFrequency;
            }
        }
        for (int c = 1; c < width - 1; c++)
        {
            for (int i = 1; i < height - 1; i++)
            {
                if (isLand[c - 1, i] && isLand[c + 1, i] && isLand[c, i - 1] && isLand[c, i + 1]) isLand[c, i] = true;
            }
        }
        string x = "";
        for (int c = 0; c < width; c++)
        {
            x += "\n";
            for (int i = 0; i < height; i++)
            {
                if(isLand[c,i])
                {
                    if(c == 0 && i == 0 || c == 0 && !isLand[c, i - 1] ||i == 0 && !isLand[c - 1, i] || c > 0 && i > 0 && !isLand[c - 1, i] && !isLand[c, i - 1])
                        Instantiate(bottomLeftTile[Random.Range(0, bottomLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if (i == 0 && c > 0 && c < width-1 && isLand[c + 1, i] && isLand[c - 1, i] || i > 0 && c > 0 && c < width - 1 && !isLand[c,i - 1] && isLand[c + 1, i] && isLand[c - 1, i])
                        Instantiate(bottomTile[Random.Range(0, bottomTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if(c == width-1 && i == 0 || c == width-1 && !isLand[c, i - 1] || i == 0 && !isLand[c + 1, i] || c > width-1 && i > 0 && !isLand[c + 1, i] && !isLand[c, i - 1])
                        Instantiate(bottomRightTile[Random.Range(0, bottomRightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if (c == width-1 && i > 0 && i < height - 1 && isLand[c, i + 1] && isLand[c, i - 1] || c < width-1 && i > 0 && i < height-1 && !isLand[c + 1, i] && isLand[c, i + 1] && isLand[c, i - 1])
                        Instantiate(rightTile[Random.Range(0, rightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if (c == width-1 && i == height-1 || c == width-1 && !isLand[c, i + 1] || i == height-1 && !isLand[c + 1, i] || c > width - 1 && i > height - 1 && !isLand[c + 1, i] && !isLand[c, i + 1])
                        Instantiate(topRightTile[Random.Range(0, topRightTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if (c < height - 1 && c > 0 && i == height - 1 && isLand[c + 1, i] && isLand[c - 1, i] || i < height -1 && c > 0 && c < width-1 && !isLand[c, i + 1] && isLand[c + 1, i] && isLand[c - 1, i])
                        Instantiate(topTile[Random.Range(0, topTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if (c == 0 && i == height - 1 || c == 0 && !isLand[c, i + 1] || i == height - 1 && !isLand[c - 1, i] || c > 0 && i < height -1 && !isLand[c - 1, i] && !isLand[c, i + 1])
                        Instantiate(topLeftTile[Random.Range(0, topLeftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                    else if (c == 0 && isLand[c, i + 1] && isLand[c, i - 1] || !isLand[c + 1, i] && isLand[c, i + 1] && isLand[c, i - 1])
                        Instantiate(leftTile[Random.Range(0, leftTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);
                    else
                        Instantiate(middleTile[Random.Range(0, middleTile.Length)], new Vector3(c, i) + position, Quaternion.identity, land);

                }
                if (isLand[c, i]) x += " 1"; else x += " 0";
            }
        }
        Debug.Log(x);

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
                    
