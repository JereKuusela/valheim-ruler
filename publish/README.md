# Ruler

Adds a new command for measuring and visualizing distances.

Install on the client (modding [guide](https://youtu.be/L9ljm2eKLrk)).

## Usage

- `ruler`: Spawns the ruler object at your current position. If used at the same position, the ruler is removed.
- `ruler [radius]`: Spawns the ruler with a custom radius. This can be used to visualize the distance.

For example `bind o ruler` to quickly use the ruler with the O key. Or `bind o ruler 10` if you want to visualize a radius of 10 meters.

The distance is shown on the left side of the HUD. Following information is shown:

- Distance on each axis.
- Total distance.
- Distance on the XZ plane (without up / down direction).

## Configuration

- Color (default: `red`): Color of the ruler object.
- Draw 3d (default: `true`): Whether to draw the ruler object as a sphere or as a circle.
- Draw axis (default `true`): Whether to draw axis lines on the ruler object.
- Line width (default: `10`): Size of the ruler object lines.
- Show text (default: `true`): Whether to show text on the HUD.

## Credits

Thanks for Azumatt for creating the mod icon!

Sources: [GitHub](https://github.com/JereKuusela/valheim-ruler)

Donations: [Buy me a computer](https://www.buymeacoffee.com/jerekuusela)
