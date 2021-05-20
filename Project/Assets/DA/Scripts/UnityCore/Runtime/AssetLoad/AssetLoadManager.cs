using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DA.AssetLoad
{
    public class AssetLoadManager : MonoBehaviour
    {
#if UNITY_EDITOR
        public const string SIMULATION_MODE = "Asset bundle simulation mode";
        public readonly static bool IsSimulationMode = UnityEditor.EditorPrefs.GetBool(SIMULATION_MODE, false);
#endif
    }
}