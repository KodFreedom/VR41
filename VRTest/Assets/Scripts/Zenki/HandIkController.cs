using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIkController : MonoBehaviour
{
    [SerializeField] Transform kLeftHandIKHandler = null;
    [SerializeField] Transform kRightHandIKHandler = null;

    private Animator animator_ = null;

    // Use this for initialization
    void Start ()
    {
        animator_ = GetComponent<Animator>();
	}

    // IK処理
    private void OnAnimatorIK()
    {
        UpdateLookIK();
        UpdateHandIK();
    }

    private void UpdateLookIK()
    {
        animator_.SetLookAtWeight(1f);
        var main_camera = Camera.main.transform;
        animator_.SetLookAtPosition(main_camera.position + main_camera.forward * 10f);
    }

    private void UpdateHandIK()
    {
        animator_.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator_.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        animator_.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator_.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        if (kLeftHandIKHandler)
        {
            animator_.SetIKPosition(AvatarIKGoal.LeftHand, kLeftHandIKHandler.position);
            animator_.SetIKRotation(AvatarIKGoal.LeftHand, kLeftHandIKHandler.rotation);
        }

        if(kRightHandIKHandler)
        {
            animator_.SetIKPosition(AvatarIKGoal.RightHand, kRightHandIKHandler.position);
            animator_.SetIKRotation(AvatarIKGoal.RightHand, kRightHandIKHandler.rotation);
        }
    }
}
