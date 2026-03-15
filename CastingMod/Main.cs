using System;
using System.Collections.Generic;
using BepInEx;
using GorillaLocomotion;
using OVR.OpenVR;
using UnityEngine;

/*
 
 *  Vyn's casting mod - developed by Frapster/vyn (@vynthefluff on github)
 *  Sorry for not writing many comments - I normally don't purposefully open-
 *  source my projects, so I rarely remember to xD.
 *  Enjoy the casting mod :3
 
*/

namespace vynscastingmod
{
    [BepInPlugin(modId, modName, modVer)]
    public class Main : BaseUnityPlugin
    {
        public const string modId = "com.vyn.castingClient";
        public const string modName = "CastingClient";
        public const string modVer = "1.0.0";
        
        public void Update()
        {
            if (!initialized)
            {
                try
                {
                    if (OpenVR.IsHmdPresent())
                    {
                        Destroy(this); // If user is in VR, destroy the casting mod.
                        return;
                    }
                }
                catch (Exception) { } // Normally if OpenVR isnt initialised, they're running in the oculus version without a headset connected, so they likely arent in VR.


                if (GTPlayer.Instance == null) return;
                localPlayer = GTPlayer.Instance;
                VRRig offlineRig = GorillaTagger.Instance.offlineVRRig;
                loadedRigs.Add(offlineRig);
                
                Destroy(GorillaTagger.Instance.thirdPersonCamera);
                camera = new GameObject("CastingClient").AddComponent<Camera>();

                camera.cameraType = CameraType.Game;
                camera.fieldOfView = 90;
                camera.nearClipPlane = 0.01f;
                camera.farClipPlane = 2500;
                
                Application.targetFrameRate = int.MaxValue; // Gtag's fps is capped at 144 by default - no thanks.
                initialized = true;
            }

        }

        private List<VRRig> loadedRigs = new List<VRRig>();
        private VRRig target;
        private GTPlayer localPlayer;
        private bool initialized = false;
        public Camera camera;
    }
}