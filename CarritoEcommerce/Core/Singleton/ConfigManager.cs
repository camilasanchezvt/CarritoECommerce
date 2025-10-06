namespace Etapa2Envios.Core.Singleton
{
    public sealed class ConfigManager
    {
        private static readonly ConfigManager _instance = new ConfigManager();
        private ConfigManager() { }

        public static ConfigManager Instance => _instance;

        public decimal EnvioGratisDesde { get; set; } = 50000m;
        public decimal IVA { get; set; } = 0.21m; // 21% por defecto
    }
}
