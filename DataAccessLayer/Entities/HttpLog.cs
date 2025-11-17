
namespace DataAccessLayer.Entities;

public class HttpLog
{
    public int Id { get; set; }

    public DateTime Timestamp { get; set; }

    public required string RequestMethod { get; set; }

    public required string RequestPath { get; set; }

    public required string QueryString { get; set; }

    public required string RequestHeaders { get; set; }

    public required string RequestBody { get; set; }

    public int ResponseStatusCode { get; set; }

    public required string ResponseHeaders { get; set; }

    public required string ResponseBody { get; set; }
}