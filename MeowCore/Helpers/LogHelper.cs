using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeowCore.Helpers
{
    /// <summary>
    /// Helper class for logging API requests and responses.
    /// Use this class to create, update, and log request/response information in controllers.
    /// </summary>
    /// <typeparam name="Req">Type of the request object.</typeparam>
    /// <typeparam name="Res">Type of the response object.</typeparam>
    public class LogHelper<Req, Res>
    {
        /// <summary>
        /// Unique identifier for the log entry.
        /// </summary>
        public Guid id { get; private set; } = Guid.Empty;

        /// <summary>
        /// Name of the controller handling the request.
        /// </summary>
        public string controller { get; private set; } = string.Empty;

        /// <summary>
        /// Name of the endpoint/action being called.
        /// </summary>
        public string endpoint { get; private set; } = string.Empty;

        /// <summary>
        /// List of endpoint parameters and their values.
        /// </summary>
        public List<Dictionary<string, string>> endPointParams { get; private set;} = new List<Dictionary<string, string>>();

        /// <summary>
        /// HTTP status code for the response.
        /// </summary>
        public int statusCode { get; private set; } = 0;

        /// <summary>
        /// Request object sent to the endpoint.
        /// </summary>
        public Req? request { get; private set; }

        /// <summary>
        /// Response object returned by the endpoint.
        /// </summary>
        public Res? response { get; private set; } 

        /// <summary>
        /// Exception message if an error occurred.
        /// </summary>
        public string exceptionMessage { get; private set; } = string.Empty;

        /// <summary>
        /// Inner exception message if available.
        /// </summary>
        public string exceptionInnerException { get; private set; } = string.Empty;

        /// <summary>
        /// Stack trace of the exception if available.
        /// </summary>
        public string exceptionStackTrace { get; private set; } = string.Empty;

        private ILogger _logger;

        /// <summary>
        /// Assigns the logger instance to use for logging.
        /// </summary>
        /// <typeparam name="T">Type of the logger (usually the controller).</typeparam>
        /// <param name="logger">Logger instance.</param>
        public void CreateLogger<T>(ILogger<T> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates and logs the initial entry for a request.
        /// </summary>
        /// <param name="controller">Controller name.</param>
        /// <param name="endpoint">Endpoint/action name.</param>
        /// <param name="endPointParams">Parameters for the endpoint.</param>
        /// <param name="request">Request object.</param>
        /// <returns>Initial LogHelper object.</returns>
        public LogHelper<Req, Res> GetInitialLog(string controller, string endpoint, List<Dictionary<string, string>> endPointParams, Req? request)
        {
            var logModel = new LogHelper<Req, Res>
            {
                id = Guid.NewGuid(),
                controller = controller,
                endpoint = endpoint,
                endPointParams = endPointParams,
                request = request
            };

            Audit(logModel);

            return logModel;
        }

        /// <summary>
        /// Updates and logs the entry with the response and status code.
        /// </summary>
        /// <param name="logModel">Existing log model.</param>
        /// <param name="response">Response object.</param>
        /// <param name="statusCode">HTTP status code.</param>
        public void LogUpdated(LogHelper<Req, Res> logModel, Res response, int statusCode) {
            logModel.response = response;
            logModel.statusCode = statusCode;
            Audit(logModel);
        }

        /// <summary>
        /// Updates and logs the entry with error details.
        /// </summary>
        /// <param name="logModel">Existing log model.</param>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="exceptionMessage">Exception message.</param>
        /// <param name="exceptionInnerException">Inner exception message.</param>
        /// <param name="exceptionStackTrace">Exception stack trace.</param>
        public void LogError(LogHelper<Req, Res> logModel, int statusCode, string exceptionMessage, string exceptionInnerException, string exceptionStackTrace, string? controller, string? endpoint)
        {
            logModel.controller = controller ?? "unknown";
            logModel.endpoint = endpoint ?? "unknown";
            logModel.statusCode = statusCode;
            logModel.exceptionMessage = exceptionMessage;
            logModel.exceptionInnerException = exceptionInnerException;
            logModel.exceptionStackTrace = exceptionStackTrace;
            
            Audit(logModel); 
        }

        /// <summary>
        /// Logs the serialized log entry using the assigned logger.
        /// </summary>
        /// <param name="log">LogHelper object to serialize and log.</param>
        private void Audit(LogHelper<Req, Res> log) {
            _logger.LogInformation(JsonConvert.SerializeObject(log));
        }
    }
}
