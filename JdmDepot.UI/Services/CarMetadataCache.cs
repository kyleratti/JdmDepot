using JdmDepot.Common.DataModels;
using Microsoft.Extensions.Caching.Memory;

namespace JdmDepot.UI.Services;

public class CarMetadataCache
{
	private IMemoryCache _cache;

	public CarMetadataCache(IMemoryCache cache)
	{
		_cache = cache;
	}

	public ValueTask<IReadOnlyCollection<MaintenanceItem>> GetMaintenanceItemsForCar(string carId, string engineId)
	{
		var maintenanceItems = _cache.GetOrCreate($"{nameof(CarMetadataCache)}.{carId}.{engineId}.Maintenance",
			_ => JdmDepot.Data.YamlDataReader.parseMaintenanceItemsForCarId(carId, engineId)) ?? Array.Empty<MaintenanceItem>();

		return ValueTask.FromResult(maintenanceItems);
	}

	public ValueTask<IReadOnlyDictionary<SupplierId, Supplier>> GetSuppliers()
	{
		var suppliers = _cache.GetOrCreate($"{nameof(CarMetadataCache)}.Suppliers",
			_ => JdmDepot.Data.YamlDataReader.parseSuppliers()) ?? Array.Empty<Supplier>();

		var result = suppliers.ToDictionary(x => x.Id, x => x);

		return ValueTask.FromResult<IReadOnlyDictionary<SupplierId, Supplier>>(result.AsReadOnly());
	}
}
