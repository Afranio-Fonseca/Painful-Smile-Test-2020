﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Object containers")]
    [SerializeField]
    Transform land = null;

    [Header("Tile Sprites")]
    [SerializeField]
    GameObject[] middleTile = null;
    [SerializeField]
    GameObject[] topLeftTile = null;
    [SerializeField]
    GameObject[] topTile = null;
    [SerializeField]
    GameObject[] topRightTile = null;
    [SerializeField]
    GameObject[] rightTile = null;
    [SerializeField]
    GameObject[] bottomRightTile = null;
    [SerializeField]
    GameObject[] bottomTile = null;
    [SerializeField]
    GameObject[] bottomLeftTile = null;
    [SerializeField]
    GameObject[] leftTile = null;
    [SerializeField]
    GameObject[] innerTopLeftTile = null;
    [SerializeField]
    GameObject[] innerTopRightTile = null;
    [SerializeField]
    GameObject[] innerBottomLeftTile = null;
    [SerializeField]
    GameObject[] innerBottomRightTile = null;

    /*
    [Header("Enemy prefabs")]
    [SerializeField]
    GameObject chaser = null;
    [SerializeField]
    GameObject shooter = null;
    */

    [Header("Settings")]
    [SerializeField]
    int minIslandWidth = 5;
    [SerializeField]
    int maxIslandWidth = 15;
    [SerializeField]
    int minIslandHeight = 5;
    [SerializeField]
    int maxIslandHeight = 15;
    [SerializeField]
    int islandDistance = 15;
    [SerializeField]
    float islandCheckInterval = 0.2f;

    /*
    [SerializeField]
    int minEnemiesPerIsland = 6;
    [SerializeField]
    int maxEnemiesPerIsland = 8;
    */
    [SerializeField]
    [Range(0, 100)]
    float islandCurvesFrequency = 50;

    List<GameObject> islandList = new List<GameObject>();

    float timeSinceLastCheck = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(islandDistance < maxIslandWidth || islandDistance < maxIslandHeight)
        {
            Debug.LogWarning("Island distance can't be lower than the max island size, setting it to highest value of island size.");
            islandDistance = Mathf.Max(maxIslandHeight, maxIslandWidth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastCheck += Time.deltaTime;
        if(timeSinceLastCheck >= islandCheckInterval && PlayerManager.instance != null)
        {
            LayerMask lm = LayerMask.GetMask("Island");
            Vector3 spawnPoint = PlayerManager.instance.transform.position + PlayerManager.instance.transform.up * islandDistance - new Vector3(islandDistance / 2, islandDistance / 2);
            if (Physics2D.OverlapCircle(PlayerManager.instance.transform.position, islandDistance / 2, lm) == null && !Physics2D.OverlapArea(spawnPoint, spawnPoint + new Vector3(maxIslandWidth, maxIslandHeight), lm))
            {
                Vector3 islandPosition = PlayerManager.instance.transform.position + PlayerManager.instance.transform.up * islandDistance - new Vector3(islandDistance / 2, islandDistance / 2);
                CreateIsland(islandPosition);
            }
            timeSinceLastCheck = 0;
        }
    }

    public void CreateIsland(Vector3 position)
    {
        Transform islandContainer = Instantiate(new GameObject("island"), land).GetComponent<Transform>();
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
                else isLand[c, i] = Random.value * 100 > islandCurvesFrequency;
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
        islandList.Add(islandContainer.gameObject);


        /*
        int enemiesPerIsland = Random.Range(minEnemiesPerIsland, maxEnemiesPerIsland);
        for (int c = 0; c < enemiesPerIsland; c++)
        {
            GameObject enemy = Random.value > 0.5f ? chaser : shooter;
            int side = Random.Range(0, 3);
            switch (side)
            {
                case 0:
                    Instantiate(enemy, position + new Vector3(Random.value * width, height + 2), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(enemy, position + new Vector3(width + 2, Random.value * height), Quaternion.Euler(0, 0, 270));
                    break;
                case 2:
                    Instantiate(enemy, position + new Vector3(Random.value * width, -2), Quaternion.Euler(0, 0, 180));
                    break;
                case 3:
                    Instantiate(enemy, position + new Vector3(-2, Random.value * height), Quaternion.Euler(0, 0, 90));
                    break;
            }
        }
        */
    }
}