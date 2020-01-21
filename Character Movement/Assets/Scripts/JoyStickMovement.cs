using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SteamVR libraries  
using Valve.VR;
using Valve.VR.InteractionSystem;

public class JoyStickMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 controllerInput;
    [SerializeField] private float _speed = 2;
    private CharacterController _characterController;
    // Start is called before the first frame update  
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame  
    void Update()
    {
        //movement direction  
        Vector3 _direction = Player.instance.hmdTransform.TransformDirection(new Vector3(controllerInput.axis.x, 0, controllerInput.axis.y));
        //applying gravity
        Vector3 _gravity = new Vector3(0, 9.8f, 0);
        //Moving the player using character controller  
        _characterController.Move(_speed * Time.deltaTime * Vector3.ProjectOnPlane(_direction, Vector3.up) - _gravity * Time.deltaTime);
    }

}
