using Fusion;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : NetworkBehaviour
{
    public Player Local {get; set;}
    private NetworkCharacterControllerPrototype _cc;
    [SerializeField] private float speed = 5f;
    public CameControl localCam;
    Vector2 view;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    private void Update()
    {
        view.x = Input.GetAxis("Mouse X");
        view.y = Input.GetAxis("Mouse Y");
        localCam.SetMouseVector(view);
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            transform.forward =  localCam.transform.forward;
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            Vector3 move = transform.right * data.direction.x + transform.forward * data.direction.z;
            move.Normalize();
            _cc.Move(speed * move * Runner.DeltaTime);
        }
    }
}
