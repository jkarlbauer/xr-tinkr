
using UnityEngine;
using Siccity.GLTFUtility;
using Xrtinkr.System;
using System;

namespace Xrtinkr.DataLoading
{
    public class LoadGltfData : MonoBehaviour
    {
        [SerializeField]
        private string _sampleDataPath = "/sophie-adjusted.glb";

        [SerializeField]
        private string _fallbackDataPath = "sophie-adjusted.glb";
        private void OnEnable()
        {
            string persistentDataPath = Application.persistentDataPath + _sampleDataPath;

            if (SystemState.isOculusQuest)
            {
                try
                {
                    ImportGLTF(persistentDataPath);
                }
                catch(Exception e)
                {
                    Debug.LogError("Could not load file. " + e.StackTrace);
                }
            }

            if (SystemState.isDesktop)
            {
                try
                {
                    ImportGLTF(_fallbackDataPath);
                }
                catch (Exception e)
                {
                    Debug.LogError("Could not load file. " + e.StackTrace);
                }
            }

        }

        private void ImportGLTF(string filepath)
        {
            GameObject result = Importer.LoadFromFile(filepath);
            result.transform.localScale /= 10;
            Debug.Log("Loaded GLB file");
        }
    }

}
