using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Camera miniMapCamera;
    [SerializeField] Transform ghost;
    Transform followTarget;
    private bool isFollowTargetGhost = false;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector2 cameraBounds = new Vector2(70, 70);
    [SerializeField] float moveSpeed = 30;
    [SerializeField] float zoomSpeed = 12;

    
    private Vector3 firstTouchStartPos;
    private Vector3 firstTouchEndPos;


    private float currentCameraSize = 15;
    private float targetCameraSize;
    private void Awake()
    {
        GameObject.Find("MainTower").GetComponent<MainTower>().OnGameOver += () =>
        {
            enabled = false;
        };
        PLayerController.instance.OnActiveBuildingChanged += ((sender, e) =>
        {
            if (e.activeBuilding == null)
            {
                isFollowTargetGhost = false;
                transform.position = ghost.position;
                followTarget = transform;
                virtualCamera.Follow = followTarget;
            }
            else
            {
                isFollowTargetGhost = true;
                followTarget = ghost;
                virtualCamera.Follow = followTarget;
            }
        });
        mainCamera = Camera.main;
        followTarget = transform;
    }
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
        if (Input.touchCount == 1)
        {
            if (!isFollowTargetGhost)
            {
                Vector3 moveDirection = Vector3.zero;
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    firstTouchStartPos = mainCamera.ScreenToWorldPoint(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    firstTouchEndPos = mainCamera.ScreenToWorldPoint(touch.position);
                    if (Vector3.Distance(firstTouchStartPos, firstTouchEndPos) < 0.25f)
                    {
                        moveDirection = Vector3.zero;
                    }
                    else
                    {
                        moveDirection = (firstTouchStartPos - firstTouchEndPos).normalized;
                    }
                }

                Vector3 tempPosition = followTarget.position + moveDirection * moveSpeed * Time.deltaTime;
                CheckBorders(tempPosition);
            }
            else
            {
                CheckBorders(followTarget.position);
            }

        }
        
    }

    private void CameraZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            Vector3 firstTouchCurrentPos = firstTouch.position;
            Vector3 secondTouchCurrentPos = secondTouch.position;

            Vector3 firstTouchPrevPos = firstTouchCurrentPos - (Vector3)firstTouch.deltaPosition;
            Vector3 secondTouchPrevPos = secondTouchCurrentPos - (Vector3)secondTouch.deltaPosition;

            float previousDistance = Vector3.Magnitude(firstTouchPrevPos - secondTouchPrevPos);
            float currentDistance = Vector3.Magnitude(firstTouchCurrentPos - secondTouchCurrentPos);
            float distanceDelta = (currentDistance - previousDistance);

            currentCameraSize = virtualCamera.m_Lens.OrthographicSize;
            targetCameraSize = Mathf.Clamp(currentCameraSize - distanceDelta * Time.deltaTime, 8, 20);
            virtualCamera.m_Lens.OrthographicSize = targetCameraSize;
            miniMapCamera.orthographicSize = targetCameraSize;
            CheckBorders(followTarget.position);
        }
    }

    private void CheckBorders(Vector3 targetPos)
    {
        float xBorder = cameraBounds.x - currentCameraSize;
        if (targetPos.x > xBorder)
        {
            targetPos.x = xBorder;
        }
        else if (targetPos.x < -xBorder)
        {
            targetPos.x = -xBorder;
        }

        float yBorder = cameraBounds.y - currentCameraSize;
        if (targetPos.y > yBorder)
        {
            targetPos.y = yBorder;
        }
        else if (targetPos.y < -yBorder)
        {
            targetPos.y = -yBorder;
        }

        followTarget.position = targetPos;
        miniMapCamera.transform.position = mainCamera.transform.position;
    }
}
