using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInstancer : MonoBehaviour
{
    public GameObject grassPrefab;
    public Material grassMaterial;
    public int grassCount = 10; 
    public float areaSize = 1f; 

    void Start()
    {
        GameObject grassContainer = new GameObject("GrassContainer");

        // 计算每个网格单元的大小
        int gridCount = Mathf.CeilToInt(Mathf.Sqrt(grassCount));
        float cellSize = areaSize / gridCount;

        int placedGrass = 0;

        for (int x = 0; x < gridCount; x++)
        {
            for (int z = 0; z < gridCount; z++)
            {
                if (placedGrass >= grassCount)
                    break;

                // 计算草的位置，加上随机偏移
                float posX = -areaSize / 2 + x * cellSize + Random.Range(0, cellSize);
                float posZ = -areaSize / 2 + z * cellSize + Random.Range(0, cellSize);

                GameObject grass = Instantiate(grassPrefab, grassContainer.transform);
                grass.transform.position = new Vector3(posX, 0, posZ);
                grass.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                MeshRenderer mr = grass.GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.material = grassMaterial;
                }

                placedGrass++;
            }
        }
    }
}
