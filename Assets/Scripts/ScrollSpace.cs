using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSpace : MonoBehaviour
{
    public float parralax = 3.0f;
    private MeshRenderer meshRenderer;
    private Material material;
    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        double scale = 1.0;
        double width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        double height = Camera.main.orthographicSize * 2.0 * Screen.height / Screen.width;
        if (width > height) {
            scale = width;
        }
        else {
            scale = height;
        }
        transform.localScale = new Vector3((float)scale, (float)scale, 1.0f);
        Vector2 offset = material.mainTextureOffset;
        offset.x = transform.position.x / transform.localScale.x / parralax;
        offset.y = transform.position.y / transform.localScale.y / parralax;
        material.mainTextureOffset = offset;
    }
}
