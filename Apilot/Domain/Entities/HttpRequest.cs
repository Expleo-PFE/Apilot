using Apilot.Domain.Common;

namespace Apilot.Domain.Entities;

public class HttpRequest : BaseEntity
{
    public string Name { get; set; }
    public HttpMethod HttpMethod { get; set; }
    public string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public Authentication Authentication { get; set; }
    public string Body { get; set; }
    
    public int FolderId { get; set; }
    public Folder Folder { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
    
    public List<HttpResponse> HttpResponses { get; set; }
    
}