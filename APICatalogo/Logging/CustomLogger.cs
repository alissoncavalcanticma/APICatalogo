namespace APICatalogo.Logging
{
    public class CustomLogger : ILogger
    {

        public readonly string _loggerName;
        public readonly CustomLoggerProviderConfiguration _loggerConfig;

        public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig) 
        {
            _loggerName = loggerName;
            _loggerConfig = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";

            EscreverTextoNoArquivo(mensagem);
        }

        public void EscreverTextoNoArquivo(string mensagem) {
            string caminhoArquivoLog = @"d:\dados\log\log.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true)) {
                try
                {
                    streamWriter.WriteLine(mensagem);
                    streamWriter.Close();
                }
                catch (Exception) {
                    throw;
                }
            }
        }
    }
}
