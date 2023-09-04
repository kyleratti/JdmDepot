using JdmDepot.UI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JdmDepot.UI.Views.Cars.Mk2_Mitsubishi_Pajero;

public class _4M40Maintenance : PageModel {
	public MaintenanceItemsModel MaintenanceItemsModel { get; }

	public _4M40Maintenance(MaintenanceItemsModel maintenanceItemsModel)
	{
		MaintenanceItemsModel = maintenanceItemsModel;
	}
}
