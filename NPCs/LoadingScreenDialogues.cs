using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenDialogues : MonoBehaviour
{
    [SerializeField] string[] lines;
    [SerializeField] string lineToWrite;

    void Start()
    {
        lineToWrite=lines[Random.Range(0, lines.Length)];
    }

}
