using ChessTaskEFSignalR.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace ChessTaskEFSignalR.Services
{
    public class Play : IPlay
    {
        private int _counter;
        private int _prevStep;
        private double _interval;

        private IPlayState _playState;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IRepository<Step> _repository;

        private readonly object _balanceLock = new object();


        public Play(IRepository<Step> repository, IHubContext<GameHub> hubContext, IPlayState playState)
        {
            _counter = 0;
            _prevStep = 0;
            _interval = 0.5;

            _repository = repository;
            _hubContext = hubContext;
            _playState = playState;

        }
        public void StartGame(int coordX, int coordY, double timeInterval)
        {
            _playState.ChangeStateBegin();
            _interval = timeInterval;
            _repository.CorrectDataBase(coordX, coordY);
            PlayState.EventChangeState += CheckStateGame;
        }
        public async Task CheckStateGame(States state)
        {
            if (state == States.Go)
            {
                _counter = 0;
                _prevStep = 0;
                await Task.Run(() => ProceedGame(state), _playState.Cts.Token);
            }
            else if (state == States.Pause)
            {
                await Task.Delay(TimeSpan.FromSeconds(_interval), _playState.Cts.Token);
            }
            else if (state == States.Continue)
            {
                await Task.Run(() => ProceedGame(state), _playState.Cts.Token);
            }

        }
        private void SendMessage(int x, int y, int prevX, int prevY, string message)
        {
            _hubContext.Clients.All.SendAsync("Receive", x, y, prevX, prevY, message);
        }

        private void ProceedGame(States state)
        {
            lock (_balanceLock)
            {
                while (!_playState.Cts.Token.IsCancellationRequested && _counter < _repository.Count() && _playState.State == state)
                {
                    var item = _repository.GetElemByNumber(_counter) ?? _repository.GetElemFirst();
                    var prevItem = _repository.GetElemByNumber(_prevStep) ?? _repository.GetElemFirst();
                    SendMessage(item.X, item.Y, prevItem.X, prevItem.Y, $"Ход коня №{_counter}: ");/*{Thread.CurrentThread.ManagedThreadId} поток: */

                    _prevStep = _counter;
                    _counter++;
                    if (_playState.State == States.Pause || _playState.Cts.Token.IsCancellationRequested)
                        return;
                    Thread.Sleep(TimeSpan.FromSeconds(_interval));
                }
            }
        }
        
    }
}
