using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MeshGenerator
{
    public static float HEX_RADIUS = 0.5773502691896257645092f;
    public static float HALF_HEX_RADIUS = 0.5773502691896257645092f / 2;

    public static Mesh GenerateMesh(int width, int length, float[,] noiseMap, TerrainColorData terrainColorData)
    {
        int ridges = CalculateRidges();
        Vector3[] vertices = new Vector3[(width * length + length / 2) * 6 + ridges * 2];
        int[] triangles = new int[vertices.Length * 2 + ridges * 2];
        Vector2[] uv = new Vector2[vertices.Length];
        Color[] colors = new Color[vertices.Length];
        int i = 0, vertexIndex = 0, triangleIndex = 0;
        for (float y = 0; i < length; y += HEX_RADIUS + HALF_HEX_RADIUS, i++)
        {
            int j = 0;
            for (float x = i % 2 == 0 ? 0f : -0.5f; x < width; x++, vertexIndex += 6, triangleIndex += 12, j++)
            {
                TerrainLevel terrainLevel = GetTerrainColour(Noise.AccessNoiseMapWithFloats(noiseMap, x + 1, y + 1), terrainColorData);
                float height = terrainLevel.height;
                Color color = terrainLevel.color;

                // Create vertices
                vertices[vertexIndex] = new Vector3(x - 0.5f, height, y - HALF_HEX_RADIUS);
                vertices[vertexIndex + 1] = new Vector3(x - 0.5f, height, y + HALF_HEX_RADIUS);
                vertices[vertexIndex + 2] = new Vector3(x, height, y + HEX_RADIUS);
                vertices[vertexIndex + 3] = new Vector3(x + 0.5f, height, y + HALF_HEX_RADIUS);
                vertices[vertexIndex + 4] = new Vector3(x + 0.5f, height, y - HALF_HEX_RADIUS);
                vertices[vertexIndex + 5] = new Vector3(x, height, y - HEX_RADIUS);

                // Create uvs
                uv[vertexIndex] = new Vector2(x - 0.5f, y - HALF_HEX_RADIUS);
                uv[vertexIndex + 1] = new Vector2(x - 0.5f, y + HALF_HEX_RADIUS);
                uv[vertexIndex + 2] = new Vector2(x, y + HEX_RADIUS);
                uv[vertexIndex + 3] = new Vector2(x + 0.5f, y + HALF_HEX_RADIUS);
                uv[vertexIndex + 4] = new Vector2(x + 0.5f, y - HALF_HEX_RADIUS);
                uv[vertexIndex + 5] = new Vector2(x, y - HEX_RADIUS);

                // Create vertex colours
                colors[vertexIndex] = color;
                colors[vertexIndex + 1] = color;
                colors[vertexIndex + 2] = color;
                colors[vertexIndex + 3] = color;
                colors[vertexIndex + 4] = color;
                colors[vertexIndex + 5] = color;

                // Create triangles
                triangles[triangleIndex] = vertexIndex;
                triangles[triangleIndex + 1] = vertexIndex + 2;
                triangles[triangleIndex + 2] = vertexIndex + 4;

                triangles[triangleIndex + 3] = vertexIndex;
                triangles[triangleIndex + 4] = vertexIndex + 1;
                triangles[triangleIndex + 5] = vertexIndex + 2;

                triangles[triangleIndex + 6] = vertexIndex + 2;
                triangles[triangleIndex + 7] = vertexIndex + 3;
                triangles[triangleIndex + 8] = vertexIndex + 4;

                triangles[triangleIndex + 9] = vertexIndex + 4;
                triangles[triangleIndex + 10] = vertexIndex + 5;
                triangles[triangleIndex + 11] = vertexIndex;
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.colors = colors;

        mesh.RecalculateNormals();
        return mesh;
    }

    static int CalculateRidges()
    {
        return 0;
    }

    static TerrainLevel GetTerrainColour(float terrainValue, TerrainColorData dataObject)
    {
        float currentRange = 0f;
        for (int i = 0; i < dataObject.terrainColors.Length; i++)
        {
            TerrainColor color = dataObject.terrainColors[i];
            if (terrainValue <= Mathf.Min(currentRange + color.range, 1f))
                return new TerrainLevel(color.heightOffset, Color.Lerp(color.minColor, color.maxColor, (terrainValue - currentRange) * (1f / color.range)));
            currentRange += color.range;
        }
        Debug.LogWarning("Error with terrain data. Value: " + terrainValue + " Current range: " + currentRange);
        return new TerrainLevel(0f, Color.magenta);
    }
}
