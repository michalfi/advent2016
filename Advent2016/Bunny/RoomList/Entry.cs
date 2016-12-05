namespace Advent2016.Bunny.RoomList
{
    public class Entry
    {
        public Entry(string encryptedName, int sectorId, string declaredChecksum)
        {
            EncryptedName = encryptedName;
            SectorId = sectorId;
            DeclaredChecksum = declaredChecksum;
        }

        public string EncryptedName { get; }

        public int SectorId { get; }

        public string DeclaredChecksum { get; }
    }
}
