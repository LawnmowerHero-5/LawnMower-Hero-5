using System;
using UnityEngine;

public class SteeringTest : MonoBehaviour
{
   
   //Todo Clean up all of this code
   #region Variables
   
   [Header("Hands")]
   //Left Hand
   [SerializeField] private GameObject leftHand;

   private Transform _leftHandOriginalParent;

   private bool _leftHandOnWheel;
   //Right Hand
   [SerializeField] private GameObject rightHand;
   private Transform _rightHandOriginalParent;
   private bool _rightHandOnWheel;

   [Header("Transforms")] 
   [SerializeField] private Transform[] grabbablePositions;
   [SerializeField] private Transform directionalObject;

   [Header("Rotation")] 
   public float wheelRotation;

   public float turnDampening = 250; // the higher it is, the slower the object turns to target rotation
   // will change this 
   public float turnMultiplier = 0;
   public GameObject vehicle;

   public enum WhichHands
   {
      NoHands,
      LeftHandOnly,
      RightHandOnly,
      BothHands
   }

   public WhichHands _handStates;
   #endregion

   private void Update()
   {
      wheelRotation = -transform.rotation.eulerAngles.z;
      
      ReleaseHandsFromWheel();
      
      HandStates();

      wheelRotation = -transform.rotation.eulerAngles.z;
      
      TurnVehicle();
   }


   #region Hands

   private void OnTriggerStay(Collider other)
   {
      
      if (other.CompareTag("PlayerHands"))
      {
         if (!_rightHandOnWheel) // &&input to hold the wheel TODO
         {
            PlaceHandOnWheel(ref rightHand, ref _rightHandOriginalParent, ref _rightHandOnWheel);
         }
         if (!_leftHandOnWheel) // &&input to hold the wheel TODO
         {
            PlaceHandOnWheel(ref leftHand, ref _leftHandOriginalParent, ref _leftHandOnWheel);
         }
      }
   }

   private void ReleaseHandsFromWheel() // HandsOnWheel???
   {
      if (_rightHandOnWheel) // &&input to hold the wheel TODO
      {
         rightHand.transform.parent = _rightHandOriginalParent;
         rightHand.transform.position = _rightHandOriginalParent.position;
         rightHand.transform.rotation = _rightHandOriginalParent.rotation;
         _rightHandOnWheel = false;
      }
      if (_leftHandOnWheel) // &&input to hold the wheel TODO
      {
         leftHand.transform.parent = _leftHandOriginalParent;
         leftHand.transform.position = _leftHandOriginalParent.position;
         leftHand.transform.rotation = _leftHandOriginalParent.rotation;
         _leftHandOnWheel = false;
      }

      if (!_rightHandOnWheel && !_leftHandOnWheel)
      {
         transform.parent = null;
      }
   }

   private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handsOnWheel)
   {
      var shortestDistance = Vector3.Distance(grabbablePositions[0].position, hand.transform.position);
      var bestGrab = grabbablePositions[0];

      foreach (var grabbablePosition in grabbablePositions)
      {
         if (grabbablePosition.childCount == 0)
         {
            var distance = Vector3.Distance(grabbablePosition.position, hand.transform.position);
            if (distance < shortestDistance)
            {
               shortestDistance = distance;
               bestGrab = grabbablePosition;
            }
         }
      }

      originalParent = hand.transform.parent;

      hand.transform.parent = bestGrab.transform;
      hand.transform.position = bestGrab.transform.position;

      handsOnWheel = true;
   }
   
   private void HandStates()
   {
      switch (_handStates)
      {
         case WhichHands.NoHands:
            StateSwitch("NoHands");
            break;
         case WhichHands.BothHands:
            StateSwitch("BothHands");
            break;
         case WhichHands.LeftHandOnly:
            StateSwitch("LeftHandOnly");
            break;
         case WhichHands.RightHandOnly:
            StateSwitch("RightHandOnly");
            break;
      }
   }

   private void StateSwitch(string stateName)
   {
      switch (stateName)
      {
         case "NoHands" when _rightHandOnWheel && !_leftHandOnWheel:
            _handStates = WhichHands.RightHandOnly;
            break;
         case "NoHands" when _leftHandOnWheel && !_rightHandOnWheel:
            _handStates = WhichHands.LeftHandOnly;
            break;
         case "NoHands":
         {
            if (_leftHandOnWheel && _rightHandOnWheel)
            {
               _handStates = WhichHands.BothHands;
            }

            break;
         }
         case "RightHandOnly" when _leftHandOnWheel && _rightHandOnWheel:
            _handStates = WhichHands.BothHands;
            break;
         case "RightHandOnly" when _leftHandOnWheel && !_rightHandOnWheel:
            _handStates = WhichHands.LeftHandOnly;
            break;
         case "RightHandOnly":
         {
            if (!_leftHandOnWheel && !_rightHandOnWheel)
            {
               _handStates = WhichHands.NoHands;
            }

            break;
         }
         case "LeftHandOnly" when _leftHandOnWheel && _rightHandOnWheel:
            _handStates = WhichHands.BothHands;
            break;
         case "LeftHandOnly" when !_leftHandOnWheel && _rightHandOnWheel:
            _handStates = WhichHands.RightHandOnly;
            break;
         case "LeftHandOnly":
         {
            if (!_leftHandOnWheel && !_rightHandOnWheel)
            {
               _handStates = WhichHands.NoHands;
            }

            break;
         }
         case "BothHands" when _leftHandOnWheel && !_rightHandOnWheel:
            _handStates = WhichHands.LeftHandOnly;
            break;
         case "BothHands" when !_leftHandOnWheel && _rightHandOnWheel:
            _handStates = WhichHands.RightHandOnly;
            break;
         case "BothHands":
         {
            if (!_leftHandOnWheel && !_rightHandOnWheel)
            {
               _handStates = WhichHands.NoHands;
            }

            break;
         }
      }

      if (stateName == "NoHands") return;
      Rotation(stateName);
   }
   #endregion

   
   #region Rotation
   // Rotates the wheel based on where the hands are
   private void Rotation(string stateName)
   {
      if (stateName == "BothHands")
      {
         Quaternion newRotLeft = Quaternion.Euler(0, vehicle.transform.rotation.y, _leftHandOriginalParent.transform.rotation.eulerAngles.z);
         Quaternion newRotRight = Quaternion.Euler(0, 0, _rightHandOriginalParent.transform.rotation.eulerAngles.z);
         Quaternion finalRot = Quaternion.Slerp(newRotLeft, newRotRight, 1f / 2f);
         directionalObject.rotation = finalRot;
         
         // might be changing the wrong thing.
         transform.parent = directionalObject;
      }
      else if (stateName == "LeftHandOnly")
      {
         Quaternion newRot = Quaternion.Euler(0, vehicle.transform.rotation.y, _leftHandOriginalParent.transform.rotation.eulerAngles.z);
         directionalObject.rotation = newRot;
         
         // might be changing the wrong thing.
         transform.parent = directionalObject;
      }
      else if(stateName == "RightHandOnly")
      {
         Quaternion newRot = Quaternion.Euler(0, vehicle.transform.rotation.y, _rightHandOriginalParent.transform.rotation.eulerAngles.z);
         directionalObject.rotation = newRot;
         
         // might be changing the wrong thing.
         transform.parent = directionalObject;
      }
   }
    
   private void TurnVehicle()
   {
      var turn = -transform.rotation.eulerAngles.x;
      if (turn < -350)
      {
         turn = turn + 360;
      }
      // Todo: Output field of the rotation (Done ???)
      turnMultiplier = turn / 360;

      print(turnMultiplier);

   }
   #endregion
   
}
