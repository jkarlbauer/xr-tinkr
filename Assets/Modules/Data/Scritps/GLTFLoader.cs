
using UnityEngine;
using Siccity.GLTFUtility;
using Xrtinkr.System;
using System;

namespace Xrtinkr.Data
{
    public class GLTFLoader : MonoBehaviour
    {
        [SerializeField]
        private string optionalFileName = null;

        private IFilePicker _filePicker;
        private void OnEnable()
        {
            

            if (SystemState.isOculusQuest)
            {
                _filePicker = new QuestFilePickerImpl();
            }

            if (SystemState.isDesktop)
            {  
                _filePicker = new DesktopFilePickerImpl();
            }


            string _filepath = TryPickFile();
            TryImportGLTF(_filepath);
        }

        private string TryPickFile()
        {
            string path = "";

            try
            {
                path = _filePicker.PickFile(optionalFileName);

            }catch(Exception e)
            {
                Debug.LogError("could not pick GLB file");
            }

            return path;
        }

        private void TryImportGLTF(string filepath)
        {
            try
            {
                ImportGLTF(filepath);
            }catch(Exception e)
            {
                Debug.LogError("could not load GLTF File");
            }
        }

        private void ImportGLTF(string filepath)
        {
            GameObject result = Importer.LoadFromFile(filepath);
            Debug.Log($"Loaded GLB file from: {filepath}");

            GLTFParser gltfParser = new GLTFParser(result);
            gltfParser.Parse();
        }
    }

}
