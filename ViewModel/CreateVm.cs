using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication13.ViewModel
{
    public class CreateVm
    {

        public string Name { get; set; }
        public string Position { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }

    }

}

