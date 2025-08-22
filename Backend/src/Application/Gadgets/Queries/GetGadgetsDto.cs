using Domain;
using Domain.Entities;

namespace Application.Gadgets.Queries;
public class GetGadgetsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Gadget, GetGadgetsDto>();
        }
    }
}
