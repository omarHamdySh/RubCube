using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowController : MonoBehaviour
{
    [SerializeField] private Material trueMaterial;
    [SerializeField] private Material falseMaterial;
    [SerializeField] private MeshRenderer mesh;
    public bool isPopulatable;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //if (isHollow)
        //{
        //    if (!mesh)
        //    {
        //        mesh = GetComponent<MeshRenderer>();
        //    }

        //    hollowMaterial = new Material(sourceMat);
        //    mesh.material = hollowMaterial;
        //    ChangeMatColor(falseColor);
        //}
    }


    public void changeColorToFalse()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = falseMaterial;
        mesh.sharedMaterial.DOFade(0.2f, "_ColorTint", 0.5f).SetLoops(-1, LoopType.Yoyo).From(.05f);

    }

    public void changeColorToTrue()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = trueMaterial;
        mesh.sharedMaterial.DOFade(0.2f, "_ColorTint", 0.5f).SetLoops(-1, LoopType.Yoyo).From(.05f);
    }
}
