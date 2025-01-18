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
        // ��l�ƥؼЦ�m
        targetPosition = transform.position;
        // �V CubeGestureListener ���U
        CubeGestureListener.RegisterFlower(this);
    }

    void OnDestroy()
    {
        // ����P���ɨ������U
        CubeGestureListener.UnregisterFlower(this);
    }

    void Update()
    {
        // �ϥ� SmoothDamp ��{�󥭷ƪ��u�ʲ���
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref currentVelocity,
            1f / smoothing
        );
    }

    public void Move(int direction)
    {
        // �p�G��V���Q���\�A�h����
        if (direction != AllowedSwipeDirection && AllowedSwipeDirection != 0)
        {
            return;
        }
        // �p��s���ؼЦ�m
        Vector3 newTargetPosition = targetPosition + (Vector3.right * direction * moveSpeed);

        // ����ʽd��
        newTargetPosition.x = Mathf.Clamp(
            newTargetPosition.x,
            -maxMovementRange,
            maxMovementRange
        );

        // ��s�ؼЦ�m
        targetPosition = newTargetPosition;
    }

}
