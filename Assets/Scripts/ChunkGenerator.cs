using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ChunkGenerator : MonoBehaviour
{
    public int chunkSize = 16;       // Size of Chunk          //
    public Block blockPrefab;        // Block Prefab           //
    public Material[] blockTextures; // Default is Grass Block //

    public float totalTimeAnim = 10f;
    public float noiseScale = 0.1f;
    public float offset;

    public int caveCutoff = 35;

    int[][] height;

    int[][][] cave;

    private void Start()
    {
        height = new int[chunkSize][];
        for (int i = 0; i < chunkSize; i++)
        {
            height[i] = new int[chunkSize];
            for (int j = 0; j < chunkSize; j++)
            {
                height[i][j] = CalculateHeight(i, j);
            }
        }

        cave = new int[chunkSize][][];
        for(int i = 0; i < chunkSize; ++i)
        {
            cave[i] = new int[chunkSize][];
            for(int j = 0;j < chunkSize; ++j)
            {
                cave[i][j] = new int[chunkSize];
                for(int k = 0; k < chunkSize; ++k)
                {
                    cave[i][j][k] = (CalculateHeight(i, k) + CalculateHeight(j,k) + CalculateHeight(i, k) / 3);
                }
            }
        }

        StartCoroutine(GenerateChunk());

        
    }

    int CalculateHeight(int x, int z)
    {
        float xCoord = x * noiseScale;
        float zCoord = z * noiseScale;
        float sample = Mathf.PerlinNoise(xCoord + offset/100f, zCoord + offset/100f);
        return 8 + Mathf.FloorToInt(sample * 10f);
    }

    IEnumerator GenerateChunk()
    {
        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    // Instantiate the block at (x, y, z) and set scale //
                    if(y < height[x][z] && cave[z][x][y] < caveCutoff)
                    {
                        Block cube = Instantiate(blockPrefab, new Vector3(x - 7.5f, y, z - 7.5f), Quaternion.identity);
                        StartCoroutine(ScaleCube(cube));

                        // Set The Material //
                        StartCoroutine(SetMaterial(cube, new Vector3(x, y, z)));
                    }
                    

                    // Wait for a short time before generating the next block //
                    
                }
                yield return new WaitForSeconds(0.01f);
            }
            
        }
    }

    private IEnumerator ScaleCube(Block cube)
    {
        float time = 0;
        float duration = 0.5f; // Duration of the scale animation

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = EaseInOut(time, duration);
            cube.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        cube.transform.localScale = Vector3.one; // Ensure it ends at the desired scale
    }

    private float EaseInOut(float current, float total)
    {
        float x = current / total;
        return x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    private IEnumerator SetMaterial(Block cube, Vector3 pos)
    {
        int matIndex = -1;

        if(pos.y == 0)
        {
            matIndex = 0; // Bedrock
        }
        else 
        if(pos.y == 1)
        {
            if(Random.Range(0f,100f) > 50f)
            {
                matIndex = 0; // Bedrock
            }
            else
            {
                matIndex = 1; // Deepslate

                if(Random.Range(0f, 100f) < 2f)
                {
                    matIndex = 4;
                }
            }
        }
        else
        if(pos.y == 2)
        {
            if (Random.Range(0f, 100f) > 85f)
            {
                matIndex = 0; // Bedrock
            }
            else
            {
                matIndex = 1; // Deepslate

                if (Random.Range(0f, 100f) < 1f)
                {
                    matIndex = 4;
                }
            }
        }
        else
        if(pos.y >= 3 && pos.y <= 5)
        {
            matIndex = 1; // Deepslate

            if (Random.Range(0f, 100f) < 0.5f)
            {
                matIndex = 4;
            }
        }
        else
        if(pos.y == 6)
        {
            if (Random.Range(0f, 100f) > 50f)
            {
                matIndex = 1; // Deepslate
            }
            else
            {
                matIndex = 2; // Stone

                if (Random.Range(0f, 100f) < 5f)
                {
                    matIndex = 5;
                }
                if (Random.Range(0f, 100f) < 8f)
                {
                    matIndex = 6;
                }
            }
        }
        else
        if (pos.y == 7)
        {
            if (Random.Range(0f, 100f) > 85f)
            {
                matIndex = 1; // Deepslate
            }
            else
            {
                matIndex = 2; // Stone
                if (Random.Range(0f, 100f) < 8f)
                {
                    matIndex = 5;
                }
                if (Random.Range(0f, 100f) < 5f)
                {
                    matIndex = 6;
                }
            }
        }
        else
        if (pos.y >= 8 && pos.y <= 10)
        {
            matIndex = 2; // Stone
            if (Random.Range(0f, 100f) < 8f)
            {
                matIndex = 5;
            }
            if (Random.Range(0f, 100f) < 5f)
            {
                matIndex = 6;
            }
        }
        else
        if (pos.y == 11)
        {
            if (Random.Range(0f, 100f) > 50f)
            {
                matIndex = 2; // Stone
            }
            else
            {
                matIndex = 3; // Dire
            }
        }
        else
        if (pos.y == 12)
        {
            if (Random.Range(0f, 100f) > 85f)
            {
                matIndex = 2; // Stone
            }
            else
            {
                matIndex = 3; // Dirt
            }
        }
        else
        if (pos.y > 10 && pos.y < height[(int)pos.x][(int)pos.z]-1)
        {
            matIndex = 3; // Dirt
        }

        if(pos.y == height[(int)pos.x][(int)pos.z] - 1)
        {
            matIndex = -1;

            if(pos.y < 12)
            {
                matIndex = 7;
            }
        }

        yield return null;

        if(matIndex != -1)
        {
            foreach (Renderer r in cube.sides)
            {
                r.material = blockTextures[matIndex];
            }
        }
        
    }
}
