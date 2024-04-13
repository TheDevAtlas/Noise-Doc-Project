using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PerlinNoisePlane : MonoBehaviour
{
    public int widthSegments = 10;
    public int lengthSegments = 10;
    public float noiseScale = 0.1f;
    public float heightMultiplier = 2f;
    public float speed = 1f;
    public Material planeMaterial;
    public int maxLayers = 5;
    public float layerBuildupTime = 7f;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private float startTime;

    void Start()
    {
        GetComponent<MeshRenderer>().material = planeMaterial;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreatePlane();
        startTime = Time.time;
    }

    void Update()
    {
        if (baseVertices == null)
            return;

        float timeSinceStart = Time.time - startTime;
        int currentLayerCount = Mathf.Clamp((int)((timeSinceStart - layerBuildupTime) / layerBuildupTime * maxLayers), 0, maxLayers);
        float time = Time.time * speed;

        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            float height = 0f;

            for (int layer = 0; layer <= currentLayerCount; layer++)
            {
                float layerScale = noiseScale * (1 + layer * 0.5f); // Increasing scale for higher layers
                float layerOffset = layer * 100f; // Offset to get different parts of the noise field
                height += Mathf.PerlinNoise((vertex.x + layerOffset) * layerScale + time,
                                            (vertex.z + layerOffset) * layerScale + time) * heightMultiplier / (layer + 1);
            }

            vertex.y = height;
            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals(); // To update flat shading correctly
    }

    void CreatePlane()
    {
        mesh.Clear();

        int[] triangles = new int[widthSegments * lengthSegments * 6];
        Vector3[] vertices = new Vector3[(widthSegments + 1) * (lengthSegments + 1) * 6]; // Expanded for flat shading
        Vector2[] uv = new Vector2[vertices.Length];
        int vertexIndex = 0, triangleIndex = 0;
        float halfWidth = widthSegments / 2f;
        float halfLength = lengthSegments / 2f;

        for (int z = 0; z < lengthSegments; z++)
        {
            for (int x = 0; x < widthSegments; x++)
            {
                Vector3 vertexOffset = new Vector3(x - halfWidth, 0, z - halfLength);
                int baseIndex = vertexIndex;

                vertices[vertexIndex] = vertexOffset;
                vertices[vertexIndex + 1] = vertexOffset + Vector3.forward;
                vertices[vertexIndex + 2] = vertexOffset + Vector3.right;
                vertices[vertexIndex + 3] = vertexOffset + Vector3.forward;
                vertices[vertexIndex + 4] = vertexOffset + Vector3.right + Vector3.forward;
                vertices[vertexIndex + 5] = vertexOffset + Vector3.right;

                uv[vertexIndex] = new Vector2((float)x / widthSegments, (float)z / lengthSegments);
                uv[vertexIndex + 1] = new Vector2((float)x / widthSegments, (float)(z + 1) / lengthSegments);
                uv[vertexIndex + 2] = new Vector2((float)(x + 1) / widthSegments, (float)z / lengthSegments);
                uv[vertexIndex + 3] = new Vector2((float)x / widthSegments, (float)(z + 1) / lengthSegments);
                uv[vertexIndex + 4] = new Vector2((float)(x + 1) / widthSegments, (float)(z + 1) / lengthSegments);
                uv[vertexIndex + 5] = new Vector2((float)(x + 1) / widthSegments, (float)z / lengthSegments);

                triangles[triangleIndex++] = baseIndex;
                triangles[triangleIndex++] = baseIndex + 1;
                triangles[triangleIndex++] = baseIndex + 2;
                triangles[triangleIndex++] = baseIndex + 3;
                triangles[triangleIndex++] = baseIndex + 4;
                triangles[triangleIndex++] = baseIndex + 5;

                vertexIndex += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals(); // For flat shading
        baseVertices = new Vector3[vertices.Length];
        vertices.CopyTo(baseVertices, 0);
    }
}
