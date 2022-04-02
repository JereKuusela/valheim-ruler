using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using Service;
using UnityEngine;

namespace Ruler;
[BepInPlugin("valheim.jerekuusela.ruler", "Ruler", "1.0.0.0")]
public class Ruler : BaseUnityPlugin {
  public void Awake() {
    Settings.Init(Config);
    Harmony harmony = new("valheim.jerekuusela.ruler");
    harmony.PatchAll();
    MessageHud_UpdateMessage.GetMessage = GetMessage;
  }
  public static string Tag = "valheim.jerekuusela.ruler";
  private static GameObject ruler = null;
  public static void Remove() {
    if (ruler) Destroy(ruler);
    ruler = null;
  }
  public static void Draw(Vector3 position, float radius) {
    if (ruler) Remove();
    GameObject obj = new();
    obj.layer = LayerMask.NameToLayer(Visualization.Draw.TriggerLayer);
    obj.transform.position = position;
    ruler = obj;
    var line = Visualization.Draw.DrawSphere(Ruler.Tag, obj, radius);
    Visualization.Draw.AddText(line, Format.Coordinates(position), "Ruler");
  }
  public static void Toggle(Vector3 position, float radius) {
    if (ruler && Vector3.Distance(ruler.transform.position, position) < 0.1f) Remove();
    else Draw(position, radius);
  }
  private static string GetText(Vector3 position) {
    var delta = position - ruler.transform.position;
    var distXZ = Utils.DistanceXZ(position, ruler.transform.position);
    return $"Ruler distance ({Format.Coordinates(delta)}): {Format.Float(delta.magnitude)} m, XZ: {Format.Float(distXZ)} m";
  }

  public static List<string> GetMessage() {
    List<string> lines = new();
    if (ruler == null) return lines;
    var position = Player.m_localPlayer.transform.position;
    lines.Add(GetText(position));
    return lines;
  }
}
