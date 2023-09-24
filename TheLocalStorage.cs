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

        /// <summary>
        /// Set a data item in the Local Storage of the user's browser
        /// </summary>
        /// <param name="key">The key identifier of the data item</param>
        /// <param name="value">The value of the data item</param>
        public async ValueTask SetItem(string key, string value)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("SetItem", key, value);
        }




        /// <summary>
        /// Get a Local Storage item's value by the key identifier
        /// </summary>
        /// <param name="key">The key identifier of the local storage item</param>
        /// <returns>The value of the Local Storage item</returns>
        public async ValueTask<string> GetItem(string key)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("GetItem", key);
        }

  


        /// <summary>
        /// Remove a Local Storage item by the key identifier
        /// </summary>
        /// <param name="key">The key identifier of the local storage item</param>
        public async ValueTask RemoveItem(string key)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("RemoveItem", key);
        }

        /// <summary>
        /// Clear all items in the Local Storage
        /// </summary>
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