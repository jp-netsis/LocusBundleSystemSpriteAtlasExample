using System.Collections;
using System.Collections.Generic;
using BundleSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class AtlasLoader : MonoBehaviour
{
    public BundledAssetPath _bundledAssetPath; 

    void OnEnable()
    {
        DontDestroyOnLoad(this);
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
        StartCoroutine(LoadAtlas(tag, atlasCallback));
    }
    
    IEnumerator LoadAtlas(string tag, System.Action<SpriteAtlas> atlasCallback)
    {
        if (!BundleManager.Initialized)
        {
            //show log message
            BundleManager.LogMessages = true;
            //show some ongui elements for debugging
            BundleManager.ShowDebugGUI = true;
            Debug.Log($"{Time.frameCount} > BundleManager.Initialize Start");
            yield return BundleManager.Initialize();
            Debug.Log($"{Time.frameCount} > BundleManager.Initialize End");
        }

        Debug.Log($"{Time.frameCount} > bundledAssetPath.LoadAsync<SpriteAtlas> [{tag}] Start");
        var request = _bundledAssetPath.LoadAsync<SpriteAtlas>();
        yield return request;
        Debug.Log($"{Time.frameCount} > bundledAssetPath.LoadAsync<SpriteAtlas> [{tag}] End");
        
        if(request.Asset!=null)
            atlasCallback(request.Asset);
    }
}
