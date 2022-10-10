
using UnityEngine;
using Siccity.GLTFUtility;

public class LoadGltfData : MonoBehaviour
{
    //TODO make async
    //TODO register callbacks
    private string _dataPath = "sophies_tiny_house.glb";

    private void OnEnable()
    {
        ImportGLTF(_dataPath);
    }

    void ImportGLTF(string filepath)
    {
        GameObject result = Importer.LoadFromFile(filepath);
    }
    

}
