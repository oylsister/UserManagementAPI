namespace UserManagementAPI.Entities
{
    public class RequestLogData
    {
        public required string Uri { get; set; } 
        public required int Count { get; set; } = 0;
    }
}
