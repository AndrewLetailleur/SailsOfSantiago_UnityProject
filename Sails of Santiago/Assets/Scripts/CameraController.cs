using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour {

    private Camera cam;
    [SerializeField]
    private Transform defaultTarget;
    private Transform t;
    [SerializeField]
    private Vector3 offset;

    private void Awake() {

    }

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        t = defaultTarget;
        if (offset == null) offset = new Vector3(0.0f, 100.0f, 40.0f);
        transform.position = t.position - offset;
        UpdatePosition();

    }

    private void UpdatePosition() {
        transform.position = t.position - offset;
        transform.LookAt(t);
    }

    // Update is called once per frame
    void LateUpdate () {
        UpdatePosition();
	}
}
