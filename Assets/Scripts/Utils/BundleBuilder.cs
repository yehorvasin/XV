using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BundleBuilder : Editor
{
    [MenuItem("Assets/ Build Asset Bundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);
    }
}
