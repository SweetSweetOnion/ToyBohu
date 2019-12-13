using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veilleuse : MonoBehaviour
{

    [SerializeField] private Material materialUnlit;
    [SerializeField] private Material materialLit;

    public void ActivateLight()
    {
        GetComponent<MeshRenderer>().material = materialLit;
    }

    public void DeactivateLight()
    {
        GetComponent<MeshRenderer>().material = materialUnlit;
    }
}
