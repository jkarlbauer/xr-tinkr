
using UnityEngine;
using Siccity.GLTFUtility;
using System.IO;

public class LoadGltfData : MonoBehaviour
{
    private string _dataPath = "sophies_tiny_house.glb";

    private void OnEnable()
    {    
        _dataPath = Application.persistentDataPath + "sophies_tiny_house.glb";
        ImportGLTF(_dataPath);
    }

    void ImportGLTF(string filepath)
    {
        GameObject result = Importer.LoadFromFile(filepath);
    }
    

}
