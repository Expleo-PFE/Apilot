using System.ComponentModel.DataAnnotations;
using Apilot.Application.DTOs.Request;
using Apilot.Domain.Entities;
using Apilot.Domain.Enums;

namespace Apilot.Application.DTOs.History;

public class CreateHistoryRequest
{
    [Required]
    public int WorkspaceId { get; set; }
   
    public HistoryRequestEntity Request { get; set; }
    
   
}