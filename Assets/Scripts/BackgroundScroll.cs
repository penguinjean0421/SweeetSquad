using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform player;
    public Transform mainCamera;

    [Header("Scroll Speed")]
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.1f;

    Material backgroundMaterial;

    Vector2 lastCameraPos;

    void Awake()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        lastCameraPos = new Vector2(mainCamera.position.x, mainCamera.position.y);
    }

    void LateUpdate()
    {
        if (mainCamera == null) { return; }

        transform.position = new Vector3(mainCamera.position.x, mainCamera.position.y, transform.position.z);


        float cameraMovementX = mainCamera.position.x - lastCameraPos.x;
        float cameraMovementY = mainCamera.position.y - lastCameraPos.y;

        Vector2 offset = backgroundMaterial.GetTextureOffset("_BaseMap");

        offset.x += cameraMovementX * scrollSpeedX;
        offset.y += cameraMovementY * scrollSpeedY;

        backgroundMaterial.SetTextureOffset("_BaseMap", offset);

        lastCameraPos = new Vector2(mainCamera.position.x, mainCamera.position.y);
    }
}