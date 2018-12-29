using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour {
    CharacterController myCharacterController;
    public float moveV, moveH;
    public float rH, rV;
    public float spd = 5;
    Animator myAnimator;

    public GameObject effect;

    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        rH = Input.GetAxisRaw("Horizontal");
        rV = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveH, 0, moveV);

        myAnimator.SetFloat("speed", movement.magnitude);


        //Quaternion rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        //动画切换发生在speed>0.1，如果位移的动画播放不匹配，请检查动画参数speed的过渡值
        if (movement.magnitude>0.1f)
        {
            myCharacterController.Move(-movement * spd * Time.deltaTime);
        }
        
        if (rH!=0||rV!=0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-new Vector3(rH, 0, rV)), 0.2f);
        }
        
        //if (moveH!=0|| moveV!=0)
        //{
        //    myAnimator.SetBool("iswalk", true);
        //    Vector3 movement = new Vector3(moveH, 0, moveV);
        //    Quaternion rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        //    //myCharacterController.SimpleMove(rotation * movement * 10);
        //    Vector3 dir = rotation * movement;

        //    myCharacterController.SimpleMove(dir * spd);

        //    if (!(Mathf.Abs(moveH) < 0.5 && Mathf.Abs(moveV) < 0.5))
        //    {
        //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.7f);
        //    }
        //}
        //else
        //{
        //    myAnimator.SetBool("iswalk", false);
        //}

        //Vector3 dir = Camera.main.transform.TransformVector(movement);
        //myCharacterController.SimpleMove(dir*10);
        //transform.Rotate(new Vector3(0, moveH, 0)*3);

        if (Input.GetKeyDown(KeyCode.J))
        {
            myAnimator.SetTrigger("skill1");
           
        }

    }
    void MySkill1()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Camera.main.DOShakePosition(1f);
    }
   
}
