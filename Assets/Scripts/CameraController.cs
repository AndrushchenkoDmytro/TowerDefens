using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float moveSpeed = 20;
    [SerializeField] float zoomSpeed = 10;
    private float currentCameraSize = 15;
    private float targetCameraSize;

    private void Start()
    {
        currentCameraSize = virtualCamera.m_Lens.OrthographicSize;
        targetCameraSize = currentCameraSize;
    }

    void Update()
    {
        CameraMovement();
        CameraZoom();
    }

    private void CameraMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(x, y).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void CameraZoom()
    {
        targetCameraSize -= Input.mouseScrollDelta.y;
        targetCameraSize = Mathf.Clamp(targetCameraSize, 8, 20);
        currentCameraSize = Mathf.Lerp(currentCameraSize, targetCameraSize, Time.deltaTime * zoomSpeed);
        virtualCamera.m_Lens.OrthographicSize = currentCameraSize;
    }
}
