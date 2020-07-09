using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAnimation : MonoBehaviour
{
    private Transform brickTransform;
    private SpriteRenderer brickSpriteRenderer;
    private float animationDuration;
    private Ease animationEase = Ease.InOutElastic;
    float minValueDuration = 1f, maxValueDuration = 2f;
    float minValuePosition = 0, maxValuePosition = 3f;

    private void Awake()
    {
        brickTransform = GetComponent<Transform>();
        brickSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        MoveSquares();
        ChangeColour();        
    }
    
    private void MoveSquares()
    {
        brickTransform
            .DOMoveY(this.transform.position.y, Random.Range(minValueDuration, maxValueDuration))
            .SetEase(animationEase)
            .From(10);
    }

    private void ChangeColour()
    {
        brickSpriteRenderer.
            DOColor(Color.black, 0f)
            .OnComplete(ChangeAlpha);
    }

    Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), 1f, 1f, 1f);
    }
    
    private void ChangeAlpha()
    {
        brickSpriteRenderer.DOFade(0, 0f);
        brickSpriteRenderer.DOFade(1f, 2f);
    }
}
