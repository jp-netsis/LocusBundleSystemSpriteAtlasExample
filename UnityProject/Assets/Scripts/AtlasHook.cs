using System.Collections;
using System.Collections.Generic;
using BundleSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class AtlasHook : MonoBehaviour
{
    public UnityEvent _completeInitialize;
        
    IEnumerator Start()
    {
        //show log message
        BundleManager.LogMessages = true;
        //show some ongui elements for debugging
        BundleManager.ShowDebugGUI = true;
        yield return BundleManager.Initialize();
        DontDestroyOnLoad(this);
        _completeInitialize?.Invoke();
    }

    void OnEnable()
    {
        Debug.Log($"SpriteAtlasManager.atlasRequested += OnAtlasRequested;");
        SpriteAtlasManager.atlasRequested += OnAtlasRequested;
    }

    void OnDisable()
    {
        Debug.Log($"SpriteAtlasManager.atlasRequested -= OnAtlasRequested;");
        SpriteAtlasManager.atlasRequested -= OnAtlasRequested;
    }

    void OnAtlasRequested(string tag, System.Action<SpriteAtlas> atlasCallback)
    {
        Debug.Log($"OnAtlasRequested {tag}");
        var request = BundleManager.Load<SpriteAtlas>(
            AssetBundleConst.ABConst_CommonAtlas.ASSETBUNDLE_NAME,
            AssetBundleConst.ABConst_CommonAtlas.COMMONATLAS_SPRITEATLAS);

        var name = request == null ? "null" : request.name;
        Debug.Log($"LoadSpriteAtlas {name}");
        atlasCallback.Invoke(request);
    }
}
