using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MeshGenerator
{
    public static Mesh GenerateMesh(int width, int length, HexArray hexArray)
    {
        int ridges = CalculateRidges();
        Vector3[] vertices = new Vector3[(width * length + length / 2) * 6 + ridges * 2];
        int[] triangles = new int[vertices.Length * 2];
        Vector2[] uv = new Vector2[vertices.Length];
        Color[] colors = new Color[vertices.Length];
        int i = 0, vertexIndex = 0, triangleIndex = 0;
        for (float y = 0; i < length; y += HexArray.HEX_RADIUS + HexArray.HALF_HEX_RADIUS, i++)
        {
            int j = 0;
            for (float x = i % 2 == 0 ? 0f : -0.5f; x < width; x++, vertexIndex += 6, triangleIndex += 12, j++)
            {
                Hex hex = hexArray[j, i];

                // Create vertices
                vertices[vertexIndex] = new Vector3(x - 0.5f, hex.position.y, y - HexArray.HALF_HEX_RADIUS);
                vertices[vertexIndex + 1] = new Vector3(x - 0.5f, hex.position.y, y + HexArray.HALF_HEX_RADIUS);
                vertices[vertexIndex + 2] = new Vector3(x, hex.position.y, y + HexArray.HEX_RADIUS);
                vertices[vertexIndex + 3] = new Vector3(x + 0.5f, hex.position.y, y + HexArray.HALF_HEX_RADIUS);
                vertices[vertexIndex + 4] = new Vector3(x + 0.5f, hex.position.y, y - HexArray.HALF_HEX_RADIUS);
                vertices[vertexIndex + 5] = new Vector3(x, hex.position.y, y - HexArray.HEX_RADIUS);

                // Create uvs
                uv[vertexIndex] = new Vector2(x - 0.5f, y - HexArray.HALF_HEX_RADIUS);
                uv[vertexIndex + 1] = new Vector2(x - 0.5f, y + HexArray.HALF_HEX_RADIUS);
                uv[vertexIndex + 2] = new Vector2(x, y + HexArray.HEX_RADIUS);
                uv[vertexIndex + 3] = new Vector2(x + 0.5f, y + HexArray.HALF_HEX_RADIUS);
                uv[vertexIndex + 4] = new Vector2(x + 0.5f, y - HexArray.HALF_HEX_RADIUS);
                uv[vertexIndex + 5] = new Vector2(x, y - HexArray.HEX_RADIUS);

                // Create vertex colours
                colors[vertexIndex] = hex.color;
                colors[vertexIndex + 1] = hex.color;
                colors[vertexIndex + 2] = hex.color;
                colors[vertexIndex + 3] = hex.color;
                colors[vertexIndex + 4] = hex.color;
                colors[vertexIndex + 5] = hex.color;

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
}
