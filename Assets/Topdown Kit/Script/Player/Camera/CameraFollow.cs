using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float distance;
    Vector3 offset;
    Transform player;
	// Use this for initialization
	void Start ()
    {
        offset = transform.forward * distance;
        player = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = Vector3.Lerp(transform.position, player.position - offset,5*Time.deltaTime);
	}
}
