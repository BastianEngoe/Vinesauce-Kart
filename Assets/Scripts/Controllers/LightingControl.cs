using UnityEngine;

[ExecuteInEditMode]
public class LightingControl : MonoBehaviour
{
    public bool dynamicPerfAdjust = false;
    public bool Shadows = false;
    public bool AdvancedAmbient = true;
    public bool AdvancedFog = true;
    public Texture2D LitProbe;
    public Texture2D AmbientProbe;
    public Texture2D EdgeLitProbe;
    public Cubemap SkyBoxMap;
    public static bool EnableShadows = true;
    public static bool EnableAdvancedAmbient = true;
    public static bool EnableAdvancedFog = false;
    public GameObject mainSceneLight;
    public Material[] modifiedMaterials;
    public Material LitMat;
    public Material AmbientMat;
    public Material EdgeLitMat;
    public Material SkyboxMat;

    void UpdateLighting()
    {
        if (Application.isMobilePlatform)
        {
            mainSceneLight.GetComponent<Light>().shadowBias = .125f;
            if (Screen.height > 800)
            {
                Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);
            }
            QualitySettings.shadowCascades = 1;
            QualitySettings.shadowDistance = 100;
        }

        LitMat.mainTexture = LitProbe;
        AmbientMat.mainTexture = AmbientProbe;
        EdgeLitMat.mainTexture = EdgeLitProbe;
        SkyboxMat.SetTexture("_Tex", SkyBoxMap);

        foreach (Material material in modifiedMaterials)
        {
            if (material is null)
                continue;

            SetKeywordValue(material, "SHADOW_ON", EnableShadows);
            mainSceneLight.SetActive(EnableShadows);

            SetKeywordValue(material, "ADVAMBIENT_ON", EnableAdvancedAmbient);

            SetKeywordValue(material, "ADVFOG_ON", AdvancedFog);
            SetKeywordValue(material, "ADVFOG_OFF", !AdvancedFog);
        }

        #region Local Function.

        void SetKeywordValue(Material material, string keywordName, bool value)
        {
            if (value)
            {
                material.EnableKeyword(keywordName);
            }
            else
            {
                material.DisableKeyword(keywordName);
            }
        }

        #endregion
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 30;
        UpdateLighting();
    }

#if UNITY_EDITOR_WIN
    void OnRenderObject()
    {
        EnableShadows = Shadows;
        EnableAdvancedAmbient = AdvancedAmbient;
        EnableAdvancedFog = AdvancedFog;
        UpdateLighting();
    }
#endif
}
