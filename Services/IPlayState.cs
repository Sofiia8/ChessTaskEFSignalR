using System.Threading.Tasks;
using System.Threading;
using System;

namespace ChessTaskEFSignalR.Services
{
    public interface IPlayState
    {
        static public event Func<States, Task> EventChangeState;
        public States State { get; set; }
        public CancellationTokenSource Cts { get; set; }
        public void ChangeStateBegin();
        public void ChangeStatePause();
        public void ChangeStateGo();
        public void ChangeStateClose();
    }
}
