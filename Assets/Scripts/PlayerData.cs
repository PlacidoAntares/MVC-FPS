using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform playerObj;
    public float mouseSensitivity;
    public bool lockCursor = true;
    public float cameraPitch = 0.0f;
    public Vector3 camOffset;
    public float walkSpeed;
    public float gravValue = -13.0f;
    public float velocityY = 0.0f;
    public float jumpMagnitude;
    public CharacterController controller = null;
    public Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
}
