using System;

namespace Application.Dto.Image
{
    public class ImageResponseDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsUploaded { get; set; }
        public PropertyResponseDto Property { get; set; }
    }
}

