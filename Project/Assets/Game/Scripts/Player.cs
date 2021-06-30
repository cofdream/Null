using DA.AssetLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public PlayerController PlayerController;
        public string HeroModePath;
        public string CameraPath;

        private AssetLoader loader;


        private void Start()
        {
            loader = AssetLoader.GetAssetLoader();


            var go = loader.LoadAsset<GameObject>(HeroModePath);
            go = Instantiate(go, transform);

            PlayerController.Animator = go.GetComponent<Animator>();
            PlayerController.Rigidbody = go.GetComponent<Rigidbody>();
            PlayerController.ControllerHangPoint = go.GetComponent<ControllerHangPoint>();
            PlayerController.ModeTransform = go.transform;


            CameraHangPoint cameraHangPoint = loader.LoadAsset<CameraHangPoint>(CameraPath);

            cameraHangPoint = Instantiate(cameraHangPoint);
            Transform followTransform = PlayerController.ControllerHangPoint.FollowTarget;
            cameraHangPoint.CM_VCamera.Follow = followTransform;
        }

        private void OnDestroy()
        {
            loader.Unload(HeroModePath);
            loader.Unload(CameraPath);

            loader.UnloadAll();
        }
    }
}
