using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialAnimation : MonoBehaviour
{
    public GameObject animatedElement;
    // Start is called before the first frame update
    void Start()
    {
        animatedElement.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 150, transform.position.z ), 1f).SetEase(Ease.InFlash).SetLoops(-1, LoopType.Yoyo).From(transform.position);
    }


}
