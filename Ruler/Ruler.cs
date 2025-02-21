using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using Service;
using UnityEngine;

namespace Ruler;
[BepInPlugin(GUID, NAME, VERSION)]
public class Ruler : BaseUnityPlugin
{
  const string GUID = "ruler";
  const string NAME = "Ruler";
  const string VERSION = "1.7";
  public void Awake()
  {
    Settings.Init(Config);
    Harmony harmony = new(GUID);
    harmony.PatchAll();
    MessageHud_UpdateMessage.GetMessage = GetMessage;
  }
  public static string Tag = $"{GUID}.{NAME}";
  private static GameObject? ruler = null;
  public static bool Enabled => ruler;
  public static void Remove()
  {
    if (ruler) Destroy(ruler);
    ruler = null;
  }

  private static Vector3 Position;
  private static float Radius;
  public static void Refresh() => Draw(Position, Radius);
  public static void Draw(Vector3 position, float radius)
  {
    Position = position;
    Radius = radius;
    if (ruler) Remove();
    GameObject obj = new();
    obj.layer = LayerMask.NameToLayer(Visualization.Draw.TriggerLayer);
    obj.transform.position = position;
    ruler = obj;
    if (Settings.configDraw3d.Value)
    {
      var line = Visualization.Draw.DrawSphere(Ruler.Tag, obj, radius);
      Visualization.Draw.AddText(line, Format.Coordinates(position), "Ruler");
    }
    else
    {
      var line = Visualization.Draw.DrawCircleY(Ruler.Tag, obj, radius);
      Visualization.Draw.AddText(line, Format.Coordinates(position), "Ruler");
    }
    if (Settings.configDrawAxis.Value)
    {
      var line = Visualization.Draw.DrawLineWithFixedRotation(Ruler.Tag, obj, -Vector3.forward * radius, Vector3.forward * radius);
      Visualization.Draw.AddText(line, Format.Coordinates(position), "Ruler");
      line = Visualization.Draw.DrawLineWithFixedRotation(Ruler.Tag, obj, -Vector3.right * radius, Vector3.right * radius);
      Visualization.Draw.AddText(line, Format.Coordinates(position), "Ruler");
      if (Settings.configDraw3d.Value)
      {
        line = Visualization.Draw.DrawLineWithFixedRotation(Ruler.Tag, obj, -Vector3.up * radius, Vector3.up * radius);
        Visualization.Draw.AddText(line, Format.Coordinates(position), "Ruler");
      }
    }
  }
  public static void Toggle(Vector3 position, float radius)
  {
    if (ruler != null && Vector3.Distance(ruler.transform.position, position) < 0.1f) Remove();
    else Draw(position, radius);
  }
  private static string GetText(Vector3 position)
  {
    if (ruler == null) return "";
    var delta = position - ruler.transform.position;
    var distXZ = Utils.DistanceXZ(position, ruler.transform.position);
    return $"Ruler distance ({Format.Coordinates(delta)}): {Format.Float(delta.magnitude)} m, XZ: {Format.Float(distXZ)} m";
  }

  public static List<string> GetMessage()
  {
    List<string> lines = new();
    if (ruler == null || !Settings.configShowText.Value) return lines;
    var position = Player.m_localPlayer.transform.position;
    lines.Add(GetText(position));
    return lines;
  }
}
