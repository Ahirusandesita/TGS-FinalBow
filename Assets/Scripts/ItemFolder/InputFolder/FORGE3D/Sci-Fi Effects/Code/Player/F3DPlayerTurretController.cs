using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace FORGE3D
{
    public class F3DPlayerTurretController : MonoBehaviour
    {
        RaycastHit hitInfo; // Raycast structure
        public F3DTurret turret;
        bool isFiring; // Is turret currently in firing state
        public F3DFXController fxController;

        Mouse mouse = Mouse.current;
        Keyboard keyboard = Keyboard.current;

        void Update()
        {
            CheckForTurn();
            CheckForFire();
        }

        void CheckForFire()
        {
            // Fire turret
            if (!isFiring && mouse.leftButton.wasPressedThisFrame)
            {
                isFiring = true;
                fxController.Fire();
            }

            // Stop firing
            if (isFiring && mouse.leftButton.wasReleasedThisFrame)
            {
                isFiring = false;
                fxController.Stop();
            }
        }

        void CheckForTurn()
        {
            // Construct a ray pointing from screen mouse position into world space
            Ray cameraRay = Camera.main.ScreenPointToRay(mouse.position.ReadValue());

            // Raycast
            if (Physics.Raycast(cameraRay, out hitInfo, 500f))
            {
                turret.SetNewTarget(hitInfo.point);
            }
        }
    }
}