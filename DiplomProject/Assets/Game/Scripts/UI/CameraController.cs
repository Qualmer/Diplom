using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform Target;
	public Camera Camera;

    void Update()
    {
		Camera.transform.position = new Vector3(Target.position.x, Target.position.y, -10);
    }
}
