using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowController : MonoBehaviour
{
    public bool isHollow;
    [SerializeField] private Material sourceMat;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Color matColor;

    private Material hollowMaterial;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (isHollow)
        {
            if (!mesh)
            {
                mesh = GetComponent<MeshRenderer>();
            }

            hollowMaterial = new Material(sourceMat);
            mesh.material = hollowMaterial;
            ChangeMatColor(matColor);
        }
    }

    /// <summary>
    /// Change the Color of material
    /// </summary>
    /// <param name="color"></param>
    public void ChangeMatColor(Color color)
    {
        if (isHollow)
        {
            //hollowMaterial.SetColor("_OutlineColor", color);
            hollowMaterial.color = color;
        }
    }
}
