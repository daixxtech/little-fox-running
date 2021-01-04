using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor {
    public static class UIUtil {
        private static readonly StringBuilder BUILDER = new StringBuilder();

        [MenuItem("Tools/Copy UI Transform Path %#X")]
        public static void CopyUITransformPath() {
            Transform cur = Selection.activeTransform;
            if (cur == null) {
                return;
            }
            BUILDER.Clear().Insert(0, cur.name);
            while (true) {
                Transform parent = cur.parent;
                if (parent == null || parent.GetComponent<Canvas>() != null) {
                    break;
                }
                BUILDER.Insert(0, "/").Insert(0, parent.name);
                cur = parent;
            }
            string path = BUILDER.ToString();
            Debug.Log($"[CopyPath] {path}");
            GUIUtility.systemCopyBuffer = path;
        }
    }
}
