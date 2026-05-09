public struct DungRoomData
{
    public int Id { get; }
    public Point TopLeftCoords { get; private set; }
    public Size Size { get; private set; }
    public Point CenterCoords { get; private set; }
    public Point DoorCoord { get; set; }

    public DungRoomData(int id, Point topLeftCoords, Size size, Point? doorCoord = null)
    {
        Id = id;
        TopLeftCoords = topLeftCoords;
        Size = size;
        CenterCoords = new((int)Size.Width / 2, (int)Size.Height / 2);
        DoorCoord = doorCoord ?? new(0, 0);
    }
}
