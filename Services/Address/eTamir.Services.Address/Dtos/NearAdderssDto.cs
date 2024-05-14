using eTamir.Services.Address.Models;

namespace eTamir.Services.Address.Dtos
{
    public class NearAdderssDto
    {
        public double[] Coordinates { get; set; }
        public double Proximity { get; set; }
    }
}