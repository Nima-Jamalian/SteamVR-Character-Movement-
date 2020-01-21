using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SteamVR libraries  
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Locomotion : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private GameObject _leftHand;
    [SerializeField] private GameObject _rightHand;

    private Vector3 _previousPosLeft;
    private Vector3 _previousPosRight;
    private Vector3 _direction;

    Vector3 _gravity = new Vector3(0, 9.8f, 0);

    [SerializeField] private float _speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Setting the pos at the start of the game
        SetPreviousPos();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Calculating the velocity of the player hand movement 
         * Left hand velocity = left hand current pose - left hand precious pose 
         * Right hand velocity = right current pose - right hand precious 
         * total velocity =+ magnitude of Left hand velocity + Magnitude of hand velocity
         * 
         * Move the player based on the total velocity
         */
        Vector3 _leftVelocity = _leftHand.transform.position - _previousPosLeft;
        Vector3 _rightVelocity = _rightHand.transform.position - _previousPosRight;
        float _totalVelocity =+ _leftVelocity.magnitude * 0.8f + _rightVelocity.magnitude * 0.8f;

        if(_totalVelocity >= 0.05)
        {
            //Getting the direction that player is facing
            _direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, 1));
            //Moving the player using character controller  
            _characterController.Move(_speed * Time.deltaTime * Vector3.ProjectOnPlane(_direction, Vector3.up));
        }
        //Applying Gravity to the player
        _characterController.Move(-_gravity * Time.deltaTime);
        SetPreviousPos(); 
    }

    private void SetPreviousPos()
    {
        _previousPosLeft = _leftHand.transform.position;
        _previousPosRight = _rightHand.transform.position;
    }

}
