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
    //
    public Transform gunBarrel;
    public float gunDamage;
    [HideInInspector] public GameObject target;
    [HideInInspector] public EnemyData targetData;
    public int weaponRange;
    public Camera fpsCam;
    public float weaponCD;
    public bool readyToFire;
    [HideInInspector] public WaitForSeconds shotDuration = new WaitForSeconds(0.007f);
    [HideInInspector] public LineRenderer laserLine;
    [HideInInspector] public RaycastHit hit;
    private void Start()
    {
        laserLine = fpsCam.gameObject.GetComponentInChildren<LineRenderer>();
        laserLine.enabled = false;
        //Debug.Log(laserLine);
        readyToFire = true;
        playerObj = this.gameObject.transform;
        controller = GetComponent<CharacterController>();
    }
}
