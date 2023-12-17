using UnityEngine;

public class ParallaxEfect : MonoBehaviour
{
    [SerializeField] private float ParallaxMultiplier;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float XOffset;
    Vector3 previousCameraPosition;


    private void Start()
    {
        if (cameraTransform==null)
        {
            cameraTransform = FindObjectOfType<Camera>().GetComponent<Transform>();
        }

    }
    void FixedUpdate()
    {

        float deltaX = ((cameraTransform.position.x - previousCameraPosition.x) * ParallaxMultiplier);
        transform.Translate(new Vector3(deltaX,0, 0));
        previousCameraPosition= cameraTransform.position;

    }

}
