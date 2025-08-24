namespace MeowCore.Models
{
    /// <summary>
    /// Model for health status response.
    /// </summary>
    public class HealthStatusModel
    {
        /// <summary>
        /// Service status.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Current UTC timestamp.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}