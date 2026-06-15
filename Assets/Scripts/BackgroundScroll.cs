using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform player;
    public Transform mainCamera;

    public float scrollSpeed = 0.1f;

    Material backgroundMaterial;
    float lastCameraX;

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

        lastCameraX = mainCamera.position.x;
    }

    void LateUpdate()
    {
        if (player == null || mainCamera == null) { return; }


        float cameraMovementX = mainCamera.position.x - lastCameraX;

        transform.position = new Vector3(mainCamera.position.x, transform.position.y, transform.position.z);

        Vector2 offset = backgroundMaterial.GetTextureOffset("_BaseMap");
        offset.x += cameraMovementX * scrollSpeed;
        backgroundMaterial.SetTextureOffset("_BaseMap", offset);

        lastCameraX = mainCamera.position.x;
    }
}