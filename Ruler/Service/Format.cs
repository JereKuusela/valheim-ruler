using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Service {
  public class Format {
    public const string FORMAT = "0.##";

    public static string GetValidColor(bool valid) => valid ? "yellow" : "grey";
    public static string String(string value, string color = "yellow") => "<color=" + color + ">" + value + "</color>";
    public static string String(string value, bool valid) => "<color=" + GetValidColor(valid) + ">" + value + "</color>";
    public static string Float(double value, string format = FORMAT, string color = "yellow") => String(value.ToString(format, CultureInfo.InvariantCulture), color);
    public static string Meters(double value, string color = "yellow") => String(value.ToString(FORMAT, CultureInfo.InvariantCulture) + " meters", color);
    public static string Coordinates(Vector3 coordinates, string format = "F0", string color = "yellow") {
      var values = coordinates.ToString(format).Replace("(", "").Replace(")", "").Split(',').Select(value => Format.String(value.Trim(), color));
      return Format.JoinRow(values);
    }
    public static string JoinLines(IEnumerable<string> lines) => string.Join("\n", lines.Where(line => line != ""));
    public static string JoinRow(IEnumerable<string> lines) => string.Join(", ", lines.Where(line => line != ""));
  }
}