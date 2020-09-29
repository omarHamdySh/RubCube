using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialAnimation : MonoBehaviour
{
    public enum AnimationType
    {
        Vertical,
        Horizontal
    }

    public GameObject animatedElement;
    public AnimationType animationType;
    public int direction = 1;
    // Start is called before the first frame update
    private void Start()
    {

        Vector3 position = transform.position;
        switch (animationType)
        {
            case AnimationType.Horizontal:
                animatedElement.transform.DOMove(new Vector3(position.x + 150 * direction, position.y, position.z), 1f).SetEase(Ease.InFlash).SetLoops(-1, LoopType.Yoyo).From(transform.position);
                break;
            case AnimationType.Vertical:
                animatedElement.transform.DOMove(new Vector3(position.x, position.y + 150 * direction, position.z), 1f).SetEase(Ease.InFlash).SetLoops(-1, LoopType.Yoyo).From(transform.position);
                break;
        }
    }

}
