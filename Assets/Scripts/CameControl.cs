
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameControl : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform P_Transform;
    private float xRotation;
    Vector2 mouseVector;

    [SerializeField] GameObject cameraAnchor;
    private Camera localCam;
    private NetworkCharacterControllerPrototype _ncc;
    private float cameraRotaX;
    private float cameraRotaY;


    private void Awake()
    {
        localCam = GetComponent<Camera>();
        _ncc = GetComponentInParent<NetworkCharacterControllerPrototype>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        if (localCam.enabled)
            localCam.transform.parent = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (cameraAnchor == null)
            return;

        if (!localCam.enabled)
            return;

        localCam.transform.position = cameraAnchor.transform.position;

        cameraRotaX -= mouseVector.y * _ncc.rotationSpeed;
        Mathf.Clamp(cameraRotaX, -90, 90);

        cameraRotaY += mouseVector.x * _ncc.rotationSpeed;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        localCam.transform.rotation = Quaternion.Euler(cameraRotaX, cameraRotaY, 0);
    }

    public void SetMouseVector(Vector2 v)
    { mouseVector = v; }
}
