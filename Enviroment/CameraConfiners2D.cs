using Cinemachine;
using UnityEngine;

public class CameraConfiners2D : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner cameraConfiner;

    private void OnEnable()
    {
        Debug.Log("Carrega");
        cameraConfiner = FindObjectOfType<CinemachineConfiner>();
        cameraConfiner.m_BoundingShape2D = gameObject.GetComponent<PolygonCollider2D>();
    }
}
