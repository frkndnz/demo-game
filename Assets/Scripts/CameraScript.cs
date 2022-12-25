using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraScript : MonoBehaviour
{
    public static float mulX;
    public static float mulY;
    public float camMovementSpeed;
    public float X_distance, Y_distance;
    private PixelPerfectCamera my_pixelcamera;


    private void SetSensivity()
    {
        mulX = 1280f / Screen.width;
        mulY = 720f / Screen.height;

    }
    private void Start()
    {
        my_pixelcamera = GetComponent<PixelPerfectCamera>();
        SetSensivity();
        OrtographicSize();
    }
    public float orthoSizeInMeters = 10f;
    private float orthoSize;
    private Camera mCamera;
    private void OrtographicSize()
    {
        mCamera = Camera.main;
        orthoSize = orthoSizeInMeters * Screen.height / Screen.width * 0.5f;
        mCamera.orthographicSize = orthoSize;
    }

    private void LateUpdate()
    {
        MoveCamera();
        OrtographicSize();
    }
    Vector2 mousePosition;
    void MoveCamera()
    {
        mousePosition = Input.mousePosition;
        //if (mousePosition.x < X_distance * mulX)
        //{
        //    transform.position -= Vector3.right * Time.deltaTime * camMovementSpeed;
        //}
        //if (mousePosition.x > (Screen.width - X_distance) * mulX)
        //{
        //    transform.position += Vector3.right * Time.deltaTime * camMovementSpeed;
        //}
        //if (mousePosition.y > (Screen.height -Y_distance) * mulY)
        //{
        //    transform.position += Vector3.up * Time.deltaTime * camMovementSpeed;
        //}
        //if (mousePosition.y < Y_distance * mulY)
        //{
        //    transform.position -= Vector3.up * Time.deltaTime * camMovementSpeed;
        //}

        // camera zoom
        zoom_input += Input.GetAxis("Mouse ScrollWheel")*30f;
        zoom_input = Mathf.Clamp(zoom_input,minZoom, maxZoom);
        my_pixelcamera.assetsPPU = (int)zoom_input;

    }
    public float minZoom, maxZoom;
    float zoom_input;
}
