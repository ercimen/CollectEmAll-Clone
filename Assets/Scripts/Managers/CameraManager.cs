using UnityEngine;

public class CameraManager : SingletonBase<CameraManager>
{
    [SerializeField] private float sizeOffset = 1f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void AutoPositionCamera(Vector2 gridSize)
    {         
        float aspectRatio = (float)Screen.width / Screen.height;
        float cameraSize = Mathf.Max(gridSize.x / 2 - 0.5f, gridSize.y / 2 / aspectRatio);

        mainCamera.transform.position = new Vector3(gridSize.x / 2 - 0.5f, gridSize.y / 2, -10f);
        mainCamera.orthographicSize = cameraSize+sizeOffset;
    }
}
