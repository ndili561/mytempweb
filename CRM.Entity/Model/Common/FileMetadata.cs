namespace CRM.Entity.Model.Common
{
    public class FileMetadata: BaseDto
    {
        public FileMetadata()
        {
            
        }
        public FileMetadata(string filePath, byte[] fileContent)
        {
            FileUNCPath = filePath;
            FileContent = fileContent;
        }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileUNCPath { get; set; }
        public byte[] FileContent { get; set; }
        public string UploadedBy { get; set; }
        public int? PersonId { get; set; }
        public int? UserId { get; set; }
        public string AlternateText { get; set; }
    }
}
