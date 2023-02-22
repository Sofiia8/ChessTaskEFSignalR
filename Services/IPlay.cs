namespace ChessTaskEFSignalR.Services
{
    public interface IPlay
    {
        public void StartGame(int coordX, int coordY, double timeInterval);
    }
}
