using UnityEngine;
using UnityEditor;

public class AnimationClipConverter : MonoBehaviour
{
    [MenuItem("Tools/Convert Legacy Animation Clips")]
    static void ConvertLegacyClips()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj is AnimationClip)
            {
                AnimationClip legacyClip = obj as AnimationClip;
                if (legacyClip.legacy)
                {
                    AnimationClip newClip = new AnimationClip();
                    EditorUtility.CopySerialized(legacyClip, newClip);
                    newClip.legacy = false;

                    string path = AssetDatabase.GetAssetPath(legacyClip);
                    string newPath = path.Replace(".anim", "_converted.anim");
                    AssetDatabase.CreateAsset(newClip, newPath);
                    AssetDatabase.SaveAssets();

                    Debug.Log($"Converted {legacyClip.name} to non-legacy: {newPath}");
                }
            }
        }
    }
}
