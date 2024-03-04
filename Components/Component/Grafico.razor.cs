using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using projeto.db;
using projeto.Models;

namespace projeto.Components.Component
{
    public class GraficoBase : ComponentBase
    {
        [Inject]
        private Consultas consultas { get; set; }
        public LineChartDataset temperatura;
        public LineChartDataset umidade;
        public List<double> temperaturaData = [];
        public List<double> umidadeData = [];
        public LineChart lineChart = default!;
        public LineChartOptions lineChartOptions = default!;
        public ChartData chartData = default!;

        private List<string> Labels()
        {
            List<string> lista = new List<string>();
            for (int i = 0; i <= 50; i += 5)
            {
                lista.Add($"{i}");
            }
            return lista;
        }
        protected override void OnInitialized()
        {
            chartData = new ChartData
            {
                Datasets = DataSets(temperaturaData, umidadeData),
                Labels = Labels(),
            };
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await lineChart.InitializeAsync(chartData, ChartOptions());
                while (true)
                {
                    try
                    {
                        if (temperaturaData.Count > 10 || umidadeData.Count > 10)
                        {
                            temperaturaData.RemoveAt(0);
                            umidadeData.RemoveAt(0);
                            consultas.DeletarDados();
                        }

                        Clima? clima = await consultas.GetClima();
                        temperaturaData.Add(clima.Temperatura);
                        umidadeData.Add(clima.Umidade);

                        chartData = new ChartData
                        {
                            Datasets = DataSets(temperaturaData, umidadeData),
                            Labels = Labels(),
                        };

                        await lineChart.UpdateAsync(chartData, ChartOptions());
                    }
                    catch
                    {
                        continue;
                    }
                    await InvokeAsync(StateHasChanged);

                    await Task.Delay(5000);
                    await base.OnAfterRenderAsync(firstRender);
                }
            }
        }
        List<IChartDataset> DataSets(List<double> dataTemperatura, List<double> dataUmidade)
        {
            var colors = ColorBuilder.CategoricalTwelveColors;
            var datasets = new List<IChartDataset>();
            temperatura = new LineChartDataset
            {
                Label = "Temperatura",
                BackgroundColor = new List<string> { colors[0] },
                Data = dataTemperatura,
                BorderColor = new List<string> { colors[0] },
                BorderWidth = new List<double> { 2 },
                HoverBorderWidth = new List<double> { 4 },
                PointBackgroundColor = new List<string> { colors[0] },
                PointRadius = new List<int> { 0 },
                PointHoverRadius = new List<int> { 4 }
            };

            umidade = new LineChartDataset
            {
                Label = "Umidade",
                BackgroundColor = new List<string> { colors[1] },
                Data = dataUmidade,
                BorderColor = new List<string> { colors[1] },
                BorderWidth = new List<double> { 2 },
                HoverBorderWidth = new List<double> { 4 },
                PointBackgroundColor = new List<string> { colors[1] },
                PointRadius = new List<int> { 0 },
                PointHoverRadius = new List<int> { 4 }
            };
            datasets.Add(temperatura);
            datasets.Add(umidade);
            return datasets;
        }
        LineChartOptions ChartOptions()
        {
            lineChartOptions = new();
            lineChartOptions.Responsive = true;
            lineChartOptions.Interaction = new Interaction { Mode = InteractionMode.Index };

            lineChartOptions.Scales.Y!.Max = 100;
            lineChartOptions.Scales.X!.Max = 1000;

            lineChartOptions.Scales.X!.Title!.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            lineChartOptions.Scales.X.Title.Display = true;

            lineChartOptions.Plugins.Title!.Text = "Clima";
            lineChartOptions.Plugins.Title.Display = true;
            return lineChartOptions;
        }

    }
}