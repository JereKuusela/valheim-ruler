using System;
using UnityEngine;
namespace Visualization;
public partial class Draw {
  ///<summary>Adds a box collider to a given line.</summary>
  private static void AddBoxCollider(GameObject obj) {
    var renderers = obj.GetComponentsInChildren<LineRenderer>();
    Array.ForEach(renderers, renderer => {
      var start = renderer.GetPosition(0);
      var end = renderer.GetPosition(renderer.positionCount - 1);
      var width = renderer.widthMultiplier;
      var collider = obj.AddComponent<BoxCollider>();
      collider.isTrigger = true;
      collider.center = start + (end - start) / 2;
      var size = (end - start);
      size.x = Math.Abs(size.x);
      size.y = Math.Abs(size.y);
      size.z = Math.Abs(size.z);
      collider.size = size + 2 * new Vector3(width, width, width);
    });
  }

  private static void AddBoxCollider(GameObject obj, Vector3 center, Vector3 size) {
    var collider = obj.AddComponent<BoxCollider>();
    collider.isTrigger = true;
    collider.center = center;
    collider.size = size;
  }
  ///<summary>Creates a line.</summary>
  private static GameObject DrawLineSub(GameObject obj, Vector3 start, Vector3 end) {
    var renderer = CreateRenderer(obj);
    renderer.SetPosition(0, start);
    renderer.SetPosition(1, end);
    return obj;
  }
  ///<summary>Creates a renderer with a line that doesn't rotate with the object.</summary>
  public static GameObject DrawLineWithFixedRotation(string tag, GameObject parent, Vector3 start, Vector3 end) {
    // Box colliders don't work with non-perpendicular lines so the line must be rotated from a forward line.
    var parentObj = CreateLineRotater(CreateObject(parent.gameObject, tag, true), start, end);
    var forwardEnd = Vector3.forward * (end - start).magnitude;
    var obj = DrawLineSub(parentObj, Vector3.zero, forwardEnd);
    Draw.AddBoxCollider(obj);
    return obj;
  }
}
