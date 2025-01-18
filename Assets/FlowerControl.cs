using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerControl : MonoBehaviour
{
    [Tooltip("Speed of horizontal movement")]
    public float moveSpeed = 5f;

    [Tooltip("Maximum horizontal movement range")]
    public float maxMovementRange = 5f;

    [Tooltip("Smoothing factor for movement")]
    public float smoothing = 5f;

    private Vector3 targetPosition;
    private Vector3 currentVelocity = Vector3.zero;

    public int AllowedSwipeDirection { get; set; } = 0;

    void Start()
    {
        // 初始化目標位置
        targetPosition = transform.position;
        // 向 CubeGestureListener 註冊
        CubeGestureListener.RegisterFlower(this);
    }

    void OnDestroy()
    {
        // 物件銷毀時取消註冊
        CubeGestureListener.UnregisterFlower(this);
    }

    void Update()
    {
        // 使用 SmoothDamp 實現更平滑的線性移動
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            1f / smoothing
        );
    }

    public void Move(int direction)
    {
        // 如果方向不被允許，則忽略
        if (direction != AllowedSwipeDirection && AllowedSwipeDirection != 0)
        {
            return;
        }
        // 計算新的目標位置
        Vector3 newTargetPosition = targetPosition + (Vector3.right * direction * moveSpeed);

        // 限制移動範圍
        newTargetPosition.x = Mathf.Clamp(
            newTargetPosition.x,
            -maxMovementRange,
            maxMovementRange
        );

        // 更新目標位置
        targetPosition = newTargetPosition;
    }

}
