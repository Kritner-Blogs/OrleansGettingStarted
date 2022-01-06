using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kritner.Orleans.GettingStarted.GrainInterfaces;
using Orleans;

namespace Kritner.Orleans.GettingStarted.Grains;

public class ObservableManager : Grain, IObservableManager, IGrainMarker
{
    private GrainObserverManager<IObserverSample> _subsManager;

    public override async Task OnActivateAsync()
    {
        _subsManager = new GrainObserverManager<IObserverSample>();
        await base.OnActivateAsync();
    }

    public Task SendMessageToObservers(string message)
    {
        _subsManager.Notify(n => n.ReceiveMessage(message));
        return Task.CompletedTask;
    }

    public Task Subscribe(IObserverSample observer)
    {
        _subsManager.Subscribe(observer);
        return Task.CompletedTask;
    }

    public Task Unsubscribe(IObserverSample observer)
    {
        _subsManager.Unsubscribe(observer);
        return Task.CompletedTask;
    }
}