using UserManagementAPI.Entities;

namespace UserManagementAPI.Services
{
    public class RequestCounterService
    {
        private readonly Dictionary<string, List<RequestLogData>> _requestCounts = [];

        public void IncrementCount(string method, RequestLogData data)
        {
            if (_requestCounts.ContainsKey(method))
            {
                var existingData = _requestCounts[method].FirstOrDefault(d => d.Uri == data.Uri);
                if (existingData != null)
                {
                    existingData.Count += data.Count;
                }
                else
                {
                    _requestCounts[method].Add(data);
                }
            }
            else
            {
                _requestCounts[method] = new List<RequestLogData> { data };
            }
        }

        public int GetCount(string method)
        {
            return _requestCounts.ContainsKey(method) ? _requestCounts[method].Count : 0;
        }

        public IReadOnlyDictionary<string, List<RequestLogData>> GetAllCount()
        {
            return _requestCounts;
        }
    }
}
