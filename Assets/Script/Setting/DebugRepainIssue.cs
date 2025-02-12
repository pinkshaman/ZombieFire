using UnityEngine;
using UnityEditor;
using System;
using System.Threading;
using System.Threading.Tasks;

public class DebugRepainIssue : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("<color=yellow>[DebugRepaintIssue]</color> Script kiểm tra lỗi Repaint...");
        TestRepaintSafe();
    }

    // ✅ Cách sửa: Đảm bảo gọi Repaint trên Main Thread
    private void TestRepaintSafe()
    {
        Task.Run(() =>
        {
            Debug.Log("<color=green>[DebugRepaintIssue]</color> Đang thử gọi SceneView.RepaintAll() theo cách an toàn...");

            // ✅ Chuyển về Main Thread để tránh lỗi
            EditorApplication.delayCall += () => SceneView.RepaintAll();
        });
    }

    [ContextMenu("Test Safe Repaint")]
    private void TestSafeRepaint()
    {
        Debug.Log("<color=green>[DebugRepaintIssue]</color> Gọi SceneView.RepaintAll() đúng cách trên Main Thread.");
        EditorApplication.delayCall += () => SceneView.RepaintAll(); // ✅ Cách gọi an toàn
    }

    [ContextMenu("Test Wrong Repaint")]
    private void TestRepaintInWrongThread()
    {
        Task.Run(() =>
        {
            try
            {
                Debug.Log("<color=red>[DebugRepaintIssue]</color> Đang thử gọi SceneView.RepaintAll() SAI cách...");

                // ❌ Lỗi: Gọi từ Thread khác
                SceneView.RepaintAll();
            }
            catch (Exception e)
            {
                Debug.LogError($"<color=red>[DebugRepaintIssue]</color> Lỗi Repaint phát hiện: {e.Message}\n{e.StackTrace}");
            }
        });
    }
}
