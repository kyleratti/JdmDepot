namespace JdmDepot.Common.DataModels;

public abstract record FluidType
{
	public static readonly FluidType Oil = new OilFluidType();
	public static readonly FluidType Coolant = new CoolantFluidType();

	public T Merge<T>(
		Func<OilFluidType, T> oilFluidType,
		Func<CoolantFluidType, T> coolantFluidType) => this switch
	{
		OilFluidType x => oilFluidType(x),
		CoolantFluidType x => coolantFluidType(x),
		_ => throw new NotImplementedException(GetType().FullName),
	};
}

public record OilFluidType : FluidType
{
	internal OilFluidType()
	{
		// c'tor
	}
}

public record CoolantFluidType : FluidType
{
	internal CoolantFluidType()
	{
		// c'tor
	}
}

public abstract record MaintenanceItem
{
	public T Merge<T>(
		Func<FluidMaintenanceItem, T> fluidMaintenanceItem,
		Func<FilterMaintenanceItem, T> filterMaintenanceItem,
		Func<BeltMaintenanceItem, T> beltMaintenanceItem) => this switch
	{
		FluidMaintenanceItem x => fluidMaintenanceItem(x),
		FilterMaintenanceItem x => filterMaintenanceItem(x),
		BeltMaintenanceItem x => beltMaintenanceItem(x),
		_ => throw new NotImplementedException(GetType().FullName),
	};
}

public record FluidMaintenanceItem(
	string Name,
	FluidType TypeOfFluid,
	decimal CapacityQuarts,
	string Specification,
	IReadOnlyCollection<PartLink> Links,
	string Notes
) : MaintenanceItem;

public record FilterMaintenanceItem(
	string Name,
	string ManufacturerPartNumber,
	IReadOnlyCollection<string> AlternativePartNumbers,
	IReadOnlyCollection<PartLink> Links,
	string Notes
) : MaintenanceItem;

public record BeltMaintenanceItem(
	string Name,
	string ManufacturerPartNumber,
	IReadOnlyCollection<string> AlternativePartNumbers,
	ushort Quantity,
	IReadOnlyCollection<PartLink> Links,
	string Notes
) : MaintenanceItem;
