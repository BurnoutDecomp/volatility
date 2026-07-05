using System.Reflection;
using Volatility.Resources;
using Volatility.Utilities;

namespace Volatility.Operations.Resources;

public static class TextureFormatConverter
{
    public static readonly Dictionary<GPUTEXTUREFORMAT, D3DFORMAT> X360toTUBMapping = new()
    {
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT1, D3DFORMAT.D3DFMT_DXT1 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT2_3, D3DFORMAT.D3DFMT_DXT3 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT4_5, D3DFORMAT.D3DFMT_DXT5 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8, D3DFORMAT.D3DFMT_A8 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_16_16_16_16, D3DFORMAT.D3DFMT_A16B16G16R16 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_8_8_8, D3DFORMAT.D3DFMT_A8R8G8B8 },
    };

    public static readonly Dictionary<GPUTEXTUREFORMAT, DXGI_FORMAT> X360toBPRMapping = new()
    {
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT1, DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT2_3, DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT4_5, DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8, DXGI_FORMAT.DXGI_FORMAT_A8_UNORM },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_16_16_16_16, DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UNORM },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_8_8_8, DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM },
    };

    public static readonly Dictionary<D3DFORMAT, DXGI_FORMAT> TUBtoBPRMapping = new()
    {
        { D3DFORMAT.D3DFMT_DXT1, DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM },
        { D3DFORMAT.D3DFMT_DXT3, DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM },
        { D3DFORMAT.D3DFMT_DXT5, DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM },
        { D3DFORMAT.D3DFMT_A8, DXGI_FORMAT.DXGI_FORMAT_A8_UNORM },
        { D3DFORMAT.D3DFMT_A8B8G8R8, DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM },
        { D3DFORMAT.D3DFMT_A8R8G8B8, DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM },
    };

    public static readonly Dictionary<DXGI_FORMAT, D3DFORMAT> BPRtoTUBMapping = new()
    {
        { DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM, D3DFORMAT.D3DFMT_DXT1 },
        { DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM, D3DFORMAT.D3DFMT_DXT3 },
        { DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM, D3DFORMAT.D3DFMT_DXT5 },
        { DXGI_FORMAT.DXGI_FORMAT_A8_UNORM, D3DFORMAT.D3DFMT_A8 },
        { DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM, D3DFORMAT.D3DFMT_A8B8G8R8 },
    };

    public static readonly Dictionary<CELL_GCM_COLOR_FORMAT, D3DFORMAT> PS3toTUBMapping = new()
    {
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT1, D3DFORMAT.D3DFMT_DXT1 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT23, D3DFORMAT.D3DFMT_DXT3 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT45, D3DFORMAT.D3DFMT_DXT5 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_B8, D3DFORMAT.D3DFMT_A8 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8, D3DFORMAT.D3DFMT_A8R8G8B8 },
    };

    public static readonly Dictionary<CELL_GCM_COLOR_FORMAT, GPUTEXTUREFORMAT> PS3toX360Mapping = new()
    {
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT1, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT1 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT23, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT2_3 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT45, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT4_5 },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_B8, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_B },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_8_8_8 },
    };

    public static readonly Dictionary<D3DFORMAT, GPUTEXTUREFORMAT> TUBtoX360Mapping = new()
    {
        { D3DFORMAT.D3DFMT_DXT1, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT1 },
        { D3DFORMAT.D3DFMT_DXT3, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT2_3 },
        { D3DFORMAT.D3DFMT_DXT5, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT4_5 },
    };

    public static readonly Dictionary<GPUTEXTUREFORMAT, CELL_GCM_COLOR_FORMAT> X360toPS3Mapping = new()
    {
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT1, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT1 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT2_3, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT23 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT4_5, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT45 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_B, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_B8 },
        { GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_8_8_8, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8 },
    };

    public static readonly Dictionary<CELL_GCM_COLOR_FORMAT, DXGI_FORMAT> PS3toBPRMapping = new()
    {
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT1, DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT23, DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT45, DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_B8, DXGI_FORMAT.DXGI_FORMAT_A8_UNORM },
        { CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8, DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM }
    };

    public static readonly Dictionary<DXGI_FORMAT, CELL_GCM_COLOR_FORMAT> BPRtoPS3Mapping = new()
    {
        { DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT1 },
        { DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT23 },
        { DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT45 },
        { DXGI_FORMAT.DXGI_FORMAT_A8_UNORM, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_B8 },
        { DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8 },
        { DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8 },
    };

    public static readonly Dictionary<D3DFORMAT, CELL_GCM_COLOR_FORMAT> TUBtoPS3Mapping = new()
    {
        { D3DFORMAT.D3DFMT_DXT1, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT1 },
        { D3DFORMAT.D3DFMT_DXT3, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT23 },
        { D3DFORMAT.D3DFMT_DXT5, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_COMPRESSED_DXT45 },
        { D3DFORMAT.D3DFMT_A8, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_B8 },
        { D3DFORMAT.D3DFMT_A8R8G8B8, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8 },
        { D3DFORMAT.D3DFMT_A8B8G8R8, CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8 },
    };

    public static readonly Dictionary<DXGI_FORMAT, GPUTEXTUREFORMAT> BPRtoX360Mapping = new()
    {
        { DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT1 },
        { DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT2_3 },
        { DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM, GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_DXT4_5 },
    };

    public static void CopyProperties(TextureBase source, TextureBase destination)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        Type srcType = source.GetType();
        Type dstType = destination.GetType();

        Type typeToReflect = srcType == dstType
            ? srcType
            : typeof(TextureBase);

        IEnumerable<PropertyInfo> props = typeToReflect
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead
                     && p.CanWrite
                     && p.GetIndexParameters().Length == 0);

        foreach (PropertyInfo prop in props)
        {
            object? value = prop.GetValue(source);
            prop.SetValue(destination, value);
        }
    }

    public static void ConvertFormat(
        TextureBase sourceTexture,
        TextureBase destinationTexture,
        string localSourceFormat,
        string localDestinationFormat,
        out bool flipEndian,
        out int sourceFormatIndex,
        out int destinationFormatIndex,
        Action<string> warningLogger)
    {
        flipEndian = false;
        sourceFormatIndex = 0;
        destinationFormatIndex = 0;

        switch ((sourceTexture, destinationTexture))
        {
            case (TexturePS3 ps3, TextureX360 x360):
                PS3toX360Mapping.TryGetValue(ps3.Format, out GPUTEXTUREFORMAT ps3x360Format);
                x360.Format.DataFormat = ps3x360Format;
                x360.Format.Endian = GPUENDIAN.GPUENDIAN_NONE;
                flipEndian = false;
                sourceFormatIndex = (int)ps3.Format;
                destinationFormatIndex = (int)ps3x360Format;
                if (ps3x360Format == GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_1_REVERSE)
                    warningLogger($"Destination texture format is {ps3x360Format}! (Source is {ps3.Format})");
                break;
            case (TextureX360 x360, TexturePS3 ps3):
                X360toPS3Mapping.TryGetValue(x360.Format.DataFormat, out CELL_GCM_COLOR_FORMAT x360ps3Format);
                ps3.Format = x360ps3Format;
                flipEndian = false;
                sourceFormatIndex = (int)x360.Format.DataFormat;
                destinationFormatIndex = (int)x360ps3Format;
                if (x360ps3Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_INVALID)
                    warningLogger($"Destination texture format is {x360ps3Format}! (Source is {x360.Format.DataFormat})");
                break;
            case (TextureBPR bprsrc, TextureBPR bprdst):
                bprdst.Format = bprsrc.Format;
                sourceFormatIndex = (int)bprsrc.Format;
                destinationFormatIndex = sourceFormatIndex;
                break;
            case (TexturePS3 ps3, TextureBPR bpr):
                PS3toBPRMapping.TryGetValue(ps3.Format, out DXGI_FORMAT ps3bprFormat);
                bpr.Format = ps3bprFormat;
                flipEndian = true;
                sourceFormatIndex = (int)ps3.Format;
                destinationFormatIndex = (int)ps3bprFormat;
                if (ps3bprFormat == DXGI_FORMAT.DXGI_FORMAT_UNKNOWN)
                    warningLogger($"Destination texture format is {ps3bprFormat}! (Source is {ps3.Format})");
                break;
            case (TextureX360 x360, TextureBPR bpr):
                X360toBPRMapping.TryGetValue(x360.Format.DataFormat, out DXGI_FORMAT x360bprFormat);
                bpr.Format = x360bprFormat;
                flipEndian = true;
                sourceFormatIndex = (int)x360.Format.DataFormat;
                destinationFormatIndex = (int)x360bprFormat;
                if (x360bprFormat == DXGI_FORMAT.DXGI_FORMAT_UNKNOWN)
                    warningLogger($"Destination texture format is {x360bprFormat}! (Source is {x360.Format.DataFormat})");
                break;
            case (TextureBPR bpr, TexturePS3 ps3):
                BPRtoPS3Mapping.TryGetValue(bpr.Format, out CELL_GCM_COLOR_FORMAT bprps3format);
                ps3.Format = bprps3format;
                flipEndian = true;
                sourceFormatIndex = (int)bpr.Format;
                destinationFormatIndex = (int)bprps3format;
                if (bprps3format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_INVALID)
                    warningLogger($"Destination texture format is {bprps3format}! (Source is {bpr.Format})");
                break;
            case (TextureBPR bpr, TextureX360 x360):
                BPRtoX360Mapping.TryGetValue(bpr.Format, out GPUTEXTUREFORMAT bprx360Format);
                x360.Format.DataFormat = bprx360Format;
                flipEndian = true;
                sourceFormatIndex = (int)bpr.Format;
                destinationFormatIndex = (int)bprx360Format;
                if (bprx360Format == GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_1_REVERSE)
                    warningLogger($"Destination texture format is {bprx360Format}! (Source is {bpr.Format})");
                break;
            // D3D9 (TUB + Decomp)
            case (TexturePC tub, TextureDecomp decomp):
                decomp.Format = tub.Format;
                sourceFormatIndex = (int)tub.Format;
                destinationFormatIndex = (int)tub.Format;
                break;
            case (TextureDecomp decomp, TexturePC tub):
                tub.Format = decomp.Format;
                sourceFormatIndex = (int)decomp.Format;
                destinationFormatIndex = (int)decomp.Format;
                break;

            // D3D9 -> other platforms
            case (TextureD3D9Base d3d9, TextureBPR bpr):
                TUBtoBPRMapping.TryGetValue(d3d9.Format, out DXGI_FORMAT d3d9bprFormat);
                bpr.Format = d3d9bprFormat;
                sourceFormatIndex = (int)d3d9.Format;
                destinationFormatIndex = (int)d3d9bprFormat;
                if (d3d9bprFormat == DXGI_FORMAT.DXGI_FORMAT_UNKNOWN)
                    warningLogger($"Destination texture format is {d3d9bprFormat}! (Source is {d3d9.Format})");
                break;
            case (TextureD3D9Base d3d9, TextureX360 x360):
                TUBtoX360Mapping.TryGetValue(d3d9.Format, out GPUTEXTUREFORMAT d3d9x360Format);
                x360.Format.DataFormat = d3d9x360Format;
                flipEndian = true;
                sourceFormatIndex = (int)d3d9.Format;
                destinationFormatIndex = (int)d3d9x360Format;
                if (d3d9x360Format == GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_1_REVERSE)
                    warningLogger($"Destination texture format is {d3d9x360Format}! (Source is {d3d9.Format})");
                break;
            case (TextureD3D9Base d3d9, TexturePS3 ps3):
                TUBtoPS3Mapping.TryGetValue(d3d9.Format, out CELL_GCM_COLOR_FORMAT d3d9ps3Format);
                ps3.Format = d3d9ps3Format;
                flipEndian = true;
                sourceFormatIndex = (int)d3d9.Format;
                destinationFormatIndex = (int)d3d9ps3Format;
                if (d3d9ps3Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_INVALID)
                    warningLogger($"Destination texture format is {d3d9ps3Format}! (Source is {d3d9.Format})");
                break;

            // other platforms -> D3D9
            case (TextureX360 x360, TextureD3D9Base d3d9):
                X360toTUBMapping.TryGetValue(x360.Format.DataFormat, out D3DFORMAT x360d3d9Format);
                d3d9.Format = x360d3d9Format;
                flipEndian = true;
                sourceFormatIndex = (int)x360.Format.DataFormat;
                destinationFormatIndex = (int)x360d3d9Format;
                if (x360d3d9Format == D3DFORMAT.D3DFMT_UNKNOWN)
                    warningLogger($"Destination texture format is {x360d3d9Format}! (Source is {x360.Format.DataFormat})");
                break;
            case (TexturePS3 ps3, TextureD3D9Base d3d9):
                PS3toTUBMapping.TryGetValue(ps3.Format, out D3DFORMAT ps3d3d9Format);
                d3d9.Format = ps3d3d9Format;
                flipEndian = true;
                sourceFormatIndex = (int)ps3.Format;
                destinationFormatIndex = (int)ps3d3d9Format;
                if (ps3d3d9Format == D3DFORMAT.D3DFMT_UNKNOWN)
                    warningLogger($"Destination texture format is {ps3d3d9Format}! (Source is {ps3.Format})");
                break;
            case (TextureBPR bpr, TextureD3D9Base d3d9):
                BPRtoTUBMapping.TryGetValue(bpr.Format, out D3DFORMAT bprd3d9Format);
                d3d9.Format = bprd3d9Format;
                sourceFormatIndex = (int)bpr.Format;
                destinationFormatIndex = (int)bprd3d9Format;
                if (bprd3d9Format == D3DFORMAT.D3DFMT_UNKNOWN)
                    warningLogger($"Destination texture format is {bprd3d9Format}! (Source is {bpr.Format})");
                break;
            default:
                throw new NotImplementedException($"Conversion technique {localSourceFormat} > {localDestinationFormat} is not yet implemented.");
        }
    }

    public static bool TryConvertTexture(TextureBase srcTexture, TextureBase destTexture, byte[] bitmap, string outPath)
    {
        switch (srcTexture, destTexture)
        {
            case (TexturePS3 ps3, TextureBPR bpr):
                if (ps3.Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8)
                {
                    if (bpr.Format == DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM)
                    {
                        DDSTextureUtilities.A8R8G8B8toR8G8B8A8(bitmap, ps3.Width, ps3.Height, ps3.MipmapLevels);
                        break;
                    }

                    if (bpr.Format == DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM)
                    {
                        DDSTextureUtilities.A8R8G8B8toB8G8R8A8(bitmap, ps3.Width, ps3.Height, ps3.MipmapLevels);
                        break;
                    }
                }
                bitmap = Array.Empty<byte>();
                return false;
            // D3D9-family (TUB TexturePC + Decomp) share the same D3DFORMAT-keyed bitmap paths.
            case (TexturePS3 ps3, TextureD3D9Base d3d9):
                if (ps3.Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8)
                {
                    if (d3d9.Format == D3DFORMAT.D3DFMT_A8R8G8B8)
                    {
                        break;
                    }

                    if (d3d9.Format == D3DFORMAT.D3DFMT_A8B8G8R8)
                    {
                        DDSTextureUtilities.A8R8G8B8toA8B8G8R8(bitmap, ps3.Width, ps3.Height, ps3.MipmapLevels);
                        break;
                    }
                }
                bitmap = Array.Empty<byte>();
                return false;
            case (TexturePS3 ps3, TextureX360 x360):
                if (ps3.Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8
                && x360.Format.DataFormat == GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_8_8_8)
                {
                    break;
                }
                bitmap = Array.Empty<byte>();
                return false;
            case (TextureD3D9Base d3d9, TextureBPR bpr):
                if (d3d9.Format == D3DFORMAT.D3DFMT_A8R8G8B8
                && bpr.Format == DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM)
                    DDSTextureUtilities.A8R8G8B8toB8G8R8A8(bitmap, destTexture.Width, destTexture.Height, destTexture.MipmapLevels);
                if (d3d9.Format == D3DFORMAT.D3DFMT_A8B8G8R8
                && bpr.Format == DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM)
                    DDSTextureUtilities.A8B8G8R8toB8G8R8A8(bitmap, destTexture.Width, destTexture.Height, destTexture.MipmapLevels);
                break;
            case (TextureBPR bpr, TexturePS3 ps3):
                if (ps3.Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8)
                {
                    if (bpr.Format == DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM)
                    {
                        DDSTextureUtilities.R8G8B8A8toA8R8G8B8(bitmap, bpr.Width, bpr.Height, bpr.MipmapLevels);
                        bitmap = PS3TextureUtilities.EncodePS3A8R8G8B8(bitmap, bpr.Width, bpr.Height, bpr.MipmapLevels);
                        break;
                    }

                    if (bpr.Format == DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM)
                    {
                        DDSTextureUtilities.B8G8R8A8toA8R8G8B8(bitmap, bpr.Width, bpr.Height, bpr.MipmapLevels);
                        bitmap = PS3TextureUtilities.EncodePS3A8R8G8B8(bitmap, bpr.Width, bpr.Height, bpr.MipmapLevels);
                        break;
                    }
                }
                bitmap = Array.Empty<byte>();
                return false;
            case (TextureD3D9Base d3d9, TexturePS3 ps3):
                if (ps3.Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8)
                {
                    if (d3d9.Format == D3DFORMAT.D3DFMT_A8R8G8B8)
                    {
                        bitmap = PS3TextureUtilities.EncodePS3A8R8G8B8(bitmap, d3d9.Width, d3d9.Height, d3d9.MipmapLevels);
                        break;
                    }

                    if (d3d9.Format == D3DFORMAT.D3DFMT_A8B8G8R8)
                    {
                        DDSTextureUtilities.A8B8G8R8toA8R8G8B8(bitmap, d3d9.Width, d3d9.Height, d3d9.MipmapLevels);
                        bitmap = PS3TextureUtilities.EncodePS3A8R8G8B8(bitmap, d3d9.Width, d3d9.Height, d3d9.MipmapLevels);
                        break;
                    }
                }
                bitmap = Array.Empty<byte>();
                return false;
            case (TextureX360 x360, TexturePS3 ps3):
                if (ps3.Format == CELL_GCM_COLOR_FORMAT.CELL_GCM_TEXTURE_A8R8G8B8
                && x360.Format.DataFormat == GPUTEXTUREFORMAT.GPUTEXTUREFORMAT_8_8_8_8)
                {
                    bitmap = PS3TextureUtilities.EncodePS3A8R8G8B8(bitmap, x360.Width, x360.Height, x360.MipmapLevels);
                    break;
                }
                bitmap = Array.Empty<byte>();
                return false;
            // --- BP-Decomp (our x64 PC) bitmap conversions; mirror the TUB cases (same D3DFORMAT). ---
            // X360 -> Decomp: the detiled X360 pixel data is still stored as 16-bit BIG-ENDIAN words
            // (GPUENDIAN 8in16). Platform-4 (Decomp) bundles are little-endian -- like the LE resource
            // structures the rest of the port already produces -- so the pixel blob must be byte-swapped
            // too. For DXT, writing it through un-swapped renders as full-white coverage + colour-shifted
            // blocks (the X360 DXT byte order read as PC DXT). [This case previously didn't exist; the
            // blob fell through to default + was written as-is -> the byte-order bug.]
            case (TextureX360 x360, TextureDecomp decomp):
                if (decomp.Format is D3DFORMAT.D3DFMT_DXT1 or D3DFORMAT.D3DFMT_DXT3 or D3DFORMAT.D3DFMT_DXT5)
                {
                    X360TextureUtilities.SwapEndian8in16(bitmap);
                    break;
                }
                // Uncompressed X360 surfaces: 8bpp (A8) needs no swap; 32bpp ARGB would need an 8in32 +
                // channel pass -- add when a non-DXT X360 source is actually ported (DXT is what the font
                // atlas uses). Write the detiled blob as-is for those for now.
                break;
            default:
                bitmap = Array.Empty<byte>();
                return false;
        }
        File.WriteAllBytes(outPath, bitmap);
        return true;
    }
}
