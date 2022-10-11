
using UnityEngine;
using Siccity.GLTFUtility;

public class LoadGltfData : MonoBehaviour
{
    private void OnEnable()
    {    
        string _dataPath = Application.persistentDataPath + "/sample.glb";

        _dataPath = "sophies_tiny_house.glb";

        ImportGLTF(_dataPath);
    }

    void ImportGLTF(string filepath)
    {
        GameObject result = Importer.LoadFromFile(filepath);
        result.transform.localScale /= 10;
        Debug.Log("Loaded GLB file");
        CenterObjectInScene(result);
    }

    private void CenterObjectInScene(GameObject go)
    {
        MeshFilter[] meshfilters = go.GetComponentsInChildren<MeshFilter>();

        Vector3 centerOfMass = Vector3.zero;
        int cnt = 0;
        foreach(MeshFilter mf in meshfilters)
        {
            centerOfMass += mf.sharedMesh.bounds.center;
            cnt++;
        }

        if(cnt > 0)
        {
            centerOfMass /= cnt;
        }

        Debug.Log("Center of mass: " + centerOfMass);
        GameObject parent = new GameObject("parent");
        parent.transform.position = centerOfMass;
        go.transform.parent = parent.transform;
        parent.transform.position = Vector3.zero;

    }
}
