public class DungGeneratorTest
{
    [Fact]
    public void GetDrawingStartPointTest()
    {
        var service = new DungGeneratorService(logger: null);

        var doorOffset = 3;

        var firstRoom = new DungRoomData(
            id: 0,
            topLeftCoords: new Point(0, 0),
            size: new Size(10, 15),
            doorCoord: new Point(10, 10)
        );

        var newRoomSize = new Size(20, 30);

        for (var i = 0; i < 1000; i++)
        {
            var startCoord = service.GetDrawingStartPoint((int)newRoomSize.Height);
            Assert.True(startCoord.Y > startCoord.Y - newRoomSize.Height - (2 * doorOffset));
            Assert.True(startCoord.Y > startCoord.Y - doorOffset);
        }
    }
}
