using RecompOne.Runtime.Assets.Textures;

namespace Recompiled;

public static class TextureSourcesPatch
{
    public static void Register()
    {
        TextureSources.Declare("alucard", 0x200, 0x100, 0x40, 0x100, exclusive: true, snap: true);
        TextureSources.Declare("alucard", 0x000, 0x100, 0x40, 0x100, exclusive: true, snap: true);
        TextureSources.Declare("tileset", 0x200, 0x000, 0x200, 0x100, 16, 16);
        TextureSources.Declare("entities", 0x040, 0x100, 0x1C0, 0x100);
        TextureSources.Declare("weapons", 0x240, 0x100, 0x40, 0x100);
        TextureSources.Declare("items", 0x280, 0x180, 0x20, 0x40, snap: true);
        TextureSources.Declare("familiars", 0x2C0, 0x100, 0x40, 0x100);
        TextureSources.Declare("hud", 0x3C0, 0x100, 0x40, 0x100);
        TextureSources.Declare("dra", 0x200, 0x100, 0x200, 0x100);
    }
}
