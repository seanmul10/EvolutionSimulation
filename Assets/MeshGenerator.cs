using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MeshGenerator
{
    public static Mesh GenerateMesh(int width, int length, HexArray hexArray, float edgeHeight)
    {
        int ridges = CalculateRidges(hexArray);
        Vector3[] vertices = new Vector3[(width * length + length / 2) * 6 + ridges * 4];
        int[] triangles = new int[(width * length + length / 2) * 12 + (ridges * 6)];
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

                int ridgeVertices = 0;
                int ridgeTriangles = 0;

                Hex neighbour = hexArray.GetNeighbour(j, i, HexDirection.EAST);
                if (neighbour.position.y < hex.position.y)
                {
                    vertices[vertexIndex + 6 + ridgeVertices] = new Vector3(x - 0.5f, hex.position.y, y - HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 7 + ridgeVertices] = new Vector3(x - 0.5f, hex.position.y, y + HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 8 + ridgeVertices] = new Vector3(x - 0.5f, neighbour.position.y, y - HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 9 + ridgeVertices] = new Vector3(x - 0.5f, neighbour.position.y, y + HexArray.HALF_HEX_RADIUS);

                    uv[vertexIndex + 6 + ridgeVertices] = new Vector2(x - 0.5f, y - HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 7 + ridgeVertices] = new Vector2(x - 0.5f, y + HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 8 + ridgeVertices] = new Vector2(x - 0.5f, y - HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 9 + ridgeVertices] = new Vector2(x - 0.5f, y + HexArray.HALF_HEX_RADIUS);

                    colors[vertexIndex + 6 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 7 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 8 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 9 + ridgeVertices] = hex.color;

                    triangles[triangleIndex + 12 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;
                    triangles[triangleIndex + 13 + ridgeTriangles] = vertexIndex + 8 + ridgeVertices;
                    triangles[triangleIndex + 14 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;

                    triangles[triangleIndex + 15 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;
                    triangles[triangleIndex + 16 + ridgeTriangles] = vertexIndex + 7 + ridgeVertices;
                    triangles[triangleIndex + 17 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;

                    ridgeVertices += 4;
                    ridgeTriangles += 6;
                }
                neighbour = hexArray.GetNeighbour(j, i, HexDirection.NORTHEAST);
                if (neighbour.position.y < hex.position.y)
                {
                    vertices[vertexIndex + 6 + ridgeVertices] = new Vector3(x - 0.5f, hex.position.y, y + HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 7 + ridgeVertices] = new Vector3(x, hex.position.y, y + HexArray.HEX_RADIUS);
                    vertices[vertexIndex + 8 + ridgeVertices] = new Vector3(x - 0.5f, neighbour.position.y, y + HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 9 + ridgeVertices] = new Vector3(x, neighbour.position.y, y + HexArray.HEX_RADIUS);

                    uv[vertexIndex + 6 + ridgeVertices] = new Vector2(x - 0.5f, y + HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 7 + ridgeVertices] = new Vector2(x, y + HexArray.HEX_RADIUS);
                    uv[vertexIndex + 8 + ridgeVertices] = new Vector2(x - 0.5f, y + HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 9 + ridgeVertices] = new Vector2(x, y + HexArray.HEX_RADIUS);

                    colors[vertexIndex + 6 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 7 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 8 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 9 + ridgeVertices] = hex.color;

                    triangles[triangleIndex + 12 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;
                    triangles[triangleIndex + 13 + ridgeTriangles] = vertexIndex + 8 + ridgeVertices;
                    triangles[triangleIndex + 14 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;

                    triangles[triangleIndex + 15 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;
                    triangles[triangleIndex + 16 + ridgeTriangles] = vertexIndex + 7 + ridgeVertices;
                    triangles[triangleIndex + 17 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;

                    ridgeVertices += 4;
                    ridgeTriangles += 6;
                }
                neighbour = hexArray.GetNeighbour(j, i, HexDirection.NORTHWEST);
                if (neighbour.position.y < hex.position.y)
                {
                    vertices[vertexIndex + 6 + ridgeVertices] = new Vector3(x, hex.position.y, y + HexArray.HEX_RADIUS);
                    vertices[vertexIndex + 7 + ridgeVertices] = new Vector3(x + 0.5f, hex.position.y, y + HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 8 + ridgeVertices] = new Vector3(x, neighbour.position.y, y + HexArray.HEX_RADIUS);
                    vertices[vertexIndex + 9 + ridgeVertices] = new Vector3(x + 0.5f, neighbour.position.y, y + HexArray.HALF_HEX_RADIUS);

                    uv[vertexIndex + 6 + ridgeVertices] = new Vector2(x, y + HexArray.HEX_RADIUS);
                    uv[vertexIndex + 7 + ridgeVertices] = new Vector2(x + 0.5f, y + HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 8 + ridgeVertices] = new Vector2(x, y + HexArray.HEX_RADIUS);
                    uv[vertexIndex + 9 + ridgeVertices] = new Vector2(x + 0.5f, y + HexArray.HALF_HEX_RADIUS);

                    colors[vertexIndex + 6 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 7 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 8 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 9 + ridgeVertices] = hex.color;

                    triangles[triangleIndex + 12 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;
                    triangles[triangleIndex + 13 + ridgeTriangles] = vertexIndex + 8 + ridgeVertices;
                    triangles[triangleIndex + 14 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;

                    triangles[triangleIndex + 15 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;
                    triangles[triangleIndex + 16 + ridgeTriangles] = vertexIndex + 7 + ridgeVertices;
                    triangles[triangleIndex + 17 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;

                    ridgeVertices += 4;
                    ridgeTriangles += 6;
                }
                neighbour = hexArray.GetNeighbour(j, i, HexDirection.WEST);
                if (neighbour.position.y < hex.position.y)
                {
                    vertices[vertexIndex + 6 + ridgeVertices] = new Vector3(x + 0.5f, hex.position.y, y + HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 7 + ridgeVertices] = new Vector3(x + 0.5f, hex.position.y, y - HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 8 + ridgeVertices] = new Vector3(x + 0.5f, neighbour.position.y, y + HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 9 + ridgeVertices] = new Vector3(x + 0.5f, neighbour.position.y, y - HexArray.HALF_HEX_RADIUS);

                    uv[vertexIndex + 6 + ridgeVertices] = new Vector2(x + 0.5f, y + HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 7 + ridgeVertices] = new Vector2(x + 0.5f, y - HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 8 + ridgeVertices] = new Vector2(x + 0.5f, y + HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 9 + ridgeVertices] = new Vector2(x + 0.5f, y - HexArray.HALF_HEX_RADIUS);

                    colors[vertexIndex + 6 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 7 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 8 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 9 + ridgeVertices] = hex.color;

                    triangles[triangleIndex + 12 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;
                    triangles[triangleIndex + 13 + ridgeTriangles] = vertexIndex + 8 + ridgeVertices;
                    triangles[triangleIndex + 14 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;

                    triangles[triangleIndex + 15 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;
                    triangles[triangleIndex + 16 + ridgeTriangles] = vertexIndex + 7 + ridgeVertices;
                    triangles[triangleIndex + 17 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;

                    ridgeVertices += 4;
                    ridgeTriangles += 6;
                }
                neighbour = hexArray.GetNeighbour(j, i, HexDirection.SOUTHWEST);
                if (neighbour.position.y < hex.position.y)
                {
                    vertices[vertexIndex + 6 + ridgeVertices] = new Vector3(x + 0.5f, hex.position.y, y - HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 7 + ridgeVertices] = new Vector3(x, hex.position.y, y - HexArray.HEX_RADIUS);
                    vertices[vertexIndex + 8 + ridgeVertices] = new Vector3(x + 0.5f, neighbour.position.y, y - HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 9 + ridgeVertices] = new Vector3(x, neighbour.position.y, y - HexArray.HEX_RADIUS);

                    uv[vertexIndex + 6 + ridgeVertices] = new Vector2(x + 0.5f, y - HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 7 + ridgeVertices] = new Vector2(x, y - HexArray.HEX_RADIUS);
                    uv[vertexIndex + 8 + ridgeVertices] = new Vector2(x + 0.5f, y - HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 9 + ridgeVertices] = new Vector2(x, y - HexArray.HEX_RADIUS);

                    colors[vertexIndex + 6 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 7 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 8 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 9 + ridgeVertices] = hex.color;

                    triangles[triangleIndex + 12 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;
                    triangles[triangleIndex + 13 + ridgeTriangles] = vertexIndex + 8 + ridgeVertices;
                    triangles[triangleIndex + 14 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;

                    triangles[triangleIndex + 15 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;
                    triangles[triangleIndex + 16 + ridgeTriangles] = vertexIndex + 7 + ridgeVertices;
                    triangles[triangleIndex + 17 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;

                    ridgeVertices += 4;
                    ridgeTriangles += 6;
                }
                neighbour = hexArray.GetNeighbour(j, i, HexDirection.SOUTHEAST);
                if (neighbour.position.y < hex.position.y)
                {
                    vertices[vertexIndex + 6 + ridgeVertices] = new Vector3(x, hex.position.y, y - HexArray.HEX_RADIUS);
                    vertices[vertexIndex + 7 + ridgeVertices] = new Vector3(x - 0.5f, hex.position.y, y - HexArray.HALF_HEX_RADIUS);
                    vertices[vertexIndex + 8 + ridgeVertices] = new Vector3(x, neighbour.position.y, y - HexArray.HEX_RADIUS);
                    vertices[vertexIndex + 9 + ridgeVertices] = new Vector3(x - 0.5f, neighbour.position.y, y - HexArray.HALF_HEX_RADIUS);

                    uv[vertexIndex + 6 + ridgeVertices] = new Vector2(x, y - HexArray.HEX_RADIUS);
                    uv[vertexIndex + 7 + ridgeVertices] = new Vector2(x - 0.5f, y - HexArray.HALF_HEX_RADIUS);
                    uv[vertexIndex + 8 + ridgeVertices] = new Vector2(x, y - HexArray.HEX_RADIUS);
                    uv[vertexIndex + 9 + ridgeVertices] = new Vector2(x - 0.5f, y - HexArray.HALF_HEX_RADIUS);

                    colors[vertexIndex + 6 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 7 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 8 + ridgeVertices] = hex.color;
                    colors[vertexIndex + 9 + ridgeVertices] = hex.color;

                    triangles[triangleIndex + 12 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;
                    triangles[triangleIndex + 13 + ridgeTriangles] = vertexIndex + 8 + ridgeVertices;
                    triangles[triangleIndex + 14 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;

                    triangles[triangleIndex + 15 + ridgeTriangles] = vertexIndex + 9 + ridgeVertices;
                    triangles[triangleIndex + 16 + ridgeTriangles] = vertexIndex + 7 + ridgeVertices;
                    triangles[triangleIndex + 17 + ridgeTriangles] = vertexIndex + 6 + ridgeVertices;

                    ridgeVertices += 4;
                    ridgeTriangles += 6;
                }
                vertexIndex += ridgeVertices;
                triangleIndex += ridgeTriangles;
            }
        }
        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uv,
            colors = colors
        };

        mesh.RecalculateNormals();
        return mesh;
    }

    static int CalculateRidges(HexArray hexArray)
    {
        int ridges = 0;
        for (int i = 0; i < hexArray.Count; i++)
        {
            Hex neighbour = hexArray.GetNeighbour(hexArray.GetHexIndex(hexArray[i]), HexDirection.EAST);
            if (neighbour.position.y < hexArray[i].position.y)
                ridges++;
            neighbour = hexArray.GetNeighbour(hexArray.GetHexIndex(hexArray[i]), HexDirection.NORTHEAST);
            if (neighbour.position.y < hexArray[i].position.y)
                ridges++;
            neighbour = hexArray.GetNeighbour(hexArray.GetHexIndex(hexArray[i]), HexDirection.NORTHWEST);
            if (neighbour.position.y < hexArray[i].position.y)
                ridges++;
            neighbour = hexArray.GetNeighbour(hexArray.GetHexIndex(hexArray[i]), HexDirection.WEST);
            if (neighbour.position.y < hexArray[i].position.y)
                ridges++;
            neighbour = hexArray.GetNeighbour(hexArray.GetHexIndex(hexArray[i]), HexDirection.SOUTHWEST);
            if (neighbour.position.y < hexArray[i].position.y)
                ridges++;
            neighbour = hexArray.GetNeighbour(hexArray.GetHexIndex(hexArray[i]), HexDirection.SOUTHEAST);
            if (neighbour.position.y < hexArray[i].position.y)
                ridges++;
        }
        return ridges;
    }
}
