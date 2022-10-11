
using UnityEngine;
using Siccity.GLTFUtility;

public class LoadGltfData : MonoBehaviour
{
    [SerializeField]
    private string _sampleDataPath = "/sophie-adjusted.glb";

    [SerializeField]
    private string _fallbackDataPath = "sophie-adjusted.glb";
    private void OnEnable()
    {
        string persistentDataPath = Application.persistentDataPath + _sampleDataPath;

        try
        {
            ImportGLTF(persistentDataPath);
        }
        catch
        {
            ImportGLTF(_fallbackDataPath);
        }
    }

    private void ImportGLTF(string filepath)
    {
        GameObject result = Importer.LoadFromFile(filepath);
        result.transform.localScale /= 10;
        Debug.Log("Loaded GLB file");
    }
}
