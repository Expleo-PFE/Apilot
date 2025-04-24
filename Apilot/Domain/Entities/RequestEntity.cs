using Apilot.Domain.Common;
using Apilot.Domain.Enums;

namespace Apilot.Domain.Entities;

public class RequestEntity : BaseEntity
{
    public string Name { get; set; }
    public ApiHttpMethod HttpMethod { get; set; }
    public string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public AuthenticationEntity Authentication { get; set; }
    public string Body { get; set; }
    
    public int FolderId { get; set; }
    public FolderEntity FolderEntity { get; set; }
    public int CollectionId { get; set; }
    public CollectionEntity CollectionEntity { get; set; }
    
    public List<ResponseEntity> Responses { get; set; }
    
}