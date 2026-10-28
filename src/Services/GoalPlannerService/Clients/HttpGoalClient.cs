using System.Net;
using Aspirra.GoalPlannerService.Interfaces;

namespace Aspirra.GoalPlannerService.Clients
{
    public class HttpGoalClient : IGoalClient
    {
        private readonly HttpClient _httpClient;

        public HttpGoalClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> GoalExistsAsync(string goalId)
        {
            using var response = await _httpClient.GetAsync(
                $"api/goals/{Uri.EscapeDataString(goalId)}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            throw new HttpRequestException(
                $"Academic service returned status code {(int)response.StatusCode}.");
        }
    }
}
