namespace CRM.Dto.Common
{
    public class FileMetadata: BaseDto
    {
        public FileMetadata()
        {
            
        }
        public FileMetadata(string filePath, byte[] fileContent)
        {
            FileUncPath = filePath;
            FileContent = fileContent;
        }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileUncPath { get; set; }
        public byte[] FileContent { get; set; }
        public string UploadedBy { get; set; }
        public int? PersonId { get; set; }
        public int? UserId { get; set; }
    }
}
