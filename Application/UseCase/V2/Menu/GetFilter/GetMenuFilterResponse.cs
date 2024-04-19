using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase.V2.Menu.GetFilter
{
    public class GetMenuFilterResponse
    {
        public Guid IdMenu { get; set; }
        public DateTime EatingDate { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime CloseDate { get; set; }
    }
}
