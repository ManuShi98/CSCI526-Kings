using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoints : MonoBehaviour
{
  [SerializeField]
  private GameObject buildTreePrefab;

  public GameObject activeBuildTree;

  public void ShowBuildTree()
  {
    Vector3 mousePos = Input.mousePosition;
    mousePos.z = Camera.main.nearClipPlane;
    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
    activeBuildTree = Instantiate(buildTreePrefab, worldPosition, Quaternion.identity);
  }

  public void CloseBuildTree()
  {
    Destroy(gameObject);
  }

  public void BuildTower(GameObject towerPrefab)
  {
    Instantiate(towerPrefab, transform.position, Quaternion.identity);
    Debug.Log("build");
    CloseBuildTree();
  }
}
