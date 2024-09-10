using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInstancer : MonoBehaviour
{
    public GameObject grassPrefab; // 草的预制件
    public Material grassMaterial; // 草的材质
    public int grassCount = 10; // 草的数量
    public float areaSize = 1; // 草的分布范围大小

    void Start()
    {
        // 创建一个 GameObject 来容纳所有的草模型
        GameObject grassContainer = new GameObject("GrassContainer");

        // 生成指定数量的草
        for (int i = 0; i < grassCount; i++)
        {
            // 实例化草的预制件
            GameObject grass = Instantiate(grassPrefab, grassContainer.transform);
            grass.transform.position = new Vector3(
                Random.Range(-areaSize / 2, areaSize / 2),
                0, 
                Random.Range(-areaSize / 2, areaSize / 2)
            );

            // 随机旋转草
            grass.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            // 设置材质
            MeshRenderer mr = grass.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                mr.material = grassMaterial;
            }
        }
    }
}
