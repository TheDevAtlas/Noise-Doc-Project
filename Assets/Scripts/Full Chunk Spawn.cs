using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullChunkSpawn : MonoBehaviour
{
    [Header("Chunk Settings")]
    public int chunkSize;

    [Header("Block Settings")]
    public GameObject blockPrefab;
    public Material[] blockTypes;
    public int terrainHeightOffset;

    float[][][] worldGen; // if data is below 0, air. if data is above 0, solid //
    int[][][] worldGenMat;

    public float moveDelay;
    public Vector3 targetPosition;

    private void Start()
    {
        // Generate The World Data //
        GenerateData();

        // Generate The World Mesh //
        StartCoroutine(GenerateWorld());

        StartCoroutine(MoveChunk());
    }

    void GenerateData()
    {
        worldGen = new float[chunkSize][][];
        worldGenMat = new int[chunkSize][][];
        float scale = 0.07f;  // Scale factor for the Perlin noise, smaller values make smoother terrain
        float caveThreshold = 0.2f;  // Threshold for determining if a space is part of a cave

        // Perlin worms control variables
        float wormFrequency = 0.25f;  // Frequency of worms, affects the density of cave systems
        float wormScale = 0.2f;  // Scale for the worm Perlin noise

        for (int y = 0; y < chunkSize; y++)
        {
            worldGen[y] = new float[chunkSize][];
            worldGenMat[y] = new int[chunkSize][];
            for (int x = 0; x < chunkSize; x++)
            {
                worldGen[y][x] = new float[chunkSize];
                worldGenMat[y][x] = new int[chunkSize];
                for (int z = 0; z < chunkSize; z++)
                {
                    // Use Perlin noise to set initial terrain height
                    float terrainHeight = (Mathf.PerlinNoise(x * scale, z * scale) * chunkSize) + terrainHeightOffset;

                    // Determine if the current point is inside a cave
                    bool isCave = Mathf.PerlinNoise(x * wormScale + 100, z * wormScale + 100) - Mathf.PerlinNoise(y * wormFrequency, x * wormFrequency + z * wormFrequency) > caveThreshold;

                    if (y < terrainHeight && !isCave || y == 0)
                    {
                        worldGen[y][x][z] = 1;  // solid block
                    }
                    else
                    {
                        worldGen[y][x][z] = -1;  // air
                    }

                    if(y == 0)
                    {
                        worldGenMat[y][x][z] = 0;
                    }
                    else
                    if(y == 1 || y == 2)
                    {
                        worldGenMat[y][x][z] = Random.Range(0, 2);
                    }
                    else
                    if (y == 3 || y == 4)
                    {
                        worldGenMat[y][x][z] = 1;
                    }
                    else
                    if (y == 5 || y == 6)
                    {
                        worldGenMat[y][x][z] = Random.Range(1, 3);
                    }
                    else
                    if (y == 7)
                    {
                        worldGenMat[y][x][z] = 2;
                    }
                    else
                    if (y == 8)
                    {
                        worldGenMat[y][x][z] = Random.Range(2, 4); ;
                    }
                    else
                    if (y > 8)
                    {
                        worldGenMat[y][x][z] = 3;
                    }

                    if(y < terrainHeight && (y+1) > terrainHeight || y == 15)
                    {
                        worldGenMat[y][x][z] = 4;
                    }

                }
            }
        }
    }


    IEnumerator GenerateWorld()
    {
        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (worldGen[y][x][z] >= 0)
                    {
                        // spawn cube //
                        GameObject g = Instantiate(blockPrefab, new Vector3(x,y,z) - (new Vector3(chunkSize,chunkSize,chunkSize) / 2f - new Vector3(0.5f, 0.5f,0.5f)), Quaternion.identity);
                        g.transform.parent = transform;
                        StartCoroutine(ScaleCube(g));

                        // change material //
                        g.GetComponent<Renderer>().material = blockTypes[worldGenMat[y][x][z]];
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ScaleCube(GameObject block)
    {
        float time = 0;
        float duration = 0.5f; // Duration of the scale animation

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = EaseInOut(time, duration);
            block.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        block.transform.localScale = Vector3.one; // Ensure it ends at the desired scale
    }

    private float EaseInOut(float current, float total)
    {
        float x = current / total;
        return x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    IEnumerator MoveChunk()
    {
        yield return new WaitForSeconds(moveDelay);

        float time = 0;
        float duration = 1f; // Duration of the scale animation

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = EaseInOut(time, duration);
            transform.position = scale * targetPosition;
            yield return null;
        }

        //Destroy(gameObject);
        //block.transform.localScale = Vector3.one; // Ensure it ends at the desired scale
    }
}
