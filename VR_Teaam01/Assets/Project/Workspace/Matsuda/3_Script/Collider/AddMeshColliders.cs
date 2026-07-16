using UnityEngine;

/* エディター用
 * 子供オブジェクトにMeshColliderを追加する
*/

public class AddMeshColliders : MonoBehaviour
{
    [ContextMenu("Add Mesh Colliders")]
    void AddColliders()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(true);

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.GetComponent<Collider>() == null)
            {
                mf.gameObject.AddComponent<MeshCollider>();
            }
        }

        Debug.Log("完了");
    }
}
