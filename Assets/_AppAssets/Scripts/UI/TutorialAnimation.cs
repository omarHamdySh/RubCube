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
    public float easeValue = 150;
    // Start is called before the first frame update
    private void Start()
    {

        Vector2 position = transform.localPosition;
        switch (animationType)
        {
            case AnimationType.Horizontal:
                animatedElement.transform.DOLocalMoveX(position.x + easeValue * direction, 1f).SetEase(Ease.InFlash).SetLoops(-1, LoopType.Yoyo).From(transform.localPosition);
                break;
            case AnimationType.Vertical:
                animatedElement.transform.DOLocalMoveY(position.y + easeValue * direction, 1f).SetEase(Ease.InFlash).SetLoops(-1, LoopType.Yoyo).From(transform.localPosition);
                break;
        }
    }

}
