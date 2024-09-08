using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterWaves : MonoBehaviour
{
    public int gridSize = 100; // 网格尺寸
    public float waveHeight = 1f; // 波浪高度
    public float waveFrequency = 1f; // 波浪频率
    public float waveSpeed = 1f; // 波浪速度

    private Mesh mesh;
    private Vector3[] vertices;

    void Start()
    {
        GenerateMesh(); // 创建高分辨率网格
    }

    void Update()
    {
        UpdateWave(); // 更新波浪效果
    }

    void GenerateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // 获取平面的大小（在Unity中，默认平面的大小是10x10）
        float sizeX = transform.localScale.x * 10;
        float sizeZ = transform.localScale.z * 10;

        // 计算网格的偏移量，使其中心位于(0, 0, 0)
        float halfSizeX = sizeX / 2f;
        float halfSizeZ = sizeZ / 2f;

        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        int[] triangles = new int[gridSize * gridSize * 6];

        for (int i = 0, y = 0; y <= gridSize; y++)
        {
            for (int x = 0; x <= gridSize; x++, i++)
            {
                // 将顶点位置缩放到平面的范围内，并减去半尺寸，使网格中心位于原点
                float posX = (x / (float)gridSize) * sizeX - halfSizeX;
                float posZ = (y / (float)gridSize) * sizeZ - halfSizeZ;
                vertices[i] = new Vector3(posX, 0, posZ);
            }
        }

        int tris = 0;
        int verts = 0;
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + gridSize + 1;
                triangles[tris + 2] = verts + 1;
                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + gridSize + 1;
                triangles[tris + 5] = verts + gridSize + 2;

                verts++;
                tris += 6;
            }
            verts++;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

void UpdateWave()
{
    float timeFactor = Time.time * waveSpeed; // 时间因子控制波浪速度

    for (int i = 0; i < vertices.Length; i++)
    {
        float x = vertices[i].x;
        float z = vertices[i].z;

        // 使用Perlin Noise增加波浪的随机性
        float perlinValue = Mathf.PerlinNoise(x * waveFrequency + timeFactor, z * waveFrequency + timeFactor);

        // 计算顶点的Y位置，使用Perlin Noise生成的值来替代单一的正弦波
        vertices[i].y = perlinValue * waveHeight;
    }

    mesh.vertices = vertices;
    mesh.RecalculateNormals(); // 更新法线，使光照效果正确
}

}
