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
        //if you don't call atlascallback right away, it'll be called even for same tag.
        Debug.Log($"OnAtlasRequested {tag}");
        Debug.Log($"{Time.frameCount} > bundledAssetPath.LoadAsync<SpriteAtlas> [{tag}] Start");
        var asset = _bundledAssetPath.Load<SpriteAtlas>();
        Debug.Log($"{Time.frameCount} > bundledAssetPath.LoadAsync<SpriteAtlas> [{tag}] End");
        atlasCallback(asset);
    }
}
