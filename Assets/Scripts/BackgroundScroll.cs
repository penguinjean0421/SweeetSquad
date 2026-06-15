using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform player;
    public float scrollSpeed = 0.1f;
    Material backgroundMaterial;

    void Awake()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offset = player.position.x * scrollSpeed;

        backgroundMaterial.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}