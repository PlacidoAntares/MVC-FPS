using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public GameObject Player;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData = Player.GetComponent<PlayerData>();
        playerData.controller = playerData.playerObj.GetComponent<CharacterController>();
        if (playerData.lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        PlayerMovement();
        Jump();
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //remember to rotate both camera and player along the Y axis
        playerData.player.transform.Rotate(Vector3.up * mouseDelta.x * playerData.mouseSensitivity);
        playerData.playerCam.transform.Rotate(Vector3.up * mouseDelta.x* playerData.mouseSensitivity);
        playerData.playerObj.transform.Rotate(Vector3.up * mouseDelta.x * playerData.mouseSensitivity);
        //only rotate the camera along x axis
        playerData.cameraPitch -= mouseDelta.y * playerData.mouseSensitivity;
        playerData.cameraPitch = Mathf.Clamp(playerData.cameraPitch, -90.0f, 90.0f);
        playerData.playerCam.localEulerAngles = Vector3.right * playerData.cameraPitch;
    }

    void PlayerMovement()
    {
        playerData.player.transform.position = playerData.playerObj.transform.position + playerData.camOffset;
        playerData.playerCam.transform.position = playerData.playerObj.transform.position + playerData.camOffset;
        //grab input axis for keys
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        //
        //
        playerData.velocityY += playerData.gravValue * Time.deltaTime;
        playerData.velocity = (playerData.playerObj.transform.forward * inputDir.y + playerData.playerObj.transform.right * inputDir.x) * playerData.walkSpeed + Vector3.up * playerData.velocityY;
        playerData.controller.Move(playerData.velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerData.controller.isGrounded)
        {
            Debug.Log("Jumping");
            playerData.velocityY = Mathf.Sqrt(playerData.jumpMagnitude * -2f * playerData.gravValue);
            playerData.velocity.y += playerData.velocityY;

        }
    }
}
