# Ruler

Adds a new command for measuring and visualizing distances.

# Manual Installation

1. Install the [BepInExPack Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download the latest zip.
3. Extract it in the \<GameDirectory\>\BepInEx\plugins\ folder.
4. Optionally also install the [Configuration manager](https://github.com/BepInEx/BepInEx.ConfigurationManager/releases/tag/v16.4) if you want to change the line color or width.

# Usage

- `ruler`: Spawns the ruler object at your current position. If used at the same position, the object is removed.
- `ruler [radius]`: Spawns the ruler with a custom radius. This can be used to visualize the distance.

For example `bind o ruler` to quickly use the ruler with the O key. Or `bind o ruler 10` if you want to visualize a radius of 10 meters.

The distance is shown on the left side of the HUD. Following information is shown:

- Distance on each axis.
- Total distance.
- Distance on the XZ plane (without up / down direction).

# Changelog

- v1.0:
	- Initial release.

Thanks for Azumatt for creating the mod icon!