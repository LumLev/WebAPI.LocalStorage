using Microsoft.JSInterop;

namespace WebAPI.LocalStorage
{
   
    public class TheLocalStorage : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public TheLocalStorage(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/WebAPI.LocalStorage/TheLocalStorage.js").AsTask());
        }

        public async ValueTask SetItem(string key, string value)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("SetItem", key, value);
        }


        public async ValueTask<string> GetItem(string key)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("GetItem", key);
        }

        public async ValueTask RemoveItem(string key)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("RemoveItem", key);
        }

        public async ValueTask Clear()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("Clear");
        }


        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}