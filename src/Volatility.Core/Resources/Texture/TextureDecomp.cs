namespace Volatility.Resources;

// TextureDecomp - the x64 PC decomp texture resource.
// For the most part it is "TUB but x64" 
//   off   field                    notes
//   0x00  mpD3DTexture (uint64_t)  runtime D3D handle; written 0
//   0x08  mpTextureDataStruct      runtime; 0
//   0x10  mpReserved8              runtime; 0
//   0x18  flags (4 bytes)          0
//   0x1C  miFormat (int32_t)       raw Windows D3DFORMAT (DXT stored as its FOURCC)
//   0x20  width (uint16_t)
//   0x22  height (uint16_t)
//   0x24  depth (uint8_t)
//   0x25  mip levels (uint8_t)
//   0x26  flags (uint16_t)         0
//   = 40 (0x28) bytes of fields
[ResourceRegistration(RegistrationPlatforms.Decomp, PullAll = true)]
public class TextureDecomp : TextureD3D9Base
{
    public override Endian ResourceEndian => Endian.LE;
    public override Platform ResourcePlatform => Platform.Decomp;
    public override Arch ResourceArch => Volatility.Resources.Arch.x64;   // our format is always x64

    public TextureDecomp() : base() { }

    public override void WriteToStream(ResourceBinaryWriter writer, Endian endianness = Endian.Agnostic)
    {
        base.WriteToStream(writer, endianness);

        PushAll();

        writer.WritePointer(0UL, ResourceArch);   // mpD3DTexture
        writer.WritePointer(0UL, ResourceArch);   // mpTextureDataStruct
        writer.WritePointer(0UL, ResourceArch);   // mpReserved8
        writer.Write((uint)0);                    // mbFlagC..mbFlagF
        writer.Write(ToWindowsD3DFormat(Format)); // miFormat
        writer.Write(Width);
        writer.Write(Height);
        writer.Write((byte)Depth);
        writer.Write(MipmapLevels);
        writer.Write((ushort)0);                  // muFlags
        writer.Write(new byte[8]);                // Pad to 0x10
    }

    public override void ParseFromStream(ResourceBinaryReader reader, Endian endianness = Endian.Agnostic)
    {
        base.ParseFromStream(reader, endianness);

        reader.BaseStream.Seek(0x1C, SeekOrigin.Begin);   // skip 3x8-byte ptrs + 4 flag bytes
        Format = FromWindowsD3DFormat(reader.ReadUInt32());
        Width = reader.ReadUInt16();
        Height = reader.ReadUInt16();
        Depth = reader.ReadByte();
        MipmapLevels = reader.ReadByte();
        // trailing 2-byte muFlags ignored
    }

    // miFormat is written/read via the To/FromWindowsD3DFormat helpers
    public override void PushInternalFormat() { }
    public override void PullInternalFormat() { }

    // The header currently carries depth directly, dimension is inferred from it
    public override void PushInternalDimension() { }
    public override void PullInternalDimension()
    {
        _Dimension = Depth > 1 ? DIMENSION.DIMENSION_3D : DIMENSION.DIMENSION_2D;
    }

    // No usage-flag field in our header
    public override void PushInternalFlags() { }
}
