public struct ScreenSize
{
    public ScreenSize(uint width, uint height, uint marginTop, uint marginLeft, uint totalWidth, uint totalHeight)
    {
        Width = width;
        Height = height;
        MarginTop = marginTop;
        MarginLeft = marginLeft;
        TotalWidth = totalWidth;
        TotalHeight = totalHeight;
    }

    public uint Width;
    public uint Height;
    public uint MarginTop;
    public uint MarginLeft;
    public uint TotalWidth;
    public uint TotalHeight;
}