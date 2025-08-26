/**
 * Model for health status response.
 */
export interface HealthStatusResponse {
  /**
   * Service status.
   */
  status: string;

  /**
   * Current UTC timestamp (ISO string).
   */
  timestamp: string; // Use string for ISO date format
}