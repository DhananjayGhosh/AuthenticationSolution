namespace Authentication.Domain.Services
{
    public interface IWordDocumentRead
    {
        string GetDocumentHtml(string doctorId, string modality, string fileName);
    }
}
