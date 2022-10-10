
using UnityEngine;
using Siccity.GLTFUtility;
using System;

public class LoadGltfData : MonoBehaviour
{
    //TODO make async
    //TODO register callbacks

    private void OnEnable()
    {
        ImportGLTF("sophies_tiny_house.glb");
    }

    void ImportGLTF(string filepath)
    {
        GameObject result = Importer.LoadFromFile(filepath);
    }
    

}
