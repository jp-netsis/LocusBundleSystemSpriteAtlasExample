using System.Collections;
using System.Collections.Generic;
using BundleSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class AtlasHook : MonoBehaviour
{
    public BundledAssetPath _bundledAssetPath; 
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
        var request1 = _bundledAssetPath.Load<SpriteAtlas>();
        
        var request2 = BundleManager.Load<SpriteAtlas>(
            AssetBundleConst.ABConst_CommonAtlas.ASSETBUNDLE_NAME,
            AssetBundleConst.ABConst_CommonAtlas.COMMONATLAS_SPRITEATLAS);

        var name1 = request1 == null ? "null" : request1.name;
        Debug.Log($"LoadSpriteAtlas request1 {name1}");
        var name2 = request2 == null ? "null" : request2.name;
        Debug.Log($"LoadSpriteAtlas request2 {name2}");
        if(request1!=null)
            atlasCallback.Invoke(request1);
        if(request2!=null)
            atlasCallback.Invoke(request2);
    }
}
