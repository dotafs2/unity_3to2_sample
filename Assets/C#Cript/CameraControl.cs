using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 10.0f;  // 相机移动速度
    public float lookSpeed = 2.0f;   // 相机旋转速度
    public float lookXLimit = 80.0f; // 垂直旋转角度限制

    private float rotationX = 0.0f;  // 当前X轴旋转角度

    void Start()
    {
        // 隐藏并锁定鼠标光标到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 鼠标旋转相机
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); // 限制垂直旋转角度

        float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * lookSpeed;

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        // 键盘移动相机
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // 左右移动 (A/D)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // 前后移动 (W/S)
        float moveY = 0;

        // 使用Shift和Space键向上和向下移动相机
        if (Input.GetKey(KeyCode.LeftShift)) moveY = -moveSpeed * Time.deltaTime; // 向下移动
        if (Input.GetKey(KeyCode.Space)) moveY = moveSpeed * Time.deltaTime; // 向上移动

        // 移动相机
        transform.Translate(new Vector3(moveX, moveY, moveZ));
    }
}
