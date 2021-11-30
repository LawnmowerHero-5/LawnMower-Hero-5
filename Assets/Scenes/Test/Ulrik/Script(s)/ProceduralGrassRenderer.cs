using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGrassRenderer : MonoBehaviour
{
    [Tooltip("A mesh to create grass from. A blade sprouts from the center of every triangle")] [SerializeField]
    private Mesh sourceMesh = default;

    [Tooltip("The grass geometry creating a compute shader")] [SerializeField]
    private ComputeShader grassComputeShader = default;

    [Tooltip("The material to render the grass mesh")] [SerializeField]
    private Material material = default;
    
    // The structure to send to the compute shader
    // This layout kind assures that the data is laid out sequentiually
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct SourceVertex
    {
        public Vector3 position;
    }
} 
