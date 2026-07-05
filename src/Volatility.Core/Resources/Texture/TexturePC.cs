namespace Volatility.Resources;

[ResourceRegistration(RegistrationPlatforms.TUB, PullAll = true)]
public class TexturePC : TextureD3D9Base
{
    public override Endian ResourceEndian => Endian.LE;
    public override Platform ResourcePlatform => Platform.TUB;

    public readonly nint TextureDataPtr;      // Set at game runtime, 0
    public readonly nint TextureInterfacePtr; // Set at game runtime, 0
    public uint Unknown0;                       // Flags
    public readonly ushort MemoryClass = 1;     // D3DPool, Always 1
    public byte Unknown1;                       // Flags
    public byte Unknown2;                       // Flags
    private byte[] OutputFormat = new byte[4];  // Needs to be 4 bytes long
    public TEXTURETYPE TextureType;             // Dimension in BPR
    public byte Flags;                          // Flags

    public TexturePC() : base() { }

    public override void WriteToStream(ResourceBinaryWriter writer, Endian endianness = Endian.Agnostic)
    {
        base.WriteToStream(writer, endianness);

        PushAll(); // Need to determine if should be moved

        writer.WritePointer((ulong)TextureDataPtr, ResourceArch);
        writer.WritePointer((ulong)TextureInterfacePtr, ResourceArch);
        writer.Write(Unknown0);     // Unknown
        writer.Write(MemoryClass);
        writer.Write(Unknown1);     // Unknown
        writer.Write(Unknown2);     // Unknown
        writer.Write(OutputFormat);

        // Not validated!!
        // TODO: Validate correct variable size
        writer.Write(Width);
        writer.Write(Height);
        writer.Write((byte)Depth);
        writer.Write(MipmapLevels);

        writer.Write((byte)TextureType);
        writer.Write(Flags);
        writer.Write(new byte[4]);  // Padding
    }

    public override void ParseFromStream(ResourceBinaryReader reader, Endian endianness = Endian.Agnostic)
    {
        base.ParseFromStream(reader, endianness);

        reader.BaseStream.Seek(8, SeekOrigin.Begin);    // Skip over Data & Interface pointers
        Unknown0 = reader.ReadUInt32();
        reader.BaseStream.Seek(2, SeekOrigin.Current);  // Skip over MemoryClass
        Unknown1 = reader.ReadByte();
        Unknown2 = reader.ReadByte();
        OutputFormat = reader.ReadBytes(4);
        Width = reader.ReadUInt16();
        Height = reader.ReadUInt16();
        reader.BaseStream.Seek(1, SeekOrigin.Current);
        MipmapLevels = reader.ReadByte();
        TextureType = (TEXTURETYPE)reader.ReadByte();
        Flags = reader.ReadByte();
        // Skip reading 4 byte padding
    }

    public override void PushInternalFormat()
    {
        OutputFormat = BitConverter.GetBytes(ToWindowsD3DFormat(Format));
    }

    public override void PullInternalFormat()
    {
        Format = FromWindowsD3DFormat(BitConverter.ToUInt32(OutputFormat, 0));
    }

    public override void PushInternalFlags()
    {
        bool grOrWorld = (UsageFlags & (TextureBaseUsageFlags.WorldTexture | TextureBaseUsageFlags.GRTexture)) != 0;
        bool any = (UsageFlags & TextureBaseUsageFlags.AnyTexture) != 0;

        Unknown1 = Convert.ToByte(grOrWorld);
        Unknown2 = Convert.ToByte(any);
        Flags = (byte)(any ? (Flags | 0x08) : (Flags & ~0x08));
    }

    public override void PullInternalFlags()
    {
        // TODO: More accurate/efficient flag calcuation!

        TextureBaseUsageFlags f = UsageFlags & ~TextureBaseUsageFlags.AnyTexture;

        // Assuming Unknown 2 is probably world, and Unknown 1 is GR?
        if (Unknown2 != 0) f |= TextureBaseUsageFlags.WorldTexture;
        if (Unknown1 != 0) f |= TextureBaseUsageFlags.GRTexture;

        // We're just a prop in this cruel game of life
        if (Unknown1 == 0 && Unknown2 != 0 && Flags != 0) f |= TextureBaseUsageFlags.PropTexture;

        UsageFlags = f;

        // Run after, if directory exists then we use that instead
        base.PullInternalFlags();
    }

    public override void PushInternalDimension()
    {
        TextureType = Dimension switch
        {
            DIMENSION.DIMENSION_1D or DIMENSION.DIMENSION_CUBE => TEXTURETYPE.TEXTURETYPE_1D,
            DIMENSION.DIMENSION_3D => TEXTURETYPE.TEXTURETYPE_3D,
            _ => TEXTURETYPE.TEXTURETYPE_2D,
        };
    }

    public override void PullInternalDimension()
    {
        Dimension = TextureType switch    // Idk how to handle 1D textures, doc says 1D is cube in TUB
        {
            TEXTURETYPE.TEXTURETYPE_1D => DIMENSION.DIMENSION_CUBE,
            TEXTURETYPE.TEXTURETYPE_3D => DIMENSION.DIMENSION_3D,
            _ => DIMENSION.DIMENSION_2D,
        };
    }
}

public enum TEXTURETYPE : byte
{
    TEXTURETYPE_2D = 0,
    TEXTURETYPE_1D = 1,
    TEXTURETYPE_3D = 2,
    TEXTURETYPE_UNKNOWN2D = 3
}
