using api.Models;

namespace api.Dtos
{
    public class TopsideDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public Guid ProjectId { get; set; }
        public TopsideCostProfileDto? CostProfile { get; set; }
        public TopsideCessationCostProfileDto? CessationCostProfile { get; set; }
        public double DryWeight { get; set; }
        public double OilCapacity { get; set; }
        public double GasCapacity { get; set; }
        public double FacilitiesAvailability { get; set; }
        public ArtificialLift ArtificialLift { get; set; }
        public Maturity Maturity { get; set; }
        public Currency Currency { get; set; }
    }

    public class TopsideCostProfileDto : TimeSeriesCostDto
    {

    }

    public class TopsideCessationCostProfileDto : TimeSeriesCostDto
    {

    }
}
