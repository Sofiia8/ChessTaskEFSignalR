using System.Threading.Tasks;
using System.Threading;
using System;

namespace ChessTaskEFSignalR.Services
{
    public class PlayState : IPlayState
    {
        public static event Func<States, Task> EventChangeState;
        private States _state;
        public States State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value; EventChangeState?.Invoke(_state);
            }
        }
        public CancellationTokenSource Cts { get; set; }

        public PlayState()
        {
            ChangeStateBegin();
        }

        public void ChangeStateBegin()
        {
            Cts?.Dispose();
            State = States.Begin;
            Cts = new CancellationTokenSource();
        }
        public void ChangeStateGo()
        {
            if (State == States.Go)
            {
                State = States.Pause;
                State = States.Go;
            }
            else
                State = States.Go;
        }
        public void ChangeStatePause()
        {
            if (State == States.Go || State == States.Continue)
                State = States.Pause;
            else if (State == States.Pause)
                State = States.Continue;
        }
        public void ChangeStateClose()
        {
            Cts.Cancel();
            EventChangeState = null;
        }

    }
}
