using Microsoft.AspNetCore.Components;
using projeto.db;
using projeto.Models;

namespace projeto.Components
{
    public class HomeBase : ComponentBase
    {
        [Inject]
        Consultas consultas { get; set; }
        public IEnumerable<Clima> clima = [];
        public int i = 0;
        protected override async Task OnAfterRenderAsync(bool first)
        {
            if (first)
            {
                while (true)
                {
                    // clima = await consultas.GetData();
                    // Console.WriteLine(i);
                    // i++;
                    // StateHasChanged();
                    // await Task.Delay(5000);
                }
            }
        }
    }
}