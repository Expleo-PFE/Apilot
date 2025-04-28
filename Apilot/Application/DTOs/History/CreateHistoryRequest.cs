using Apilot.Application.DTOs.Request;

namespace Apilot.Application.DTOs.History;

public class CreateHistoryRequest
{
    
    public required RequestDto Requests { get; set; } 
    
    public required string CreatedBy { get; set; }
   
}