using Microsoft.AspNetCore.SignalR;
using ChessTaskEFSignalR.Services;

namespace ChessTaskEFSignalR
{
    public class GameHub : Hub
    {
        private readonly IPlayState _playState;

        public GameHub(IPlayState playState)
        {
            _playState = playState;
        }

        public void ChangeStateGo()
        {
            _playState.ChangeStateGo();
        }
        public void ChangeStatePause()
        {
            _playState.ChangeStatePause();
        }
        public void ChangeStateClose()
        {
            _playState.ChangeStateClose();
        }
    }
}
