namespace JdmDepot.Common.DataModels;

public abstract record PartNumber
{
	public bool IsManufacturerPartNumber => this is ManufacturerPartNumber;
	public bool IsAlternatePartNumber => this is AlternatePartNumber;

	public T Merge<T>(
		Func<ManufacturerPartNumber, T> manufacturerPartNumber,
		Func<AlternatePartNumber, T> alternatePartNumber) => this switch
	{
		ManufacturerPartNumber x => manufacturerPartNumber(x),
		AlternatePartNumber x => alternatePartNumber(x),
		_ => throw new NotImplementedException(GetType().FullName),
	};
}

public record ManufacturerPartNumber(string PartNumber) : PartNumber;

public record AlternatePartNumber(SupplierId Supplier, string PartNumber) : PartNumber;
