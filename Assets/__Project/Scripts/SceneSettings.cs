using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public SceneType currentScene;
    public enum SceneType
    {
        FloatingTree,
        TreeOnACliff,
        LandOfFog,
        MainScene
    };
}
