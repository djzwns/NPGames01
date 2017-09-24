using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CutSceneAnima : MonoBehaviour
{
    protected SkeletonAnimation skeletonAnimation;
    
    void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    protected void SetAnimation(string _animName, bool _loop)
    {
        skeletonAnimation.AnimationState.SetAnimation(1, _animName, _loop);
    }

    protected void AddAimation(string _animName, bool _loop)
    {
        skeletonAnimation.AnimationState.AddAnimation(1, _animName, _loop, 0);
    }
}
