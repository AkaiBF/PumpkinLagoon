using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 DownLeftBound;
    public Vector2 TopRightBound;
    public Transform target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
        offset = this.transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = Vector3.Slerp( this.transform.position, target.position + offset, 1);
    }
}
