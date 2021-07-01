using DA.AssetLoad;
using Game.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public PlayFiniteStateMachine PlayFiniteStateMachine;

        public string HeroModePath;
        public string CameraPath;

        private AssetLoader loader;


        private void Start()
        {
            loader = AssetLoader.GetAssetLoader();

            var go = loader.LoadAsset<GameObject>(HeroModePath);
            go = Instantiate(go);

            var playerController = go.AddComponent<PlayerController>();
            playerController.PlayFiniteStateMachine = PlayFiniteStateMachine;
            playerController.Animator = go.GetComponent<Animator>();
            playerController.Rigidbody = go.GetComponent<Rigidbody>();
            playerController.ControllerHangPoint = go.GetComponent<ControllerHangPoint>();
            playerController.ModeTransform = go.transform;


            var cameraHangPoint = playerController.CameraHangPoint = loader.LoadAsset<CameraHangPoint>(CameraPath);

            cameraHangPoint = Instantiate(cameraHangPoint);
            Transform followTransform = playerController.ControllerHangPoint.FollowTarget;
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
