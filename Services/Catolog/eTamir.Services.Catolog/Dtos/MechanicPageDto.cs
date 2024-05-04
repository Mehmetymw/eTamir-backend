using System.Collections.Generic;

namespace eTamir.Services.Catolog.Dtos
{
    public class MechanicPageDto
    {
        public List<MechanicDto> Mechanics { get; set; }
        public int TotalCount { get; set; }
    }
}