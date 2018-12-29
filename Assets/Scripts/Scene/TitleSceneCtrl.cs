using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TinyTeam.UI;
using UnityEngine.SceneManagement;

public class TitleSceneCtrl : MonoBehaviour {

    public Camera cam;//主摄像机
    public Transform targetPoint;//摄像机移动的目标点

    // Use this for initialization
    void Start () {
        //cube.position = Vector3.MoveTowards(Vector3.forward, Vector3.forward*10, 10);
        cam.transform.DOMove(targetPoint.position,8);
        TTUIPage.ShowPage<TitlePanel>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.anyKeyDown && Time.time > 5) 
        //{
        //    SceneManager.LoadScene("My Character Creation");
        //}
    }
}
