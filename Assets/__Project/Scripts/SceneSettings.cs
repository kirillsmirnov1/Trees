using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public static SceneSettings Instance { get; private set; }

    public SceneType currentScene;

    private void Awake()
    {
        Instance = this;
    }

    public enum SceneType
    {
        FloatingTree,
        TreeOnACliff,
        LandOfFog,
        MainScene
    };
}
