using UnityEditor;

#if UNITY_EDITOR
namespace Core.Tools
{

    public static class Tools
    {
        private static SaveLoadService _saveLoadService;

        [InitializeOnLoadMethod]
        private static void InitService() =>
            _saveLoadService = new SaveLoadService();
        
        [MenuItem("Tools/ClearProgress")]
        public static void ClearProgress() => _saveLoadService.ResetSave();
    }
}
#endif