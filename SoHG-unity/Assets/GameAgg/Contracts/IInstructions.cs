using System;

namespace Sohg.GameAgg.Contracts
{
    public interface IInstructions
    {
        bool IsOpened();
        void Show(string text);
        void OnClose(Action onCloseAction);
    }
}
