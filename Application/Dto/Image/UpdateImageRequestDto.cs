using System;
namespace Application.Dto.Image
{
	public class UpdateImageRequestDto
	{
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}

