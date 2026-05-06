public struct DungRoomData
{
    public int Id { get; }
    public Point TopLeftCoords { get; private set; }
    public Size Size { get; private set; }
    public Point CenterCoords { get; private set; }

    public DungRoomData(int id, Point topLeftCoords, Size size)
    {
        Id = id;
        TopLeftCoords = topLeftCoords;
        Size = size;
        CenterCoords = new((int)Size.Width / 2, (int)Size.Height / 2);
    }
}
