using System;
using System.ComponentModel;


[Flags, Serializable]
public enum TerrainFlags
{
    [Description("Nothing")] NOTHING = 0,
    [Description("Can Walk On")] CAN_WALK_ON = 1 << 0,
    [Description("Water Source")] WATER_SOURCE = 1 << 1,
    [Description("Tree Growth")] TREE_GROWTH = 1 << 2,
    [Description("Everything")] EVERYTHING = ~0,
}
