using System.Collections;
using System.Collections.Generic;
using BundleSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BundleTest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator LoadAsync()
    {
        if (!BundleManager.Initialized)
        {
            //show log message
            BundleManager.LogMessages = true;
            //show some ongui elements for debugging
            BundleManager.ShowDebugGUI = true;
            yield return BundleManager.Initialize();
        }

        Debug.Log(Utility.CombinePath(BundleManager.RemoteURL, AssetbundleBuildSettings.ManifestFileName).Replace('\\', '/'));
        //get download size from latest bundle manifest
        var manifestReq = BundleManager.GetManifest();
        yield return manifestReq;
        if (!manifestReq.Succeeded)
        {
            //handle error
            Debug.LogError(manifestReq.ErrorCode);
        }

        Debug.Log($"Need to download { BundleManager.GetDownloadSize(manifestReq.Result) * 0.000001f } mb");

        //start downloading
        var downloadReq = BundleManager.DownloadAssetBundles(manifestReq.Result);
        while(!downloadReq.IsDone)
        {
            if(downloadReq.CurrentCount >= 0)
            {
                Debug.Log($"Current File {downloadReq.CurrentCount}/{downloadReq.TotalCount}, " +
                          $"Progress : {downloadReq.Progress * 100}%, " +
                          $"FromCache {downloadReq.CurrentlyLoadingFromCache}");
            }
            yield return null;
        }
        
        if(!downloadReq.Succeeded)
        {
            //handle error
            Debug.LogError(downloadReq.ErrorCode);
        }
    }

    public void OnClick()
    {
        StartCoroutine(CoOnClick());
    }

    IEnumerator CoOnClick()
    {
        yield return LoadAsync();
        BundleManager.LoadScene(
            AssetBundleConst.ABConst_RemoteMain.ASSETBUNDLE_NAME,
            AssetBundleConst.ABConst_RemoteMain.MAIN,
            LoadSceneMode.Single);
    }
}
