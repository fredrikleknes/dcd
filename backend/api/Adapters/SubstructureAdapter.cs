using api.Dtos;
using api.Models;

namespace api.Adapters
{
    public class SubstructureAdapter
    {
        public Substructure Convert(SubstructureDto substructureDto)
        {
            var substructure = new Substructure();
            substructure.ProjectId = substructureDto.ProjectId;
            substructure.Name = substructureDto.Name;
            substructure.DryWeight = substructureDto.DryWeight;
            substructure.Maturity = substructureDto.Maturity;
            substructure.CostProfile = Convert(substructureDto.CostProfile, substructure);

            return substructure;
        }

        private SubstructureCostProfile Convert(SubstructureCostProfileDto substructureCostProfileDto, Substructure substructure)
        {
            return new SubstructureCostProfile
            {
                Substructure = substructure,
                EPAVersion = substructureCostProfileDto.EPAVersion,
                Currency = substructureCostProfileDto.Currency,
                StartYear = substructureCostProfileDto.StartYear,
                Values = substructureCostProfileDto.Values
            };
        }
    }
}
