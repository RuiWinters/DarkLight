using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour,IPointerDownHandler,IDragHandler,IBeginDragHandler,IPointerUpHandler
{
    public GameObject muban;
    GameObject obj;
    Ray ray;
    RaycastHit hit;
    bool isDown = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        obj = Instantiate(muban);
        obj.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = new Color(1, 1, 0);
        isDown = true;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isDown)
        {
            return;
        }

        Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,1));

        obj.transform.position = v;
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray,out hit,100,LayerMask.GetMask("Ground")))
        //{
        //    obj.transform.position = hit.point;
        //}
        //else
        //{
        //    obj.transform.position = ray.GetPoint(20);
        //}
	}

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        isDown = false;
    }
}
