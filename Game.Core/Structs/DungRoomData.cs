public struct DungRoomData
{
    public Point TopLeftCoords { get; private set; }
    public Size Size { get; private set; }
    public Point CenterCoords { get; private set; }

    public DungRoomData(Point topLeftCoords, Size size)
    {
        TopLeftCoords = topLeftCoords;
        Size = size;
        CenterCoords = new((int)Size.Width / 2, (int)Size.Height / 2);
    }
}
