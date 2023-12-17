using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide_UI : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float CameraDistance;
    [SerializeField] float Guide_Y;
    [SerializeField] RectTransform GuideTransform;

    public Transform[] PointsToBeGuided;
    public int ActualPoint;
    [SerializeField] Transform ObjectTransform;

    void Start()
    {

    }

    void Update()
    {
        float DistancePlayerObject = PlayerTransform.position.x - ObjectTransform.position.x;
        if (DistancePlayerObject < CameraDistance && Mathf.Abs(DistancePlayerObject)>CameraDistance)
        {           
           float nuevaPosicionX = 888f;
           Vector2 posicionActual = GuideTransform.anchoredPosition;
           posicionActual.x = nuevaPosicionX;
           GuideTransform.anchoredPosition = posicionActual;
            gameObject.GetComponent<Image>().enabled = true;
        }
        else if (DistancePlayerObject > CameraDistance && Mathf.Abs(DistancePlayerObject) > CameraDistance)
        {
            float nuevaPosicionX = -888f;
            Vector2 posicionActual = GuideTransform.anchoredPosition;
            posicionActual.x = nuevaPosicionX;
            GuideTransform.anchoredPosition = posicionActual;
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }

    public void GetGuidedPoints(Transform[] transforms)
    {
        Array.Clear(PointsToBeGuided, 0, PointsToBeGuided.Length);
        PointsToBeGuided = transforms;
    }
}
