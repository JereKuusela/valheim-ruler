﻿using System.Globalization;
using BepInEx.Configuration;
using UnityEngine;
using Visualization;

namespace Ruler;
public class Settings {
#nullable disable
  public static ConfigEntry<string> configColor;
  public static ConfigEntry<int> configLineWidth;
  public static ConfigEntry<bool> configDrawAxis;
  public static ConfigEntry<bool> configDraw3d;
  public static ConfigEntry<bool> configShowText;
#nullable enable
  public static void Init(ConfigFile config) {
    var section = "Ruler";
    configLineWidth = config.Bind(section, "Line width", 10, "");
    OnWidthChanged(configLineWidth, Ruler.Tag);
    configColor = config.Bind(section, "Color", "red", "");
    OnColorChanged(configColor, Ruler.Tag);
    configDrawAxis = config.Bind(section, "Draw axis", true, "Draws axis inside the ruler.");
    configDrawAxis.SettingChanged += (s, e) => Ruler.Refresh();
    configDraw3d = config.Bind(section, "Draw 3d", true, "Draws sphere instead of a circle.");
    configDraw3d.SettingChanged += (s, e) => Ruler.Refresh();
    configShowText = config.Bind(section, "Show text", true, "Shows distance on the HUD.");
    InitCommands();
  }
  private static Color ParseColor(string color) {
    if (ColorUtility.TryParseHtmlString(color, out var parsed)) return parsed;
    return Color.white;
  }

  private static void OnWidthChanged(ConfigEntry<int> entry, string tag) {
    entry.SettingChanged += (s, e) => Draw.SetLineWidth(tag, entry.Value);
    Draw.SetLineWidth(tag, entry.Value);
  }
  private static void OnColorChanged(ConfigEntry<string> entry, string tag) {
    entry.SettingChanged += (s, e) => Draw.SetColor(tag, ParseColor(entry.Value));
    Draw.SetColor(tag, ParseColor(entry.Value));
  }

  private static void InitCommands() {
    new Terminal.ConsoleCommand("ruler", "[radius] - Toggles or sets the ruler.", (Terminal.ConsoleEventArgs args) => {
      if (!Player.m_localPlayer) return;
      var radius = 0.1f;
      if (args.Length > 1)
        float.TryParse(args[1], NumberStyles.Float, CultureInfo.InvariantCulture, out radius);
      Ruler.Toggle(Player.m_localPlayer.transform.position, radius);
    });
  }
}
