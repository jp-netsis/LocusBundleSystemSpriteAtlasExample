using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BundleSystem;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //show log message
        BundleManager.LogMessages = true;
        //show some ongui elements for debugging
        BundleManager.ShowDebugGUI = true;
        Debug.Log($"{Time.frameCount} > BundleManager.Initialize Start");
        yield return BundleManager.Initialize();
        Debug.Log($"{Time.frameCount} > BundleManager.Initialize End");
        
        BundleSystem.BundleManager.LoadScene("StaticScene", "TitleScene", LoadSceneMode.Single);
    }
}
