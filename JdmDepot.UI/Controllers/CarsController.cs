using JdmDepot.Common.DataModels;
using JdmDepot.UI.Models;
using JdmDepot.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JdmDepot.UI.Controllers;

[Route("cars")]
public class CarsController : Controller
{
	private readonly CarMetadataCache _carMetadataCache;

	public CarsController(CarMetadataCache carMetadataCache)
	{
		_carMetadataCache = carMetadataCache;
	}

	[Route("mk2-mitsubishi-pajero")]
	public IActionResult Mk2MitsubishiPajero()
	{
		return View("Mk2-Mitsubishi-Pajero/Index");
	}

	[Route("mk2-mitsubishi-pajero/maintenance/4m40")]
	public async Task<IActionResult> Mk2MitsubishiPajeroMaintenance4M40()
	{
		var items = await _carMetadataCache.GetMaintenanceItemsForCar("mk2-mitsubishi-pajero", "4m40");

		return View("MaintenanceItems", new MaintenanceItemsModel(
			Title: Resources.MK2_Mitsubishi_Pajero_4M40_Maintenance,
			MaintenanceItems: items));
	}
}
