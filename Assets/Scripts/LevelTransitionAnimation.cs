using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionAnimation : MonoBehaviour
{
    public Animator transitionAnimation;
    
    public void LoadLevel()
    {
        transitionAnimation.SetTrigger("End");
    }   
    
    public void OnFadeComplete()
    {
        //transitionAnimation.SetTrigger("Start");
    }
}
