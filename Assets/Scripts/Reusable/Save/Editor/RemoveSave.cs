using UnityEditor;

namespace Reusable.Save.Editor
{
    public class RemoveSave
    {
        [MenuItem("Saves/Clear Saves", priority = 90)]
        public static void Init()
        {
            SaveManager.ClearSaves();
        }
    }
}