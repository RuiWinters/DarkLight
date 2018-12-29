using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCtrl : MonoBehaviour {

    protected Animator m_Animator;

    public bool isActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
	}
	
    void OnAnimatorIK()
    {
        if (m_Animator)
        {
            if (isActive)
            {
                if (lookObj)
                {
                    m_Animator.SetLookAtWeight(1);
                    m_Animator.SetLookAtPosition(lookObj.position);
                }

                if (rightHandObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
            }
            else
            {
                m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                m_Animator.SetLookAtWeight(0);
            }
            
        }
    }
}
