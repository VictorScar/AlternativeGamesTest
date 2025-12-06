using System;
using System.Threading;
using System.Threading.Tasks;
using AlternativeGamesTest.UI.Base;

namespace AlternativeGamesTest.Scenario
{
    public static class UIClickableViewExtensions
    {
        public static async Task WaitForClickAsync(this UIClickableView view, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            Action handler = () => tcs.TrySetResult(true);
            view.onClick += handler;

            using (cancellationToken.Register(() => tcs.TrySetCanceled()))
            {
                try
                {
                    await tcs.Task;
                }
                finally
                {
                    view.onClick -= handler;
                }
            }
        }
    }
}